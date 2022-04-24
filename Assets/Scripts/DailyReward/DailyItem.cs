using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyItem : MonoBehaviour
{

    [SerializeField]
    private Text messageText;

    [SerializeField]
    private Image lockImage;


    private event Action OnActionEvent;

    internal void Set(string message, bool imageLock, Action action )
    {
        messageText.text = message;
        lockImage.enabled = imageLock;

        OnActionEvent = action;
    }


    public void SetTime(string message)
    {
        messageText.text = message;
    }

    public void OnClickButton()
    {
        OnActionEvent?.Invoke();
    }
    
}
