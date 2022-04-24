using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RatePopup : MonoBehaviour
{

    [SerializeField]
    private CanvasGroup[] onStars = new CanvasGroup[5];

    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Button submitButton;


    public void ChangeSlider()
    {
        for(int i = 0; i < (int)slider.value - 1; i++)
        {
            onStars[i].DOFade(1f, 0.2f);
        }

        for (int i = (int)slider.value - 1; i < 5; i++)
        {
            onStars[i].DOFade(0f, 0.1f);
        }

        if(slider.value > 1)
        {
            submitButton.interactable = true;
        }
        else
        {
            submitButton.interactable = false;
        }
    }

    public void OnClickNotNow()
    {
        int leaveTotalSecond = Constants.GetNowTotalSecond();

        PlayerPrefs.SetInt(Constants.key_rate_leave_total_second, leaveTotalSecond);
        PlayerPrefs.Save();

        Constants.HidePopup();
    }


    public void OnClickSubmit()
    {

        if(slider.value > 5)
        {
            SendStore();
        }
        else
        {
            SendEmail();
        }

        AppMetricaSendEventContrrol.RateUs("auto_show", (int)slider.value - 1);

        Constants.HidePopup();
    }



    private void SendStore()
    {
        Constants.open_rate = 1;
        Application.OpenURL("http://play.google.com/store/apps/details?id=com.biroroapp.huntasmaster");
    }

    private void SendEmail()
    {
        Constants.open_rate = 1;
        string email = "biroroapp@gmail.com";
        string subject = MyEscapeURL("Hunt as Master Issue");
        string body = MyEscapeURL("Tell us what you didn't like : ");
        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
    }

    private string MyEscapeURL(string url)
    {
        return WWW.EscapeURL(url).Replace("+", "%20");
    }

    

}
