
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ItemsStatusView : MonoBehaviour, IStatus
{

    [SerializeField]
    private Text coinsText;

    [SerializeField]
    private Text diamondsText;

    [SerializeField]
    private Text energesText;


    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;


    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        GameManager.Instance.AddStatus(this);
    }

    public void Show()
    {
        canvasGroup.alpha = 1f;
        rectTransform.DOAnchorPosX(0f, 0.3f);
        UpdateStatus();
    }

    public void Hide()
    {
        rectTransform.DOAnchorPosX(2000f, 0.1f).OnComplete( () => {
            canvasGroup.alpha = 0f;
        });
    }

    public void UpdateStatus()
    {
        coinsText.text = Constants.GetCoinNumber();
        diamondsText.text = Constants.GetDiamondNumber();
        energesText.text = Constants.GetEnergyNumber();
    }


    public void SetCoin(float count)
    {
        
    }

    public void SetDiamond(float count)
    {
        
    }

    public void SetEnergy(float count)
    {
        
    }

    public void ShowSubcripe()
    {
        
    }

    
}
