using System;
using System.Collections;
using System.Collections.Generic;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using ConsentManager.Common;
using UnityEngine;
using UnityEngine.Networking;

namespace ConsentManager
{
    public class AppodelManager : Singleton<AppodelManager>, IBannerAdListener, IInterstitialAdListener,
        IRewardedVideoAdListener, IConsentInfoUpdateListener, IConsentFormListener
    {

        #region Application keys

#if UNITY_EDITOR && !UNITY_ANDROID && !UNITY_IPHONE
        public static string appKey = "";
#elif UNITY_ANDROID
        public static string appKey = "7264f616221c9572f4ff4ed6c25d01aea98fa16ce751cc40";
#elif UNITY_IPHONE
       public static string appKey = "e68b2f7da4c9974daf8d5cb95463c5a35256bd6eef48f12f";
#else
	public static string appKey = "";
#endif

        #endregion

        private ConsentForm consentForm;
        private ConsentManager consentManager;
        private Consent currentConsent;

        protected override void Awake()
        {
            base.Awake();
        }


        private void Start()
        {
            consentManager = ConsentManager.getInstance();

            if (PlayerPrefs.GetInt(Constants.key_gdpr_popup) == 0)
            {
                if (Application.internetReachability == NetworkReachability.NotReachable)
                {
                    // Constants.OpenPopup(Constants.popup_gdpr);
                    RequestConsentInfoUpdate();
                }
                else
                {
                    StartCoroutine(SetCountry());
                }
            }
            else
            {
                Initialize();
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
                    //Constants.OpenPopup(Constants.popup_gdpr);
                    RequestConsentInfoUpdate();
                }
                else
                {
                    if (contries.Contains(ipApiData.countryCode))
                    {
                        //Constants.OpenPopup(Constants.popup_gdpr);
                        RequestConsentInfoUpdate();
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



        public void RequestConsentInfoUpdate()
        {
            consentManager.requestConsentInfoUpdate(appKey, this);
        }

        public void LoadConsentForm()
        {
            consentForm = new ConsentForm.Builder().withListener(this).build();
            if (consentForm != null)
            {
                consentForm.load();
            }
        }

        public void ShowFormAsActivity()
        {
            if (consentForm != null)
            {
                consentForm.showAsActivity();
            }
            else
            {
                Debug.Log("showForm - false");
            }
        }

        public void ShowFormAsDialog()
        {
            if (consentForm != null)
            {
                consentForm.showAsDialog();
            }
            else
            {
                Debug.Log("showForm - false");
            }
        }

        public void Initialize()
        {
            InitWithConsent(currentConsent != null);
        }

        private void InitWithConsent(bool isConsent)
        {
            Appodeal.setLogLevel(Appodeal.LogLevel.Verbose);
            Appodeal.setUserId("1");
            Appodeal.setUserAge(1);
            Appodeal.setUserGender(UserSettings.Gender.OTHER);
            Appodeal.disableLocationPermissionCheck();
            Appodeal.setTriggerOnLoadedOnPrecache(Appodeal.INTERSTITIAL, true);
            Appodeal.setSmartBanners(true);
            Appodeal.setBannerAnimation(false);
            Appodeal.setTabletBanners(false);
            Appodeal.setBannerBackground(false);
            Appodeal.setChildDirectedTreatment(false);
            Appodeal.muteVideosIfCallsMuted(true);
            Appodeal.setSharedAdsInstanceAcrossActivities(true);
            Appodeal.setAutoCache(Appodeal.INTERSTITIAL, false);
            Appodeal.setAutoCache(Appodeal.REWARDED_VIDEO, false);
           // Appodeal.setUseSafeArea(true);


            if (isConsent)
            {
                Appodeal.initialize(appKey,
                    Appodeal.INTERSTITIAL | Appodeal.BANNER | Appodeal.REWARDED_VIDEO, currentConsent);
            }
            else
            {
                Appodeal.initialize(appKey,
                    Appodeal.INTERSTITIAL | Appodeal.BANNER | Appodeal.REWARDED_VIDEO, true);
            }

            Appodeal.setBannerCallbacks(this);
            Appodeal.setInterstitialCallbacks(this);
            Appodeal.setRewardedVideoCallbacks(this);

            ShowBannerBottom();

            Appodeal.isLoaded(Appodeal.REWARDED_VIDEO);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            Appodeal.destroy(Appodeal.BANNER);
        }

        #region Banner

        public void ShowBannerBottom()
        {
            Appodeal.show(Appodeal.BANNER_BOTTOM, "default");
        }

        public void onBannerLoaded(int height, bool isPrecache)
        {

        }

        public void onBannerFailedToLoad()
        {
            AppMetricaSendEventContrrol.VideoAdsAvailable("banner", "menu", "not_available", false);
        }

        public void onBannerShown()
        {
            AppMetricaSendEventContrrol.VideoAdsAvailable("banner", "menu", "success", true);
            AppMetricaSendEventContrrol.VideoAdsStarted("banner", "menu", "show", true);
        }

        public void onBannerClicked()
        {
            AppMetricaSendEventContrrol.VideoAdsWatch("banner", "menu", "clicked", true);
        }

        public void onBannerExpired()
        {

        }

        #endregion

        #region Interstitial

        public void ShowInterstitial()
        {
            if (Appodeal.isLoaded(Appodeal.INTERSTITIAL) && Appodeal.canShow(Appodeal.INTERSTITIAL, "default") && !Appodeal.isPrecache(Appodeal.INTERSTITIAL))
            {
                Appodeal.show(Appodeal.INTERSTITIAL);

                AppMetricaSendEventContrrol.VideoAdsAvailable("interstitial", "game_end", "success", true);
            }
            else
            {
                Appodeal.cache(Appodeal.INTERSTITIAL);

                AppMetricaSendEventContrrol.VideoAdsAvailable("interstitial", "game_end", "not_available", false);
            }
        }

        public void onInterstitialLoaded(bool isPrecache)
        {

        }

        public void onInterstitialFailedToLoad()
        {

        }

        public void onInterstitialShowFailed()
        {

        }

        public void onInterstitialShown()
        {
            AppMetricaSendEventContrrol.VideoAdsStarted("interstitial", "game_end", "start", true);
            AppMetricaSendEventContrrol.VideoAdsWatch("interstitial", "game_end", "watched", true);
        }

        public void onInterstitialClosed()
        {

        }

        public void onInterstitialClicked()
        {
            AppMetricaSendEventContrrol.VideoAdsWatch("interstitial", "game_end", "clicked", true);
        }

        public void onInterstitialExpired()
        {

        }

        #endregion

        #region Rewarded

        private Action rewardedAction;
        private Action rewardedCancelAction;

        private string rewardedPlacement;

        public void ShowRewardedVideo(string placement, Action action, Action cancelAction)
        {
            rewardedAction = action;
            rewardedCancelAction = cancelAction;
            rewardedPlacement = placement;
            
            if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO) && Appodeal.canShow(Appodeal.REWARDED_VIDEO, "default"))
            {
                Appodeal.show(Appodeal.REWARDED_VIDEO);

                AppMetricaSendEventContrrol.VideoAdsAvailable("rewarded", placement, "success", true);

            }
            else
            {
                Constants.HasRewarded = false;
                Appodeal.cache(Appodeal.REWARDED_VIDEO);

                AppMetricaSendEventContrrol.VideoAdsAvailable("rewarded", placement, "not_available", false);
            }
        }

        public bool HasRewarded()
        {
            return (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO) && Appodeal.canShow(Appodeal.REWARDED_VIDEO, "default")) || Constants.HasRewarded;
        }

        public void onRewardedVideoLoaded(bool precache)
        {
            Constants.HasRewarded = true;
        }

        public void onRewardedVideoFailedToLoad()
        {
            Constants.HasRewarded = false;
            rewardedCancelAction?.Invoke();
        }

        public void onRewardedVideoShowFailed()
        {
            rewardedCancelAction?.Invoke();
        }

        public void onRewardedVideoShown()
        {
            AppMetricaSendEventContrrol.VideoAdsStarted("rewarded", rewardedPlacement, "start", true);
        }

        public void onRewardedVideoFinished(double amount, string name)
        {
            AppMetricaSendEventContrrol.VideoAdsWatch("rewarded", rewardedPlacement, "watched", true);
        }

        public void onRewardedVideoClosed(bool finished)
        {
            Constants.HasRewarded = false;
            if (!finished)
            {
                rewardedCancelAction?.Invoke();
                AppMetricaSendEventContrrol.VideoAdsWatch("rewarded", rewardedPlacement, "canceled", true);
            }
            else
            {
                rewardedAction?.Invoke();
            }
        }

        public void onRewardedVideoExpired()
        {

        }

        public void onRewardedVideoClicked()
        {
            AppMetricaSendEventContrrol.VideoAdsWatch("rewarded", rewardedPlacement, "clicked", true);
        }

        public void onConsentInfoUpdated(Consent consent)
        {
            currentConsent = consent;

            LoadConsentForm();
        }

        public void onFailedToUpdateConsentInfo(ConsentManagerException error)
        {
            Initialize();
        }

        public void onConsentFormLoaded()
        {
            ShowFormAsDialog();
        }

        public void onConsentFormError(ConsentManagerException consentManagerException)
        {
            Initialize();
        }

        public void onConsentFormOpened()
        {
            
        }

        public void onConsentFormClosed(Consent consent)
        {
            Initialize();
        }

        #endregion

    }
}
