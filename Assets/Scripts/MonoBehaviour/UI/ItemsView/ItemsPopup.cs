using System;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class ItemsPopup : MonoBehaviour
{
    protected const string param_english = "ENGLISH";
    protected const string param_russian = "RUSSIAN";
    protected const string param_spanish = "SPANISH";
    protected const string param_italian = "ITALIAN";
    protected const string param_german = "GERMAN";
    protected const string param_french = "FRENCH";
    protected const string param_japanese = "JAPANESE";
    protected const string param_chinese = "CHINESE";
    protected const string param_korean = "KOREAN";
    protected const string param_portuguese = "PORTUGUESE";

    [SerializeField]
    private Image iconImage;

    [SerializeField]
    private LocalizationParamsManager titleParamManager;

    [SerializeField]
    private LocalizationParamsManager messageParamManager;

    [SerializeField]
    private GameObject unlockTextObject;

    [SerializeField]
    private Text priceText;

    [SerializeField]
    private Button priceButton;

    [SerializeField]
    private Image coinImage;

    [SerializeField]
    private Image diamondImage;

    private Action<int> buttonAction;
    private Action okAction;

    private int index;


    public void Set(int index, Sprite icon, string title, string message,
                    string price, bool isButton, Action<int> action,
                    bool isShowPriceButton, bool isCoin, bool isUse, Action okAction = null)
    {
        this.index = index;
        iconImage.sprite = icon;

        titleParamManager.SetParameterValue(param_english, title);
        titleParamManager.SetParameterValue(param_russian, title);
        titleParamManager.SetParameterValue(param_spanish, title);
        titleParamManager.SetParameterValue(param_italian, title);
        titleParamManager.SetParameterValue(param_german, title);
        titleParamManager.SetParameterValue(param_french, title);
        titleParamManager.SetParameterValue(param_japanese, title);
        titleParamManager.SetParameterValue(param_chinese, title);
        titleParamManager.SetParameterValue(param_korean, title);
        titleParamManager.SetParameterValue(param_portuguese, title);

        messageParamManager.SetParameterValue(param_english, message);
        messageParamManager.SetParameterValue(param_russian, message);
        messageParamManager.SetParameterValue(param_spanish, message);
        messageParamManager.SetParameterValue(param_italian, message);
        messageParamManager.SetParameterValue(param_german, message);
        messageParamManager.SetParameterValue(param_french, message);
        messageParamManager.SetParameterValue(param_japanese, message);
        messageParamManager.SetParameterValue(param_chinese, message);
        messageParamManager.SetParameterValue(param_korean, message);
        messageParamManager.SetParameterValue(param_portuguese, message);

        priceText.text = price;

        buttonAction = action;
        this.okAction = okAction;

        priceButton.gameObject.SetActive(isShowPriceButton);

        priceButton.interactable = isButton;

        if (isCoin)
            coinImage.enabled = true;
        else if(!isUse)
            diamondImage.enabled = true;

        unlockTextObject.SetActive(!isUse);
    }


    public void OnClickPriceButton()
    {
        buttonAction?.Invoke(index);
    }

    public void OnClickOkButton()
    {
        okAction?.Invoke();
    }

}
