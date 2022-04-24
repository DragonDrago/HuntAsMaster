using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using ConsentManager;

public class UpgradeView : MonoBehaviour, IStatus
{
    [Header("Tutorial")]
    [SerializeField]
    private GameObject tutorialBgObject;
    [SerializeField]
    private RectTransform handTransform;
    [SerializeField]
    private Button targetParentButton;
    [SerializeField]
    private Button moneyParentButton;
    [SerializeField]
    private Button offlineParentButton;


    //strong page
    [Header("Strong page")]
    [SerializeField]
    private GameObject strongPage;
    [SerializeField]
    private Text strongButtonText;
    [SerializeField]
    private Text strongText;
    [SerializeField]
    private Button strongButton;
    [SerializeField]
    private Button strongWatchButton;

    //target page
    [Header("Target page")]
    [SerializeField]
    private GameObject targetPage;
    [SerializeField]
    private Text targetButtonText;
    [SerializeField]
    private Text targetText;
    [SerializeField]
    private Button targetButton;
    [SerializeField]
    private Button targetWatchButton;

    //money page
    [Header("Money page")]
    [SerializeField]
    private GameObject moneyPage;
    [SerializeField]
    private Text moneyButtonText;
    [SerializeField]
    private Text moneyText;
    [SerializeField]
    private Button moneyButton;
    [SerializeField]
    private Button moneyWatchButton;

    //offline page
    [Header("Offline page")]
    [SerializeField]
    private GameObject offlinePage;
    [SerializeField]
    private Text offlineButtonText;
    [SerializeField]
    private Text offlineText;
    [SerializeField]
    private Button offlineButton;
    [SerializeField]
    private Button offlineWatchButton;


    private float coinStrong;
    private float coinTarget;
    private float coinMoney;
    private float coinOffline;

    private Action strongWatchDone, strongWatchError;
    private Action targetWatchDone, targetWatchError;
    private Action moneyWatchDone, moneyWatchError;
    private Action offlineWatchDone, offlineWatchError;

    private RectTransform rectTransform;
    private int currentPage;

    private int watchCount = -1;

    private bool isShowTutorial;


    private void Start()
    {
        GameManager.Instance.AddStatus(this);

        rectTransform = GetComponent<RectTransform>();

        strongWatchDone += StrongWatchDone;
        strongWatchError += StrongWatchError;

        targetWatchDone += TargetWatchDone;
        targetWatchError += TargetWatchError;

        moneyWatchDone += MoneyWatchDone;
        moneyWatchError += MoneyWatchError;

        offlineWatchDone += OfflineWatchDone;
        offlineWatchError += OfflineWatchError;
    }

    public void Show()
    {
        //gameObject.SetActive(true);

        handTransform.gameObject.SetActive(false);

        if (Constants.currentLevel == 1 && PlayerPrefs.GetInt(Constants.key_show_tutorial_upgrade) == 0)
        {
            ShowTutorial();
        }
        else
        {
            if ( (Constants.currentLevel + 1) % 5 == 0 && AppodelManager.Instance.HasRewarded())
            {
                watchCount++;

                if (watchCount == 4)
                    watchCount = 0;

                switch(watchCount)
                {
                    case 0:
                        targetButton.gameObject.SetActive(true);
                        moneyButton.gameObject.SetActive(true);
                        offlineButton.gameObject.SetActive(true);

                        offlineWatchButton.gameObject.SetActive(false);

                        strongButton.gameObject.SetActive(false);
                        strongWatchButton.gameObject.SetActive(true);

                        break;

                    case 1:
                        strongButton.gameObject.SetActive(true);
                        moneyButton.gameObject.SetActive(true);
                        offlineButton.gameObject.SetActive(true);

                        strongWatchButton.gameObject.SetActive(false);

                        targetButton.gameObject.SetActive(false);
                        targetWatchButton.gameObject.SetActive(true);

                        if(isShowTutorial)
                        {
                            ShowTutorialMoneyPage();
                        }

                        break;
                    case 2:
                        strongButton.gameObject.SetActive(true);
                        targetButton.gameObject.SetActive(true);
                        offlineButton.gameObject.SetActive(true);

                        targetWatchButton.gameObject.SetActive(false);

                        moneyButton.gameObject.SetActive(false);
                        moneyWatchButton.gameObject.SetActive(true);

                        if(isShowTutorial)
                        {
                            ShowTutorialOffline();
                        }

                        break;
                    case 3:
                        strongButton.gameObject.SetActive(true);
                        targetButton.gameObject.SetActive(true);
                        moneyButton.gameObject.SetActive(true);

                        moneyWatchButton.gameObject.SetActive(false);

                        offlineButton.gameObject.SetActive(false);
                        offlineWatchButton.gameObject.SetActive(true);

                        break;
                    default:



                        break;
                }

                currentPage = watchCount;
            }
            else
            {
                if(!AppodelManager.Instance.HasRewarded())
                {
                    strongButton.gameObject.SetActive(true);
                    strongWatchButton.gameObject.SetActive(false);

                    targetButton.gameObject.SetActive(true);
                    targetWatchButton.gameObject.SetActive(false);

                    moneyButton.gameObject.SetActive(true);
                    moneyWatchButton.gameObject.SetActive(false);

                    offlineButton.gameObject.SetActive(true);
                    offlineWatchButton.gameObject.SetActive(false);
                }
                

            }


            SwitchPage(currentPage);

            //300f
            rectTransform.DOAnchorPosY(300f, 0.3f).From(new Vector2(0f, -2000f)).SetEase(Ease.InFlash);
        }




        
    }


