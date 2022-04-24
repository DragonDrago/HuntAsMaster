using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class GroundController : MonoBehaviour, IChangeStatus, IGetPath, IController
{
    [SerializeField]
    private GameObject[] groundPrefabs = new GameObject[12];

    [SerializeField]
    private SplineComputer[] groupSpline0;

    [SerializeField]
    private SplineComputer[] groupSpline1;

    [SerializeField]
    private SplineComputer[] groupSpline2;

    [SerializeField]
    private SplineComputer[] groupSpline3;

    [SerializeField]
    private SplineComputer[] groupSpline4;

    [SerializeField]
    private SplineComputer[] groupSpline5;

    [SerializeField]
    private SplineComputer[] groupSpline6;

    [SerializeField]
    private SplineComputer[] groupSpline7;

    [SerializeField]
    private SplineComputer[] groupSpline8;

    [SerializeField]
    private SplineComputer[] groupSpline9;


    private List<int> randoms = new List<int>();

    private GameObject currentGround;

    private int currentIndex = -1;


    private void Awake()
    {
        ChangeGround();
    }

    private void Start()
    {
        GameManager.Instance.AddGroundChangeStatus(this);
        GameManager.Instance.AddPath(this);
        GameManager.Instance.AddController(this);
    }

    public void SetGround(int index)
    {
        if (currentIndex == index)
            return;

        if (currentGround != null)
            Destroy(currentGround);

        currentGround = Instantiate(groundPrefabs[index], transform);
        //currentGround.transform.localPosition = new Vector3(0f, 2f, 0f);
    }


    private SplineComputer GetSplineComputer()
    {
        if (Constants.currentLevel < 10)
        {
            return GetSpline(groupSpline0);
        }
        else if (Constants.currentLevel < 20)
        {
            return GetSpline(groupSpline1);
        }
        else if (Constants.currentLevel < 30)
        {
            return GetSpline(groupSpline2);
        }
        else if (Constants.currentLevel < 40)
        {
            return GetSpline(groupSpline3);
        }
        else if (Constants.currentLevel < 50)
        {
            return GetSpline(groupSpline4);
        }
        else if (Constants.currentLevel < 60)
        {
            return GetSpline(groupSpline5);
        }
        else if (Constants.currentLevel < 70)
        {
            return GetSpline(groupSpline6);
        }
        else if (Constants.currentLevel < 80)
        {
            return GetSpline(groupSpline7);
        }
        else if(Constants.currentLevel < 90)
        {
            return GetSpline(groupSpline8);
        }
        else
        {
            return GetSpline(groupSpline9);
        }
    }


    private SplineComputer GetSpline(SplineComputer[] groupSpline)
    {
        
        if(randoms.Count == 0)
        {
            randoms = Constants.RandomNumber(0, groupSpline.Length, groupSpline.Length);
        }

        int index = randoms[0];
        randoms.Remove(index);

        return groupSpline[index];
    }

    public SplineComputer GetSpline()
    {
        return GetSplineComputer();
    }

    public void OnLoaded()
    {

    }

    public void OnPlay()
    {
        
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
       
    }

    public void OnVectory()
    {
        if(currentGround != null)
        {
            currentGround.SetActive(false);
        }
    }

    public void OnFinish()
    {
        if (currentGround != null)
        {
            currentGround.SetActive(false);
        }
    }

    public void OnRestart()
    {
        if (currentGround != null)
        {
            currentGround.SetActive(true);
        }
    }

    public void ChangePlayer()
    {
        
    }

    public void ChangeWeapon()
    {
        
    }

    public void ChangeGround()
    {
        SetGround(Constants.mapItemCurrentIndex);
    }
}
