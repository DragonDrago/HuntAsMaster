using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour, IController
{
    [SerializeField]
    private Enemy[] enemies = new Enemy[60];

    [SerializeField]
    private Enemy[] bossEnemies = new Enemy[23];

    [SerializeField]
    private GameObject joystickObject;

    [SerializeField]
    private ParticleSystem particleCoin;

    [SerializeField]
    private ParticleSystem particleDiamond;

    [SerializeField]
    private UIView wowUIView;

    [SerializeField]
    private TextMeshProUGUI wowText;

    private List<IEnemy> enemyList = new List<IEnemy>();


    private Enemy enemy;

    private AudioSource audioSource;


    private Action<Vector3, bool> action;
    private Action finishAction;

    private int defaultIndex;
    private int bossIndex;
    private bool isBackward, isFailed;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        action += KilledEnemy;

        finishAction += EnemyFinish;

        GameManager.Instance.AddController(this);
    }

    public void Play()
    {
        isFailed = false;
        isBackward = false;

      //  RemovAllEnemy();

        if(Constants.currentLevel < 3)
        {
            // bitta enemy
            Constants.enemyCount = 1;
            CreateEnemy(enemies[Constants.enemy_default_current_index]);
        }
        else if(Constants.currentLevel < 10)
        {
            // 2 ta enemy
            if(Constants.currentLevel == 9)
            {
                Constants.enemyCount = 1;
                CreateEnemy(bossEnemies[Constants.enemy_boss_current_index]);
            }
            else
            {
                Constants.enemyCount = 2;
                int index = Constants.enemy_default_current_index;

                CreateEnemy(enemies[Random.Range(0, index)]);
                CreateEnemy(enemies[index]);
            }
        }
        else if(Constants.currentLevel < 20)
        {
            //3 ta enemy
            

            if (Constants.currentLevel == 14 || Constants.currentLevel == 19)
            {
                Constants.enemyCount = 1;
                CreateEnemy(bossEnemies[Constants.enemy_boss_current_index]);
               
            }
            else
            {
                Constants.enemyCount = 4;
                int index = Constants.enemy_default_current_index;
                CreateEnemy(enemies[Random.Range(0, index / 2)]);
                CreateEnemy(enemies[Random.Range(index / 2, index)]);

                CreateEnemy(enemies[index]);

                CreateEnemy(bossEnemies[Random.Range(0, Constants.enemy_boss_current_index)]);
            }
        }
        else
        {
            // 3 tadan 9 tagacha
     
            if((Constants.currentLevel + 1) % 5 == 0)
            {
                bossIndex = Constants.enemy_boss_current_index;

                if (bossIndex < bossEnemies.Length)
                {
                    Constants.enemyCount = 1;
                    CreateEnemy(bossEnemies[bossIndex]);
                }
                else
                {
                    if(bossIndex > bossEnemies.Length)
                        Constants.enemy_boss_current_index = bossEnemies.Length;

                    Constants.enemyCount = 2;

                    CreateEnemy(bossEnemies[Random.Range(0, 14)]);
                    CreateEnemy(bossEnemies[Random.Range(14, 23)]);
                }
            }
            else // default enemies
            {
                Constants.enemyCount = Random.Range(5, 9);

                if (Constants.enemy_boss_current_index > bossEnemies.Length)
                    Constants.enemy_boss_current_index = bossEnemies.Length;

                defaultIndex = Constants.enemy_default_current_index;

                if(defaultIndex < enemies.Length)
                {
                    switch (Constants.enemyCount)
                    {
                        case 5: //20 / 4 21 / 
                            CreateEnemy(enemies[Random.Range(0, defaultIndex / 4)]);
                            CreateEnemy(enemies[Random.Range(defaultIndex / 4, defaultIndex / 2)]);
                            CreateEnemy(enemies[Random.Range(defaultIndex / 2, defaultIndex)]);

                            CreateEnemy(enemies[defaultIndex]);

                            CreateEnemy(bossEnemies[Random.Range(0, Constants.enemy_boss_current_index)]);

                            break;
                        case 6:
                            CreateEnemy(enemies[Random.Range(0, defaultIndex / 4)]);
                            CreateEnemy(enemies[Random.Range(defaultIndex / 4, defaultIndex / 2)]);
                            CreateEnemy(enemies[Random.Range(defaultIndex / 2, (defaultIndex / 2) + (defaultIndex / 4))]);
                            CreateEnemy(enemies[Random.Range((defaultIndex / 2) + (defaultIndex / 4), defaultIndex)]);

                            CreateEnemy(enemies[defaultIndex]);

                            CreateEnemy(bossEnemies[Random.Range(0, Constants.enemy_boss_current_index)]);
                            break;
                        case 7:
                            //21 / 5 : 4 , /3 : 7, /2 : 10
                            CreateEnemy(enemies[Random.Range(0, defaultIndex / 5)]);
                            CreateEnemy(enemies[Random.Range(defaultIndex / 5, defaultIndex / 3)]);
                            CreateEnemy(enemies[Random.Range(defaultIndex / 3, defaultIndex / 2)]);
                            CreateEnemy(enemies[Random.Range(defaultIndex / 2, (defaultIndex / 5) + (defaultIndex / 2))]);
                            CreateEnemy(enemies[Random.Range((defaultIndex / 5) + (defaultIndex / 2), defaultIndex)]);

                            CreateEnemy(enemies[defaultIndex]);

                            CreateEnemy(bossEnemies[Random.Range(0, Constants.enemy_boss_current_index)]);

                            break;
                        case 8:

                            CreateEnemy(enemies[Random.Range(0, defaultIndex / 5)]);
                            CreateEnemy(enemies[Random.Range(defaultIndex / 5, defaultIndex / 3)]);
                            CreateEnemy(enemies[Random.Range(defaultIndex / 3, defaultIndex / 2)]);
                            CreateEnemy(enemies[Random.Range(defaultIndex / 2, (defaultIndex / 5) + (defaultIndex / 2))]);
                            CreateEnemy(enemies[Random.Range((defaultIndex / 5) + (defaultIndex / 2), defaultIndex)]);
                            CreateEnemy(enemies[Random.Range(defaultIndex / 2, defaultIndex)]);

                            CreateEnemy(enemies[defaultIndex]);

                            CreateEnemy(bossEnemies[Random.Range(0, Constants.enemy_boss_current_index)]);
                            break;
                    }
                }
                else
                {
                    defaultIndex = enemies.Length;

                    switch (Constants.enemyCount)
                    {
                        case 5: //20 / 4 21 / 
                            CreateEnemy(enemies[Random.Range(0, defaultIndex / 4)]);
                            CreateEnemy(enemies[Random.Range(defaultIndex / 4, defaultIndex / 2)]);
                            CreateEnemy(enemies[Random.Range(defaultIndex / 2, defaultIndex)]);

                            CreateEnemy(bossEnemies[Random.Range(0, Constants.enemy_boss_current_index)]);
                            CreateEnemy(bossEnemies[Random.Range(0, Constants.enemy_boss_current_index)]);
                            break;
                        case 6:
                            CreateEnemy(enemies[Random.Range(0, defaultIndex / 4)]);
                            CreateEnemy(enemies[Random.Range(defaultIndex / 4, defaultIndex / 2)]);
                            CreateEnemy(enemies[Random.Range(defaultIndex / 2, (defaultIndex / 2) + (defaultIndex / 4))]);
                            CreateEnemy(enemies[Random.Range((defaultIndex / 2) + (defaultIndex / 4), defaultIndex)]);

                            CreateEnemy(bossEnemies[Random.Range(0, Constants.enemy_boss_current_index)]);
                            CreateEnemy(bossEnemies[Random.Range(0, Constants.enemy_boss_current_index)]);
                            break;
                        case 7:
                            //21 / 5 : 4 , /3 : 7, /2 : 10
                            CreateEnemy(enemies[Random.Range(0, defaultIndex / 5)]);
                            CreateEnemy(enemies[Random.Range(defaultIndex / 5, defaultIndex / 3)]);
                            CreateEnemy(enemies[Random.Range(defaultIndex / 3, defaultIndex / 2)]);
                            CreateEnemy(enemies[Random.Range(defaultIndex / 2, (defaultIndex / 5) + (defaultIndex / 2))]);
                            CreateEnemy(enemies[Random.Range((defaultIndex / 5) + (defaultIndex / 2), defaultIndex)]);

                            CreateEnemy(bossEnemies[Random.Range(0, Constants.enemy_boss_current_index)]);
                            CreateEnemy(bossEnemies[Random.Range(0, Constants.enemy_boss_current_index)]);
                            break;
                        case 8:

                            CreateEnemy(enemies[Random.Range(0, defaultIndex / 5)]);
                            CreateEnemy(enemies[Random.Range(defaultIndex / 5, defaultIndex / 3)]);
                            CreateEnemy(enemies[Random.Range(defaultIndex / 3, defaultIndex / 2)]);
                            CreateEnemy(enemies[Random.Range(defaultIndex / 2, (defaultIndex / 5) + (defaultIndex / 2))]);
                            CreateEnemy(enemies[Random.Range((defaultIndex / 5) + (defaultIndex / 2), defaultIndex)]);
                            CreateEnemy(enemies[Random.Range(defaultIndex / 2, defaultIndex)]);

                            CreateEnemy(bossEnemies[Random.Range(0, Constants.enemy_boss_current_index)]);
                            CreateEnemy(bossEnemies[Random.Range(0, Constants.enemy_boss_current_index)]);
                            break;
                    }
                }

                

                

            }
            
        }


        

    }

    private void CreateEnemy(Enemy enemyPrefab)
    {
        enemy = Instantiate(enemyPrefab, transform);

        enemy.Set(action, finishAction, GameManager.Instance.GetPath());

        if (!enemyList.Contains(enemy))
        {
            enemyList.Add(enemy);
        }
    }

    public void AddEnemy(IEnemy enemy)
    {
        if (!enemyList.Contains(enemy))
        {
            enemyList.Add(enemy);
        }
    }

    public void KilledEnemy(Vector3 pos, bool isFriend)
    {
        Constants.enemyKillCount++;

        GameManager.Instance.LevelStateEnemyCount(Constants.enemyKillCount);

        if(!isFriend)
        {
            wowText.text = GetWow();
            wowUIView.Show();
        }

        particleCoin.transform.position = new Vector3(pos.x, 1f, pos.z);
        particleCoin.Play();

        if (Constants.enemyKillCount == enemyList.Count)
        {
            Constants.isFinish = true;
            joystickObject.SetActive(false);

            StartCoroutine(VectoryWait());
        }
    }

    private IEnumerator VectoryWait()
    {
        yield return new WaitForSeconds(2f);

        AppMetricaSendEventContrrol.FinishLevel("Win");

        GameManager.Instance.ControllerVectory();

    }

    public void EnemyFinish()
    {
        if (isFailed)
            return;

        isFailed = true;

        if(Constants.isSound)
        {
            audioSource.Play();
        }

        if(isBackward)
        {
            AppMetricaSendEventContrrol.FinishLevel("Faild");

            GameManager.Instance.ControllerFinish();
        }
        else
        {
            GameManager.Instance.ControllerTargetEnd();
            GameManager.Instance.ControllerFaild();
        }

    }

    public void RemovAllEnemy()
    {
        Constants.enemyKillCount = 0;

        foreach (IEnemy enemy in enemyList)
        {
            enemy.DestroyEnemy();
        }

        enemyList.Clear();
    }

    public void OnLoaded()
    {

    }

    public void OnPlay()
    {
        StopAllCoroutines();
        Play();

        wowUIView.Hide();

        StartCoroutine(PlayRoutine());
    }

    private IEnumerator PlayRoutine()
    {
        foreach (IEnemy enemy in enemyList)
        {
            enemy.OnPlay();

            yield return new WaitForSeconds(3f);
        }
    }

    public void OnPause()
    {
        foreach (IEnemy enemy in enemyList)
        {
            enemy.OnPause();
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

    public void OnFinish()
    {
        RemovAllEnemy();
    }

    public void OnFaild()
    {
        foreach (IEnemy enemy in enemyList)
        {
            enemy.OnFaild();
        }
    }

    public void OnVectory()
    {
        StopAllCoroutines();

        if (defaultIndex < enemies.Length)
        {
            Constants.enemy_default_current_index++;
        }

        if (Constants.currentLevel > 5 && Constants.currentLevel % 5 == 0)
        {
            if (bossIndex < bossEnemies.Length)
                Constants.enemy_boss_current_index++;
        }
    }

    public void OnBackward()
    {
        foreach (IEnemy enemy in enemyList)
        {
            enemy.OnBackward();
        }

        isFailed = false;

        isBackward = true;
    }

    public void OnRestart()
    {
        StopAllCoroutines();

        wowUIView.Hide();
        RemovAllEnemy();
    }


    private string GetWow()
    {
        int random = Random.Range(0, 9);

        switch(random)
        {
            case 0:
                return "wow!";
            case 1:
                return "super!";
            case 2:
                return "amazing!";
            case 3:
                return "good!";
            case 4:
                return "cool!";
            case 5:
                return "nice!";
            case 6:
                return "great!";
            case 7:
                return "excellent";
            default:
                return "wow!";
        }
    }


}
