using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using ConsentManager;

public class GameMenu : MonoBehaviour, IController
{

    [SerializeField]
    private RectTransform pauseTransform;

    [SerializeField]
    private RectTransform skipLevelTransform;

    [SerializeField]
    private ExtraLife extraLife;

    [SerializeField]
    private RectTransform joystickTransform;

    [SerializeField]
    private GameLevelBar levelBar;

    [SerializeField]
    private StatusView statusView;

    [SerializeField]
    private FinishView finishView;

    private Action watchAction, watchCancelAction;


    private int tempFriendCount;


    private bool isFailed;

    private void Start()
    {
        GameManager.Instance.AddController(this);

        watchAction += WatchDone;
        watchCancelAction += WatchCanceled;
    }

    public void ShowPauseButton()
    {
        pauseTransform.DOAnchorPosY(-250f, 0.5f).SetDelay(3f);

    }

    private void ShowSkipLevel()
    {
        skipLevelTransform.DOAnchorPosX(0f, 0.25f).From(new Vector2(-3200f, -700f)).SetEase(Ease.InOutSine).SetDelay(0.5f).OnComplete(() =>
        {

            skipLevelTransform.DOShakeScale(0.7f, 0.1f, 1, 10f).SetLoops(4, LoopType.Yoyo).SetDelay(0.5f).OnComplete(() =>
            {

                skipLevelTransform.DOAnchorPosX(3200f, 0.25f).SetEase(Ease.InOutSine).SetDelay(0.5f);

            });

        });
    }

    private void HidePauseButton()
    {
        pauseTransform.DOAnchorPosY(400f, 0.5f).SetEase(Ease.InOutSine);
    }


    public void OnClickPause()
    {
        //Constants.isPause = true;
        //GameManager.Instance.ControllerPause();
        Time.timeScale = 0;
    }

    public void OnClickPausedAndPlay()
    {
        //Constants.isPause = false;
        //GameManager.Instance.ControllerPause();
        Time.timeScale = 1;
    }

    public void OnClickSkipLevel()
    {
        //if (isClickLocked)
        //    return;

        //isClickLocked = true;

        skipLevelTransform.DOKill();
        skipLevelTransform.gameObject.SetActive(false);
        //skipLevelTransform.anchoredPosition = new Vector2(3200f, skipLevelTransform.anchoredPosition.y);
        AppodelManager.Instance.ShowRewardedVideo("skip_level", watchAction, watchCancelAction);
    }

    private void WatchDone()
    {
        //isClickLocked = false;
       // DOTween.KillAll();
       // skipLevelTransform.anchoredPosition = new Vector2(3200f, skipLevelTransform.anchoredPosition.y);
        GameManager.Instance.ControllerVectory();
    }

    private void WatchCanceled()
    {
        //isClickLocked = false;

       // DOTween.KillAll();
       // skipLevelTransform.anchoredPosition = new Vector2(3200f, skipLevelTransform.anchoredPosition.y);

    }


    public void OnClickGoMain()
    {


    }

    public void OnLoaded()
    {

    }

    public void OnPlay()
    {
        skipLevelTransform.anchoredPosition = new Vector2(3200f, skipLevelTransform.anchoredPosition.y);
        skipLevelTransform.gameObject.SetActive(true);

        if (Constants.joystickControl == 0)
        {
            // right control
            joystickTransform.pivot = new Vector2(1f, 0f);
            joystickTransform.anchorMin = new Vector2(1f, 0f);
            joystickTransform.anchorMax = new Vector2(1f, 0f);
            joystickTransform.anchoredPosition = new Vector2(-50f, 300f);
        }
        else
        {
            joystickTransform.pivot = new Vector2(0f, 0f);
            joystickTransform.anchorMin = new Vector2(0, 0f);
            joystickTransform.anchorMax = new Vector2(0, 0f);
            joystickTransform.anchoredPosition = new Vector2(50f, 300f);
        }

        levelBar.Show();

        statusView.Hide();

        if(AppodelManager.Instance.HasRewarded())
            ShowSkipLevel();
    }

    public void OnPause()
    {
        
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
        tempFriendCount = Constants.currentFriendCount;
        Constants.currentFriendCount = 0;

        joystickTransform.gameObject.SetActive(false);
        HidePauseButton();
        extraLife.Show();
        statusView.ShowEnergy();
    }

    public void OnVectory()
    {
        joystickTransform.gameObject.SetActive(false);

        Constants.currentLevel += 1;
        statusView.ShowAll();

        levelBar.Hide();

        HidePauseButton();
        finishView.ShowCompleted();
        isFailed = false;
    }

    public void OnFinish()
    {
        joystickTransform.gameObject.SetActive(false);

        statusView.ShowAll();

        levelBar.Hide();

        extraLife.Hide();

        HidePauseButton();
        finishView.ShowFailed();

        isFailed = true;
    }

    public void OnBackward()
    {
        Constants.currentFriendCount = tempFriendCount;
        
        joystickTransform.gameObject.SetActive(true);

        ShowPauseButton();

        statusView.Hide();

        extraLife.Hide();
    }

    public void OnRestart()
    {
        joystickTransform.gameObject.SetActive(true);

        if (isFailed)
        {
            finishView.HideFailed();
        }
        else
        {
            finishView.HideCompleted();
        }

        if(Constants.currentLevel > 2)
        {
            if(Constants.removeForceAdsPurchase == 0)
                AppodelManager.Instance.ShowInterstitial();
        }
        
    }



}
