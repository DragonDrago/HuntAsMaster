using System;
using UnityEngine;
using UnityEngine.UI;
using I2.Loc;
using ConsentManager;

public class ShopView : MonoBehaviour
{
    // Remove force ads
    [Header("Remove Force ADS")]
    [SerializeField]
    private GameObject removeForceAdsObject;
    [SerializeField]
    private Text removeAdsText;

    private Action removeForceAdsActionDone, removeForceAdsActionError;

    // Extra Weapon
    [Header("Extra Weapon")]
    [SerializeField]
    private GameObject extraWeaponObject;
    [SerializeField]
    private Text extraWeaponText;

    private Action extraWeaponActionDone, extraWeaponActionError;

    // Subcripe
    [Header("Subcripe")]
    [SerializeField]
    private GameObject subcripeObject;
    [SerializeField]
    private LocalizationParamsManager subcripeParamText;

    private Action subcripeActionDone, subcripeActionError;

    // Energy
    [Header("Energy")]
    [SerializeField]
    private Text energyWatchCountText;

    [SerializeField]
    private Text energyDiamondCountText;
    [SerializeField]
    private Text energyDiamondCostText;

    [SerializeField]
    private Text energyCountText;
    [SerializeField]
    private Text energyCostText;

    private Action energyWatchVideoActionDone, energyWatchVideoActionError;
    private Action energyBuyActionDone, energyBuyActionError;

    //Coin
    [Header("Coin")]
    [SerializeField]
    private Text coinWatchCountText;

    [SerializeField]
    private Text coin0CountText;
    [SerializeField]
    private Text coin0CostText;

    [SerializeField]
    private Text coin1CountText;
    [SerializeField]
    private Text coin1CostText;

    [SerializeField]
    private Text coin2CountText;
    [SerializeField]
    private Text coin2CostText;

    [SerializeField]
    private Text coin3CountText;
    [SerializeField]
    private Text coin3CostText;

    private Action coinWatchVideoActionDone, coinWatchVideoActionError;

    // Diamond
    [Header("Diamond")]
    [SerializeField]
    private Text diamondWatchCountText;

    [SerializeField]
    private Text diamond0CountText;
    [SerializeField]
    private Text diamond0CostText;

    [SerializeField]
    private Text diamond1CountText;
    [SerializeField]
    private Text diamond1CostText;

    [SerializeField]
    private Text diamond2CountText;
    [SerializeField]
    private Text diamond2CostText;

    [SerializeField]
    private Text diamond3CountText;
    [SerializeField]
    private Text diamond3CostText;

    private Action diamondWatchVideoActionDone, diamondWatchVideoActionError;
    private Action diamond0ActionDone, diamond0ActionError;
    private Action diamond1ActionDone, diamond1ActionError;
    private Action diamond2ActionDone, diamond2ActionError;
    private Action diamond3ActionDone, diamond3ActionError;
    

    private void Start()
    {
        removeForceAdsActionDone += RemoveForceAdsSuccess;
        removeForceAdsActionError += RemoveForceAdsError;

        extraWeaponActionDone += ExtraWeaponSuccess;
        extraWeaponActionError += ExtraWeaponError;

        subcripeActionDone += SubcripeSuccess;
        subcripeActionError += SubcripeError;

        energyWatchVideoActionDone += EnergyWatchVideoSuccess;
        energyWatchVideoActionError += EnergyWatchVideoError;
        energyBuyActionDone += EnergyBuySuccess;
        energyBuyActionError += EnergyBuyError;

        coinWatchVideoActionDone += CoinWatchVideoSuccess;
        coinWatchVideoActionError += CoinWatchVideoError;

        diamondWatchVideoActionDone += DiamondWatchVideoSuccess;
        diamondWatchVideoActionError += DiamondWatchVideoError;

        diamond0ActionDone += Diamond0Success;
        diamond0ActionError += Diamond0Error;
        diamond1ActionDone += Diamond1Success;
        diamond1ActionError += Diamond1Error;
        diamond2ActionDone += Diamond2Success;
        diamond2ActionError += Diamond2Error;
        diamond3ActionDone += Diamond3Success;
        diamond3ActionError += Diamond3Error;
    }

