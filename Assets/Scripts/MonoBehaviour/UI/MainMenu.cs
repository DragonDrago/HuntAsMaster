using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Doozy.Engine.UI;
using System;
using I2.Loc;

public class MainMenu : MonoBehaviour, IInfoStatus
{
    [SerializeField]
    private StatusView statusView;
    
    [SerializeField]
    private GameObject playButtonObject;
    [SerializeField]
    private RectTransform tabToPlayTransform;

    [SerializeField]
    private GameObject infoGroupObject;

    [SerializeField]
    private GameObject mapButtonObject;

    [SerializeField]
    private GameObject skinButtonObject;

    [SerializeField]
    private GameObject weaponButtonObjeect;

    [SerializeField]
    private GameObject shopButtonObject;

    [SerializeField]
    private UpgradeView upgradeView;

    [SerializeField]
    private GiftView giftView;

    [SerializeField]
    private LocalizationParamsManager levelParamText;

    [SerializeField]
    private LocalizationParamsManager bestScoreParamText;

    [SerializeField]
    private GameObject mapInfoObject;
    [SerializeField]
    private RectTransform mapInfoIconTransform;

    [SerializeField]
    private GameObject skinInfoObject;
    [SerializeField]
    private RectTransform skinInfoIconTransform;

    [SerializeField]
    private GameObject weaponInfoObject;
    [SerializeField]
    private RectTransform weaponInfoIconTransform;
    private UIPopup m_popup;

    [SerializeField]
    private TabPages mapTabPages;
    [SerializeField]
    private TabPages skinTabPages;
    [SerializeField]
    private TabPages weaponTabPages;


    private Action<int> useAction;
    private Action okAction;
    private PagesItemBase pagesItemBase = null;

    private int currentElement = 3;

    private bool isStatusShow;


    private void Start()
    {
        GameManager.Instance.AddInfoStatus(this);

        useAction += OnClickUseButton;
        okAction += OnClickOkButton;
    }

    public void Show()
    {
        if (Constants.currentLevel == 0 && !isStatusShow)
        {
            infoGroupObject.SetActive(false);
            isStatusShow = true;
            statusView.Hide();
        }
        else
        {
            levelParamText.SetParameterValue("LEVEL", (Constants.currentLevel + 1).ToString());
            bestScoreParamText.SetParameterValue("SCORE", Constants.bestScore.ToString());
            
            infoGroupObject.SetActive(true);
            statusView.ShowAll();
        }

        
        playButtonObject.SetActive(true);
        tabToPlayTransform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f).From(Vector3.one).SetLoops(-1, LoopType.Yoyo);

        if(Constants.currentLevel < 3)
        {
            mapButtonObject.SetActive(false);
            skinButtonObject.SetActive(false);
            weaponButtonObjeect.SetActive(false);
            shopButtonObject.SetActive(false);
        }
        else
        {
            ShowMapInfo();
            ShowSkinInfo();
            ShowWeaponInfo();

            mapButtonObject.SetActive(true);
            skinButtonObject.SetActive(true);
            weaponButtonObjeect.SetActive(true);
            shopButtonObject.SetActive(true);
        }

        if(Constants.currentLevel < 1)
        {
            upgradeView.Hide();
        }
        else
        {
            upgradeView.Show();
        }

        if (Constants.currentLevel < 4)
        {
            giftView.Hide();
        }
        else
        {
            giftView.Show();
        }

        if(Constants.currentLevel == 7 && PlayerPrefs.GetInt(Constants.key_open_offline_popup) == 0)
        {
            Constants.OpenPopup(Constants.popup_offline_coin);
        }

