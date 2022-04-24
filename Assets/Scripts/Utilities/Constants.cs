using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using UnityEngine;
using UnityEngine.Networking;

public class Constants
{

    private const string key_total_coins = "keyTotalCons";
    private const string key_total_diamonds = "keyTotalDiamonds";
    private const string key_total_energes = "keyTotalEnerges";
    private const string key_current_level = "keyCurrentLevel";


    public static string key_item_lock_map = "ItemLockMap";
    public static string key_item_info_map = "ItemInfoMap";
    public static string key_item_lock_skin = "ItemLockSkin";
    public static string key_item_info_skin = "ItemInfoSkin";
    public static string key_item_lock_weapon = "ItemLockWeapon";
    public static string key_item_info_weapon = "ItemInfoWeapon";

    public static string key_item_info_map_has = "ItemInfoMapHas";
    public static string key_item_info_skin_has = "ItemInfoSkinHas";
    public static string key_item_info_weapon_has = "ItemInfoWeaponHas";

    public static string key_subcripe_popup = "keySubcripePopup";
    public static string key_gdpr_popup = "keyGDPRPopup";
    public static string key_open_offline_popup = "keyOpenOfflinePopup";

    public static string key_leave_total_second_offline_coin = "dailyLeaveTotalSecondOfflineCoin";
    public static string key_show_tutorial_upgrade = "ShowTutorialUpgrade";

    public static string key_rate_leave_total_second = "keyRateLeaveTotalSecond";

    public static string tag_Targeting = "Targeting";

    public static string name_items_popup = "ItemsPopup";
    public static string popup_offline_coin = "OfflineCoinPopup";
    public static string popup_subsripe = "SubscripePopup";
    public static string popup_friend = "FriendPopup";
    public static string popup_chest = "ChestPopup";
    public static string popup_gdpr = "GDPRPopup";
    public static string popup_rate = "RatePopup";
    public static string popup_joystick_control = "JoystickControlPopup";
    public static string popup_language = "LanguagePopup";

    public static int itemMapCount = 12;
    public static int itemSkinCount = 30;
    public static int itemWeaponCount = 66;

    public static UIPopup currentPopup;

    public static int joystickControl {
        //0 - reght 1- left
        get { return PlayerPrefs.GetInt("keyJoystickControl"); }
        set { PlayerPrefs.SetInt("keyJoystickControl", value); }
    }

    public static string language_current {

        get { return PlayerPrefs.GetString("keyCurrentLanguage"); }
        set { PlayerPrefs.SetString("keyCurrentLanguage", value); }
    }

    public static bool isEnemySentEvent;

    public static bool isSound;
    public static bool isMusic;
    public static bool isVibration;

    public static float total_coins {
        get { return PlayerPrefs.GetFloat(key_total_coins); }
        set { PlayerPrefs.SetFloat(key_total_coins, value); }
    }

    public static float total_diamonds {

        get { return PlayerPrefs.GetFloat(key_total_diamonds); }
        set { PlayerPrefs.SetFloat(key_total_diamonds, value); }
    }

    public static float total_energies {

        get { return PlayerPrefs.GetFloat(key_total_energes); }
        set { PlayerPrefs.SetFloat(key_total_energes, value); }
    }

    public static int open_rate {

        get { return PlayerPrefs.GetInt("keyOpenRate"); }
        set { PlayerPrefs.SetInt("keyOpenRate", value); }
    }

    public static int joystick_popup_show_onetime
    {
        get { return PlayerPrefs.GetInt("keyJoystickControlOnetime"); }
        set { PlayerPrefs.SetInt("keyJoystickControlOnetime", value); }
    }

    #region Gift 

    private const string key_gift_current_index = "keyGiftIndex";
    private const string key_gift_part_index = "keyPartIndex";
    private const string key_has_gift = "keyHasGift";

    public static int gift_current_index {

        get { return PlayerPrefs.GetInt(key_gift_current_index); }
        set { PlayerPrefs.SetInt(key_gift_current_index, value); }
    }

    public static int gift_part_index {

        get { return PlayerPrefs.GetInt(key_gift_part_index); }
        set { PlayerPrefs.SetInt(key_gift_part_index, value); }
    }

    public static int gift_has {

        get { return PlayerPrefs.GetInt(key_has_gift); }
        set { PlayerPrefs.SetInt(key_has_gift, value); }
    }

