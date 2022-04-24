using System;
using System.Collections;
using System.Collections.Generic;
using ConsentManager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OfflineCoinPopup : MonoBehaviour
{
    

    [SerializeField]
    private GameObject coinInfoObject;

    [SerializeField]
    private GameObject diamondInfoObject;


    [SerializeField]
    private TextMeshProUGUI coinValueText;

    [SerializeField]
    private TextMeshProUGUI diamondValueText;

    [SerializeField]
    private Button doubleButton;

    [SerializeField]
    private GameObject particleObject;


    private Action watchAction, cancelAction;

    private float coin, diamond;

    private int totalSecond = 86400;

    private bool isHasDiamond;

    public void Show()
    {
        
        isHasDiamond = Constants.subcripePurchase == 0 ? false : true;

        coin = CheckCoin();

        //has diamond
        if (isHasDiamond)
        {
            coin += 1000;
            diamond = 100;
            diamondValueText.text = Constants.ConvertShortNumber(diamond);

            diamondInfoObject.SetActive(true);
        }

       
        coin += Constants.currentOfflineUpgradeLevel * 10;
        coinValueText.text = Constants.ConvertShortNumber(coin);

        coinInfoObject.SetActive(true);


        //has ads
        watchAction += RewardedWatched;
        cancelAction += OnClickCollect;

        doubleButton.interactable = Constants.HasRewarded;

        if (!AppodelManager.Instance.HasRewarded())
            StartCoroutine(WaitForRewarded());
    }

    private float CheckCoin()
    {
        //totalSecond = PlayerPrefs.GetInt(key_total_second, 86400);

        int enter_total_second = Constants.GetNowTotalSecond();
        int delta_second = enter_total_second - PlayerPrefs.GetInt(Constants.key_leave_total_second_offline_coin);

        totalSecond -= delta_second;

        if (0 >= totalSecond)
        {
            // max coin
            coin = 86400 / (int)60;
        }
        else
        {
            coin = (86400 - totalSecond) / (int)60;
        }

        return coin;
    }

    private IEnumerator WaitForRewarded()
    {
        yield return new WaitForSeconds(1f);

        if (!AppodelManager.Instance.HasRewarded())
            StartCoroutine(WaitForRewarded());
        else
        {
            doubleButton.interactable = true;
        }
    }


    public void Hide()
    {
        StopAllCoroutines();
    }


    public void OnClickCollect()
    {
        particleObject.SetActive(true);

        Constants.total_coins += coin;

        if(isHasDiamond)
        {
            Constants.total_diamonds += diamond;
        }

        GameManager.Instance.UpdateStatus();

        Constants.HidePopup(0.75f);
    }

    public void OnClickDoubleCollect()
    {
        AppodelManager.Instance.ShowRewardedVideo("offline_earning_collect", watchAction, cancelAction);
    }

    private void RewardedWatched()
    {
        coinValueText.text = Constants.ConvertShortNumber(2 * coin);

        particleObject.SetActive(true);

        Constants.total_coins += 2 * coin;

        if (isHasDiamond)
        {
            Constants.total_diamonds += 2 * diamond;
        }

        GameManager.Instance.UpdateStatus();

        Constants.HidePopup(0.75f);
    }

    
}
