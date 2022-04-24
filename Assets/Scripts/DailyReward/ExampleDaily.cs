using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleDaily : MonoBehaviour
{

    private DailyReward dailyReward;

    private void Start()
    {
        dailyReward = FindObjectOfType<DailyReward>();
    }


    public void OpenDaily()
    {

        dailyReward.Open();

    }


}