    public static bool hasUseGiftSkin;
    public static bool hasUseGiftWeapon;

    public enum GiftEnum { None, Skin, Weapon };
    public static GiftEnum giftEnum = GiftEnum.None;

    //skin gift index
    public static int gift_current_skin_index
    {
        get { return PlayerPrefs.GetInt("keyGiftCurrentSkinIndex"); }
        set { PlayerPrefs.SetInt("keyGiftCurrentSkinIndex", value); }
    }
    public static List<int> gift_skins_index = new List<int>() { 1, 2, 3, 4, 7, 8, 9, 10 };

    //weapon gift index
    public static int gift_current_weapon_index
    {
        get { return PlayerPrefs.GetInt("keyGiftCurrentWeaponIndex"); }
        set { PlayerPrefs.SetInt("keyGiftCurrentWeaponIndex", value); }
    }

    public static List<int> gift_weapons_index = new List<int>() { 1, 3, 6, 2, 4, 7, 5, 8, 9, 10, 12, 13,
    11, 14, 17, 21, 18, 22, 19, 23, 20, 27, 24, 29, 25, 34, 30, 35, 31, 32};

    public static int GetGiftCount
    {
        get { return gift_skins_index.Count + gift_weapons_index.Count; }
    }

    #endregion // gift endregion


    #region Extra elements

    public static int current_extra_index {

        get { return PlayerPrefs.GetInt("keyExtraCurrentIndex"); }
        set { PlayerPrefs.SetInt("keyExtraCurrentIndex", value); }
    }

    public static int current_extra_map_index {

        get { return PlayerPrefs.GetInt("keyCurrentExtraMapIndex"); }
        set { PlayerPrefs.SetInt("keyCurrentExtraMapIndex", value); }
    }
    public static List<int> extra_maps = new List<int>() { 3, 4, 5, 6 };

    public static int current_extra_skin_index {

        get { return PlayerPrefs.GetInt("keyCurrentExtraSkinIndex"); }
        set { PlayerPrefs.SetInt("keyCurrentExtraSkinIndex", value); }
    }
    public static List<int> exrta_skins = new List<int>() { 12, 13, 14, 15, 16, 17, 18, 19, 20 };

    public static int current_extra_weapon_index
    {
        get { return PlayerPrefs.GetInt("keyCurrentExtraWeaponIndex"); }
        set { PlayerPrefs.SetInt("keyCurrentExtraWeaponIndex", value); }
    }
    public static List<int> extra_weapons = new List<int>() { 37, 38, 40, 41, 42, 43, 44, 45, 47, 49, 50, 52 };
    #endregion  // Extra elements end


    #region Levels region

    private static bool hasAddCoin;
    private static bool hasAddDiamond;

    public static int currentLevel {

        get { return PlayerPrefs.GetInt(key_current_level); }
        set {
            PlayerPrefs.SetInt(key_current_level, value);
            hasAddCoin = false;
            hasAddDiamond = false;
        }

    }

    public static int currentStrongUpgradeLevel {

        get { return PlayerPrefs.GetInt("keyStrongUpgradeLevel", 1); }
        set { PlayerPrefs.SetInt("keyStrongUpgradeLevel", value); }
    }

    public static int currentTargetUpgradeLevel {

        get { return PlayerPrefs.GetInt("keyTargetUpgradeLevel", 1); }
        set { PlayerPrefs.SetInt("keyTargetUpgradeLevel", value); }
    }

    public static int currentMoneyUpgradeLevel {

        get { return PlayerPrefs.GetInt("keyMoneyUpgradeLevel", 1); }
        set { PlayerPrefs.SetInt("keyMoneyUpgradeLevel", value); }
    }

    public static int currentOfflineUpgradeLevel {

        get { return PlayerPrefs.GetInt("keyOfflineUpgradeLevel", 1); }
        set { PlayerPrefs.SetInt("keyOfflineUpgradeLevel", value); }
    }

    #endregion //level upgrade 

    #region    // friend region

    public static int currentFriendCount {

        get { return PlayerPrefs.GetInt("keyFriendCount"); }
        set {

            PlayerPrefs.SetInt("keyFriendCount", value);
            GameManager.Instance.FriendResetTime();
        }
    }


    #endregion // friend end region

    public static int enemyKillCoin = 100;
    public static int enemyKillDiamond = 10;
    public static int enemyKillEnergy = 1;