    public void Hide()
    {
        rectTransform.DOAnchorPosY(-2000f, 0.3f);
    }

    public void OnClickUpgradeButton(int index)
    {
        currentPage = index;

        SwitchPage(index);
    }


    public void OnClickStrongUpgradeBtn()
    {
        Constants.total_coins -= coinStrong;

        Constants.currentStrongUpgradeLevel++;

        StrongValueUpdate();

        GameManager.Instance.UpdateStatus();

        if(isShowTutorial)
        {
            ShowTutorialTargetPage();
        }
    }


    public void OnClickTargetUpgradeBtn()
    {
        Constants.total_coins -= coinTarget;

        Constants.currentTargetUpgradeLevel++;

        TargetValueUpdate();

        GameManager.Instance.UpdateStatus();

        if(isShowTutorial)
        {
            ShowTutorialMoneyPage();
        }
    }


    public void OnClickMoneyUpgradeBtn()
    {
        Constants.total_coins -= coinMoney;

        Constants.currentMoneyUpgradeLevel++;

        MoneyValueUpdate();

        GameManager.Instance.UpdateStatus();

        if(isShowTutorial)
        {
            ShowTutorialOffline();
        }
    }


    public void OnClickOfflineUpgradeBtn()
    {
        Constants.total_coins -= coinOffline;

        Constants.currentOfflineUpgradeLevel++;

        OfflineValueUpdate();

        GameManager.Instance.UpdateStatus();
    }


    private void SwitchPage(int index)
    {
        switch(index)
        {
            case 0:

                StrongValueUpdate();

                strongPage.SetActive(true);

                targetPage.SetActive(false);
                moneyPage.SetActive(false);
                offlinePage.SetActive(false);

                break;
            case 1:

                TargetValueUpdate();

                targetPage.SetActive(true);

                strongPage.SetActive(false);
                moneyPage.SetActive(false);
                offlinePage.SetActive(false);

                break;
            case 2:

                MoneyValueUpdate();

                moneyPage.SetActive(true);

                strongPage.SetActive(false);
                targetPage.SetActive(false);
                offlinePage.SetActive(false);

                break;
            case 3:

                OfflineValueUpdate();

                offlinePage.SetActive(true);

                strongPage.SetActive(false);
                targetPage.SetActive(false);
                moneyPage.SetActive(false);

                break;
        }
    }

    private void OfflineValueUpdate()
    {
        int upgrade = Constants.currentOfflineUpgradeLevel;
        coinOffline = 2 * Mathf.Pow(upgrade, 2);
        offlineButtonText.text = Constants.ConvertShortNumber(coinOffline);

        offlineText.text = (100 + (upgrade - 1) * 10) + " %";

        if (Constants.total_coins >= coinOffline)
        {
            offlineButton.interactable = true;
        }
        else
        {
            offlineButton.interactable = false;
        }

    }

    private void MoneyValueUpdate()
    {
        int upgrade = Constants.currentMoneyUpgradeLevel;
        coinMoney = 2 * Mathf.Pow(upgrade, 2);
        moneyButtonText.text = Constants.ConvertShortNumber(coinMoney);

        moneyText.text = (100 + (upgrade - 1) * 10) + " %";

        if (Constants.total_coins >= coinMoney)
        {
            moneyButton.interactable = true;
        }
        else
        {
            moneyButton.interactable = false;
        }

    }

    private void TargetValueUpdate()
    {
        int upgrade = Constants.currentTargetUpgradeLevel;
        coinTarget = 3 * Mathf.Pow(upgrade, 2);
        targetButtonText.text = Constants.ConvertShortNumber(coinTarget);

        targetText.text = (100 + (upgrade - 1) * 10) + " %";

        if (Constants.total_coins >= coinTarget)
        {
            targetButton.interactable = true;
        }
        else
        {
            targetButton.interactable = false;
        }

    }

    private void StrongValueUpdate()
    {
        int upgrade = Constants.currentStrongUpgradeLevel;
        coinStrong = 5 * Mathf.Pow(upgrade, 2);
        strongButtonText.text = Constants.ConvertShortNumber(coinStrong);

        strongText.text = (100 + (upgrade - 1) * 10) + " %";

        if (Constants.total_coins >= coinStrong)
        {
            strongButton.interactable = true;
        }
        else
        {
            strongButton.interactable = false;
                
        }
    }

