using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Doozy.Engine.UI;
using UnityEngine;
using UnityEngine.UI;

public class PagesItemBase : MonoBehaviour
{
    protected string key_lock = "";
    protected string key_info = "";

    [SerializeField]
    protected GameObject priceObject;

    [SerializeField]
    protected GameObject infoObject;
    [SerializeField]
    protected RectTransform infoTransform;

    [SerializeField]
    protected GameObject lockObject;

    [SerializeField]
    protected Image iconImage;
    public Image GetIconImage { get { return iconImage; } }

    [SerializeField]
    protected TMPro.TextMeshProUGUI priceText;

    [SerializeField]
    protected string title = "title";
    public string GetTile { get { return title; } }

    [SerializeField]
    protected string message = "meessage";
    public string GetMessage { get { return message; } }

    [SerializeField]
    protected float price = 200f;

    [SerializeField]
    private CanvasGroup selectGlowImage;

    [SerializeField]
    private GameObject subcripeImage;

    [SerializeField]
    private bool isSubcripeItem;

    protected UIPopup m_popup;

    protected Action<int> buttonAction;

    protected int _lock, _info;
    protected bool isShowPriceButton;

    protected bool isShowTab;

    protected virtual void Start()
    {

    }

    public void Set(int index)
    {
        SetKeys();

        //ResetPlayerPrefs(index);

        priceText.text = price.ToString();

        if (index == 0)
        {
            if (PlayerPrefs.GetInt(key_lock + index) == 0)
            {
                PlayerPrefs.SetInt(key_lock + index, 1);
                PlayerPrefs.SetInt(key_info + index, 1);
                PlayerPrefs.Save();
            }
        }

        SetValue(index);

        buttonAction += UpdateStatus;
    }

    protected virtual void SetKeys()
    {

    }

    public void SetValue(int index)
    {
        isShowTab = true;

        _lock = PlayerPrefs.GetInt(key_lock + index, 0);
        _info = PlayerPrefs.GetInt(key_info + index, 0);

        priceObject.SetActive(_lock == 0 && !isSubcripeItem ? true : false);

        if(_info == 0)
        {
            infoObject.SetActive(_lock == 0 ? false : true);
        }
        
        lockObject.SetActive(_lock == 0 && !isSubcripeItem ? true : false);

        subcripeImage.SetActive(_lock == 0 && isSubcripeItem);


        UpdateSelectItem(index);

    }

    private void UpdateSelectItem(int index)
    {
        if (Constants.pagesItemCurrentIndex == index)
        {
            if (Constants.currentSelectGlowImage != null)
            {
                Constants.currentSelectGlowImage.DOKill();
                Constants.currentSelectGlowImage.alpha = 0;
            }

            Constants.currentSelectGlowImage = selectGlowImage;

            selectGlowImage.DOFade(1f, 1f).From(0f).SetLoops(-1, LoopType.Yoyo);

            SelectedItem();
        }
    }

    protected virtual void SelectedItem()
    {

    }


    public void OnClickItem(int index)
    {
        SetPopup(index);
    }

    protected virtual void SetPopup(int index)
    {
        isShowPriceButton = _lock == 0 ? true : false;

        if (Constants.pagesItemCurrentIndex == index || isShowPriceButton)
        {
            //show popup

            if(isSubcripeItem)
            {
                Constants.OpenPopup(Constants.popup_subsripe);
            }
            else
            {
                m_popup = UIPopupManager.GetPopup(Constants.name_items_popup);

                if (m_popup == null)
                    return;

                ShowPoup(index);
            }
        }
        else
        {
            // select item
            if(PlayerPrefs.GetInt(key_info + index) == 0)
            {
                PlayerPrefs.SetInt(key_info + index, 1);

                infoObject.SetActive(false);

            }
            
            Constants.SetPageItemIndex(index);

            UpdateSelectItem(index);
        }
    }

    protected virtual void ShowPoup(int index)
    {

    }

    private void UpdateStatus(int index)
    {

        Constants.SetPageItemIndex(index);

        UpdateValues(index);

        GameManager.Instance.UpdateStatus();

        SetValue(index);
    }

    protected virtual void UpdateValues(int index)
    {

    }



}
