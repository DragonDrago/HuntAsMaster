using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HasGDPR : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetInt(Constants.key_gdpr_popup) == 0)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Constants.OpenPopup(Constants.popup_gdpr);
            }
            else
            {
                StartCoroutine(SetCountry());
            }
        }
    }

    private IEnumerator SetCountry()
    {
        string ip = new System.Net.WebClient().DownloadString("https://api.ipify.org");
        string uri = $"http://ip-api.com/json/{ip}";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            IpApiData ipApiData = IpApiData.CreateFromJSON(webRequest.downloadHandler.text);

            List<string> contries = new List<string>()
             {
                "AU", "BE", "BG", "HR", "CY", "CZ", "DK", "EE", "FI", "FR", "DE", "GR", "HU", "IE",
                "IT", "LV", "LT", "LU", "MT", "NL", "PL", "PT", "RO", "SK", "SI", "ES", "SE"
             };


            if (webRequest.error != null)
            {
                Constants.OpenPopup(Constants.popup_gdpr);
            }
            else
            {
                if (contries.Contains(ipApiData.countryCode))
                {
                    Constants.OpenPopup(Constants.popup_gdpr);
                }
                else
                {
                    PlayerPrefs.SetInt(Constants.key_gdpr_popup, 1);
                }
            }
        }

    }

    [Serializable]
    private class IpApiData
    {
        public string country;
        public string countryCode;

        public static IpApiData CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<IpApiData>(jsonString);
        }
    }





}
