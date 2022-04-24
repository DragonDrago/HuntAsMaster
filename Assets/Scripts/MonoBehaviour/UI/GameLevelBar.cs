using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using I2.Loc;

public class GameLevelBar : MonoBehaviour, ILevelState
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Text currentLevelText;

    [SerializeField]
    private Text nextLevelText;

    [SerializeField]
    private Text enemtCountText;

    [SerializeField]
    private GameObject currentLevelObject;

    [SerializeField]
    private GameObject currentBossLevelObject;

    [SerializeField]
    private GameObject bosLevelObject;

    [SerializeField]
    private GameObject nextLevelObject;

    [SerializeField]
    private LocalizationParamsManager scoreParamText;

    [SerializeField]
    private LocalizationParamsManager bestScoreParamText;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text bestScoreText;


    [SerializeField]
    private bool isGame;

    private void Start()
    {
        if(isGame)
            GameManager.Instance.AddLevelState(this);
    }

    public void Show()
    {

        gameObject.SetActive(true);

        transform.DOScale(Vector3.one, 1f).SetEase(Ease.InOutSine);

        if((Constants.currentLevel + 1) % 5 == 0 && Constants.currentLevel >= 8)
        {
            currentBossLevelObject.SetActive(true);
            currentLevelObject.SetActive(false);
        }
        else
        {
            currentBossLevelObject.SetActive(false);
            currentLevelObject.SetActive(true);
        }

        if((Constants.currentLevel + 2) % 5 == 0 && Constants.currentLevel >= 8)
        {
            bosLevelObject.SetActive(true);
            nextLevelObject.SetActive(false);
        }
        else
        {
            bosLevelObject.SetActive(false);
            nextLevelObject.SetActive(true);
        }

        SetValue();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetValue()
    {
        Constants.score = 0;
        scoreText.text ="0";

        currentLevelText.text = (Constants.currentLevel + 1).ToString();
        nextLevelText.text = (Constants.currentLevel + 2).ToString();

        slider.maxValue = Constants.enemyCount;

        slider.value = 0;

        enemtCountText.text = "0 / " + Constants.enemyCount;
    }

    public void UpdateEnemyCount(int count)
    {
        slider.DOValue(count, 1f);
        enemtCountText.text = count + " / " + Constants.enemyCount;

    }

    public void SetScore(int score)
    {
        Constants.score += score;

        bestScoreText.text = "+" + score;
        bestScoreText.DOFade(1f, 0.3f);

        bestScoreText.rectTransform.DOAnchorPosY(0f, 1f).OnComplete(() => {

            scoreText.text = Constants.score.ToString();

            bestScoreText.text = "0";
            bestScoreText.DOFade(0f, 0.3f).OnComplete( () => {
                bestScoreText.rectTransform.DOAnchorPosY(-500f, 0.1f);
            });
        });
    }

    public void FailedShow()
    {
        gameObject.SetActive(true);

        slider.maxValue = Constants.enemyCount;
        slider.value = Constants.enemyKillCount;

        currentLevelText.text = (Constants.currentLevel + 1).ToString();
        nextLevelText.text = (Constants.currentLevel + 2).ToString();

        if ((Constants.currentLevel + 1) % 5 == 0 && Constants.currentLevel >= 8)
        {
            currentBossLevelObject.SetActive(true);
            currentLevelObject.SetActive(false);
        }
        else
        {
            currentBossLevelObject.SetActive(false);
            currentLevelObject.SetActive(true);
        }

        if ((Constants.currentLevel + 2) % 5 == 0 && Constants.currentLevel >= 8)
        {
            bosLevelObject.SetActive(true);
            nextLevelObject.SetActive(false);
        }
        else
        {
            bosLevelObject.SetActive(false);
            nextLevelObject.SetActive(true);
        }

        enemtCountText.text = Constants.enemyKillCount + " / " + Constants.enemyCount;

        scoreParamText.SetParameterValue("SCORE", Constants.score.ToString());

        if(Constants.bestScore < Constants.score)
        {
            Constants.bestScore = Constants.score;

            bestScoreParamText.SetParameterValue("SCORE", Constants.score.ToString());
        }
        else
        {
            bestScoreParamText.SetParameterValue("SCORE", Constants.bestScore.ToString());
        }

        
    }

}
