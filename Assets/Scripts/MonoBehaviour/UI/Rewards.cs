using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Rewards : MonoBehaviour
{

    [SerializeField]
    private Text energyText;

    [SerializeField]
    private Text coinText;

    [SerializeField]
    private Text diamondText;

    [SerializeField]
    private GameObject energyBonusObject;

    [SerializeField]
    private GameObject coinBonusObject;

    [SerializeField]
    private GameObject diamondBonusObject;

    [SerializeField]
    private Transform energyTransform;

    [SerializeField]
    private Transform coinTransform;

    [SerializeField]
    private Transform diamondTransform;
        

    public void Show(float coinPrice, bool coinBonus,
                     float diamondPrice, bool diamondbonus,
                     float energyPrice, bool energybonus)
    {

        gameObject.SetActive(true);
        transform.DOScale(Vector3.one, 0.1f);

        SetCoin(coinPrice, coinBonus);

        if (diamondPrice != 0)
        {
            SetDiamond(diamondPrice, diamondbonus);
        }

        if(energyPrice != 0)
        {
            SetEnery(energyPrice, energybonus);

            Constants.total_energies += energyPrice;
        }

        GameManager.Instance.UpdateStatus();
    }

    private void SetEnery(float price, bool bonus)
    {
        energyText.text = "+" + Constants.ConvertShortNumber(price);
        energyBonusObject.SetActive(bonus);

        energyTransform.gameObject.SetActive(true);
        energyTransform.DOScale(Vector3.one, 1f).SetEase(Ease.OutFlash);
    }

    private void SetCoin(float price, bool bonus)
    {
        coinText.text = "+" + Constants.ConvertShortNumber(price);
        coinBonusObject.SetActive(bonus);

        coinTransform.gameObject.SetActive(true);
        coinTransform.DOScale(Vector3.one, 1f).SetEase(Ease.OutFlash);
    }

    private void SetDiamond(float price, bool bonus)
    {
        diamondText.text = "+" + Constants.ConvertShortNumber(price);
        diamondBonusObject.SetActive(bonus);

        diamondTransform.gameObject.SetActive(true);
        diamondTransform.DOScale(Vector3.one, 1f).SetEase(Ease.OutFlash);
    }


    public void Hide()
    {
        energyTransform.localScale = Vector3.zero;
        coinTransform.localScale = Vector3.zero;
        diamondTransform.localScale = Vector3.zero;

        energyTransform.gameObject.SetActive(false);
        coinTransform.gameObject.SetActive(false);
        diamondTransform.gameObject.SetActive(false);

        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }





}
