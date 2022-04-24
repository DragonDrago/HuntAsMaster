using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : MonoBehaviour
{

    private Animator animator;

    private Transform enemyTransform;

    private EnemyEvent enemyEvent;

    private bool isMove;
    private bool isStop;
    public bool GetStop {
        get { return isStop; }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();

    }


    public void Show()
    {
        gameObject.SetActive(true);
        isMove = false;
        isStop = false;

        animator.SetBool("idle", true);
        animator.SetBool("run", false);
    }

    private void Update()
    {
        if (isStop)
            return;

        if(isMove && enemyTransform != null)
        {
            transform.LookAt(enemyTransform);
            transform.position = Vector3.MoveTowards(transform.position, enemyTransform.position, Time.deltaTime);
            float distance = Vector3.Distance(transform.position, enemyTransform.position);
            if(distance < 0.1f)
            {
                isMove = false;
                animator.SetBool("run", false);
                animator.SetBool("idle", true);
                enemyEvent.SetIsSent = false;
            }
        }
    }


    public void SetEnemyTransform(Transform enemyTrans, EnemyEvent enemyEvent)
    {
        this.enemyEvent = enemyEvent;

        enemyTransform = enemyTrans;

        animator.SetBool("idle", false);
        animator.SetBool("run", true);

        isMove = true;
        
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (isStop)
            return;

        if(other.CompareTag("Enemy"))
        {
            isMove = false;

            animator.SetTrigger("attack");
            animator.SetBool("run", false);
            enemyEvent.SetIsSent = false;
        }
    }


    public void Hide()
    {
        isMove = false;
        isStop = false;
        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    public void Stop(bool stop)
    {
        isMove = false;
        animator.SetBool("run", false);
        animator.SetBool("idle", true);
        if (stop && enemyEvent != null)
            enemyEvent.SetIsSent = false;

        isStop = stop;
    }



}
