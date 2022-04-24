using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using UnityEngine;

public class PagesItemSkin : PagesItemBase
{

    protected override void SetKeys()
    {
        key_info = Constants.key_item_info_skin;
        key_lock = Constants.key_item_lock_skin;
    }

    protected override void ShowPoup(int index)
    {
        bool isButton = Constants.total_diamonds >= price ? true : false;

        m_popup.GetComponent<ItemsPopup>().Set(index, iconImage.sprite, "", "", price.ToString(), isButton, buttonAction, isShowPriceButton, false, false);
        UIPopupManager.ShowPopup(m_popup, m_popup.AddToPopupQueue, false);
    }


    protected override void UpdateValues(int index)
    {
        Constants.total_diamonds -= price;
        GetSkin(index);
    }

    private void GetSkin(int index)
    {
        PlayerPrefs.SetInt(key_lock + index, 1);
        PlayerPrefs.SetInt(key_info + index, 1);
        PlayerPrefs.Save();
    }

    protected override void SelectedItem()
    {
        GameManager.Instance.InfoStatusSkinUpdate();
    }

}
