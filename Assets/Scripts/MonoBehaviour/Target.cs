using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, INeed
{
    
    private bool isNeed;

    private void Start()
    {
        GameManager.Instance.AddNeed(this);
        gameObject.SetActive(false);
    }

    public void OnDisabled()
    {
        // isNeed = false;
        gameObject.SetActive(false);
    }

    public void OnEnabled()
    {
        gameObject.SetActive(true);
        transform.localPosition = new Vector3(0f, 0f, -35f);
       // isNeed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (isNeed)
        //    return;

        //if(other.CompareTag(Constants.tag_Targeting))
        //{
        //    isNeed = true;

        //    GameManager.Instance.ControllerTargeting();
        //}
    }


}
