using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour, IChangeStatus, IController, IGift
{

    [SerializeField]
    private PlayerSkin[] playerPrefabs = new PlayerSkin[30];

    [SerializeField]
    private Weapon[] weaponPrefabs = new Weapon[66];

    [SerializeField]
    private Transform playerTransform;


    private Player player;

    private PlayerSkin currentPlayer;
    private Weapon currentWeapon;
    private Weapon currentArrowWeapon;

    private int currentPlayerIndex = -1;

    private void Awake()
    {
        player = GetComponentInChildren<Player>();

        ChangePlayer();

        ChangeWeapon();
    }

    private void Start()
    {
        GameManager.Instance.AddPlayerChangeSatus(this);
        GameManager.Instance.AddArrowChangeStatus(this);

        GameManager.Instance.AddController(this);
        GameManager.Instance.AddGift(this);

        currentPlayerIndex = Constants.skinItemCurrentIndex;
    }

    public void SetPlayer(int index)
    {
        if (currentPlayerIndex == index)
            return;

        currentPlayerIndex = index;

        if (currentPlayer != null)
            Destroy(currentPlayer.gameObject);

        currentPlayer = Instantiate(playerPrefabs[index], playerTransform);
        currentPlayer.transform.localPosition = Vector3.zero;
        
    }

    public void SetWeapon(int index)
    {
        if(currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }

        if(currentArrowWeapon != null)
        {
            Destroy(currentArrowWeapon.gameObject);
        }


        if(weaponPrefabs[index].weaponType == Weapon.WeaponType.bow)
        {
            currentWeapon = Instantiate(weaponPrefabs[index], currentPlayer.GetLeftHandTransform);
            currentArrowWeapon = Instantiate(currentWeapon.GetArrowPrefab, currentPlayer.GetRightHandTransform);
            
        }
        else
        {
            currentWeapon = Instantiate(weaponPrefabs[index], currentPlayer.GetRightHandTransform);
        }

        player.Set(currentPlayer.GetAnimator, currentWeapon.GetArrowPrefab);
    }

    public void OnLoaded()
    {

    }

    public void OnPlay()
    {
        Constants.isFinish = false;
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

    public void OnFaild()
    {
        Constants.isFinish = true;
    }

    public void  OnVectory()
    {
        Constants.isFinish = true;
    }

    public void OnFinish()
    {

    }

    public void OnBackward()
    {
        Constants.isFinish = false;
    }

    public void OnRestart()
    {
        if (Constants.hasUseGiftSkin)
        {
            ChangePlayer();
        }
       else if(Constants.hasUseGiftWeapon)
        {
            ChangeWeapon();
        }
    }

    public PlayerSkin GetPlayerSkin(int index)
    {
        return playerPrefabs[index];
    }

    public Weapon GetWeapon(int index)
    {
        return weaponPrefabs[index];
    }

    public void ChangePlayer()
    {
        SetPlayer(Constants.skinItemCurrentIndex);
        SetWeapon(Constants.weaponItemCurrentIndex);
    }

    public void ChangeWeapon()
    {
        SetWeapon(Constants.weaponItemCurrentIndex);
    }

    public void ChangeGround()
    {
        
    }
}
