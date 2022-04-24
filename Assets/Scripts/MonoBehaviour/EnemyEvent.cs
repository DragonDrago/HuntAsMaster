using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEvent : MonoBehaviour
{

    [SerializeField]
    private Friend friend;

    private bool isSent;
    public bool SetIsSent { set { isSent = value; } }

    public void Show()
    {
        gameObject.SetActive(true);
    }


    private void OnTriggerStay(Collider other)
    {
        if (friend.GetStop)
            return;

        if(other.CompareTag("Enemy"))
        {
            if (isSent)
                return;

            isSent = true;

            friend.SetEnemyTransform(other.transform, this);
        }
    }


    public void Hide()
    {
        isSent = false;
        gameObject.SetActive(false);
    }

}