        if(Constants.currentLevel > 12 && Constants.open_rate == 0)
        {
            if(CheckRateOpen() && Constants.currentPopup == null)
                Constants.OpenPopup(Constants.popup_rate);
        }

    }

    public void Hide()
    {
        //infoGroupObject.SetActive(false);
        tabToPlayTransform.DOKill();
        playButtonObject.SetActive(false);

        statusView.Hide();

        upgradeView.Hide();

        giftView.Hide();
    }

    private bool CheckRateOpen()
    {

        int enter_total_second = Constants.GetNowTotalSecond();

        int delta_second = enter_total_second - PlayerPrefs.GetInt(Constants.key_rate_leave_total_second);

        if ( 0 >= 86400 - delta_second)
        {
            return true;
        }

        return false;
    }


    public void OnClickPlay()
    {

        if(Constants.currentLevel == 0 && PlayerPrefs.GetInt("keyJoystickControlOnetime") == 0)
        {
            PlayerPrefs.SetInt("keyJoystickControlOnetime", 1);
            Constants.OpenPopup(Constants.popup_joystick_control);
        }

        if (Constants.extraWeaponPurchase == 1)
        {
            if(Constants.current_extra_index < 25)
            {
                ShowExtraElements();
            }
            else
            {
                PlayGame();
            }
        }
        else
        {
            PlayGame();
        }

    }

    private void PlayGame()
    {
        Hide();

        GameManager.Instance.SaveTime();

        GameManager.Instance.ControllerPlay();

        AppMetricaSendEventContrrol.StartLevel();
    }


    private void ShowExtraElements()
    {
        m_popup = UIPopupManager.GetPopup(Constants.name_items_popup);

        int index = Constants.current_extra_index;

        if (Constants.current_extra_map_index < Constants.extra_maps.Count
           && index % 3 == 0)
        {
            ExtraOpenMap();
        }
        else if(Constants.current_extra_skin_index < Constants.exrta_skins.Count
                && index % 2 == 0)
        {
            ExtraOpenSkin();
        }
        else if(Constants.current_extra_weapon_index < Constants.extra_weapons.Count)
        {
            ExtraOpenWeapon();
        }
        else
        {
            currentElement = 3;

            PlayGame();
        }

    }

    private void ExtraOpenMap()
    {
        if(HasExtraMap())
        {
            currentElement = 0;

           
            pagesItemBase = mapTabPages.GetItem(Constants.extra_maps[Constants.current_extra_map_index]);

            ShowExtraPopup();
        }
        else
        {
            if (HasExtraSkin())
            {
                ExtraOpenSkin();
            }
            else if (HasExtraWeapon())
            {
                ExtraOpenWeapon();
            }
            else
            {
                Constants.current_extra_index = 26;
                currentElement = 3;
                PlayGame();
            }
        }
    }

    private bool HasExtraMap()
    {
        for(int i = Constants.current_extra_map_index; i < Constants.extra_maps.Count; i++)
        {
            if(PlayerPrefs.GetInt(Constants.key_item_lock_map + Constants.extra_maps[i]) == 0)
            {
                Constants.current_extra_map_index = i;

                PlayerPrefs.SetInt(Constants.key_item_lock_map + Constants.extra_maps[i], 1);

                return true;
            }
        }

        Constants.current_extra_map_index = Constants.extra_maps.Count;

        return false;
    }

    private void ExtraOpenSkin()
    {
        if(HasExtraSkin())
        {
            currentElement = 1;

            
            pagesItemBase = skinTabPages.GetItem(Constants.exrta_skins[Constants.current_extra_skin_index]);

            ShowExtraPopup();

            
        }
        else
        {
            if (HasExtraMap())
            {
                ExtraOpenMap();
            }
            else if (HasExtraWeapon())
            {
                ExtraOpenWeapon();
            }
            else
            {
                Constants.current_extra_index = 26;
                currentElement = 3;
                PlayGame();
            }
        }
    }

    private bool HasExtraSkin()
    {
        for(int i = Constants.current_extra_skin_index; i < Constants.exrta_skins.Count; i++)
        {
            if (PlayerPrefs.GetInt(Constants.key_item_lock_skin + Constants.exrta_skins[i]) == 0)
            {
                Constants.current_extra_skin_index = i;

                PlayerPrefs.SetInt(Constants.key_item_lock_skin + Constants.exrta_skins[i], 1);

                return true;
            }
        }

        Constants.current_extra_skin_index = Constants.exrta_skins.Count;

        return false;
    }

    private void ExtraOpenWeapon()
    {
        if(HasExtraWeapon())
        {
            currentElement = 2;

            pagesItemBase = weaponTabPages.GetItem(Constants.extra_weapons[Constants.current_extra_weapon_index]);

            ShowExtraPopup();
        }
        else
        {
            if(HasExtraMap())
            {
                ExtraOpenMap();
            }
            else if(HasExtraSkin())
            {
                ExtraOpenSkin();
            }
            else
            {
                Constants.current_extra_index = 26;
                currentElement = 3;
                PlayGame();
            }
        }
    }

    private bool HasExtraWeapon()
    {
        for(int i = Constants.current_extra_weapon_index; i < Constants.extra_weapons.Count; i++)
        {
            if (PlayerPrefs.GetInt(Constants.key_item_lock_weapon + Constants.extra_weapons[i]) == 0)
            {
                Constants.current_extra_weapon_index = i;

                PlayerPrefs.SetInt(Constants.key_item_lock_weapon + Constants.extra_weapons[i], 1);

                return true;
            }
        }

        Constants.current_extra_weapon_index = Constants.extra_weapons.Count;

        return false;
    }

    private void ShowExtraPopup()
    {
        PlayerPrefs.Save();

        Constants.current_extra_index++;
        int index = 0;

        switch (currentElement)
        {
            case 0:
                index = Constants.extra_maps[Constants.current_extra_map_index];
                break;

            case 1:
                index = Constants.exrta_skins[Constants.current_extra_skin_index];
                break;

            case 2:
                index = Constants.extra_weapons[Constants.current_extra_weapon_index];
                break;
        }

        m_popup.GetComponent<ItemsPopup>().Set(index, pagesItemBase.GetIconImage.sprite,
                                               pagesItemBase.GetTile, pagesItemBase.GetMessage,
                                               GetUseText(), true, useAction, true, false, true, okAction);

        UIPopupManager.ShowPopup(m_popup, m_popup.AddToPopupQueue, false);
    }

    private string GetUseText()
    {

        switch (LocalizationManager.CurrentLanguage)
        {
            case "English":
                return "Use";
            case "Russian":
                return "Использовать";
            case "Spanish":
                return "Utilizar";
            case "Italian":
                return "Utilizzo";
            case "German":
                return "Verwenden";
            case "French":
                return "Utiliser";
            case "Portuguese":
                return "Usar";
            case "Japanese":
                return "使用する";
            case "Chinese":
                return "採用";
            case "Korean":
                return "사용";
            default:
                return "Use";
        }
    }

    private void OnClickUseButton(int index)
    {

        switch(currentElement)
        {
            case 0:
                Constants.mapItemCurrentIndex = Constants.extra_maps[Constants.current_extra_map_index];
                PlayerPrefs.SetInt(Constants.key_item_info_map + Constants.mapItemCurrentIndex, 1);

                break;

            case 1:
                Constants.skinItemCurrentIndex = Constants.exrta_skins[Constants.current_extra_skin_index];
                PlayerPrefs.SetInt(Constants.key_item_info_skin + Constants.skinItemCurrentIndex, 1);

                break;

            case 2:
                
                Constants.weaponItemCurrentIndex = Constants.extra_weapons[Constants.current_extra_weapon_index];
                PlayerPrefs.SetInt(Constants.key_item_info_weapon + Constants.weaponItemCurrentIndex, 1);
                break;

            default:

                break;
        }

        PlayGame();
    }

    private void OnClickOkButton()
    {
        switch (currentElement)
        {
            case 0:
                GameManager.Instance.InfoStatusMapUpdate();
                break;

            case 1:
                GameManager.Instance.InfoStatusSkinUpdate();
                break;

            case 2:
                GameManager.Instance.InfoStatusWeaponUpdate();
                break;

            default:

                break;
        }

        PlayGame();
    }

    private void ShowMapInfo()
    {
        bool show = HasMapInfo();
        mapInfoObject.SetActive(show);
    }

    private bool HasMapInfo()
    {
        if (PlayerPrefs.GetInt(Constants.key_item_info_map_has) == 1)
        {
            return false;
        }

        int mapCount = 0;
        for (int i = 1; i < Constants.itemMapCount; i++)
        {
            if (PlayerPrefs.GetInt(Constants.key_item_lock_map + i) == 1 &&
               PlayerPrefs.GetInt(Constants.key_item_info_map + i) == 0)
            {
                return true;
            }
            else
            {
                mapCount++;
            }
        }

        if(mapCount == Constants.itemMapCount)
        {
            PlayerPrefs.SetInt(Constants.key_item_info_map_has, 1);
        }


        return false;

    }

    private void ShowSkinInfo()
    {
        bool show = HasSkinInfo();

        skinInfoObject.SetActive(show);
    }

    private bool HasSkinInfo()
    {
        if (PlayerPrefs.GetInt(Constants.key_item_info_skin_has) == 1)
        {
            return false;
        }

        int skinCount = 0;
        for (int i = 1; i < Constants.itemSkinCount; i++)
        {
            if (PlayerPrefs.GetInt(Constants.key_item_lock_skin + i) == 1 &&
               PlayerPrefs.GetInt(Constants.key_item_info_skin + i) == 0)
            {
                return true;
            }
            else
            {
                skinCount++;
            }
        }

        if (skinCount == Constants.itemSkinCount)
        {
            PlayerPrefs.SetInt(Constants.key_item_info_skin_has, 1);
        }


        return false;

    }

    private void ShowWeaponInfo()
    {
        bool show = HasWeaponInfo();

        weaponInfoObject.SetActive(show);
    }

    private bool HasWeaponInfo()
    {
        if (PlayerPrefs.GetInt(Constants.key_item_info_weapon_has) == 1)
        {
            return false;
        }

        int weaponCount = 0;
        for (int i = 1; i < Constants.itemWeaponCount; i++)
        {
            if (PlayerPrefs.GetInt(Constants.key_item_lock_weapon + i) == 1 &&
               PlayerPrefs.GetInt(Constants.key_item_info_weapon + i) == 0)
            {
                return true;
            }
            else
            {
                weaponCount++;
            }
        }
    
        if (weaponCount == Constants.itemWeaponCount)
        {
            PlayerPrefs.SetInt(Constants.key_item_info_weapon_has, 1);
        }


        return false;

    }


    public void UpdateMapInfo()
    {
        ShowMapInfo();
    }

    public void UpdateSkinInfo()
    {
        ShowSkinInfo();
    }

    public void UpdateWaponInfo()
    {
        ShowWeaponInfo();
    }

}
