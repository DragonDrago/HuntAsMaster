using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using System;

public class PurchaseManager : Singleton<PurchaseManager>, IStoreListener
{
    IStoreController storeController;

    private Action doneAction, errorAction;

    private int remove_force_ads_buy;
    private int extra_weapon_buy;
    private int subscribe_buy;
    private int product_buy;

    void Start()
    {
        InitializePurchasing();

        if(PlayerPrefs.HasKey(Constants.product_key_remove_force_ads_restore) == true)
        {
            RemoveForceAdsPurchased();
        }

        if(PlayerPrefs.HasKey(Constants.product_key_extra_weapon_restore) == true)
        {
            ExtraWeaponPurchased();
        }
    }

    void InitializePurchasing()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(Constants.product_remove_force_ads, ProductType.NonConsumable);
        builder.AddProduct(Constants.product_extra_weapon, ProductType.NonConsumable);

        builder.AddProduct(Constants.product_subcripe, ProductType.Subscription);

        builder.AddProduct(Constants.product_energy, ProductType.Consumable);

        builder.AddProduct(Constants.product_diamond0, ProductType.Consumable);
        builder.AddProduct(Constants.product_diamond1, ProductType.Consumable);
        builder.AddProduct(Constants.product_diamond2, ProductType.Consumable);
        builder.AddProduct(Constants.product_diamond3, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    public void BuyProduct(string productName, Action done, Action error)
    {
        this.doneAction = done;
        this.errorAction = error;

        if(productName.Equals(Constants.product_remove_force_ads))
        {
            remove_force_ads_buy = 1;
        }
        else if(productName.Equals(Constants.product_extra_weapon))
        {
            extra_weapon_buy = 1;
        }
        else if(productName.Equals(Constants.product_subcripe))
        {
            subscribe_buy = 1;
        }
        else
        {
            product_buy = 1;
        }

        storeController.InitiatePurchase(productName);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        var product = args.purchasedProduct;

        string id = product.definition.id;
        string currency = product.metadata.isoCurrencyCode;

        string priceText = product.metadata.localizedPriceString;
        decimal price = product.metadata.localizedPrice;

        if (product.definition.id == Constants.product_remove_force_ads)
        {
            if (remove_force_ads_buy == 1)
            {
                remove_force_ads_buy = 2;
                doneAction?.Invoke();

                AppsFlyerSendEventController.SendEvent(id, currency, priceText);

                AppMetricaSendEventContrrol.PaymentSuccessed(id, currency, price, "NonConsumable");
            }
            else
            {
                // o'zi sotib olib bo'lingan
                RemoveForceAdsPurchased();
            }
        }
        else if (product.definition.id == Constants.product_extra_weapon)
        {
            if(extra_weapon_buy == 1)
            {
                extra_weapon_buy = 2;
                doneAction?.Invoke();

                AppsFlyerSendEventController.SendEvent(id, currency, priceText);

                AppMetricaSendEventContrrol.PaymentSuccessed(id, currency, price, "NonConsumable");
            }
            else
            {
                //o'zi sotib olib bo'lingan
                ExtraWeaponPurchased();
            }
        }
        else if(product.definition.id == Constants.product_subcripe)
        {
            if(subscribe_buy == 1)
            {
                subscribe_buy = 2;
                doneAction?.Invoke();

                AppsFlyerSendEventController.SendEvent(id, currency, priceText);

                AppMetricaSendEventContrrol.PaymentSuccessed(id, currency, price, "Subscription");
            }
            else
            {
                //sotib olingan
            }
        }
        else
        {
            doneAction?.Invoke();

            if(product_buy == 1)
            {
                product_buy = 2;

                AppsFlyerSendEventController.SendEvent(id, currency, priceText);

                
                AppMetricaSendEventContrrol.PaymentSuccessed(id, currency, price, "Consumable");
            }

        }    


        Debug.Log($"Purchase Complete - Product: {product.definition.id}");

        return PurchaseProcessingResult.Complete;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log($"In-App Purchasing initialize failed: {error}");
        Constants.isPurchaseInstalized = false;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
        errorAction?.Invoke();
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("In-App Purchasing successfully initialized");
        Constants.isPurchaseInstalized = true;
        storeController = controller;


        Constants.price_remove_force_ads = storeController.products.WithID(Constants.product_remove_force_ads).metadata.localizedPriceString;
        Constants.price_extra_weapon = storeController.products.WithID(Constants.product_extra_weapon).metadata.localizedPriceString;

        Constants.price_subcripe = storeController.products.WithID(Constants.product_subcripe).metadata.localizedPriceString;

        Constants.price_energy = storeController.products.WithID(Constants.product_energy).metadata.localizedPriceString;
        Constants.price_diamond0 = storeController.products.WithID(Constants.product_diamond0).metadata.localizedPriceString;
        Constants.price_diamond1 = storeController.products.WithID(Constants.product_diamond1).metadata.localizedPriceString;
        Constants.price_diamond2 = storeController.products.WithID(Constants.product_diamond2).metadata.localizedPriceString;
        Constants.price_diamond3 = storeController.products.WithID(Constants.product_diamond3).metadata.localizedPriceString;

        if (storeController.products.WithID(Constants.product_remove_force_ads).hasReceipt)
        {
            RemoveForceAdsPurchased();
        }

        if(storeController.products.WithID(Constants.product_extra_weapon).hasReceipt)
        {
            ExtraWeaponPurchased();
        }

        if(storeController.products.WithID(Constants.product_subcripe).hasReceipt)
        {
            Constants.subcripePurchase = 1;
            Constants.removeForceAdsPurchase = 1;
        }
        else
        {
            Constants.subcripePurchase = 0;

            if(Constants.removeAdsNoBuy == 0)
            {
                Constants.removeForceAdsPurchase = 0;
            }
        }
    }

    private void RemoveForceAdsPurchased()
    {
        Constants.removeForceAdsPurchase = 1;
        Constants.removeAdsNoBuy = 1;
    }

    private void ExtraWeaponPurchased()
    {
        Constants.extraWeaponPurchase = 1;                                                       
    }

}