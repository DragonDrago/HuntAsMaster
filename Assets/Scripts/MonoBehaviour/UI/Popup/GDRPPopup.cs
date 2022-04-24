using System.Collections;
using System.Collections.Generic;
using AppodealAds.Unity.Api;
using UnityEngine;

public class GDRPPopup : MonoBehaviour
{


    public void OnClickTermOfService()
    {
        Application.OpenURL("https://pages.flycricket.io/hunt-as-master/terms.html");
    }

    public void OnClickPrivacyPolicy()
    {
        Application.OpenURL("https://pages.flycricket.io/hunt-as-master/privacy.html");
    }

    public void OnClickAccept()
    {
        PlayerPrefs.SetInt(Constants.key_gdpr_popup, 1);
        //MaxSdk.SetHasUserConsent(true);
        Appodeal.updateConsent(true);
        Constants.HidePopup();
    }

}
