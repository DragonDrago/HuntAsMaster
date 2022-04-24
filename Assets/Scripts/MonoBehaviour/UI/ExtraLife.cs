using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System;
using ConsentManager;

public class ExtraLife : MonoBehaviour
{
    [SerializeField]
    private GameObject watchVideoObject;

    [SerializeField]
    private GameObject energyBuyObject;

    [SerializeField]
    private GameObject energyObject;

    [SerializeField]
    private Image filledImage;

    [SerializeField]
    private Text continueText;

    [SerializeField]
    private Text energyCountText;


    private Action watchAction, watchCancelAction;
    private Action purchaseDone, purchaseError;

    private void Start()
    {
        watchAction += BackwardGame;
        watchCancelAction += CancelWatch;

        purchaseDone += EnergyBuyDone;
        purchaseError += EnergyBuyError;

    }

    public void Show()
    {
        gameObject.SetActive(true);

        //agar ko'rsatishga video bo'lsa
        if(AppodelManager.Instance.HasRewarded())
        {
            watchVideoObject.gameObject.SetActive(true);
            StartCoroutine(FilledRoutine());
        }
        else
        {
            StopCoroutine(FilledRoutine());
            watchVideoObject.gameObject.SetActive(false);
        }
        
        if(Constants.total_energies == 0)
        {
            energyCountText.text = "+" + Constants.product_energy_count;
            energyBuyObject.SetActive(true);
            energyObject.SetActive(false);
        }
        else
        {
            energyBuyObject.SetActive(false);
            energyObject.SetActive(true);
        }
        
        continueText.DOFade(1f, 1f).SetDelay(2f);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator FilledRoutine()
    {
        float fill = 1f;

        while(fill > 0)
        {
            filledImage.fillAmount = fill;
            fill -= 0.002f;

            yield return new WaitForSeconds(0.01f);
        }

        watchVideoObject.SetActive(false);
        
    }


    public void OnClickWatchVideo()
    {
        AppodelManager.Instance.ShowRewardedVideo("extra_chance", watchAction, watchCancelAction);
    }



    public void OnClickEnergyBuy()
    {
        PurchaseManager.Instance.BuyProduct(Constants.product_energy, purchaseDone, purchaseError);
    }

    private void EnergyBuyDone()
    {
        Constants.total_energies += (Constants.product_energy_count - 1);

        GameManager.Instance.UpdateStatus();

        BackwardGame();

        Constants.InfoPopup(Constants.title_success, Constants.message_success, GameManager.Instance.GetSuccessSprite);
    }

    private void EnergyBuyError()
    {
        Constants.InfoPopup(Constants.title_error, Constants.message_error, GameManager.Instance.GetErrorSprite);
    }


    public void OnClickEnergy()
    {
        if(Constants.total_energies > 0)
        {
            Constants.total_energies -= 1;

            GameManager.Instance.UpdateStatus();

            BackwardGame();
        }
        
    }

    private void BackwardGame()
    {
        GameManager.Instance.ControllerBackward();
    }


    private  void CancelWatch()
    {

        StopAllCoroutines();

        watchVideoObject.SetActive(false);
    }


    public void OnClickContinue()
    {
        GameManager.Instance.ControllerFinish();
    }

}