    public static int enemyCount;
    public static int enemyKillCount;

    public static int enemy_default_current_index
    {
        get { return PlayerPrefs.GetInt("keyEnemyDefaultCurrentIndex"); }
        set { PlayerPrefs.SetInt("keyEnemyDefaultCurrentIndex", value); }
    }

    public static int enemy_boss_current_index
    {
        get { return PlayerPrefs.GetInt("keyEnemyBossCurrentIndex"); }
        set { PlayerPrefs.SetInt("keyEnemyBossCurrentIndex", value); }
    }


    public static float rewardCoin;
    public static float rewardDiamond;
    public static float rewardEnergy;

    public static float bonusCoin {

        get
        {

            if ((currentLevel + 1) % 5 == 0 && !hasAddCoin)
            {
                hasAddCoin = true;
                PlayerPrefs.SetFloat("BonusCoin", PlayerPrefs.GetFloat("BonusCoin", 100) + 100);
            }

            return PlayerPrefs.GetFloat("BonusCoin", 100);
        }
    }

    public static float bonusDiamond {

        get {

            if ((currentLevel + 1) % 5 == 0 && !hasAddDiamond)
            {
                hasAddDiamond = true;
                PlayerPrefs.SetFloat("BonusDiamond", PlayerPrefs.GetFloat("BonusDiamond", 10) + 10);
            }

            return PlayerPrefs.GetFloat("BonusDiamond", 10);
        }
        
    }

    public static int score;
    public static int bestScore {

        get { return PlayerPrefs.GetInt("BestScore"); }
        set { PlayerPrefs.SetInt("BestScore", value); }
    }

    public static float prosentLevel = 0;


    public static int mapItemCurrentIndex {
        get{ return PlayerPrefs.GetInt("mapItemCurrentIndex"); }

        set {
            PlayerPrefs.SetInt("mapItemCurrentIndex",  value);
            GameManager.Instance.GroundChangeStatus();
        }
    }

    public static int skinItemCurrentIndex
    {
        get { return PlayerPrefs.GetInt("skinItemCurrentIndex"); }
        set {
            PlayerPrefs.SetInt("skinItemCurrentIndex", value);
            GameManager.Instance.PlayerChangeStatus();
        }
    }

    public static int weaponItemCurrentIndex
    {
        get { return PlayerPrefs.GetInt("weaponItemCurrentIndex"); }
        set {
            PlayerPrefs.SetInt("weaponItemCurrentIndex", value);
            GameManager.Instance.PlayerChangeStatus();
        }
    }

    private static int _tabPageIndex;
    public static int tabPageIndex
    {
        get {
            return _tabPageIndex;
        }

        set {
            _tabPageIndex = value;

            switch(_tabPageIndex)
            {
                case 0:
                    pagesItemCurrentIndex = mapItemCurrentIndex;
                    break;
                case 1:
                    pagesItemCurrentIndex = skinItemCurrentIndex;
                    break;
                case 2:
                    pagesItemCurrentIndex = weaponItemCurrentIndex;
                    break;
                default:
                    pagesItemCurrentIndex = mapItemCurrentIndex;
                    break;
            }
        }
    }

    private static int _pagesItemCurrentIndex;
    public static int pagesItemCurrentIndex {
        get {
            switch (tabPageIndex)
            {
                case 0: return _pagesItemCurrentIndex = mapItemCurrentIndex;
                case 1: return _pagesItemCurrentIndex = skinItemCurrentIndex;
                case 2: return _pagesItemCurrentIndex = weaponItemCurrentIndex;
                default: return _pagesItemCurrentIndex;
            }
        }
        set { _pagesItemCurrentIndex = value; }
    }

    public static CanvasGroup currentSelectGlowImage;

    public static bool isFinish = false;
    public static bool isPause = false;


    public static void SetPageItemIndex(int index)
    {
        switch(tabPageIndex)
        {
            case 0:
                mapItemCurrentIndex = index;
                break;
            case 1:
                skinItemCurrentIndex = index;
                break;
            case 2:
                weaponItemCurrentIndex = index;
                break;
        }
    }



    public static List<int> RandomNumber(int begin, int end, int count)
    {
        List<int> result = new List<int>();

        int number = UnityEngine.Random.Range(begin, end);
        result.Add(number);

        int sikl = 0;

        while (result.Count == count)
        {
            int n = UnityEngine.Random.Range(begin, end);
            if(!result.Contains(n))
            {
                result.Add(n);
            }
            sikl++;

            if(sikl >= 100)
            {
                break;
            }
        }

        return result;
    }

