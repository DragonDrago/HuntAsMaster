using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using ConsentManager;

public class GiftSkin : MonoBehaviour
{
    [SerializeField]
    private Transform giftTransform;

    [SerializeField]
    private Transform maskTransform;

    private PlayerSkin currentPlayerSkin;
    private Weapon currentWeapon;
    private GameObject arrowObject;

    private int partIndex;

    public void Show(bool isFailed)
    {
        Constants.giftEnum = Constants.GiftEnum.None;

        gameObject.SetActive(true);

        if (isFailed)
        {
            maskTransform.localPosition = new Vector3(0f, 0f, 4f);

            if (currentPlayerSkin != null)
            {
                currentPlayerSkin.gameObject.SetActive(false);
            }

            if (currentWeapon != null)
            {
                currentWeapon.gameObject.SetActive(false);
            }

            return;
        }

        if (Constants.gift_has == 1 || !AppodelManager.Instance.HasRewarded())
        {
            maskTransform.localPosition = new Vector3(0f, 0f, 4f);

            if (currentPlayerSkin != null)
            {
                Destroy(currentPlayerSkin.gameObject);
            }

            if (currentWeapon != null)
            {
                Destroy(currentWeapon.gameObject);
            }

            return;
        }

        partIndex = Constants.gift_part_index;

        if (currentPlayerSkin == null || currentWeapon == null || partIndex == 3)
        {
            if (partIndex == 3)
                partIndex = 0;

            maskTransform.localPosition = new Vector3(0f, 0f, 4f);

            if (currentPlayerSkin != null)
            {
                Destroy(currentPlayerSkin.gameObject);
                currentPlayerSkin = null;
            }

            if (currentWeapon != null)
            {
                Destroy(currentWeapon.gameObject);
                currentWeapon = null;

                if (arrowObject != null)
                {
                    Destroy(arrowObject);
                }
            }


            if ((Constants.gift_current_index + 1) % 3 == 0)
            {
                if (Constants.gift_current_skin_index < Constants.gift_skins_index.Count)
                {
                    if(HasSkin())
                    {
                        CreateSkin();
                    }
                    else
                    {
                        if (Constants.gift_current_weapon_index < Constants.gift_weapons_index.Count)
                        {
                            if(HasWeapon())
                            {
                                CreateWeapon();
                            }
                            else
                            {
                                Constants.gift_has = 1;
                            }
                        }
                        else
                        {
                            Constants.gift_has = 1;
                        }

                    }
                }
                else
                {
                    if (Constants.gift_current_weapon_index < Constants.gift_weapons_index.Count)
                    {
                        if (HasWeapon())
                        {
                            CreateWeapon();
                        }
                        else
                        {
                            Constants.gift_has = 1;
                        }
                    }

                }
                
                // gift skin
            }
            else
            {
                if (Constants.gift_current_weapon_index < Constants.gift_weapons_index.Count)
                {
                    if (HasWeapon())
                    {
                        CreateWeapon();
                    }
                    else
                    {
                        if (Constants.gift_current_skin_index < Constants.gift_skins_index.Count)
                        {
                            if (HasSkin())
                            {
                                CreateSkin();
                            }
                            else
                            {
                                Constants.gift_has = 1;
                            }
                        }
                        else
                        {
                            Constants.gift_has = 1;
                        }
                    }
                }
                else
                {
                    if (Constants.gift_current_skin_index < Constants.gift_skins_index.Count)
                    {
                        if (HasSkin())
                        {
                            CreateSkin();
                        }
                        else
                        {
                            Constants.gift_has = 1;
                        }
                    }
                    else
                    {
                        Constants.gift_has = 1;
                    }
                }
                //gift weapon
            }


        }
        else
        {
            if(currentPlayerSkin != null)
            {
                currentPlayerSkin.gameObject.SetActive(true);
            }
            else if(currentWeapon != null)
            {
                currentWeapon.gameObject.SetActive(true);
            }
        }

        SetMask();
    }

