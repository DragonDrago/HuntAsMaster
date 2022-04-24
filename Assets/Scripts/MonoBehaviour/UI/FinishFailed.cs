using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using ConsentManager;

public class FinishFailed : MonoBehaviour
{

    [SerializeField]
    private GameLevelBar levelBar;

    [SerializeField]
    private Rewards rewards;

    [SerializeField]
    private SpinBonus spinBonus;

    [SerializeField]
    private Transform tryAganTransform;

    private bool isSpinShow;


    public void Show()
    {
        gameObject.SetActive(true);

        levelBar.FailedShow();

        float bonusCoin = Constants.bonusCoin + Constants.rewardCoin * 2;
        float bonusDiamond = Constants.bonusDiamond + Constants.rewardDiamond * 2;

        rewards.Show(Constants.rewardCoin, false, Constants.rewardDiamond, false, Constants.rewardEnergy, false);

        if(AppodelManager.Instance.HasRewarded())
        {
            isSpinShow = true;
            spinBonus.Show( bonusCoin, bonusDiamond, 1);
        }
            

        tryAganTransform.DOScale(Vector3.one, 1f).SetEase(Ease.InOutSine);
    }

    public void Hide()
    {
        
        levelBar.Hide();

        rewards.Hide();

        if(isSpinShow)
        {
            isSpinShow = false;
            spinBonus.Hide();
        }
        
        tryAganTransform.localScale = Vector3.zero;

        gameObject.SetActive(false);
    }


}
