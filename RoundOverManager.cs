using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class RoundOverManager : MonoBehaviour
{
    public TMP_Text congratualtionText;
    public TMP_Text failConditionText;
    public TMP_Text scoreValText;
    public TMP_Text maxSizeText;
    public TMP_Text coinsCountedText;
    public TMP_Text timeText;
    public bool roundOver;
    bool roundOverInitialised;

    public GameObject roundOverPanel;
    public Button continueButton;

    string _failCondition;

    bool scoreCounted;
    bool maxSizeCounted;
    bool coinsCounted;
    bool timeCounted;
    bool roundTimeSet;

    float scoreTemp;
    float maxSizeTemp;
    float coinsCountedTemp;
    float timeTemp;

    RoundOverPrize roundOverPrize = new RoundOverPrize();
    Dictionary<Prize, PrizeStruct> prizeDict = new Dictionary<Prize, PrizeStruct>
    {
        {Prize.Constellation, new PrizeStruct{Title = "Nice try!", PrizeName = "Constellation", Value = 3}},
        {Prize.Cosmic, new PrizeStruct{Title = "Great job!", PrizeName = "Cosmic", Value = 10}},
        {Prize.Galactic, new PrizeStruct{Title = "Incredible!", PrizeName = "Galactic", Value = 50}}
    };
    Prize prize;
    bool prizeSorted;
    bool prizePanelCreated;

    public static RoundOverManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        scoreCounted = false;
        maxSizeCounted = false;
        coinsCounted = false;

        scoreTemp = 0;
        maxSizeTemp = 0;
        coinsCountedTemp = 0;
    }

    public void Continue()
    {
        if (PlayerPrefs.GetInt("RoundCounter") > 2)
        {
            PlayerPrefs.SetInt("RoundCounter", 0);
            AdManager.instance.ShowRoundOverAd();
        }

        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + prizeDict[prize].Value);
        PlayerController.instance = null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name.ToString());
    }

    public void RoundOver(string failCondition)
    {
        if (!roundOverInitialised)
        {
            _failCondition = failCondition;

            PlayAnalysis.Instance.CalculateTheRoundsAverageRotationRate();

            SortRoundOverPanel();

            AchievementManager.instance.TotalRocksEatenAchievements();

            HUDController.instance.ClearHUD();

            HUDAlarms.Instance.StopAllAlarms();

            AudioManager.instance.StopGameplayMusic();
            AudioManager.instance.RoundOver();

            roundOverInitialised = true;

            if(PlayerPrefs.GetInt("Rounds played") > 5 && !Extensions.PlayerPrefsGetBool("ReviewTriggered"))
            {
                ReviewHandling.Instance.TriggerReview();
                Extensions.PlayerPrefsSetBool("ReviewTriggered", true);
                Debug.Log("Attempt review flow");
            }
        }
    }

    void SortRoundOverPanel()
    {
        SortPrize();

        if (PlayerPrefs.GetInt("Tutorial complete") == 0)
        {
            PlayerPrefs.SetInt("Tutorial complete", 1);
        }

        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }

        roundOverPanel.SetActive(true);

        congratualtionText.text = prizeDict[prize].Title;

        if (_failCondition == "TooSmol")
        {
            failConditionText.text = "Gjame ober! \n" + Chara.Name + " got too smol.";
        }
        if (_failCondition == "TooHungry")
        {
            failConditionText.text = "Gjame ober! \n" + Chara.Name + " is too hungry.";
        }
        if (_failCondition == "HealthDrained")
        {
            failConditionText.text = "Gjame ober! \n" + Chara.Name + " is out of hit points.";
        }
        roundOver = true;

        HUDController.instance.ClearHUD();

        PlayerPrefs.SetInt("Rounds played", PlayerPrefs.GetInt("Rounds played") + 1);
    }

    void SortPrize()
    {
        if (!prizeSorted)
        {
            prize = roundOverPrize.CalculatePrize();
            prizeSorted = true;
        }
    }

    void CreatePrizePanel()
    {
        SplashManager.instance.CreatePanel
            (
                new PrizePanelData
                {
                    Body = "You get a " + prizeDict[prize].PrizeName + " prize:\n" + prizeDict[prize].Value.ToString() + " Coins"
                }
            );

        AudioManager.instance.PrizeAudio(prize);
    }

    void Update()
    {
        if (roundOver)
        {
            ScoreManager.instance.SetHighScore();

            SetScoreText();

            SetMaxSizeText();

            SetCoinsCollectedText();

            SetTimeText();

            if (timeCounted)
            {
                if (!prizePanelCreated)
                {
                    CreatePrizePanel();
                    AudioManager.instance.Coin();
                    prizePanelCreated = true;
                }

            }

            if (!roundTimeSet)
            {
                RoundTimer.Instance.EndRound();
                roundTimeSet = true;
            }
        }
    }

    void SetScoreText()
    {
        if (!scoreCounted && scoreTemp < ScoreManager.instance.score * 0.9f)
        {
            scoreTemp += ScoreManager.instance.score * Time.deltaTime * 1.3f;
            scoreValText.text = " " + scoreTemp.ToString("F0");
        }
        else if (!scoreCounted && scoreTemp >= ScoreManager.instance.score * 0.9f)
        {
            scoreTemp = ScoreManager.instance.score;
            scoreValText.text = " " + scoreTemp.ToString("F0");
            scoreCounted = true;
        }
    }

    void SetMaxSizeText()
    {
        if (!maxSizeCounted && scoreCounted && maxSizeTemp < PlayerController.instance.maxSize * 9)
        {
            maxSizeTemp += PlayerController.instance.maxSize * 10 * Time.deltaTime * 1.3f;
            maxSizeText.text = " " + maxSizeTemp.ToString("F0");
        }
        else if (!maxSizeCounted && scoreCounted && maxSizeTemp >= PlayerController.instance.maxSize * 9)
        {
            maxSizeTemp = PlayerController.instance.maxSize * 10;
            maxSizeText.text = " " + maxSizeTemp.ToString("F0");
            maxSizeCounted = true;
        }
    }

    void SetCoinsCollectedText()
    {
        if (!coinsCounted && maxSizeCounted && coinsCountedTemp < PlayerController.instance.roundCoins * 0.9f)
        {
            coinsCountedTemp += PlayerController.instance.roundCoins * Time.deltaTime * 1.3f;
            coinsCountedText.text = " " + coinsCountedTemp.ToString("F0");
        }
        else if (!coinsCounted && maxSizeCounted && maxSizeTemp >= PlayerController.instance.maxSize * 0.9f)
        {
            coinsCountedTemp = PlayerController.instance.roundCoins;
            coinsCountedText.text = " "  + coinsCountedTemp.ToString("F0");
            coinsCounted = true;
        }
    }

    void SetTimeText()
    {
        if (!timeCounted && coinsCounted && timeTemp < RoundTimer.Instance.roundTime * 0.9f)
        {
            timeTemp += RoundTimer.Instance.roundTime * Time.deltaTime * 2f;
            timeText.text = " " + timeTemp.ToString("F0");
        }
        else if (!timeCounted && coinsCounted && timeTemp >= RoundTimer.Instance.roundTime * 0.9f)
        {
            timeTemp = RoundTimer.Instance.roundTime;
            timeText.text = " " + timeTemp.ToString("F0");
            timeCounted = true;
        }
    }
}
