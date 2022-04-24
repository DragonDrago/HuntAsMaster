using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendPopup : MonoBehaviour
{


    [SerializeField]
    private CanvasGroup levelGroup0;

    [SerializeField]
    private CanvasGroup levelGroup1;

    [SerializeField]
    private CanvasGroup levelGroup2;




    public void Show()
    {
        switch(Constants.currentFriendCount)
        {
            case 1:

                levelGroup0.alpha = 1;

                levelGroup1.alpha = 0.3f;
                levelGroup2.alpha = 0.3f;

                break;
            case 2:

                levelGroup1.alpha = 1;

                levelGroup0.alpha = 1f;
                levelGroup2.alpha = 0.3f;

                break;
            case 3:

                levelGroup2.alpha = 1;

                levelGroup1.alpha = 1f;
                levelGroup0.alpha = 1f;

                break;
            default:

                levelGroup0.alpha = 0.3f;

                levelGroup1.alpha = 0.3f;
                levelGroup2.alpha = 0.3f;

                break;

        }
    }




}