    public static string GetCoinNumber()
    {
        return ConvertShortNumber(total_coins);
    }

    public static string GetDiamondNumber()
    {
        return ConvertShortNumber(total_diamonds);
    }

    public static string GetEnergyNumber()
    {
        return ConvertShortNumber(total_energies);
    }

    public static void OpenPopup(string popupName)
    {
        currentPopup = UIPopupManager.GetPopup(popupName);
        UIPopupManager.ShowPopup(currentPopup, currentPopup.AddToPopupQueue, false);

    }

    public static void HidePopup(float hideTime = 0)
    {
        currentPopup.Hide(hideTime);
        currentPopup = null;
    }

    public static string title_success {

        get {

            switch (language_current)
            {
                case "English":
                    return "Successfull";
                case "Russian":
                    return "Успешный";
                case "Spanish":
                    return "Exitoso";
                case "Italian":
                    return "Riuscito";
                case "German":
                    return "Erfolgreich";
                case "French":
                    return "Réussi";
                case "Portuguese":
                    return "Bem-sucedido";
                case "Japanese":
                    return "成功";
                case "Chinese":
                    return "成功的";
                case "Korean":
                    return "성공적인";
                default:
                    return "Successfull";

            }
        }
    } 
    public static string title_error
    {
        get
        {
            switch (language_current)
            {
                case "English":
                    return "Error";
                case "Russian":
                    return "Ошибка";
                case "Spanish":
                    return "Error";
                case "Italian":
                    return "Errore";
                case "German":
                    return "Fehler";
                case "French":
                    return "Erreur";
                case "Portuguese":
                    return "Erro";
                case "Japanese":
                    return "エラー";
                case "Chinese":
                    return "錯誤";
                case "Korean":
                    return "오류";
                default:
                    return "Error";

            }

        }
    }
        

    public static string message_success {
        get {

            switch (language_current)
            {
                case "English":
                    return "Successful completed";
                case "Russian":
                    return "Успешно завершено";
                case "Spanish":
                    return "Completado con éxito";
                case "Italian":
                    return "Completato con successo";
                case "German":
                    return "Erfolgreich abgeschlossen";
                case "French":
                    return "Terminé avec succès";
                case "Portuguese":
                    return "Concluído com sucesso";
                case "Japanese":
                    return "正常に完了しました";
                case "Chinese":
                    return "成功完成";
                case "Korean":
                    return "성공적으로 완료";
                default:
                    return "Successful completed";

            }
        }
    }

    public static string message_error {
        get {
            switch (language_current)
            {
                case "English":
                    return "User Canceled";
                case "Russian":
                    return "Пользователь отменен";
                case "Spanish":
                    return "Usuario cancelado";
                case "Italian":
                    return "Utente annullato";
                case "German":
                    return "Benutzer abgebrochen";
                case "French":
                    return "Utilisateur annulé";
                case "Portuguese":
                    return "Usuário cancelado";
                case "Japanese":
                    return "ユーザーがキャンセルされました";
                case "Chinese":
                    return "用戶取消";
                case "Korean":
                    return "사용자가 취소함";
                default:
                    return "User Canceled";

            }
        }
    } 
    public static string message_noeehternet
    {
        get {
            switch (language_current)
            {
                case "English":
                    return "can't connect to the internet";
                case "Russian":
                    return "не могу подключиться к интернету";
                case "Spanish":
                    return "no puedo conectarme a internet";
                case "Italian":
                    return "impossibile connettersi a Internet";
                case "German":
                    return "kann keine Verbindung zum Internet herstellen";
                case "French":
                    return "impossible de se connecter à internet";
                case "Portuguese":
                    return "não consigo conectar a internet";
                case "Japanese":
                    return "インターネットに接続できません";
                case "Chinese":
                    return "無法連接到互聯網";
                case "Korean":
                    return "인터넷에 연결할 수 없습니다";
                default:
                    return "can't connect to the internet";

            }
        }
    }

