using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using ConsentManager;

public class ChestPopup : MonoBehaviour
{
    [SerializeField]
    private GameObject tabToOpenObject;

    [SerializeField]
    private GameObject particleDiamod;

    [SerializeField]
    private GameObject particleEnergy;

    [SerializeField]
    private GameObject particleCoin;

    [SerializeField]
    private Text countText;

    [SerializeField]
    private RectTransform anotherButtonTransform;

    [SerializeField]
    private RectTransform okButtonTransform;


    private Action watchDone, watchError;

    private bool isClickLock;

    private void Start()
    {
        watchDone += WatchDone;
        watchError += WatchError;
    }

    public void OnClickOpen()
    {
        if (isClickLock)
            return;

        isClickLock = true;

        tabToOpenObject.SetActive(false);
        GetGift();

        if (AppodelManager.Instance.HasRewarded())
            anotherButtonTransform.DOAnchorPosY(800f, 0.3f);

        okButtonTransform.DOAnchorPosY(400, 0.3f);
        //Constants.currentPopup.Hide(1.5f);
    }

    private void GetGift()
    {
        
        int random = UnityEngine.Random.Range(0, 10);

        if (random < 5)
        {
            // coin

            particleCoin.SetActive(true);
            particleCoin.GetComponent<ParticleSystem>().Play();

            float coin = (Constants.currentLevel + 1) * 10;
            countText.text = "+" + Constants.ConvertShortNumber(coin);
            Constants.total_coins += coin;
        }
        else if (random < 9)
        {
            //diamond
            

            particleDiamod.SetActive(true);
            particleDiamod.GetComponent<ParticleSystem>().Play();

            float diamond = (Constants.currentLevel + 1) * 1;
            countText.text = "+" + Constants.ConvertShortNumber(diamond);
            Constants.total_diamonds += diamond;
        }
        else if (random < 10)
        {
            //energy

            particleEnergy.SetActive(true);
            particleEnergy.GetComponent<ParticleSystem>().Play();

            int energy = UnityEngine.Random.Range(1, 3);
            countText.text = "+" + Constants.ConvertShortNumber(energy);
            Constants.total_energies += energy;
        }

        GameManager.Instance.UpdateStatus();
    }

    public void OnClickAnotherWatch()
    {
        isClickLock = false;

        countText.text = "";

        particleCoin.SetActive(false);
        particleDiamod.SetActive(false);
        particleEnergy.SetActive(false);

        AppodelManager.Instance.ShowRewardedVideo("chest_another_gift", watchDone, watchError);
    }

    private void WatchDone()
    {
        anotherButtonTransform.DOAnchorPosY(-1000f, 0.3f);
        okButtonTransform.DOAnchorPosY(-1000f, 0.3f);

        tabToOpenObject.SetActive(true);
    }

    private void WatchError()
    {

    }


    public void OnClickOk()
    {
        Constants.HidePopup();
    }

}
