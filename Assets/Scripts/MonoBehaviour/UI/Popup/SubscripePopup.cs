using System;
using System.Collections;
using System.Collections.Generic;
using I2.Loc;
using TMPro;
using UnityEngine;

public class SubscripePopup : MonoBehaviour
{

    [SerializeField]
    private LocalizationParamsManager priceText;


    private Action doneAction, errorAction;

    private void Start()
    {
        doneAction += PurchaseSuccess;
        errorAction += PurchaseError;
    }

    public void Show()
    {
        priceText.SetParameterValue("VALUE", Constants.price_subcripe);

        AppMetricaSendEventContrrol.PurchaseWindow("click_membership", "show");
    }


    public void OnClickSubcripe()
    {
        //open purchase

        PurchaseManager.Instance.BuyProduct(Constants.product_subcripe, doneAction, errorAction);
    }

    private void PurchaseSuccess()
    {
        //remove ads
        Constants.removeForceAdsPurchase = 1;

        AppMetricaSendEventContrrol.PurchaseWindow("click_membership", "buy");
        Constants.subcripePurchase = 1;

        Constants.InfoPopup(Constants.title_success,
            Constants.subscribe_succssfull,
            GameManager.Instance.GetSuccessSprite);

        GameManager.Instance.ShowSubcripeStatus();

        OnClickClose();

        Constants.OpenPopup(Constants.popup_offline_coin);
    }

    private void PurchaseError()
    {
        
        Constants.InfoPopup(Constants.title_error, Constants.message_error, GameManager.Instance.GetErrorSprite);
        OnClickClose();
    }


    public void OnClickClose()
    {
        AppMetricaSendEventContrrol.PurchaseWindow("click_membership", "close");
        Constants.HidePopup();
    }


}
