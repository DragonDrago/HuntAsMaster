using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using I2.Loc;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    

    [SerializeField]
    private Sprite successSprite;
    public Sprite GetSuccessSprite { get { return successSprite; } }

    [SerializeField]
    private Sprite errorSprite;
    public Sprite GetErrorSprite { get { return errorSprite; } }

    [SerializeField]
    private Sprite infoSprite;
    public Sprite GetInfoSprite { get { return infoSprite; } }

    [SerializeField]
    private AudioClip menuAudioClip;
    [SerializeField]
    private AudioClip gameAudioClip;

    [SerializeField]
    private AudioSource audioSource;

    private IList<IStatus> statusList = new List<IStatus>();
    private IList<IController> controllerList = new List<IController>();
    private IList<INeed> needList = new List<INeed>();
    private IList<ILevelState> levelStateList = new List<ILevelState>();

    private IList<ISaveTime> saveTimeList = new List<ISaveTime>();

    private IChangeStatus groundChangeStatus;
    private IChangeStatus playerChangeStatus;
    private IChangeStatus arrowChangeStatus;

    private IGetPath getPath;

    private IGift giftSkinandWeapon;

    private IFriendView friendView;

    private IUsedGift skinUsedGift;
    private IUsedGift weaponUsedGift;
    private IInfoStatus infoStatus;
    private IFriendControl friendControl;


    protected override void Awake()
    {
        base.Awake();
        
       // Constants.currentFriendCount =3;
        AppMetricaSendEventContrrol.Technical("Awake", PlayerPrefs.GetInt("keyInstall", 1));
        //Constants.ResetAllPlayerPrefs();

        Input.multiTouchEnabled = false;
    }

    private void Start()
    {
        

        if (Constants.language_current == "")
        {
            Constants.language_current = LocalizationManager.CurrentLanguage;
        }
        else
        {
            LocalizationManager.CurrentLanguage = Constants.language_current;
        }

        AppMetricaSendEventContrrol.Technical("Start", PlayerPrefs.GetInt("keyInstall", 1));
        PlayerPrefs.SetInt("keyInstall", 0);

    }

    public void AddFriendControl(IFriendControl control)
    {
        friendControl = control;
    }

    public void FriendControl()
    {
        friendControl.ResetFriend();
    }

    public void AddStatus(IStatus status)
    {
        if(!statusList.Contains(status))
        {
            statusList.Add(status);
        }

    }

    public void UpdateStatus()
    {
        foreach(IStatus status in statusList)
        {
            status.UpdateStatus();
        }
    }

    public void SetCoinStatus(float count)
    {
        foreach (IStatus status in statusList)
        {
            status.SetCoin(count);
        }
    }

    public void SetDiamondStatus(float count)
    {
        foreach (IStatus status in statusList)
        {
            status.SetDiamond(count);
        }
    }

    public void SetEnergyStatus(float count)
    {
        foreach (IStatus status in statusList)
        {
            status.SetEnergy(count);
        }
    }

    public void ShowSubcripeStatus()
    {
        foreach (IStatus status in statusList)
        {
            status.ShowSubcripe();
        }
    }

    public void AddGroundChangeStatus(IChangeStatus status)
    {
        groundChangeStatus = status;
    }

    public void GroundChangeStatus()
    {
        if(groundChangeStatus != null)
        {
            groundChangeStatus.ChangeGround();
        }
    }

    public void AddPlayerChangeSatus(IChangeStatus status)
    {
        playerChangeStatus = status;
    }

    public void PlayerChangeStatus()
    {
        if (playerChangeStatus != null)
        {
            playerChangeStatus.ChangePlayer();
        }
    }


    public void AddArrowChangeStatus(IChangeStatus status)
    {
        arrowChangeStatus = status;
    }

    public void ArrowChangeStatus()
    {
        if(arrowChangeStatus != null)
        {
            arrowChangeStatus.ChangeWeapon();
        }
    }

    public void AddController(IController controller)
    {
        if(!controllerList.Contains(controller))
        {
            controllerList.Add(controller);
        }

    }

    public void RemoveController(IController controller)
    {
        if(controllerList.Contains(controller))
        {
            controllerList.Remove(controller);
        }
    }


    public void ControllerLoaded()
    {
        foreach(IController controller in controllerList)
        {
            controller.OnLoaded();
        }
    }

    public void ControllerPlay()
    {
        foreach(IController controller in controllerList)
        {
            controller.OnPlay();
        }

        PlayGameMusic();

    }

    public void ControllerPause()
    {
        foreach (IController controller in controllerList)
        {
            controller.OnPause();
        }
    }

    public void ControllerTargetBegin()
    {
        foreach (IController controller in controllerList)
        {
            controller.OnTargetBegin();
        }

    }

    public void ControllerTargeting()
    {
        foreach (IController controller in controllerList)
        {
            controller.OnTargeting();
        }
        NeedEnabled();
    }

    public void ControllerTargetEnd()
    {
        foreach (IController controller in controllerList)
        {
            controller.OnTargetEnd();
        }
    }

    public void ControllerBackward()
    {
        foreach(IController controller in controllerList)
        {
            controller.OnBackward();
        }
    }

    public void ControllerFaild()
    {
        foreach(IController controller in controllerList)
        {
            controller.OnFaild();
        }
    }

    public void ControllerVectory()
    {
        foreach(IController controller in controllerList)
        {
            controller.OnVectory();
        }
    }

    public void ControllerFinish()
    {
        foreach (IController controller in controllerList)
        {
            controller.OnFinish();
        }
    }

    public void ControllerRestart()
    {
        foreach (IController controller in controllerList)
        {
            controller.OnRestart();
        }

        PlayMenuMusic();
    }

    public void AddNeed(INeed need)
    {
        if(!needList.Contains(need))
        {
            needList.Add(need);
        }
    }

    public void NeedEnabled()
    {
        foreach(INeed need in needList)
        {
            need.OnEnabled();
        }
    }

    public void NeedDisabled()
    {
        foreach (INeed need in needList)
        {
            need.OnDisabled();
        }
    }

    public void AddPath(IGetPath path)
    {
        getPath = path;
    }

    public SplineComputer GetPath()
    {
        return getPath.GetSpline();
    }

    public void AddLevelState(ILevelState state)
    {
        if(!levelStateList.Contains(state))
        {
            levelStateList.Add(state);
        }
    }

    public void LevelStateEnemyCount(int count)
    {
        foreach(ILevelState state in levelStateList)
        {
            state.UpdateEnemyCount(count);
        }
    }

    public void LevelStateScore(int score)
    {
        foreach(ILevelState state in levelStateList)
        {
            state.SetScore(score);
        }
    }

    public void AddGift(IGift gift)
    {
        giftSkinandWeapon = gift;
    }

    public PlayerSkin GetPlayerSkin(int index)
    {
        return giftSkinandWeapon.GetPlayerSkin(index);
    }

    public Weapon GetWeapon(int index)
    {
        return giftSkinandWeapon.GetWeapon(index);
    }

    public void AddFriendView(IFriendView  friend)
    {
        friendView = friend;
    }

    public void FriendResetTime()
    {
        if(friendView != null)
        {
            friendView.ResetTime();
        }
    }

    public void AddSkinUsedGift(IUsedGift usedGift)
    {
        skinUsedGift = usedGift;
    }

    public void SkinUsedGift()
    {
        skinUsedGift.UsedGift();
    }

    public void AddWeaponUsedGift(IUsedGift usedGift)
    {
        weaponUsedGift = usedGift;
    }

    public void WeaponUsedGift()
    {
        if(weaponUsedGift != null)
            weaponUsedGift.UsedGift();
    }

    public void AddSaveTime(ISaveTime saveTime)
    {
        if(!saveTimeList.Contains(saveTime))
        {
            saveTimeList.Add(saveTime);
        }
    }

    public void SaveTime()
    {
        foreach(ISaveTime save in saveTimeList)
        {
            save.SaveTime();
        }
    }

    public void AddInfoStatus(IInfoStatus infoStatus)
    {
        this.infoStatus = infoStatus;
    }

    public void InfoStatusMapUpdate()
    {
        if(infoStatus != null)
            infoStatus.UpdateMapInfo();
    }

    public void InfoStatusSkinUpdate()
    {
        if(infoStatus != null)
            infoStatus.UpdateSkinInfo();
    }

    public void InfoStatusWeaponUpdate()
    {
        if(infoStatus != null)
            infoStatus.UpdateWaponInfo();
    }

    public void PlayMenuMusic()
    {
        if (Constants.isMusic)
        {
            audioSource.clip = menuAudioClip;
            audioSource.volume = 0.2f;
            audioSource.Play();
        }
    }

    public void PlayGameMusic()
    {
        if (Constants.isMusic)
        {
            audioSource.clip = gameAudioClip;
            audioSource.volume = 0.1f;
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        audioSource.Pause();
    }

    

    private void OnApplicationPause(bool pause)
    {
        if(pause)
        {
            AppMetricaSendEventContrrol.SaveStates();
        }
    }


    

}