    public static string subscribe_succssfull
    {
        get
        {
            switch (language_current)
            {
                case "English":
                    return "Subcription successfull purchaed!";
                case "Russian":
                    return "Подписка успешно куплена!";
                case "Spanish":
                    return "¡Suscripción comprada con éxito!";
                case "Italian":
                    return "Abbonamento acquistato con successo!";
                case "German":
                    return "Abonnement erfolgreich gekauft!";
                case "French":
                    return "Abonnement acheté avec succès!";
                case "Portuguese":
                    return "Assinatura comprada com sucesso!";
                case "Japanese":
                    return "サブスクリプションが正常に購入されました！";
                case "Chinese":
                    return "訂閱成功購買！";
                case "Korean":
                    return "구독을 성공적으로 구매했습니다!";
                default:
                    return "Subcription successfull purchaed!";

            }
        }
    }

    public static string gifts_purchased_succssfull
    {
        get
        {
            switch (language_current)
            {
                case "English":
                    return "Gifts successfull purchaed!";
                case "Russian":
                    return "Подарки успешно куплены!";
                case "Spanish":
                    return "¡Regalos comprados con éxito!";
                case "Italian":
                    return "Regali acquistati con successo!";
                case "German":
                    return "Geschenke erfolgreich gekauft!";
                case "French":
                    return "Cadeaux achetés avec succès!";
                case "Portuguese":
                    return "Presentes comprados com sucesso!";
                case "Japanese":
                    return "ギフトの購入に成功しました！";
                case "Chinese":
                    return "禮品購買成功！";
                case "Korean":
                    return "선물을 성공적으로 구매했습니다!";
                default:
                    return "Gifts successfull purchaed!";

            }
        }
    }


    public static string removeads_purchased_succssfull
    {
        get
        {
            switch (language_current)
            {
                case "English":
                    return "Remove force ads successfull purchaed!";
                case "Russian":
                    return "Удалить принудительную рекламу успешно куплено!";
                case "Spanish":
                    return "¡Elimine los anuncios forzados comprados con éxito!";
                case "Italian":
                    return "Rimuovi gli annunci forzati acquistati con successo!";
                case "German":
                    return "Zwangsanzeigen entfernen erfolgreich gekauft!";
                case "French":
                    return "Supprimer les publicités forcées achetées avec succès!";
                case "Portuguese":
                    return "Remover anúncios de força comprados com sucesso!";
                case "Japanese":
                    return "正常に購入した強制広告を削除してください！";
                case "Chinese":
                    return "移除強制廣告成功購買！";
                case "Korean":
                    return "성공적으로 구매한 강제 광고 제거!";
                default:
                    return "Remove force ads successfull purchaed!";

            }
        }
    }


    public static void InfoPopup(string title, string message, Sprite sprite)
    {
        UIPopup popup = UIPopup.GetPopup("InfoPopup");

        popup.GetComponent<MessagePopup>().Set(title, message);
        popup.Data.SetImagesSprites(sprite);

        UIPopupManager.ShowPopup(popup, popup.AddToPopupQueue, false);
    }

    public static string ConvertShortNumber(float value)
    {
        string result;
        string[] valueNames = new string[] { "", "K", "M", "B", "T", "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah", "ai", "aj", "ak", "al", "am", "an", "ao", "ap", "aq", "ar", "as", "at", "au", "av", "aw", "ax", "ay", "az", "ba", "bb", "bc", "bd", "be", "bf", "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp", "bq", "br", "bs", "bt", "bu", "bv", "bw", "bx", "by", "bz", };
        int i;

        for (i = 0; i < valueNames.Length; i++)
            if (value < 900)
                break;
            else value = Mathf.Floor(value / 100f) / 10f;

        if (value == Mathf.Floor(value))
            result = value.ToString() + valueNames[i];
        else result = value.ToString("F1") + valueNames[i];
        return result;
    }


    public static int GetNowTotalSecond()
    {
        return DateTime.Now.Second +
                               DateTime.Now.Minute * 60 +
                               DateTime.Now.Hour * 60 * 60 +
                               DateTime.Now.Day * 24 * 60 * 60;
    }


    #region Purchase and WatchVideo

    //NonConsumable
    public static string product_remove_force_ads = "remove_force_ads";
    public static string product_extra_weapon = "extra_weapon";

    public static string product_key_remove_force_ads_restore = "RemoveForceAdsRestore";
    public static string product_key_extra_weapon_restore = "ExtraWeaponRestore";

    //Subscription
    public static string product_subcripe = "subscribe";

