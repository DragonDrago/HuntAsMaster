using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using ConsentManager;

public class SpinBonus : MonoBehaviour
{
    [SerializeField]
    private Text coinText;

    [SerializeField]
    private Text diamondText;

    [SerializeField]
    private Text energyText;

    [SerializeField]
    private Image iconImage;

    [SerializeField]
    private Button watchButton;

    [SerializeField]
    private Text iconText;

    [SerializeField]
    private Transform cursorTransform;

    [SerializeField]
    private Sprite[] sprites;

    [SerializeField]
    private GameObject coinEffectObject;

    [SerializeField]
    private GameObject diamondEffectObject;

    [SerializeField]
    private GameObject energyEffectObject;

    [SerializeField]
    private string placement;

    private Vector3 rotate = new Vector3();

    private Action watchAction, watchCancelActtion;

    private float coin;
    private float diamond;
    private float energy;

    private int index;

    private void Start()
    {
        watchAction += Watched;
       // watchCancelActtion += Hide;
    }

    public void Show(float coinCount, float diamondCount, float energyCount)
    {
        transform.localScale = Vector3.one;

        this.coin = coinCount;
        this.diamond = diamondCount;
        this.energy = energyCount;

        coinText.text = "+" + Constants.ConvertShortNumber(coin);
        diamondText.text = "+" + Constants.ConvertShortNumber(diamond);
        energyText.text = "+" + Constants.ConvertShortNumber(energy);

        watchButton.interactable = true;

        StartCoroutine(CursorRoutine());

    }

    public void Hide()
    {
        coinEffectObject.SetActive(false);
        diamondEffectObject.SetActive(false);
        energyEffectObject.SetActive(false);

        StopAllCoroutines();
        cursorTransform.DOKill();
        transform.localScale = Vector3.zero;
    }

    private IEnumerator CursorRoutine()
    {
        iconImage.sprite = sprites[index];

        switch(index)
        {
            case 0:

                iconText.text = coinText.text;
                rotate = new Vector3(0f, 0f, 0f);

                break;
            case 1:

                iconText.text = diamondText.text;
                rotate = new Vector3(0f, 0f, -50f);

                break;
            case 2:

                iconText.text = coinText.text;
                rotate = new Vector3(0f, 0f, 0f);

                break;

            case 3:

                iconText.text = energyText.text;
                rotate = new Vector3(0f, 0f, 50f);

                break;
        }

        cursorTransform.DOLocalRotate(rotate, 1f);
        
        yield return new WaitForSeconds(1f);

        index++;

        if(index == 4)
        {
            index = 0;
        }

        StartCoroutine(CursorRoutine());
    }
         

    public void OnClickWatchVideo()
    {
        AppodelManager.Instance.ShowRewardedVideo(placement, watchAction, watchCancelActtion);
    }

    private void Watched()
    {
        watchButton.interactable = false;
        cursorTransform.DOKill();
        StopAllCoroutines();

        int random = UnityEngine.Random.Range(0, 11);

        if(random < 8)
        {
            //coin
            iconImage.sprite = sprites[0];
            coinEffectObject.SetActive(true);

            iconText.text = coinText.text;
            rotate = new Vector3(0f, 0f, 0f);

            Constants.total_coins += coin;
                
        }
        else if(random < 10)
        {
            //diamond
            iconImage.sprite = sprites[1];
            diamondEffectObject.SetActive(true);

            iconText.text = diamondText.text;
            rotate = new Vector3(0f, 0f, -50f);

            Constants.total_diamonds += diamond;
        }
        else
        {
            //energy
            iconImage.sprite = sprites[3];
            energyEffectObject.SetActive(true);

            iconText.text = energyText.text;
            rotate = new Vector3(0f, 0f, 50f);

            Constants.total_energies += energy;
        }

        cursorTransform.localEulerAngles = rotate;

        GameManager.Instance.UpdateStatus();

    }

}