    public void Show()
    {
        if (Constants.removeForceAdsPurchase == 0)
        {
            removeAdsText.text = Constants.price_remove_force_ads;
            removeForceAdsObject.SetActive(true);
        }
        else
        {
            removeForceAdsObject.SetActive(false);
        }

        if(Constants.extraWeaponPurchase == 0)
        {
            extraWeaponText.text = Constants.price_extra_weapon;
            extraWeaponObject.SetActive(true);
        }
        else
        {
            extraWeaponObject.SetActive(false);
        }

        if(Constants.subcripePurchase == 0)
        {
            if (PlayerPrefs.GetInt(Constants.key_subcripe_popup) == 1)
            {
                subcripeObject.SetActive(false);
            }
            else
            {
                subcripeParamText.SetParameterValue("VALUE", Constants.price_subcripe.ToString());
                subcripeObject.SetActive(true);
            }
        }
        else
        {
            subcripeObject.SetActive(false);
        }
        

        // Energy
        energyWatchCountText.text = "x" + Constants.product_energy_watch_count.ToString();

        energyDiamondCountText.text = "x" + Constants.product_energy_diamond_count.ToString();
        energyDiamondCostText.text = "x" + Constants.product_energy_diamond_cost.ToString();

        energyCountText.text = "x" + Constants.product_energy_count.ToString();
        energyCostText.text = "x" + Constants.price_energy;

        // Coin
        coinWatchCountText.text = Constants.product_coin_watch_count.ToString();

        coin0CountText.text = Constants.product_coin0_count.ToString();
        coin0CostText.text = Constants.product_coin0_diamond_cost.ToString();

        coin1CountText.text = Constants.product_coin1_count.ToString();
        coin1CostText.text = Constants.product_coin1_diamond_cost.ToString();

        coin2CountText.text = Constants.product_coin2_count.ToString();
        coin2CostText.text = Constants.product_coin2_diamond_cost.ToString();

        coin3CountText.text = Constants.product_coin3_count.ToString();
        coin3CostText.text = Constants.product_coin3_diamond_cost.ToString();

        // Diamond
        diamondWatchCountText.text = Constants.product_diamond_watch_count.ToString();

        diamond0CountText.text = Constants.product_diamond0_count.ToString();
        diamond0CostText.text = Constants.price_diamond0;

        diamond1CountText.text = Constants.product_diamond1_count.ToString();
        diamond1CostText.text = Constants.price_diamond1;

        diamond2CountText.text = Constants.product_diamond2_count.ToString();
        diamond2CostText.text = Constants.price_diamond2;

        diamond3CountText.text = Constants.product_diamond3_count.ToString();
        diamond3CostText.text = Constants.price_diamond3;

    }

    #region remove force ads
    public void OnClickRemoveForceAds()
    {
        AppMetricaSendEventContrrol.PurchaseWindow("clik_remove_force_ads", "show");
        BuyProduct(Constants.product_remove_force_ads, removeForceAdsActionDone, removeForceAdsActionError);
    }

    private void RemoveForceAdsSuccess()
    {
        Constants.removeAdsNoBuy = 1;

        AppMetricaSendEventContrrol.PurchaseWindow("clik_remove_force_ads", "buy");
        removeForceAdsObject.SetActive(false);

        Constants.removeForceAdsPurchase = 1;

        PlayerPrefs.SetInt(Constants.product_key_remove_force_ads_restore, 1);

        Constants.InfoPopup(Constants.title_success,
            Constants.removeads_purchased_succssfull,
            GameManager.Instance.GetSuccessSprite);
    }

    private void RemoveForceAdsError()
    {
        AppMetricaSendEventContrrol.PurchaseWindow("clik_remove_force_ads", "close");
        Constants.InfoPopup(Constants.title_error, Constants.message_error, GameManager.Instance.GetErrorSprite);
    }

    #endregion //remove force ads end

    #region Extra weapon

    public void OnClickExtraWeapon()
    {
        AppMetricaSendEventContrrol.PurchaseWindow("clik_gifts", "show");
        BuyProduct(Constants.product_extra_weapon, extraWeaponActionDone, extraWeaponActionError);
    }

    private void ExtraWeaponSuccess()
    {
        AppMetricaSendEventContrrol.PurchaseWindow("clik_gifts", "buy");

        extraWeaponObject.SetActive(false);

        Constants.extraWeaponPurchase = 1;
        
        PlayerPrefs.SetInt(Constants.product_key_extra_weapon_restore, 1);

        Constants.InfoPopup(Constants.title_success,
            Constants.gifts_purchased_succssfull,
            GameManager.Instance.GetSuccessSprite);
    }

    private void ExtraWeaponError()
    {
        AppMetricaSendEventContrrol.PurchaseWindow("clik_gifts", "close");
        Constants.InfoPopup(Constants.title_error, Constants.message_error, GameManager.Instance.GetErrorSprite);
    }

    #endregion // extra weapon end

