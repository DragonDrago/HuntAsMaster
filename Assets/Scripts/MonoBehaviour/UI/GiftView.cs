using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using UnityEngine;
using DG.Tweening;

public class GiftView : MonoBehaviour, IStatus
{

    [SerializeField]
    private Chest chest;

    [SerializeField]
    private RectTransform subscripeTransform;

    [SerializeField]
    private FriendView friendView;

    private bool isShowSubcripePopup;
    private bool isShowFriendPopup;


    private void Start()
    {
        GameManager.Instance.AddStatus(this);

        
    }

    public void Show()
    {

        chest.Show();

        if(PlayerPrefs.GetInt(Constants.key_subcripe_popup) == 1)
        {
            ShowSubcripe();
        }
        else
        {
            subscripeTransform.DOAnchorPosX(2000f, 0.3f);
        }

        if(Constants.currentLevel > 8)
        {
            friendView.Show();
        }

        
    }

    public void Hide()
    {
        chest.Hide();

        subscripeTransform.DOAnchorPosX(2000f, 0.3f);

        friendView.Hide();
    }


    public void OnClickSubscripe()
    {
        Constants.OpenPopup(Constants.popup_subsripe);
    }

    public void UpdateStatus()
    {
        

    }

    public void SetCoin(float count)
    {
        
    }

    public void SetDiamond(float count)
    {
        
    }

    public void SetEnergy(float count)
    {
        
    }

    public void ShowSubcripe()
    {
        if (Constants.subcripePurchase == 0)
        {
            if (PlayerPrefs.GetInt(Constants.key_subcripe_popup) == 1)
            {
                subscripeTransform.DOAnchorPosX(0f, 0.3f);
            }
            else
            {
                subscripeTransform.DOAnchorPosX(2000f, 0.3f);
            }
        }
        else
        {
            subscripeTransform.DOAnchorPosX(2000f, 0.3f);
        }
    }
}