    public void OnClickStrongWatch()
    {
        AppodelManager.Instance.ShowRewardedVideo("upgrade_weapon_speed", strongWatchDone, strongWatchError);
    }

    private void StrongWatchDone()
    {
        strongButton.gameObject.SetActive(true);
        strongWatchButton.gameObject.SetActive(false);

        Constants.currentStrongUpgradeLevel++;

        StrongValueUpdate();
    }

    private void StrongWatchError()
    {
        strongButton.gameObject.SetActive(true);
        strongWatchButton.gameObject.SetActive(false);
    }

    public void OnClickTagetWatch()
    {
        AppodelManager.Instance.ShowRewardedVideo("upgrade_target", targetWatchDone, targetWatchError);

        if(isShowTutorial)
        {
            ShowTutorialMoneyPage();
        }
    }

    private void TargetWatchDone()
    {
        targetButton.gameObject.SetActive(true);
        targetWatchButton.gameObject.SetActive(false);

        Constants.currentTargetUpgradeLevel++;

        TargetValueUpdate();
    }

    private void TargetWatchError()
    {
        targetButton.gameObject.SetActive(true);
        targetWatchButton.gameObject.SetActive(false);
    }

    public void OnClickMoneyWatch()
    {
        AppodelManager.Instance.ShowRewardedVideo("upgrade_coins", moneyWatchDone, moneyWatchError);

        if(isShowTutorial)
        {
            ShowTutorialOffline();
        }
    }

    private void MoneyWatchDone()
    {
        moneyButton.gameObject.SetActive(true);
        moneyWatchButton.gameObject.SetActive(false);

        Constants.currentMoneyUpgradeLevel++;

        MoneyValueUpdate();
    }

    private void MoneyWatchError()
    {
        moneyButton.gameObject.SetActive(true);
        moneyWatchButton.gameObject.SetActive(false);
    }

    public void OnClickOfflineWatch()
    {
        AppodelManager.Instance.ShowRewardedVideo("upgrade_offline_earnings", offlineWatchDone, offlineWatchError);

    }

    private void OfflineWatchDone()
    {
        offlineButton.gameObject.SetActive(true);
        offlineWatchButton.gameObject.SetActive(false);

        Constants.currentOfflineUpgradeLevel++;

        OfflineValueUpdate();
    }

    private void OfflineWatchError()
    {
        offlineButton.gameObject.SetActive(true);
        offlineWatchButton.gameObject.SetActive(false);
    }

    // tutorial

    private void ShowTutorial()
    {
        isShowTutorial = true;

        PlayerPrefs.SetInt(Constants.key_show_tutorial_upgrade, 1);

        tutorialBgObject.SetActive(true);

        targetParentButton.interactable = false;
        moneyParentButton.interactable = false;
        offlineParentButton.interactable = false;

        StrongValueUpdate();

        strongPage.SetActive(true);

        targetPage.SetActive(false);
        moneyPage.SetActive(false);
        offlinePage.SetActive(false);

        rectTransform.DOAnchorPosY(150f, 1f).From(new Vector2(0f, -2000f)).SetEase(Ease.InFlash).OnComplete(() =>
        {

            handTransform.gameObject.SetActive(true);
            handTransform.DOAnchorPosX(-500f, 0.5f).OnComplete(() =>
            {

                handTransform.DOScale(Vector3.one, 1f).From(new Vector3(1.2f, 1.2f, 1.2f)).SetLoops(5, LoopType.Yoyo);
            });
        });


    }


    private void ShowTutorialTargetPage()
    {
        targetParentButton.interactable = true;
        
        handTransform.DOKill();
        handTransform.gameObject.SetActive(false);

        tutorialBgObject.SetActive(false);

        TargetValueUpdate();

        targetPage.SetActive(true);

        strongPage.SetActive(false);
        moneyPage.SetActive(false);
        offlinePage.SetActive(false);
    }

    private void ShowTutorialMoneyPage()
    {
        moneyParentButton.interactable = true;
        
        MoneyValueUpdate();

        moneyPage.SetActive(true);

        targetPage.SetActive(false);
        strongPage.SetActive(false);
        offlinePage.SetActive(false);
    }

    private void ShowTutorialOffline()
    {
        offlineParentButton.interactable = true;

        OfflineValueUpdate();

        offlinePage.SetActive(true);

        targetPage.SetActive(false);
        strongPage.SetActive(false);
        moneyPage.SetActive(false);

        isShowTutorial = false;
    }

    public void UpdateStatus()
    {
        StrongValueUpdate();
        TargetValueUpdate();
        MoneyValueUpdate();
        OfflineValueUpdate();
    }

    public void SetCoin(float count)
    {
        
    }

    public void SetDiamond(float count)
    {
        
    }

    public void SetEnergy(float count)
    {
        
    }

    public void ShowSubcripe()
    {
        
    }
}