    //Consumable
    public static string product_energy = "energy";
    public static string product_diamond0 = "diamond0";
    public static string product_diamond1 = "diamond1";
    public static string product_diamond2 = "diamond2";
    public static string product_diamond3 = "diamond3";

    public static int product_energy_watch_count = 1;
    public static int product_energy_diamond_count = 1;
    public static int product_energy_diamond_cost = 50;
    public static int product_energy_count = 10;

    public static int product_coin_watch_count = 500;
    public static int product_coin0_count = 3000;
    public static int product_coin1_count = 8000;
    public static int product_coin2_count = 18000;
    public static int product_coin3_count = 40000;

    public static int product_coin0_diamond_cost = 250;
    public static int product_coin1_diamond_cost = 700;
    public static int product_coin2_diamond_cost = 1500;
    public static int product_coin3_diamond_cost = 3000;

    public static int product_diamond_watch_count = 50;
    public static int product_diamond0_count = 500;
    public static int product_diamond1_count = 7000;
    public static int product_diamond2_count = 16000;
    public static int product_diamond3_count = 36000;


    public static string price_remove_force_ads = "$3.99";
    public static string price_extra_weapon = "$4.99";
    public static string price_subcripe = "$9.99";
    public static string price_energy = "$0.99";
    public static string price_diamond0 = "$0.99";
    public static string price_diamond1 = "$9.99";
    public static string price_diamond2 = "$24.99";
    public static string price_diamond3 = "$49.99";

    

    public static int removeForceAdsPurchase {

        get { return PlayerPrefs.GetInt("RemoveForceAdsPurchase", 0); }
        set { PlayerPrefs.SetInt("RemoveForceAdsPurchase", value); }
    }

    public static int removeAdsNoBuy {

        get { return PlayerPrefs.GetInt("keyRemoveAdsNoBuy"); }
        set { PlayerPrefs.SetInt("keyRemoveAdsNoBuy", value); }
    }

    public static bool HasForceADS()
    {
        return removeForceAdsPurchase == 0 ? true : false;
    }

    public static int extraWeaponPurchase {

        get { return PlayerPrefs.GetInt("ExtraWeaponPruchase"); }
        set { PlayerPrefs.SetInt("ExtraWeaponPruchase", value); }
    }

    public static int subcripePurchase {

        get { return PlayerPrefs.GetInt("SubcripePurchase"); }
        set {

            PlayerPrefs.SetInt("SubcripePurchase", value);

            if(value == 0)
            {
                SubcripePurchse(false);
            }
            else
            {
                SubcripePurchse(true);
            }
        }
    }

    public static bool HasRewarded;
    public static bool isPurchaseInstalized;

    private static string key_map_lock = "ItemLockMap";
    private static string key_skin_lock = "ItemLockSkin";
    private static string key_weapon_lock = "ItemLockWeapon";

    // subcripe 20 ta
    private static int subcribeMapIndex = 11;
    private static int[] subcribeSkinIndex = new int[] { 5, 6, 11, 23, 28};
                                                        //wand               
    private static int[] subcribeWeaponIndex = new int[] { 16,
                                                        26, 36, 48, 55, //bow 
                                                        28, 33, 39, 46, 51, 53, 57, 61, 63 }; //axe


    private static void SubcripePurchse(bool value)
    {
        if(value)
        {
            PlayerPrefs.SetInt(key_map_lock + subcribeMapIndex, 1);

            foreach(int i in subcribeSkinIndex)
            {
                PlayerPrefs.SetInt(key_skin_lock + i, 1);
            }

            foreach(int i in subcribeWeaponIndex)
            {
                PlayerPrefs.SetInt(key_weapon_lock + i, 1);
            }
        }
        else
        {
            PlayerPrefs.SetInt(key_map_lock + subcribeMapIndex, 0);

            foreach (int i in subcribeSkinIndex)
            {
                PlayerPrefs.SetInt(key_skin_lock + i, 0);
            }

            foreach (int i in subcribeWeaponIndex)
            {
                PlayerPrefs.SetInt(key_weapon_lock + i, 0);
            }
        }
    }

    

    #endregion // purchase


