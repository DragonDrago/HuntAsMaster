using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabPages : MonoBehaviour, IStatus
{
    [SerializeField]
    private PagesItemBase[] items;

    [SerializeField]
    private ShopView shopView;

    private void Start()
    {
        GameManager.Instance.AddStatus(this);
        SetItems();
    }

    private void SetItems()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].Set(i);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);

        if (shopView == null)
        {
            for (int i = 1; i < items.Length; i++)
            {
                items[i].SetValue(i);
            } 
        }
        else
        {
            shopView.Show();
            AppMetricaSendEventContrrol.PurchaseWindow("clik_shop", "show");
        }
        
    }

    public void Hide()
    {
        if (shopView == null)
        {
        }
        else
        {
            AppMetricaSendEventContrrol.PurchaseWindow("clik_shop", "close");
        }

        gameObject.SetActive(false);
    }

    public PagesItemBase GetItem(int index)
    {
        return items[index];
    }

    public void UpdateStatus()
    {
        
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
        if (shopView == null)
        {
            for (int i = 1; i < items.Length; i++)
            {
                items[i].SetValue(i);
            }
        }

    }
}
