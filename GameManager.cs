using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static bool play = false;

    public SplashManager splashManager;
    public CoinSpawner coinSpawner;
    public RockSpawning rockSpawning;
    public PowerupSpawner powerupSpawner;
    public PlanetSpawner planetSpawner;
    public StarSpawner starSpawner;

    public bool initialise;
    public bool initialised;
    bool startedSpawning;
    public bool doneSpawning;

    public static GameManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        play = false;
        initialise = false;
        initialised = false;

        if(PlayerPrefs.GetInt("Monetising Initialised") == 0)
        {
            SetDefaultMoneyThings();
            PlayerPrefs.SetInt("Monetising Initialised", 1);
        }

        if (PlayerPrefs.GetInt("HasRunOnce") == 0) // Check if first time app has been opened and set defaults
        {
            SetDefaultValues();
            SplashManager.instance.GetComponent<TutorialManager>().Intro(); // get tutorial up
        }

        if(Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }
    }

    public void StartRound()
    {
        initialise = true;
    }

    void Update()
    {
        if (initialise)
        {
            if (!initialised && PlayerController.instance !=null )
                Init();
        }
        else
        {
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        //Debugging();
    }

    void Debugging()
    {
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            GiveCoin();
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            ControlSystem.instance.autoteor = !ControlSystem.instance.autoteor;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if(Time.timeScale == 1)
            {
                Time.timeScale = 0.5f;
            }
            else
            {
                Time.timeScale = 1;
            }

        }
    }

    public void GiveCoin()//Remove me
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1);
        UiController.instance.UpdateCoinsText();
        Debug.Log("Added 1 coin");
        AudioManager.instance.Coin();
    }

    void Init()
    {
        SortAutoteor();

        if (!Extensions.PlayerPrefsGetBool("Tutorial complete"))
        {
            TutorialManager.Instance.GamePlayTutorial();

            if (!SplashManager.instance.panelsClosedQueueEmpty)
                return;
        }
        else
        {
            TutorialManager.Instance.DestroyTutorialElements();

            if (!startedSpawning) // spawing happens like a relay each spawner tells the next to start once its finished
            {
                startedSpawning = true;

                starSpawner.SpawnStars();
            }

            if (doneSpawning)
            {
                splashManager.DisplayReadyGo();
            }

            if (!splashManager.readyGoDisplayed)
            {
                return;
            }
        }

        initialised = true;
        UpgradeManager.instance.hasUpgradedViaRewardAdThisRound = false;
        PlayerController.instance.Init();
        PlayerHealth.Instance.Init();
        TurdPool.Instance.StartingPool(2);
        powerupSpawner.Init();

        CameraShake.instance.SetOriginalCameraPosition();
        FunFactor.instance.Init();
        HUDController.instance.Init();

        PlayerPrefs.SetInt("RoundCounter", PlayerPrefs.GetInt("RoundCounter") + 1);

        if (PlayerPrefs.GetInt("HasRunOnce") == 0)
        {
            PlayerPrefs.SetInt("HasRunOnce", 1);

            AchievementManager.instance.ThatlLearnYa();
        }
        RoundTimer.Instance.StartTimer();
        PlayAnalysis.Instance.Init();
        play = true;

        AudioManager.instance.Click();
    }

    void SortAutoteor()
    {
        if (AutoRocksParent.Instance != null)
        {
            if (!ControlSystem.instance.autoteor)
            {
                Destroy(Autoteor.Instance);
            }
            PlayerController.instance.transform.position = new Vector3(0, 0, 0);
            AutoRocksParent.Instance.DestroyAutoRocks();
        }
    }

    public void DebuggingReset()
    {
        PlayerPrefs.SetInt("Monetising Initialised", 0);
        SetDefaultMoneyThings();
        SetDefaultValues();
        ResetTutorial();
    }

    public void SetDefaultValues()
    {
        // HasRunOnce gets set to 1 in Init() after playerController can use it to give a first round bonus.
        PlayerPrefs.SetInt("HasRunOnce", 0);

        PlayerPrefs.SetInt("RoundCounter", 0);

        PlayerPrefs.SetInt("Max hp", 200);

        PlayerPrefs.SetFloat("HighScore", 0);

        AchievementManager.instance.ResetAchievements();
        StatManager.instance.ResetStats();

        PlayerPrefs.SetInt("CharIndex", 0);

        PlayerPrefs.SetInt("Character change opened", 0);

        Settings.instance.DefaultSettings();

        UiController.instance.UpdateCoinsText();

        PlayerPrefs.SetInt("UnusedUpgradeCounter", 0);
        PlayerPrefs.SetInt("RoundsSinceLastUpgradeReminder", 3);

        PlayAnalysis.Instance.ResetPlayAnalysis();
        ComedySplashPanels.Instance.ResetComedy();

        Extensions.PlayerPrefsSetBool("Store opened", false);
        Extensions.PlayerPrefsSetBool("ReviewTriggered", false);

        Debug.Log("Default values set");
    }

    public void SetDefaultMoneyThings()
    {
        PlayerPrefs.SetInt("Coins", 10);
        UpgradeManager.instance.ResetUpgrades();
        CharacterUnlocker.Instance.ResetCharacters();
        PlayerPrefs.SetInt("noads", 0);
        PlayerPrefs.SetInt("Purchase made", 0);
        PlayerPrefs.SetInt("Donated small", 0);
        PlayerPrefs.SetInt("Donated medium", 0);
        PlayerPrefs.SetInt("Donated large", 0);

        UiController.instance.UpdateCoinsText();

        Debug.Log("Default money things set");
    }

    void ResetTutorial()
    {
        PlayerPrefs.SetInt("Tutorial complete", 0);
    }
}