    #region Subcripe
    public void OnClickSubcripe()
    {
        AppMetricaSendEventContrrol.PurchaseWindow("clik_subsciption", "show");
        BuyProduct(Constants.product_subcripe, subcripeActionDone, subcripeActionError);
    }

    private void SubcripeSuccess()
    {
        // remove ads
        removeForceAdsObject.SetActive(false);

        Constants.removeForceAdsPurchase = 1;

        AppMetricaSendEventContrrol.PurchaseWindow("clik_subsciption", "buy");

        subcripeObject.SetActive(false);

        Constants.subcripePurchase = 1;

        Constants.InfoPopup(Constants.title_success,
            Constants.subscribe_succssfull,
            GameManager.Instance.GetSuccessSprite);

        GameManager.Instance.ShowSubcripeStatus();
    }

    private void SubcripeError()
    {
        AppMetricaSendEventContrrol.PurchaseWindow("clik_subsciption", "close");
        Constants.InfoPopup(Constants.title_error, Constants.message_error, GameManager.Instance.GetErrorSprite);
    }

    #endregion // subcripe end

    #region Energy

    public void OnClickEnergyWatchVideo()
    {
        WatchVideo("shop_free_energy", energyWatchVideoActionDone, energyWatchVideoActionError);
    }

    private void EnergyWatchVideoSuccess()
    {
        Constants.total_energies += Constants.product_energy_watch_count;

        GameManager.Instance.UpdateStatus();
    }

    private void EnergyWatchVideoError()
    {

    }

    public void OnClickEnergyDiamond()
    {
        if(Constants.total_diamonds - Constants.product_energy_diamond_cost >= 0)
        {
            Constants.total_diamonds -= Constants.product_energy_diamond_cost;
            Constants.total_energies += Constants.product_energy_diamond_count;

            GameManager.Instance.UpdateStatus();
        }
    }

    public void OnClickEnergyBuy()
    {
        AppMetricaSendEventContrrol.PurchaseWindow("clik_energy_buy", "show");
        BuyProduct(Constants.product_energy, energyBuyActionDone, energyBuyActionError);
    }

    private void EnergyBuySuccess()
    {
        AppMetricaSendEventContrrol.PurchaseWindow("clik_energy_buy", "buy");

        Constants.total_energies += Constants.product_energy_count;

        Constants.InfoPopup(Constants.title_success, Constants.message_success, GameManager.Instance.GetSuccessSprite);

        GameManager.Instance.UpdateStatus();
    }

    private void EnergyBuyError()
    {
        AppMetricaSendEventContrrol.PurchaseWindow("clik_energy_buy", "close");
        Constants.InfoPopup(Constants.title_error, Constants.message_error, GameManager.Instance.GetErrorSprite);
    }

    #endregion // energy end

    #region Coin
    public void OnClickCoinWatchVideo()
    {
        WatchVideo("shop_free_coins", coinWatchVideoActionDone, coinWatchVideoActionError);
    }

    private void CoinWatchVideoSuccess()
    {
        Constants.total_coins += Constants.product_coin_watch_count;

        GameManager.Instance.UpdateStatus();
    }

    private void CoinWatchVideoError()
    {
            
    }

    public void OnClickCoin0()
    {
        float diamond = Constants.total_diamonds - Constants.product_coin0_diamond_cost;

        if( diamond >= 0 )
        {
            Constants.total_diamonds -= Constants.product_coin0_diamond_cost;
            Constants.total_coins += Constants.product_coin0_count;

            GameManager.Instance.UpdateStatus();
        }
    }

    public void OnClickCoin1()
    {
        float diamond = Constants.total_diamonds - Constants.product_coin1_diamond_cost;

        if (diamond >= 0)
        {
            Constants.total_diamonds -= Constants.product_coin1_diamond_cost;
            Constants.total_coins += Constants.product_coin1_count;

            GameManager.Instance.UpdateStatus();
        }
    }

    public void OnClickCoin2()
    {
        float diamond = Constants.total_diamonds - Constants.product_coin2_diamond_cost;

        if (diamond >= 0)
        {
            Constants.total_diamonds -= Constants.product_coin2_diamond_cost;
            Constants.total_coins += Constants.product_coin2_count;

            GameManager.Instance.UpdateStatus();
        }
    }

    public void OnClickCoin3()
    {
        float diamond = Constants.total_diamonds - Constants.product_coin3_diamond_cost;

        if (diamond >= 0)
        {
            Constants.total_diamonds -= Constants.product_coin3_diamond_cost;
            Constants.total_coins += Constants.product_coin3_count;

            GameManager.Instance.UpdateStatus();
        }
    }

    #endregion // coin end