    public static void ResetAllPlayerPrefs()
    {

        //map
        for(int i = 0; i < 12; i++)
        {
            PlayerPrefs.SetInt(key_item_lock_map + i, 0);
            PlayerPrefs.SetInt(key_item_info_map + i, 0);
        }

        //skin
        for (int i = 0; i < 30; i++)
        {
            PlayerPrefs.SetInt(key_item_lock_skin + i, 0);
            PlayerPrefs.SetInt(key_item_info_skin + i, 0);
        }

        //weapon
        for (int i = 0; i < 66; i++)
        {
            PlayerPrefs.SetInt(key_item_lock_weapon + i, 0);
            PlayerPrefs.SetInt(key_item_info_weapon + i, 0);
        }

        PlayerPrefs.SetFloat(key_total_coins, 0);
        PlayerPrefs.SetFloat(key_total_diamonds, 0);
        PlayerPrefs.SetFloat(key_total_energes, 0);
        PlayerPrefs.SetInt(key_has_gift, 0);
        PlayerPrefs.SetInt(key_gift_current_index, 0);
        PlayerPrefs.SetInt(key_gift_part_index, 0);
        PlayerPrefs.SetInt(key_current_level, 0);

        PlayerPrefs.SetInt("mapItemCurrentIndex", 0);
        PlayerPrefs.SetInt("skinItemCurrentIndex", 0);
        PlayerPrefs.SetInt("weaponItemCurrentIndex", 0);

        PlayerPrefs.SetInt("keyOfflineUpgradeLevel", 1);
        PlayerPrefs.SetInt("keyMoneyUpgradeLevel", 1);
        PlayerPrefs.SetInt("keyTargetUpgradeLevel", 1);
        PlayerPrefs.SetInt("keyStrongUpgradeLevel", 1);

        PlayerPrefs.SetFloat("BonusDiamond", 10);
        PlayerPrefs.SetFloat("BonusCoin", 100);

        PlayerPrefs.SetInt("OnClickMoneyUpgradeBtn", 0);
        PlayerPrefs.SetInt("OnClickTargetUpgradeBtn", 0);
        PlayerPrefs.SetInt("OnClickStrongUpgradeBtn", 0);

        PlayerPrefs.SetInt("dailyTotalSecondChest", 86400);
        PlayerPrefs.SetInt("dailyLeaveTotalSecondChest", 86400);

        PlayerPrefs.SetInt("dailyTotalSecondFriend", 86400);
        PlayerPrefs.SetInt("dailyLeaveTotalSecondFriend", 86400);

        PlayerPrefs.SetInt("keyFriendCount", 0);
        PlayerPrefs.SetInt(key_subcripe_popup, 0);

        PlayerPrefs.SetInt("SubcripePurchase", 0);
        PlayerPrefs.SetInt("ExtraWeaponPruchase", 0);
        PlayerPrefs.SetInt("RemoveForceAdsPurchase", 0);

        PlayerPrefs.SetInt(key_item_info_weapon_has, 0);
        PlayerPrefs.SetInt(key_item_info_skin_has, 0);
        PlayerPrefs.SetInt(key_item_info_map_has, 0);

        PlayerPrefs.SetInt(key_gdpr_popup, 0);

        PlayerPrefs.SetInt("keyExtraCurrentIndex", 0);
        PlayerPrefs.SetInt("keyCurrentExtraMapIndex", 0);
        PlayerPrefs.SetInt("keyCurrentExtraSkinIndex", 0);
        PlayerPrefs.SetInt("keyCurrentExtraWeaponIndex", 0);

        PlayerPrefs.SetInt(key_leave_total_second_offline_coin, 0);
        PlayerPrefs.SetInt(key_rate_leave_total_second, 0);

        PlayerPrefs.SetInt(key_open_offline_popup, 0);
        PlayerPrefs.SetInt(key_show_tutorial_upgrade, 0);

        PlayerPrefs.SetInt("keyGiftCurrentSkin", 0);
        PlayerPrefs.SetInt("keyGiftCurrentWeapon", 0);
        PlayerPrefs.SetInt("keyOpenRate", 0);
        PlayerPrefs.SetInt("keyGiftCurrentSkinIndex", 0);
        PlayerPrefs.SetInt("keyGiftCurrentWeaponIndex", 0);

        PlayerPrefs.SetInt("keySound", 0);
        PlayerPrefs.SetInt("keyMusic", 0);
        PlayerPrefs.SetInt("keyVibration", 0);

        PlayerPrefs.SetInt("keyJoystickControlOnetime", 0);

        //PlayerPrefs.SetInt("keyCurrentExtraMapIndex", 0);
    }

}
