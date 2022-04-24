using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ItemsView : MonoBehaviour
{

    [SerializeField]
    private ItemsStatusView itemsStatusView;

    [SerializeField]
    private TabPages mapPage;
    [SerializeField]
    private TabPages skinPage;
    [SerializeField]
    private TabPages arrowPage;
    [SerializeField]
    private TabPages shopPage;

    [SerializeField]
    private Image bgImage;

    [SerializeField]
    private Sprite[] bgSprites;

    [SerializeField]
    private Image centerGlowImage;

    [SerializeField]
    private Color[] glowColors;

    [SerializeField]
    private Image buttonsGlowImage;

    [SerializeField]
    private Image topGlowImage;

    [SerializeField]
    private Image bottomGlowImage;


    public void ShowStatusBar()
    {
        itemsStatusView.Show();
    }

    public void HideStatusBar()
    {
        itemsStatusView.Hide();
    }

    public void OnClickItemButton(int index)
    {
        ShowPages(index);
    }

    private void ShowPages(int index)
    {

        bgImage.sprite = bgSprites[index];
        centerGlowImage.color = glowColors[index];
        buttonsGlowImage.color = glowColors[index];

        topGlowImage.color = glowColors[index];
        bottomGlowImage.color = glowColors[index];

        switch(index)
        {
            case 0:

                Constants.tabPageIndex = 0;

                mapPage.Show();

                skinPage.Hide();
                arrowPage.Hide();
                shopPage.Hide();

                break;
            case 1:

                Constants.tabPageIndex = 1;

                skinPage.Show();

                mapPage.Hide();
                arrowPage.Hide();
                shopPage.Hide();

                break;
            case 2:

                Constants.tabPageIndex = 2;

                arrowPage.Show();

                skinPage.Hide();
                mapPage.Hide();
                shopPage.Hide();

                break;
            case 3:

                shopPage.Show();

                skinPage.Hide();
                arrowPage.Hide();
                mapPage.Hide();

                break;
        }
    }


}
