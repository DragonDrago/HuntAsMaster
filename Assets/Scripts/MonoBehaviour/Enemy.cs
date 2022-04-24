using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Dreamteck.Splines;
using UnityEngine;
using MoreMountains.NiceVibrations;
using Doozy.Engine.UI;

public class Enemy : MonoBehaviour, IEnemy
{
    public enum EnemyType { slow, normal, fast, strong};
    public EnemyType enemyType = EnemyType.slow;

    [SerializeField]
    private int enemyHP = 1;

    [SerializeField]
    private AudioClip deathAudioClip;

    [SerializeField]
    private AudioClip hitAudioClip;


    private CapsuleCollider capsuleCollider;
    private Rigidbody rigidbody;

    private SplineFollower _splineFollower;
    private SplineComputer spline;

    private Animator animator;

    private AudioSource audioSource;

    private Action<Vector3, bool> action;
    private Action finishAction;

    private EnemyProgress enemyProgress;

    private bool isBackward, isClickBack;
    private bool isKilled;

    private float speed = 1;
    private int tempHP = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _splineFollower = GetComponent<SplineFollower>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        enemyProgress = GetComponentInChildren<EnemyProgress>();
    }

    public void Set(Action<Vector3, bool> action, Action finishAction, SplineComputer spline)
    {
        transform.localScale = Vector3.zero;
        capsuleCollider.enabled = false;


        _splineFollower.spline = spline;

        this.spline = spline;

        transform.localPosition = Vector3.zero;

        this.action = action;
        this.finishAction = finishAction;

        tempHP = enemyHP;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon") || other.CompareTag("Friend"))
        {
            tempHP--;

            if(tempHP <= 0)
            {
                if(Constants.isSound)
                {
                    if (audioSource != null)
                    {
                        audioSource.clip = deathAudioClip;
                        audioSource.Play();
                    }
                }
                
                enemyProgress.Hide();
                EnemyDeaded(other);
            }
            else
            {
                if (Constants.isSound)
                {
                    if (audioSource != null)
                    {
                        audioSource.clip = hitAudioClip;
                        audioSource.Play();
                    }
                }

                enemyProgress.Set(tempHP, enemyHP);

                animator.SetTrigger("Dizzy");
                _splineFollower.follow = false;

                StartCoroutine(WaitForDizzy());

                if (Constants.isVibration)
                {
                    MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                }
            }
            
        }
    }

    private IEnumerator WaitForDizzy()
    {
        float t = 0f;
        if (animator.speed == 1)
            t = 1.05f;
        else
            t = 1.05f + 0.5f;

        yield return new WaitForSeconds(t);

        if(_splineFollower != null)
        {
            animator.SetTrigger("Run");
            _splineFollower.follow = true;
        }
        
    }

    private void EnemyDeaded(Collider other)
    {
        isKilled = true;

        animator.SetTrigger("Die");
        _splineFollower.follow = false;

        bool isFirend = true;

        if (other.CompareTag("Weapon"))
        {
            GameManager.Instance.LevelStateScore(GetScore());
            isFirend = false;
            other.GetComponent<BoxCollider>().enabled = false;
        }

        action?.Invoke(transform.position, isFirend);

        if (Constants.isVibration)
        {
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        }

        DestroyComponent();
    }

    private void DestroyComponent()
    {
        Destroy(_splineFollower);
        Destroy(rigidbody);
        Destroy(capsuleCollider);
    }

    private int GetScore()
    {
        int pos = (int)transform.localPosition.z;

        float coin = 0;
        int score = 0;

        if (enemyType == EnemyType.slow)
        {
            coin = 5 * Constants.currentMoneyUpgradeLevel + Constants.enemyKillCoin;
            score = enemyHP;
        }
        else if(enemyType == EnemyType.normal)
        {
            coin = 5 * Constants.currentMoneyUpgradeLevel + Constants.enemyKillCoin + 25 * enemyHP;
            score = enemyHP * 25;
        }
        else if(enemyType == EnemyType.fast)
        {
            coin = 5 * Constants.currentMoneyUpgradeLevel + Constants.enemyKillCoin + 50 * enemyHP;
            score = enemyHP * 50;
        }
        else
        {
            coin = 5 * Constants.currentMoneyUpgradeLevel + Constants.enemyKillCoin + 100 * enemyHP;
            score = enemyHP * 100;
        }

        Constants.rewardCoin += coin;
        Constants.total_coins += coin;
        GameManager.Instance.SetCoinStatus(coin);


        if(pos < -10)
        {
            return 100 + score;
        }
        else if(pos <= 0)
        {
            return 120 + score;
        }

        pos = pos * 2 + 100 + score;



        return pos;
    }

    private void OnTriggerExit(Collider other)
    {
        if (isClickBack)
            return;

        if (other.CompareTag("Finish"))
        {
            finishAction?.Invoke();
        }
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    public void OnPlay()
    {
        transform.DOScale(new Vector3(2f, 2f, 2f), 0.3f);
        capsuleCollider.enabled = true;

        isBackward = false;
        isClickBack = false;

        animator.SetTrigger("Run");

        animator.speed = 1f;

        if (enemyType == EnemyType.normal)
        {
            speed = 1.2f;
        }
        else if(enemyType == EnemyType.fast)
        {
            speed = 2f;
        }
        else if(enemyType == EnemyType.strong)
        {
            speed = 1f;
            animator.speed = 0.5f;
        }
        else 
        {
            speed = 1;
        }

        _splineFollower.followSpeed = speed;

        _splineFollower.follow = true;
    }

    public void OnPause()
    {
        if (isKilled)
            return;

        if(Constants.isPause)
        {
            animator.SetTrigger("Idle");
            _splineFollower.follow = false;
        }
        else
        {
            animator.SetTrigger("Run");
            _splineFollower.follow = true;
        }
        
    }

    public void OnTargetBegin()
    {
        
    }

    public void OnTargeting()
    {
        
    }

    public void OnTargetEnd()
    {
        
    }

    public void OnFaild()
    {
        if (isBackward)
            return;

        _splineFollower.follow = false;
    }

    public void OnVectory()
    {

    }

    public void OnFinish()
    {
        
    }

    public void OnBackward()
    {
        if (isKilled)
            return;

        isBackward = true;
        isClickBack = true;

        _splineFollower.direction = Spline.Direction.Backward;
        _splineFollower.followSpeed = 10;
        _splineFollower.follow = true;

        spline.triggerGroups[0].triggers[0].onCross.AddListener(TriggerCross);
    }

    private void TriggerCross()
    {
        if (isKilled)
            return;

        isClickBack = false;

        _splineFollower.direction = Spline.Direction.Forward;
        _splineFollower.followSpeed = speed;
        _splineFollower.follow = true;

        GameManager.Instance.FriendControl();
    }


}
