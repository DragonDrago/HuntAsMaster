using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyReward : MonoBehaviour
{
    private const string key_day = "dailyDay";
    private const string key_reward_index = "dailyRewardIndex";
    private const string key_total_second = "dailyTotalSecond";
    private const string key_leave_total_second = "dailyLeaveTotalSecond";
    private const string key_reward = "dailyReward";


    private string[] rewards = new string[30]
                    {
                      "25 Diamond",    // 1
                      "25 Emerald",    // 2
                      "1 Thunder",     // 3
                      "30 Diamond",    // 4
                      "30 Emerald",    // 5
                      "1 Shield",      // 6
                      "40 Diamond",    // 7
                      "40 Emerald",    // 8
                      "1 Skip the Night", // 9
                      "50 Diamond",    // 10
                      "50 Emerald",    // 11
                      "1 Potion",      // 12
                      "75 Diamond",    // 13
                      "75 Emerald",    // 14
                      "1 Laser Wall",  // 15
                      "100 Diamond",   // 16
                      "100 Emerald",   // 17
                      "2 Thunder",     // 18
                      "150 Diamond",   // 19
                      "150 Emerald",   // 20
                      "2 Shield",      // 21
                      "200 Diamond",   // 22
                      "200 Emerald",   // 23
                      "2 Skip the Night",  //24
                      "250 Diamond",   // 25
                      "250 Emerald",   // 26
                      "2 Potion",      // 27
                      "300 Diamond",   // 28
                      "300 Emerald",   // 29
                      "2 Laser Wall"   // 30
                    };

    [SerializeField]
    private DailyItem itemPrefab;

    [SerializeField]
    private Transform itemsContent;

    private event Action OnEventButtonClick;

    private List<DailyItem> items = new List<DailyItem>();

    private int rewardIndex;

    private int totalSecond = 86400;


    private void Awake()
    {
       // ResetPlayerPrefs();
        rewardIndex = PlayerPrefs.GetInt(key_reward_index);
    }

    public void Open()
    {
        SetItems();
    }

    public void Close()
    {
        Save();
    }

    private void SetItems()
    {

        if (items.Count > 0)
        {
            CheckReady();

            return;
        }

        int count = rewards.Length; // 30

        if(rewardIndex > count)
        {
            count += (rewardIndex - count);
        }


        for(int i = 0; i < count; i++)
        {
            var item = Instantiate(itemPrefab, itemsContent);
            items.Add(item);

            if(i < rewardIndex)
            {
                SetItemText(i);
            }
            else
            {
                items[i].Set( "", true, null);
            }
            
        }

        CheckReady();
    }


    private void CheckReady()
    {
        bool hasBonus = HasBonus();

        totalSecond = PlayerPrefs.GetInt(key_total_second, 86400);

        int enter_total_second = GetNowTotalSecond();

        int delta_second = enter_total_second - PlayerPrefs.GetInt(key_leave_total_second);

        totalSecond -= delta_second;

        if (hasBonus)
        {
            UpdateItem();
        }
        else
        {
            items[rewardIndex].Set( "", false, null);
            StartCounter();
        }
    }

    private void StartCounter()
    {
        StopAllCoroutines();

        items[rewardIndex].SetTime(TimeSpan.FromSeconds(totalSecond).ToString());

        StartCoroutine(UpdateTime());
    }


    private IEnumerator UpdateTime()
    {
        yield return new WaitForSeconds(1);

        totalSecond -= 1;

        if(totalSecond > 0)
        {
            items[rewardIndex].SetTime(TimeSpan.FromSeconds(totalSecond).ToString());

            StartCoroutine(UpdateTime());
        }
        else
        {
            UpdateItem();
        }

    }

    private void UpdateItem()
    {
        OnEventButtonClick += OnClickButton;

        string message = "Day" + (rewardIndex + 1) + " \n ";
        if (rewardIndex >= rewards.Length)
        {
            message += PlayerPrefs.GetString(key_reward + rewardIndex);
        }
        else
        {
            message += rewards[rewardIndex];
        }

        items[rewardIndex].Set(message + " \n Open", false, OnEventButtonClick);

        StopAllCoroutines();
    }

    public void OnClickButton()
    {
        OnEventButtonClick -= OnClickButton;

        OpenReward();
    }

    private void OpenReward()
    {

        GiveReward(rewardIndex);

        SetItemText(rewardIndex);

        rewardIndex += 1;

        if (rewardIndex >= items.Count)
        {
            var item = Instantiate(itemPrefab, itemsContent);
            items.Add(item);
        }

        items[rewardIndex].Set("", false, null);

        totalSecond = 86400;

        StartCounter();
    }

    private void SetItemText(int index)
    {
        string message = "Day" + (index + 1) + " \n ";
        if (rewardIndex >= rewards.Length)
        {
            message += PlayerPrefs.GetString(key_reward + index);
        }
        else
        {
            message += rewards[index] + " \n Opened!";
        }

        items[index].Set(message, false, null);
    }


    private void GiveReward(int index)
    {

        switch(index)
        {
            case 0: //Add Diamond
                    //25 Diamond

                break;

            case 1: //Add Emerald
                    //25 Emerald

                break;

            case 2: //Add Thunder
                    //1 Thunder

                break;

            case 3: //Add Diamond
                    //30 Diamond

                break;

            case 4: //Add Emerald
                    //30 Emerald

                break;

            case 5: //Add Shield
                    //1 Shield

                break;

            case 6: //Add Diamond
                    //40 Diamond

                break;

            case 7: //Add Emerald
                    //40 Emerald

                break;

            case 8: //Add Skip the Night
                    //1 Skip the Night

                break;

            case 9: //Add Diamond
                    //50 Diamond

                break;

            case 10: //Add Emerald
                     //50 Emerald

                break;

            case 11: //Add Potion
                     //1 Potion

                break;

            case 12: //Add Diamond
                     //75 Diamond

                break;

            case 13: //Add Emerald
                     //75 Emerald

                break;

            case 14: //Add Laser Wall
                     //1 Laser Wall
                break;

            case 15: //Add Diamond
                     //100 Diamond

                break;

            case 16: //Add Emerald
                     //100 Emerald

                break;

            case 17: //Add Thunder
                     //2 Thunder

                break;

            case 18: //Add Diamond
                     //150 Diamond

                break;

            case 19: //Add Emerald
                     //150 Emerald

                break;

            case 20: //Add Shield
                     //2 Shield

                break;

            case 21: //Add Diamond
                     //200 Diamond

                break;

            case 22: //Add Emerald
                     //200 Emerald

                break;

            case 23: //Add Skip the Night
                     //2 Skip the Night

                break;

            case 24: //Add Diamond
                     //250 Diamond

                break;

            case 25: //Add Emerald
                     //250 Emerald

                break;

            case 26: //Add Potion
                     //2 Potion

                break;

            case 27: //Add Diamond
                     //300 Diamond

                break;

            case 28: //Add Emerald
                     //300 Emerald

                break;

            case 29: //Add Laser Wall
                     //2 Laser Wall

                break;

            default:

                //Add 300 Diamond
                //Add 300 Emerald

                //2 of a random booster

                int booster = UnityEngine.Random.Range(0, 4);

                string booster_name = "";

                switch(booster)
                {
                    case 0: //Add Shield
                            //2 Shield
                            booster_name = "2 Shield";

                        break;

                    case 1: //Add Skip the Night
                            //2 Skip the Night
                            booster_name = "2 Skip the Night";

                        break;

                    case 2: //Add Potion
                            //2 Potion
                            booster_name = "2 Potion";

                        break;

                    case 3: //Add Laser Wall
                            //2 Laser Wall
                            booster_name = "2 Laser Wall";

                        break;
                }

                string text = "300 Diamond, 300 Emerald, " + booster_name;

                PlayerPrefs.SetString(key_reward + rewardIndex, text);
                PlayerPrefs.Save();


                break;
        }

    }


    private void OnApplicationPause(bool pause)
    {
        if(pause)
        {
            Save();
        }
        else
        {
            if (items.Count > 0)
            {
                CheckReady();
            }
        }
    }

    private void Save()
    {

        int leaveTotalSecond = GetNowTotalSecond();

        DateTime addDay = DateTime.Now.AddDays(2);

        PlayerPrefs.SetInt(key_day, addDay.Day);
        PlayerPrefs.SetInt(key_reward_index, rewardIndex);
        PlayerPrefs.SetInt(key_leave_total_second, leaveTotalSecond);
        PlayerPrefs.SetInt(key_total_second, totalSecond);

        PlayerPrefs.Save();
    }

    private bool HasBonus()
    {
        int day = PlayerPrefs.GetInt(key_day);

        //Reset reward, return first day
        if(day <= DateTime.Now.Day)
        {
            ResetPlayerPrefs();
            return true;
        }

        if (0 >= totalSecond)
        {
            return true;
        }

        return false;

    }

    private int GetNowTotalSecond()
    {
        return DateTime.Now.Second +
                               DateTime.Now.Minute * 60 +
                               DateTime.Now.Hour * 60 * 60 +
                               DateTime.Now.Day * 24 * 60 * 60;
    }

    private static void ResetPlayerPrefs()
    {
        PlayerPrefs.SetInt(key_day, 0);
        PlayerPrefs.SetInt(key_leave_total_second, 0);
        PlayerPrefs.SetInt(key_total_second, 86400);
        PlayerPrefs.SetInt(key_reward_index, 0);
        PlayerPrefs.Save();
    }





}
