using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AppMetricaSendEventContrrol
{
    private static int startSecond;

    private static bool isStart;


    public static void StartLevel()
    {
        SendSaveLevelFinish();

        Dictionary<string, object> eventParameters = new Dictionary<string, object>();

        eventParameters.Add("level_number", Constants.currentLevel + 1);

        int levelCount = PlayerPrefs.GetInt("keyEventLevelCount");
        levelCount++;
        PlayerPrefs.SetInt("keyEventLevelCount", levelCount);
        eventParameters.Add("level_count", levelCount);

        eventParameters.Add("enemy_count", Constants.enemyCount);

        AppMetrica.Instance.ReportEvent("level_start", eventParameters);

        startSecond = DateTime.Now.Second + DateTime.Now.Minute * 60 + DateTime.Now.Hour * 60 * 60;

        isStart = true;
    }

    public static void FinishLevel(string result)
    {
        Dictionary<string, object> eventParameters = new Dictionary<string, object>();

        eventParameters.Add("level_number", Constants.currentLevel + 1);

        int levelCount = PlayerPrefs.GetInt("keyEventLevelCount");
        eventParameters.Add("level_count", levelCount);

        eventParameters.Add("enemy_count", Constants.enemyCount);
        eventParameters.Add("enemy_kill_count", Constants.enemyKillCount);

        int t = DateTime.Now.Second + DateTime.Now.Minute * 60 + DateTime.Now.Hour * 60 * 60;

        int total = Mathf.Abs(t - startSecond);

        eventParameters.Add("time", total);
        eventParameters.Add("score", Constants.score);
        eventParameters.Add("best_score", Constants.bestScore);

        eventParameters.Add("reward_coins", Constants.rewardCoin);
        eventParameters.Add("reward_diamonds", Constants.rewardDiamond);
        eventParameters.Add("reward_energis", Constants.rewardEnergy);

        eventParameters.Add("total_coins", Constants.total_coins);
        eventParameters.Add("total_diamonds", Constants.total_diamonds);
        eventParameters.Add("total_energis", Constants.total_energies);

        eventParameters.Add("result", result);

        AppMetrica.Instance.ReportEvent("level_finish", eventParameters);

        isStart = false;
    }

    public static void SaveStates()
    {
        if (!isStart)
            return;

        PlayerPrefs.SetInt("State_level_number", Constants.currentLevel + 1);
        PlayerPrefs.SetInt("State_level_count", PlayerPrefs.GetInt("keyEventLevelCount"));
        PlayerPrefs.SetInt("State_enemy_count", Constants.enemyCount);
        PlayerPrefs.SetInt("State_enemy_kill_count", Constants.enemyKillCount);
        int t = DateTime.Now.Second + DateTime.Now.Minute * 60 + DateTime.Now.Hour * 60 * 60;
        int total = Mathf.Abs(t - startSecond);
        PlayerPrefs.SetInt("State_time", total);
        PlayerPrefs.SetInt("State_score", Constants.score);
        PlayerPrefs.SetInt("State_best_score", Constants.bestScore);
        PlayerPrefs.SetFloat("State_reward_coins", Constants.rewardCoin);
        PlayerPrefs.SetFloat("State_reward_diamonds", Constants.rewardDiamond);
        PlayerPrefs.SetFloat("State_reward_energis", Constants.rewardEnergy);
        PlayerPrefs.SetFloat("State_total_coins", Constants.total_coins);
        PlayerPrefs.SetFloat("State_total_diamonds", Constants.total_diamonds);
        PlayerPrefs.SetFloat("State_total_energis", Constants.total_energies);
        PlayerPrefs.SetString("State_result", "Leave");

        PlayerPrefs.SetInt("State_send_event", 1);
    }

    public static void SendSaveLevelFinish()
    {
        if (PlayerPrefs.GetInt("State_send_event") == 0)
            return;

        Dictionary<string, object> eventParameters = new Dictionary<string, object>();

        eventParameters.Add("level_number", PlayerPrefs.GetInt("State_level_number"));
        eventParameters.Add("level_count", PlayerPrefs.GetInt("State_level_count"));
        eventParameters.Add("enemy_count", PlayerPrefs.GetInt("State_enemy_count"));
        eventParameters.Add("enemy_kill_count", PlayerPrefs.GetInt("State_enemy_kill_count"));
        eventParameters.Add("time", PlayerPrefs.GetInt("State_time"));
        eventParameters.Add("score", PlayerPrefs.GetInt("State_score"));
        eventParameters.Add("best_score", PlayerPrefs.GetInt("State_best_score"));
        eventParameters.Add("reward_coins", PlayerPrefs.GetFloat("State_reward_coins"));
        eventParameters.Add("reward_diamonds", PlayerPrefs.GetFloat("State_reward_diamonds"));
        eventParameters.Add("reward_energis", PlayerPrefs.GetFloat("State_reward_energis"));
        eventParameters.Add("total_coins", PlayerPrefs.GetFloat("State_total_coins"));
        eventParameters.Add("total_diamonds", PlayerPrefs.GetFloat("State_total_diamonds"));
        eventParameters.Add("total_energis", PlayerPrefs.GetFloat("State_total_energis"));
        eventParameters.Add("result", PlayerPrefs.GetString("State_result"));

        AppMetrica.Instance.ReportEvent("level_finish", eventParameters);

        PlayerPrefs.SetInt("State_send_event", 0);
    }


    public static void VideoAdsAvailable(string type, string placement, string result, bool connection)
    {
        Dictionary<string, object> eventParameters = new Dictionary<string, object>();

        eventParameters.Add("ad_type", type);
        eventParameters.Add("placement", placement);
        eventParameters.Add("result", result);
        eventParameters.Add("connection", connection);

        AppMetrica.Instance.ReportEvent("video_ads_available", eventParameters);
    }

    public static void VideoAdsStarted(string type, string placement, string result, bool connection)
    {
        Dictionary<string, object> eventParameters = new Dictionary<string, object>();

        eventParameters.Add("ad_type", type);
        eventParameters.Add("placement", placement);
        eventParameters.Add("result", result);
        eventParameters.Add("connection", connection);

        AppMetrica.Instance.ReportEvent("video_ads_started", eventParameters);
    }

    public static void VideoAdsWatch(string type, string placement, string result, bool connection)
    {
        Dictionary<string, object> eventParameters = new Dictionary<string, object>();

        eventParameters.Add("ad_type", type);
        eventParameters.Add("placement", placement);
        eventParameters.Add("result", result);
        eventParameters.Add("connection", connection);

        AppMetrica.Instance.ReportEvent("video_ads_watch", eventParameters);
    }


    public static void PaymentSuccessed(string product_id, string currency, decimal price, string type)
    {
        Dictionary<string, object> eventParameters = new Dictionary<string, object>();
        
        eventParameters.Add("inapp_id", product_id);
        eventParameters.Add("currency", currency);
        eventParameters.Add("price", price);
        eventParameters.Add("inapp_type", type);

        AppMetrica.Instance.ReportEvent("payment_succeed", eventParameters);
    }

    public static void RateUs(string reason, int result)
    {
        Dictionary<string, object> eventParameters = new Dictionary<string, object>();

        eventParameters.Add("show_reason", reason);
        eventParameters.Add("rate_result", result);

        AppMetrica.Instance.ReportEvent("rate_us", eventParameters);
    }

    public static void Technical(string step_name, int result)
    {
        Dictionary<string, object> eventParameters = new Dictionary<string, object>();

        eventParameters.Add("step_name", step_name);
        eventParameters.Add("first_start", result);

        AppMetrica.Instance.ReportEvent("technical", eventParameters);
    }

    public static void PurchaseWindow(string reason, string result)
    {
        Dictionary<string, object> eventParameters = new Dictionary<string, object>();

        eventParameters.Add("show_reason", reason);
        eventParameters.Add("result", result);

        AppMetrica.Instance.ReportEvent("purchase_window", eventParameters);
    }

    public static void Settings(int bgm, int sfx, int vibration, string control, string language)
    {
        Dictionary<string, object> eventParameters = new Dictionary<string, object>();

        eventParameters.Add("bgm", bgm);
        eventParameters.Add("sfx", sfx);
        eventParameters.Add("vibartion", vibration);
        eventParameters.Add("control", control);
        eventParameters.Add("language", language);

        AppMetrica.Instance.ReportEvent("settings", eventParameters);
    }

}
