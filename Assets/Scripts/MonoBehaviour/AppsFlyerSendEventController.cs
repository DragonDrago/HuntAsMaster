using System.Collections;
using System.Collections.Generic;
using AppsFlyerSDK;
using UnityEngine;

public static class AppsFlyerSendEventController
{

    public static void SendEvent(string productId, string priceCode, string price)
    {
        Dictionary<string, string> purchaseEvent = new Dictionary<string, string>();
        purchaseEvent.Add(AFInAppEvents.CONTENT_ID, productId);
        purchaseEvent.Add(AFInAppEvents.PRICE, price);
        purchaseEvent.Add(AFInAppEvents.CURRENCY, priceCode);
        purchaseEvent.Add(AFInAppEvents.REVENUE, price);
        purchaseEvent.Add(AFInAppEvents.QUANTITY, "1");
        AppsFlyer.sendEvent(AFInAppEvents.PURCHASE, purchaseEvent);
    }


}
