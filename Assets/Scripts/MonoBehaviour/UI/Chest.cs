using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Chest : MonoBehaviour, ISaveTime
{
    private const string key_total_second = "dailyTotalSecondChest";
    private const string key_leave_total_second = "dailyLeaveTotalSecondChest";

   
    [SerializeField]
    private ParticleSystem particle;

    [SerializeField]
    private Image glowImage;

    [SerializeField]
    private Image chestImage;

    [SerializeField]
    private Text infoText;

    [SerializeField]
    private Text openText;


    private RectTransform rectTransform;

    private int totalSecond = 14400;

    private void Start()
    {
        GameManager.Instance.AddSaveTime(this);

        rectTransform = GetComponent<RectTransform>();
    }

    public void Show()
    {
        CheckReady();
        rectTransform.DOAnchorPosX(0f, 0.3f).From(new Vector2(2000f, 0f));
    }


    private void CheckReady()
    {
        totalSecond = PlayerPrefs.GetInt(key_total_second, 14400);

        int enter_total_second = Constants.GetNowTotalSecond();

        int delta_second = enter_total_second - PlayerPrefs.GetInt(key_leave_total_second);

        totalSecond -= delta_second;

        if (HasBonus())
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
        infoText.gameObject.SetActive(true);
        openText.gameObject.SetActive(false);

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
        infoText.text = TimeSpan.FromSeconds(totalSecond).ToString();

       // Save();
    }

    private void UpdateItem()
    {
        infoText.gameObject.SetActive(false);
        openText.gameObject.SetActive(true);

        particle.gameObject.SetActive(true);

        glowImage.DOFade(1f, 1f).SetLoops(-1);

        StopAllCoroutines();
    }

    public void OnClickOpen()
    {
        if (!HasBonus())
            return;

        particle.gameObject.SetActive(false);

        glowImage.DOFade(0f, 1f).OnComplete( () => {
            glowImage.DOKill();
        });
        totalSecond = 14400;

        //open pupop
        Constants.OpenPopup(Constants.popup_chest);

        StartCounter();
    }

    private bool HasBonus()
    {
        if (0 >= totalSecond)
        {
            return true;
        }

        return false;

    }


    public void Hide()
    {
        StopAllCoroutines();
        glowImage.DOKill();

        rectTransform.DOAnchorPosX(2000f, 0.01f);
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

    public void SaveTime()
    {
        Save();
    }
}
