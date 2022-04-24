using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IController
{
    [SerializeField]
    private MainMenu mainMenu;



    private void Start()
    {
        

        GameManager.Instance.AddController(this);

        StartCoroutine(WaitForShow());
        //loading.Show();

        if(HasOfflineCoin())
        {
            if (Constants.currentLevel > 0)
            {
                Constants.OpenPopup(Constants.popup_offline_coin);

                if (PlayerPrefs.GetInt(Constants.key_open_offline_popup) == 0)
                    PlayerPrefs.SetInt(Constants.key_open_offline_popup, 1);
            }
        }

        if (PlayerPrefs.GetInt(Constants.key_show_tutorial_upgrade) == 0)
        {
            if (Constants.currentLevel > 0)
            {
                PlayerPrefs.SetInt(Constants.key_show_tutorial_upgrade, 1);
            }
        }

    }


    public void OnLoaded()
    {
        mainMenu.Show();

        if (Constants.currentLevel > 7)
            Constants.OpenPopup(Constants.popup_offline_coin);

        if (PlayerPrefs.GetInt(Constants.key_subcripe_popup) == 0 && Constants.subcripePurchase == 0)
        {
            if (Constants.currentLevel > 11)
            {
                PlayerPrefs.SetInt(Constants.key_subcripe_popup, 1);
                Constants.OpenPopup(Constants.popup_subsripe);
            }
        }
    }


    public void OnPlay()
    {
        mainMenu.Hide();
    }

    public void OnPause()
    {
        
    }

    public void OnTargetBegin()
    {
        
    }

    public void OnTargeting()
    {
        
    }

    public void OnTargetEnd()
    {
        
    }

    public void OnFinish()
    {
        
    }

    public void OnFaild()
    {
        
    }

    public void OnVectory()
    {
        
    }

    public void OnBackward()
    {
        
    }

    public void OnRestart()
    {
        StartCoroutine(WaitForShow());
    }

    private IEnumerator WaitForShow()
    {
        yield return new WaitForSeconds(1f);

        mainMenu.Show();

        if (PlayerPrefs.GetInt(Constants.key_subcripe_popup) == 0 && Constants.subcripePurchase == 0)
        {
            if (Constants.currentLevel > 11)
            {
                PlayerPrefs.SetInt(Constants.key_subcripe_popup, 1);
                Constants.OpenPopup(Constants.popup_subsripe);

                GameManager.Instance.ShowSubcripeStatus();
            }
        }
    }

    private bool HasOfflineCoin()
    {
        int totalSecond = 86400;

        int enter_total_second = Constants.GetNowTotalSecond();
        int delta_second = enter_total_second - PlayerPrefs.GetInt(Constants.key_leave_total_second_offline_coin, 86400);

        totalSecond -= delta_second;

        if (0 >= totalSecond)
        {
            return true;
        }
        else
        {
            if(delta_second >= 3600)
            {
                return true;
            }
        }

        return false;
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Save();
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void Save()
    {

        int leaveTotalSecond = Constants.GetNowTotalSecond();

        PlayerPrefs.SetInt(Constants.key_leave_total_second_offline_coin, leaveTotalSecond);

        PlayerPrefs.Save();
    }

}
