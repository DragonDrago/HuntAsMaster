using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System;
using I2.Loc;
using ConsentManager;

public class FinishCompleted : MonoBehaviour
{
    [SerializeField]
    private RectTransform centerRectTransform;

    [SerializeField]
    private RectTransform levelupTransform;

    [SerializeField]
    private CanvasGroup scoreGroup;

    [SerializeField]
    private Text levelUpText;

    [SerializeField]
    private LocalizationParamsManager scoreText;

    [SerializeField]
    private LocalizationParamsManager bestScoreText;

    [SerializeField]
    private Rewards rewards;

    [SerializeField]
    private Transform bonusSkinTransform;

    [SerializeField]
    private Transform usedSkinButtonTransform;

    [SerializeField]
    private SpinBonus spinBonus;

    [SerializeField]
    private Transform nextLevelTransform;


    private AudioSource audioSource;

    private Action watchActin, watchCancelAction;

    private bool isSpinShow;
    private bool isEnergyBonus;
    private bool isDiamondBonus;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        watchActin += Watched;
        watchCancelAction += WatchCancel;
    }


    public void Show()
    {
        gameObject.SetActive(true);
        bonusSkinTransform.localScale = Vector3.zero;
        usedSkinButtonTransform.localScale = Vector3.zero;

        isDiamondBonus = (Constants.currentLevel + 1) % 5 == 0;
        Constants.rewardDiamond += isDiamondBonus ? (Constants.currentLevel + 1) * 2 : 0;
        if (isDiamondBonus)
            Constants.total_diamonds += Constants.rewardDiamond;

        
        isEnergyBonus = (Constants.currentLevel + 1) % 10 == 0;
        Constants.rewardEnergy += isEnergyBonus ? 1 : 0;

        float bonusCoin = Constants.bonusCoin + Constants.rewardCoin * 2;
        float bonusDiamond = Constants.bonusDiamond + Constants.rewardDiamond * 2;

        if (Constants.gift_has == 0 && AppodelManager.Instance.HasRewarded())
        {
            centerRectTransform.pivot = new Vector2(0.5f, 1);
            centerRectTransform.anchorMax = new Vector2(0.5f, 1f);
            centerRectTransform.anchorMin = new Vector2(0.5f, 1f);
            centerRectTransform.anchoredPosition = new Vector2(0f, -150f);

            if (Constants.gift_part_index == 3)
            {
                bonusSkinTransform.DOScale(Vector3.one, 1f).SetEase(Ease.OutSine);
            }
            else
            {
                isSpinShow = true;
                spinBonus.Show(bonusCoin, bonusDiamond, 1);
            }
        }
        else
        {
            centerRectTransform.pivot = new Vector2(0.5f, 0.5f);
            centerRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            centerRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            centerRectTransform.anchoredPosition = new Vector2(0f, 500f);

            if (AppodelManager.Instance.HasRewarded())
            {
                isSpinShow = true;
                spinBonus.Show(bonusCoin, bonusDiamond, 1);
            }
            
        }

        scoreGroup.DOFade(1f, 1f);
        levelupTransform.DOScale(Vector3.one, 1f);
        levelUpText.text = (Constants.currentLevel + 1).ToString();

        scoreText.SetParameterValue("SCORE", Constants.score.ToString());

        if (Constants.bestScore < Constants.score)
        {
            Constants.bestScore = Constants.score;
            bestScoreText.SetParameterValue("SSCORE", Constants.bestScore.ToString());
            
        }
        else
        {
            bestScoreText.SetParameterValue("SCORE", Constants.bestScore.ToString());
            
        }

        rewards.Show(Constants.rewardCoin, false, Constants.rewardDiamond, isDiamondBonus, Constants.rewardEnergy, isEnergyBonus);

        nextLevelTransform.DOScale(Vector3.one, 1f).SetEase(Ease.OutSine);

        if(Constants.isSound)
        {
            if(audioSource != null)
                audioSource.Play();
        }
    }

    public void Hide()
    {
        levelupTransform.localScale = Vector3.zero;
        rewards.Hide();

        bonusSkinTransform.localScale = Vector3.zero;
        usedSkinButtonTransform.localScale = Vector3.zero;

        if(isSpinShow)
        {
            isSpinShow = false;
            spinBonus.Hide();
        }
        
        nextLevelTransform.localScale = Vector3.zero;

        gameObject.SetActive(false);
    }


    public void OnClickWatchGetSkin()
    {
        AppodelManager.Instance.ShowRewardedVideo("finish_completd_get_bonus", watchActin, watchCancelAction);
    }
   
    private void Watched()
    {
        if (Constants.giftEnum == Constants.GiftEnum.Skin)
        {
            int index = Constants.gift_skins_index[Constants.gift_current_skin_index - 1];

            PlayerPrefs.SetInt(Constants.key_item_lock_skin + index, 1);
            PlayerPrefs.Save();
        }
        else if (Constants.giftEnum == Constants.GiftEnum.Weapon)
        {
            int index = Constants.gift_weapons_index[Constants.gift_current_weapon_index - 1];

            PlayerPrefs.SetInt(Constants.key_item_lock_weapon + index, 1);
            PlayerPrefs.Save();
        }
            

        bonusSkinTransform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutSine);
        usedSkinButtonTransform.DOScale(Vector3.one, 1f).SetEase(Ease.OutSine);

    }

    private void WatchCancel()
    {
        
    }

    public void UsedGift()
    {
        if (Constants.giftEnum == Constants.GiftEnum.Skin)
        {
            int index = Constants.gift_skins_index[Constants.gift_current_skin_index - 1];

            Constants.skinItemCurrentIndex = index;

            PlayerPrefs.SetInt(Constants.key_item_lock_skin + index, 1);
            PlayerPrefs.SetInt(Constants.key_item_info_skin + index, 1);
            PlayerPrefs.Save();

            Constants.hasUseGiftSkin = true;
        }
        else if (Constants.giftEnum == Constants.GiftEnum.Weapon)
        {
            int index = Constants.gift_weapons_index[Constants.gift_current_weapon_index - 1];

            Constants.weaponItemCurrentIndex = index;

            PlayerPrefs.SetInt(Constants.key_item_lock_weapon + index, 1);
            PlayerPrefs.SetInt(Constants.key_item_info_weapon + index, 1);
            PlayerPrefs.Save();

            Constants.hasUseGiftWeapon = true;
        }

        GameManager.Instance.ControllerRestart();
    }

}