    private bool HasSkin()
    {
        for(int i = Constants.gift_current_skin_index; i < Constants.gift_skins_index.Count; i++)
        {
            if(PlayerPrefs.GetInt(Constants.key_item_lock_skin + Constants.gift_skins_index[i]) == 0)
            {
                Constants.gift_current_skin_index = i;
                return true;
            }
        }

        Constants.gift_current_skin_index = Constants.gift_skins_index.Count;

        return false;
    }

    private bool HasWeapon()
    {
        for (int i = Constants.gift_current_weapon_index; i < Constants.gift_weapons_index.Count; i++)
        {
            if (PlayerPrefs.GetInt(Constants.key_item_lock_weapon + Constants.gift_weapons_index[i]) == 0)
            {
                Constants.gift_current_weapon_index = i;
                return true;
            }
        }

        Constants.gift_current_skin_index = Constants.gift_skins_index.Count;

        return false;
    }


    private void CreateWeapon()
    {
      
        currentWeapon = Instantiate(GameManager.Instance.GetWeapon(Constants.gift_weapons_index[Constants.gift_current_weapon_index]), giftTransform);

        currentWeapon.gameObject.AddComponent<Outline>();

        if (currentWeapon.weaponType == Weapon.WeaponType.bow)
        {
            currentWeapon.transform.localPosition = new Vector3(0f, 1f, 2f);
            currentWeapon.transform.localEulerAngles = new Vector3(0f, 0f, 180f);

            arrowObject = Instantiate(currentWeapon.GetArrowPrefab.gameObject, currentWeapon.transform);
            arrowObject.transform.localPosition = new Vector3(0.5f, 0f, 2.1f);
            arrowObject.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
            arrowObject.AddComponent<Outline>();
        }
        else if (currentWeapon.weaponType == Weapon.WeaponType.axe)
        {
            currentWeapon.transform.localScale = new Vector3(2f, 2f, 2f);
            currentWeapon.transform.localPosition = new Vector3(0f, 0.5f, 2f);
            currentWeapon.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
        }
        else if (currentWeapon.weaponType == Weapon.WeaponType.wand)
        {
            currentWeapon.transform.localScale = new Vector3(2f, 2f, 2f);
            currentWeapon.transform.localPosition = new Vector3(0f, 0f, 3f);
            currentWeapon.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
        }
    }

    private void CreateSkin()
    {
        currentPlayerSkin = Instantiate(GameManager.Instance.GetPlayerSkin(Constants.gift_skins_index[Constants.gift_current_skin_index]), giftTransform);
        currentPlayerSkin.transform.localPosition = new Vector3(0f, 0.7f, 2f);
        currentPlayerSkin.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
        currentPlayerSkin.gameObject.AddComponent<Outline>();
    }


    private void SetMask()
    {
        switch(partIndex)
        {
            case 0:

                maskTransform.DOLocalMoveY(0.7f, 2f);

                Constants.gift_part_index = 1;

                break;

            case 1:

                maskTransform.DOLocalMoveY(1.4f, 2f);

                Constants.gift_part_index = 2;

                break;

            case 2:

                maskTransform.DOLocalMoveY(2.0f, 2f);

                Constants.gift_part_index = 3;

                Constants.gift_current_index++;

                if(Constants.gift_current_index >= Constants.GetGiftCount)
                {
                    Constants.gift_has = 1;
                }

                if (currentPlayerSkin != null)
                {
                    Constants.gift_current_skin_index++;

                    Constants.giftEnum = Constants.GiftEnum.Skin;
                }
                else if(currentWeapon != null)
                {
                    Constants.gift_current_weapon_index++;
                    Constants.giftEnum = Constants.GiftEnum.Weapon;
                }

                break;

        }
    }


    public void Hide()
    {
        
        if (partIndex == 3)
        {
            maskTransform.localPosition = new Vector3(0f, 0f, 4f);

            if (currentPlayerSkin != null)
            {
                Destroy(currentPlayerSkin.gameObject);
                
            }

            if(currentWeapon != null)
            {
                Destroy(currentWeapon.gameObject);

                if(arrowObject != null)
                {
                    Destroy(arrowObject);
                }
            }

            Constants.gift_part_index = 0;
        }

        gameObject.SetActive(false);
    }
}
