using System.Collections;
using System.Collections.Generic;
using I2.Loc;
using UnityEngine;

public class LanguagePopup : MonoBehaviour
{

    [SerializeField]
    private GameObject[] checkObjects = new GameObject[6];


    private void Start()
    {
        SelectLanguage();
    }

    public void OnClickLanguage(string lang)
    {
        Constants.language_current = lang;
        LocalizationManager.CurrentLanguage = lang;

        SelectLanguage();

        AppMetricaSendEventContrrol.Settings(Constants.isMusic ? 1 : 0,
                                             Constants.isSound ? 1 : 0,
                                         Constants.isVibration ? 1 : 0,
       Constants.joystickControl == 0 ? "right" : "left", lang);
    }


    public void OnClickOk()
    {
        Constants.currentPopup.Hide();
    }


    private void SelectLanguage()
    {
        foreach (GameObject obj in checkObjects)
            obj.SetActive(false);

        int index = 0;

        switch(Constants.language_current)
        {
            case "English":
                index = 0;
                break;
            case "Russian":
                index = 5;
                break;
            case "Spanish":
                index = 2;
                break;
            case "Italian":
                index = 3;
                break;
            case "German":
                index = 1;
                break;
            case "French":
                index = 4;
                break;
            case "Portuguese":
                index = 6;
                break;
            case "Japanese":
                index = 7;
                break;
            case "Chinese":
                index = 8;
                break;
            case "Korean":
                index = 9;
                break;
            default:
                break;
        }

        checkObjects[index].SetActive(true);
    }

}
