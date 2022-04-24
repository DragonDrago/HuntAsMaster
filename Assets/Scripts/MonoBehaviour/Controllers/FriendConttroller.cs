using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendConttroller : MonoBehaviour, IController, IFriendControl
{

    [SerializeField]
    private Friend[] friends = new Friend[3];

    [SerializeField]
    private EnemyEvent[] enemyEvents = new EnemyEvent[6];


    private int index;

    private bool isReset;

    private void Start()
    {
        GameManager.Instance.AddFriendControl(this);
        GameManager.Instance.AddController(this);
    }

    private void Show(int index)
    {
        this.index = index;

        for(int i = 0; i < index + 1; i++)
        {
            friends[i].Show();
        }
        
        ShowOrHide(index, true);
    }

    private void ShowOrHide(int index, bool show)
    {
        

        switch (index)
        {
            case 0:

                friends[0].transform.localPosition = Vector3.zero;
                if (show)
                {
                    enemyEvents[0].Show();
                }
                else
                {
                    enemyEvents[0].Hide();
                }

                break;
            case 1:

                friends[0].transform.localPosition = new Vector3(-8f, 0f, 0f);
                friends[1].transform.localPosition = new Vector3(8f, 0f, 0f);

                if (show)
                {
                    enemyEvents[1].Show();
                    enemyEvents[2].Show();
                }
                else
                {
                    enemyEvents[1].Hide();
                    enemyEvents[2].Hide();
                }
                

                break;
            case 2:

                friends[0].transform.localPosition = new Vector3(-10f, 0f, 0f);
                friends[1].transform.localPosition = new Vector3(0f, 0f, 0f);
                friends[2].transform.localPosition = new Vector3(10f, 0f, 0f);

                if (show)
                {
                    enemyEvents[3].Show();
                    enemyEvents[4].Show();
                    enemyEvents[5].Show();
                }
                else
                {
                    enemyEvents[3].Hide();
                    enemyEvents[4].Hide();
                    enemyEvents[5].Hide();
                }
                
                break;
        }
    }

    private void Hide()
    {
        friends[index].Hide();
        ShowOrHide(index, false);
    }

    public void OnLoaded()
    {

    }

    public void OnPlay()
    {
        isReset = false;
        if(Constants.currentFriendCount > 0)
        {
            Show(Constants.currentFriendCount - 1);
        }
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

    public void OnBackward()
    {

    }

    public void OnFaild()
    {
        if(Constants.currentFriendCount > 0)
        {
            for (int i = 0; i <= index; i++)
            {
                friends[i].Stop(true);
            }
        }
        
    }

    public void OnVectory()
    {
        if (Constants.currentLevel > 7)
        {
            if (Constants.currentFriendCount < 3)
            {
                
                Constants.currentFriendCount++;
            }
        }

        
    }

    public void OnFinish()
    {
        if(Constants.currentFriendCount > 0)
        {
            Hide();
            Constants.currentFriendCount = 0;
        }
            
    }

    public void OnRestart()
    {
        isReset = false;
        Hide();
    }

    public void ResetFriend()
    {
        if (isReset)
            return;

        isReset = true;

        if (Constants.currentFriendCount > 0)
        {
            for (int i = 0; i <= index; i++)
            {
                friends[i].Stop(false);
            }
        }

    }
}
