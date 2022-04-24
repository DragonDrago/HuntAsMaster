using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class FinishView : MonoBehaviour
{
    [SerializeField]
    private FinishFailed finishFailed;

    [SerializeField]
    private FinishCompleted finishCompleted;


    public void ShowFailed()
    {
        ResetUseGift();

        gameObject.SetActive(true);
        finishFailed.Show();
    }

    public void HideFailed()
    {
        finishFailed.Hide();
        gameObject.SetActive(false);
    }

    public void ShowCompleted()
    {
        ResetUseGift();

        gameObject.SetActive(true);
        finishCompleted.Show();
    }

    public void HideCompleted()
    {
        finishCompleted.Hide();
        gameObject.SetActive(false);
    }

    private void ResetUseGift()
    {
        Constants.hasUseGiftSkin = false;
        Constants.hasUseGiftWeapon = false;
    }

    public void OnClickNextLevel()
    {
        Constants.score = 0;
        Constants.rewardCoin = 0;
        Constants.rewardDiamond = 0;
        Constants.rewardEnergy = 0;

        GameManager.Instance.ControllerRestart();
    }


}
