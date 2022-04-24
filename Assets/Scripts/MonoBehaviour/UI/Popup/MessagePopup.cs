using System.Collections;
using System.Collections.Generic;
using I2.Loc;
using UnityEngine;

public class MessagePopup : MonoBehaviour
{

    [SerializeField]
    private LocalizationParamsManager titleParamText;

    [SerializeField]
    private LocalizationParamsManager messageParamText;


    public void Set(string title, string message)
    {
        titleParamText.SetParameterValue("VALUE", title);
        messageParamText.SetParameterValue("VALUE", message);
    }



}
