using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using UnityEngine;
using UnityEngine.UI;

public class EnemyProgress : MonoBehaviour
{

    [SerializeField]
    private Slider slider;

    [SerializeField]
    private GameObject[] lineObjects = new GameObject[9];


    [SerializeField]
    private UIView uiView;

    [SerializeField]
    private HorizontalLayoutGroup group;


    public void Set(int value, int count)
    {
        uiView.Show();

        slider.maxValue = count;
        slider.value = value;

        int lenght = 290;
        lenght -= 4 * count;

        //if (count == 10)
        //    lenght = 250;
        //else if(count == 2)
        //{
        //    lenght = 290;
        //}
        //else if(count > 2 && count <= 5)
        //{
        //    lenght = 280;
        //}
        //else if(count > 5 && count < 8)
        //{
        //    lenght = 270;
        //}
        //else
        //{
        //    lenght = 260;
        //}

        int space = lenght / count;

        group.spacing = space;
        group.padding.left = space;
        group.padding.right = space;


        for(int i = 1; i <= 9; i++)
        {
            if(i < count)
            {
                lineObjects[i - 1].SetActive(true);
            }
            else
            {
                lineObjects[i - 1].SetActive(false);
            }
        }

        StartCoroutine(HideView());
    }

    private IEnumerator HideView()
    {
        yield return new WaitForSeconds(3f);

        uiView.Hide();
    }

    public void Hide()
    {
        StopAllCoroutines();
        uiView.Hide();
    }

}
