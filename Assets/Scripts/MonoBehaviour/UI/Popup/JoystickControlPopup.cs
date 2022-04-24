using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickControlPopup : MonoBehaviour
{


    public void OnClickRight()
    {
        AppMetricaSendEventContrrol.Settings(Constants.isMusic ? 1 : 0,
                                             Constants.isSound ? 1 : 0,
                                         Constants.isVibration ? 1 : 0,
       "right",
       Constants.language_current);

        Constants.joystickControl = 0;
        Constants.HidePopup();

    }

    public void OnClickLeft()
    {
        AppMetricaSendEventContrrol.Settings(Constants.isMusic ? 1 : 0,
                                             Constants.isSound ? 1 : 0,
                                         Constants.isVibration ? 1 : 0,
       "left",
       Constants.language_current);

        Constants.joystickControl = 1;
        Constants.HidePopup();
    }
         

}
