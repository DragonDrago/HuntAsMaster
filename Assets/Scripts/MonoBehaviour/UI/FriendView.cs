using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FriendView : MonoBehaviour, IFriendView, ISaveTime
{
    private const string key_total_second = "dailyTotalSecondFriend";
    private const string key_leave_total_second = "dailyLeaveTotalSecondFriend";


    [SerializeField]
    private Text timeText;

    [SerializeField]
    private GameObject level0Object;

    [SerializeField]
    private GameObject level1Object;

    [SerializeField]
    private GameObject level2Object;

    private RectTransform rectTransform;

    private int totalSecond = 86400;

    private bool isLevelupFriend;


    private void Start()
    {
        GameManager.Instance.AddFriendView(this);
        GameManager.Instance.AddSaveTime(this);

        rectTransform = GetComponent<RectTransform>();
    }


    public void Show()
    {
        LevelUp();

        ShowLevel();

        rectTransform.DOAnchorPosX(0f, 0.3f).From(new Vector2(2000f, 0f));
    }

    public void Hide()
    {
        rectTransform.DOAnchorPosX(2000f, 0.01f);
    }

    private void CheckReady()
    {
        if (Constants.currentFriendCount == 0)
            return;


        bool hasBonus = HasBonus();
       
        totalSecond = PlayerPrefs.GetInt(key_total_second, 86400);

        int enter_total_second = Constants.GetNowTotalSecond();

        int delta_second = enter_total_second - PlayerPrefs.GetInt(key_leave_total_second);

        totalSecond -= delta_second;

        if (hasBonus)
        {
            UpdateItem();
        }
        else
        {
            StartCounter();
        }
    }

    private void StartCounter()
    {
        if (!gameObject.activeSelf)
            return;

        StopAllCoroutines();

        SetInfoText();

        StartCoroutine(UpdateTime());
    }

    private IEnumerator UpdateTime()
    {
        yield return new WaitForSeconds(1);

        totalSecond -= 1;

        if (totalSecond > 0)
        {
            SetInfoText();

            StartCoroutine(UpdateTime());
        }
        else
        {
            UpdateItem();
        }

    }

    private void SetInfoText()
    {
        timeText.text = TimeSpan.FromSeconds(totalSecond).ToString();
    }

    private void UpdateItem()
    {
        Constants.currentFriendCount = 0;

        StopAllCoroutines();
    }

    public void OnClickOpen()
    {
        Constants.OpenPopup(Constants.popup_friend);

    }

    private bool HasBonus()
    {
        if (0 >= totalSecond)
        {
            return true;
        }

        return false;

    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Save();
        }
        else
        {
            CheckReady();
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void Save()
    {

        int leaveTotalSecond = Constants.GetNowTotalSecond();

        PlayerPrefs.SetInt(key_leave_total_second, leaveTotalSecond);
        PlayerPrefs.SetInt(key_total_second, totalSecond);

        PlayerPrefs.Save();
    }


    public void ResetTime()
    {
        if (Constants.currentFriendCount > 0)
        {
            isLevelupFriend = true;
        }
        else
        {
            isLevelupFriend = false;
            StopAllCoroutines();

            level0Object.SetActive(false);
            level1Object.SetActive(false);

            level2Object.SetActive(false);

            timeText.text = "00:00:00";
        }
    }

    public void LevelUp()
    {
        if (!isLevelupFriend)
        {
            CheckReady();

            return;
        }

        Constants.OpenPopup(Constants.popup_friend);

        isLevelupFriend = false;

        if (Constants.currentFriendCount > 0)
        {
            totalSecond = 86400;

            StartCounter();
        }
    }

    private void ShowLevel()
    {
        switch (Constants.currentFriendCount)
        {
            case 1:

                level0Object.SetActive(true);

                level1Object.SetActive(false);
                level2Object.SetActive(false);

                break;
            case 2:

                level0Object.SetActive(false);
                level1Object.SetActive(true);

                level2Object.SetActive(false);

                break;
            case 3:

                level0Object.SetActive(false);
                level1Object.SetActive(false);

                level2Object.SetActive(true);

                break;
            default:

                level0Object.SetActive(false);
                level1Object.SetActive(false);

                level2Object.SetActive(false);

                break;

        }
    }

    public void SaveTime()
    {
        Save();
    }


}
