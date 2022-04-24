
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class StatusView : MonoBehaviour, IStatus
{

    [SerializeField]
    private Text coinsText;

    [SerializeField]
    private Text diamondsText;

    [SerializeField]
    private Text energesText;

    [SerializeField]
    private GameObject coinObject;

    [SerializeField]
    private GameObject diamondObject;

    [SerializeField]
    private GameObject energyObject;


    private RectTransform rectTransform;



    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        SetStatus();
    }

    private void Start()
    {
        GameManager.Instance.AddStatus(this);
    }


    private void SetStatus()
    {
        coinsText.text =  Constants.GetCoinNumber();
        diamondsText.text = Constants.GetDiamondNumber();
        energesText.text = Constants.GetEnergyNumber();
    }

    

    public void UpdateStatus()
    {
        SetStatus();
    }

    public void ShowCoin()
    {
        coinObject.SetActive(true);
        diamondObject.SetActive(false);
        energyObject.SetActive(false);

        rectTransform.DOAnchorPosX(0f, 0.3f).SetEase(Ease.InOutSine).OnComplete( () => {

            rectTransform.DOAnchorPosX(1000f, 0.3f).SetEase(Ease.InOutSine).SetDelay(1f);
        });
        
    }

    public void ShowDiamond()
    {
        coinObject.SetActive(false);
        diamondObject.SetActive(true);
        energyObject.SetActive(false);

        rectTransform.DOAnchorPosX(0f, 0.3f).SetEase(Ease.InOutSine).OnComplete( () => {

            rectTransform.DOAnchorPosX(2000f, 0.3f).SetEase(Ease.InOutSine).SetDelay(1f);
        }); 
    }

    public void ShowEnergy()
    {
        coinObject.SetActive(false);
        diamondObject.SetActive(false);
        energyObject.SetActive(true);

        rectTransform.DOAnchorPosX(0f, 1f).SetEase(Ease.InOutSine);
    }

    public void ShowCoinAndDiamond()
    {
        coinObject.SetActive(true);
        diamondObject.SetActive(true);
        energyObject.SetActive(false);

        rectTransform.DOAnchorPosX(0f, 1f).SetEase(Ease.InOutSine);

    }

    public void ShowAll()
    {
        coinObject.SetActive(true);
        diamondObject.SetActive(true);
        energyObject.SetActive(true);

        rectTransform.DOAnchorPosX(0f, 1f).SetEase(Ease.InOutSine);
    }

    public void Hide()
    {
        rectTransform.DOAnchorPosX(2000f, 1f).SetEase(Ease.InOutSine);
    }

    public void SetCoin(float count)
    {
        SetStatus();
        ShowCoin();
    }

    public void SetDiamond(float count)
    {
        Constants.total_diamonds += count;
        SetStatus();
        ShowCoin();
    }

    public void SetEnergy(float count)
    {
        Constants.total_energies += count;
        SetStatus();
        ShowCoin();
    }

    public void ShowSubcripe()
    {
        
    }
}