    #region Diamond

    public void OnClickDiamondWatchVideo()
    {
        WatchVideo("shop_free_diamonds", diamondWatchVideoActionDone, diamondWatchVideoActionError);
    }

    private void DiamondWatchVideoSuccess()
    {
        Constants.total_diamonds += Constants.product_diamond_watch_count;

        GameManager.Instance.UpdateStatus();
    }

    private void DiamondWatchVideoError()
    {

    }

    public void OnClickDiamond0()
    {
        AppMetricaSendEventContrrol.PurchaseWindow("clik_diamond0", "show");
        BuyProduct(Constants.product_diamond0, diamond0ActionDone, diamond0ActionError);
    }

    private void Diamond0Success()
    {
        AppMetricaSendEventContrrol.PurchaseWindow("clik_diamond0", "buy");

        Constants.total_diamonds += Constants.product_diamond0_count;

        Constants.InfoPopup(Constants.title_success, Constants.message_success, GameManager.Instance.GetSuccessSprite);

        GameManager.Instance.UpdateStatus();
    }

    private void Diamond0Error()
    {
        AppMetricaSendEventContrrol.PurchaseWindow("clik_diamond0", "close");
        Constants.InfoPopup(Constants.title_error, Constants.message_error, GameManager.Instance.GetErrorSprite);
    }

    public void OnClickDiamond1()
    {
        AppMetricaSendEventContrrol.PurchaseWindow("clik_diamond1", "show");
        BuyProduct(Constants.product_diamond1, diamond1ActionDone, diamond1ActionError);
    }

    private void Diamond1Success()
    {
        AppMetricaSendEventContrrol.PurchaseWindow("clik_diamond1", "buy");
        Constants.total_diamonds += Constants.product_diamond1_count;

        Constants.InfoPopup(Constants.title_success, Constants.message_success, GameManager.Instance.GetSuccessSprite);

        GameManager.Instance.UpdateStatus();
    }

    private void Diamond1Error()
    {
        AppMetricaSendEventContrrol.PurchaseWindow("clik_diamond1", "close");
        Constants.InfoPopup(Constants.title_error, Constants.message_error, GameManager.Instance.GetErrorSprite);
    }

    public void OnClickDiamond2()
    {
        AppMetricaSendEventContrrol.PurchaseWindow("clik_diamond2", "show");
        BuyProduct(Constants.product_diamond2, diamond2ActionDone, diamond2ActionError);
    }

    private void Diamond2Success()
    {
        AppMetricaSendEventContrrol.PurchaseWindow("clik_diamond2", "buy");

        Constants.total_diamonds += Constants.product_diamond2_count;

        Constants.InfoPopup(Constants.title_success, Constants.message_success, GameManager.Instance.GetSuccessSprite);

        GameManager.Instance.UpdateStatus();
    }

    private void Diamond2Error()
    {
        AppMetricaSendEventContrrol.PurchaseWindow("clik_diamond2", "close");
        Constants.InfoPopup(Constants.title_error, Constants.message_error, GameManager.Instance.GetErrorSprite);
    }

    public void OnClickDiamond3()
    {
        AppMetricaSendEventContrrol.PurchaseWindow("clik_diamond3", "show");
        //PurchaseManager.Instance.BuyProduct(Constants.product_diamond3, diamond3ActionDone, diamond3ActionError);
        BuyProduct(Constants.product_diamond3, diamond3ActionDone, diamond3ActionError);
    }

    private void Diamond3Success()
    {
        AppMetricaSendEventContrrol.PurchaseWindow("clik_diamond3", "buy");
        Constants.total_diamonds += Constants.product_diamond3_count;

        Constants.InfoPopup(Constants.title_success, Constants.message_success, GameManager.Instance.GetSuccessSprite);

        GameManager.Instance.UpdateStatus();
    }

    private void Diamond3Error()
    {
        AppMetricaSendEventContrrol.PurchaseWindow("clik_diamond3", "close");
        Constants.InfoPopup(Constants.title_error, Constants.message_error, GameManager.Instance.GetErrorSprite);
    }

    #endregion // diamond end

    private void BuyProduct(string productName, Action done, Action error)
    {
        if (Constants.isPurchaseInstalized)
        {
            PurchaseManager.Instance.BuyProduct(productName, done, error);
        }
        else
        {
            Constants.InfoPopup(Constants.title_error, Constants.message_noeehternet, GameManager.Instance.GetErrorSprite);
        }
    }

    private void WatchVideo(string placement, Action done, Action error)
    {
        AppodelManager.Instance.ShowRewardedVideo(placement, done, error);
    }

}
