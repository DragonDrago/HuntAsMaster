using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Loading : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private TextMeshProUGUI valueText;

    [SerializeField]
    private Slider slider;

    private float count;

    public void Show()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        slider.value = 0;
        count = 0;

        StartCoroutine(LoadRoutine());
    }

    private IEnumerator LoadRoutine()
    {
        

        yield return new WaitForSeconds(0.1f);

        count += 0.05f;

        if(count > 1)
        {
            slider.value = count;

            valueText.text = (slider.value * 100).ToString("F0") + " %";

            GameManager.Instance.ControllerLoaded();

            canvasGroup.DOFade(0f, 1f).OnComplete(() =>
            {

                canvasGroup.blocksRaycasts = false;
                canvasGroup.interactable = false;


                StopAllCoroutines();
            });
        }
        else
        {
            slider.value = count;

            valueText.text = (slider.value * 100).ToString("F0") + " %";

            StartCoroutine(LoadRoutine());
        }
        

    }



}
