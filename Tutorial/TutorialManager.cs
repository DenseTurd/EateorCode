using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public Queue<BasePanelData> introPanels;
    public Queue<BasePanelData> gameplayTutorialPanels;

    public bool gameplayTutorialCreated;
    public float playSpeed;

    public bool started;

    public int roundStartRockCount;
    public bool firstRockEaten;
    public float postRockEatenTimer = 1.5f;

    public int roundStartCoinCount;
    public bool firstCoinEaten;
    public float postCoinEatenTimer = 1.5f;

    public bool satiationSpawned;
    public int roundStartSatiationCount;
    public bool satiationCollected;
    public float satiationSplashTimer = 2f;

    public int roundStartBonkCount;
    public bool bonked2Times;
    public float postBonkTimer = 2.5f;

    public bool meteorSpawned;
    public bool meteorDodged;
    public float meteorDodgeTimer = 4f;
    public int roundStartMeteorsHit;

    public GameObject tutorialRocksParent;
    public GameObject tutorialCoinsParent;
    public GameObject tutorialBonksParent;

    public GameObject tutorialMeteor;

    public TutorialStages currentStage;
    public bool failTimerActive;
    public float failTimer = 7f;
    Dictionary<TutorialStages, ITutorialStage> stagesDictionary = new Dictionary<TutorialStages, ITutorialStage>
    {
        {TutorialStages.Start, new StartStage() },
        {TutorialStages.EatRocks, new EatRockStage() },
        {TutorialStages.EatCoins, new EatCoinStage() },
        {TutorialStages.Powerup, new PowerupStage() },
        {TutorialStages.BonkRocks, new BonkStage() },
        {TutorialStages.DodgeMeteor, new DodgeStage() }
    };

    public static TutorialManager Instance { get; private set; }
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        introPanels = new Queue<BasePanelData>();
        introPanels.Enqueue(new ChoicePanelData 
        { 
            Title = Strings.WelcomeTitle, 
            Body = "Would you like to play the tutorial?", 
            Yes = delegate() 
            { 
                Extensions.PlayerPrefsSetBool("Tutorial complete", false);  
                UiController.instance.Play(); 
            }, 
            No = delegate() 
            { 
                Extensions.PlayerPrefsSetBool("Tutorial complete", true); 
            } 
        }) ;
    }

    public void DestroyTutorialElements()
    {
        Destroy(tutorialBonksParent);
        Destroy(tutorialCoinsParent);
        Destroy(tutorialRocksParent);
        Destroy(this);
    }

    public void Intro()
    {
        SplashManager.instance.QueuePanels(introPanels);
        SplashManager.instance.DisplayNextGeneralPanel();
        Debug.Log("Tutorial manager intro.");
    }

    public void GamePlayTutorial()
    {
        if (!gameplayTutorialCreated)
        {
            gameplayTutorialPanels = new Queue<BasePanelData>();
            gameplayTutorialPanels.Enqueue(new TextPanelData { Title = Strings.TutorialTitle, Body = Strings.TutorialBody1 });
            currentStage = TutorialStages.Start;
            gameplayTutorialCreated = true;
            PlayerController.instance.rb.isKinematic = true;
            tutorialRocksParent.GetComponent<TutorialObjectsParent>().ResetObjects();

            Debug.Log("Tutorial manager Gameplay tutorial");
        }
    }

    void Update()
    {
        if (gameplayTutorialCreated)
        {
            ResetPlayerThings();
            stagesDictionary[currentStage].Process(this);
        }

        if (failTimerActive)
        {
            failTimer -= Time.deltaTime;
            if (failTimer <= 0)
            {
                stagesDictionary[currentStage].Reset(this);
                failTimerActive = false;
            }
        }
    }

    public void ResetPlayerThings()
    {
        PlayerController.instance.nextTurdTime = Time.time + 6f;
        PlayerController.instance.hunger = 0;
        PlayerController.instance.desiredScale = 1;

        if(gameplayTutorialCreated && PlayerHealth.Instance.GetHealth() < PlayerPrefs.GetInt("Max hp"))
        {
            PlayerHealth.Instance.Heal(PlayerPrefs.GetInt("Max hp") - PlayerHealth.Instance.GetHealth());
        }

        FunFactor.instance.helperRock1Cooldown = 100;
        FunFactor.instance.notOnARollTime = 0;
        FunFactor.instance.eligibleForRoundBasedBurger = false;

        DangerSpawner.instance.meteorAvaliableTime = Time.time + 100;
        DangerSpawner.instance.sploogeAvaliableTime = Time.time + 100;
        DangerSpawner.instance.meteorAvaliable = false;
        DangerSpawner.instance.sploogeAvaliable = false;

        PowerupSpawner.Instance.gravityPowerupAvaliableTime = Time.time + 100;
        PowerupSpawner.Instance.slowmoDelay = Time.time + 100;
        PowerupSpawner.Instance.satiationPowerupAvaliableTime = Time.time + 100;

        PlayAnalysis.Instance.initialised = false;
    }
}
