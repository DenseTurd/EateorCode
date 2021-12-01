using UnityEngine;
using UnityEngine.Advertisements;

public enum ToBeUpgraded
{
    disintegrate,
    startingSize,
    satiation,
}

public enum UpGradeMethod
{
    coins,
    rewardAd
}

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;
    public ToBeUpgraded toBeUpgraded;

    public bool disintegrateCanRewardAdUpgrade = false;
    public bool startingSizeCanRewardAdUpgrade = false;
    public bool satiationCanRewardAdUpgrade = false;
    public bool disintegrateAvaliable;
    public bool satiationAvaliable;
    public bool startingSizeAvaliable;

    public bool hasUpgradedViaRewardAdThisRound = false;

    float eligibilityCheckTimer = 0.05f;

    public int CombinedUpgradeCost { get; private set; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        CheckIfAnyUpgradesAvaliable();
        CalculateCombinedUpgradeCost();
    }

    void CheckIfAnyUpgradesAvaliable()
    {
         disintegrateAvaliable = false;
         satiationAvaliable = false;
         startingSizeAvaliable = false;
        if (PlayerPrefs.GetInt("Coins") >= PlayerPrefs.GetInt("DisintegrateUpgradeCost"))
        {
            disintegrateAvaliable = true;
        }
        if (PlayerPrefs.GetInt("Coins") >= PlayerPrefs.GetInt("SatiationUpgradeCost"))
        {
            satiationAvaliable = true;
        }
        if (PlayerPrefs.GetInt("Coins") >= PlayerPrefs.GetInt("StartingSizeUpgradeCost"))
        {
            startingSizeAvaliable = true;
        }
    }

    void Update()
    {
        if (GameManager.play)
            Destroy(this);

        eligibilityCheckTimer -= Time.deltaTime;

        if (eligibilityCheckTimer <= 0)
        {
            CheckRewardAdEligibility();
            eligibilityCheckTimer = 0.1f;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log(CanAffordAnUpgrade());
        }
    }

    void CheckRewardAdEligibility()
    {
        // Check to see if elegible for disintegrate reward ad upgrade
        if (Advertisement.IsReady(AdManager.instance.rewardAdID) && PlayerPrefs.GetInt("Coins") >= PlayerPrefs.GetInt("DisintegrateUpgradeCost") * 0.9f && PlayerPrefs.GetInt("Coins") < PlayerPrefs.GetInt("DisintegrateUpgradeCost"))
        {
            disintegrateCanRewardAdUpgrade = true;
        }
        // Check to see if elegible for starting size reward ad upgrade
        if (Advertisement.IsReady(AdManager.instance.rewardAdID) && PlayerPrefs.GetInt("Coins") >= PlayerPrefs.GetInt("StartingSizeUpgradeCost") * 0.9f && PlayerPrefs.GetInt("Coins") < PlayerPrefs.GetInt("StartingSizeUpgradeCost"))
        {
            startingSizeCanRewardAdUpgrade = true;
        }
        // Check to see if elegible for satiation reward ad upgrade
        if (Advertisement.IsReady(AdManager.instance.rewardAdID) && PlayerPrefs.GetInt("Coins") >= PlayerPrefs.GetInt("SatiationUpgradeCost") * 0.9f && PlayerPrefs.GetInt("Coins") < PlayerPrefs.GetInt("SatiationUpgradeCost"))
        {
            startingSizeCanRewardAdUpgrade = true;
        }
    }

    public void AttemptDisintegrateUpgrade()
    {
        if (disintegrateCanRewardAdUpgrade && !hasUpgradedViaRewardAdThisRound)
        {
                toBeUpgraded = ToBeUpgraded.disintegrate;
                AdManager.instance.ShowRewardAd();
        }
        else if (PlayerPrefs.GetInt("Coins") >= PlayerPrefs.GetInt("DisintegrateUpgradeCost"))
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - PlayerPrefs.GetInt("DisintegrateUpgradeCost"));
            DetermineDisintegrationUpgradeMethod(UpGradeMethod.coins);
        }
        else
        {
            Debug.Log("Not enough coins");

            AudioManager.instance.CantAffordUpgrade();
        }
    }

    public void DetermineDisintegrationUpgradeMethod(UpGradeMethod via)
    {
        if (via == UpGradeMethod.rewardAd && !hasUpgradedViaRewardAdThisRound)//Another attempt at trying to stop the multiple upgrade glitch
        {
            hasUpgradedViaRewardAdThisRound = true;
            CalculateUpgradedDisinterationRate();
            Debug.Log("Upgraded disintegration via reward ad");
        }
        if (via == UpGradeMethod.coins)
        {
            CalculateUpgradedDisinterationRate();
            Debug.Log("Upgraded disintegration via coins");
        }
    }

    public void CalculateUpgradedDisinterationRate()
    {
        PlayerPrefs.SetFloat("TurdMass", PlayerPrefs.GetFloat("TurdMass") * 0.95f);
        PlayerPrefs.SetFloat("TurdTime", PlayerPrefs.GetFloat("TurdTime") * 1.05f);
        PlayerPrefs.SetInt("DisintegrateUpgradeCost", Mathf.RoundToInt(PlayerPrefs.GetInt("DisintegrateUpgradeCost") * 1.5f));

        if (PlayerPrefs.GetInt("DisintegrateUpgradeLevel") == 0)
        {
            SplashManager.instance.CreatePanel(new TextPanelData { Title = Strings.CrumbleTitle, Body = Strings.CrumbleBody });
        }

        PlayerPrefs.SetInt("DisintegrateUpgradeLevel", PlayerPrefs.GetInt("DisintegrateUpgradeLevel") + 1);

        Debug.Log("Turd mass reduced to: " + PlayerPrefs.GetFloat("TurdMass"));
        Debug.Log("Time between turds increased to: " + PlayerPrefs.GetFloat("TurdTime"));

        PlayerPrefs.SetInt("UnusedUpgradeCounter", 0);

        CalculateCombinedUpgradeCost();

        AudioManager.instance.UpgradeSuccessful();
    }

    public void AttemptStartingSizeUpgrade()
    {
        if (startingSizeCanRewardAdUpgrade && !hasUpgradedViaRewardAdThisRound)
        {
                toBeUpgraded = ToBeUpgraded.startingSize;
                AdManager.instance.ShowRewardAd();
        }
        else if (PlayerPrefs.GetInt("Coins") >= PlayerPrefs.GetInt("StartingSizeUpgradeCost"))
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - PlayerPrefs.GetInt("StartingSizeUpgradeCost"));
            DetermineStartingSizeUpgradeMethod(UpGradeMethod.coins);
        }
        else
        {
            Debug.Log("Not enough coins");

            AudioManager.instance.CantAffordUpgrade();
        }
    }

    public void DetermineStartingSizeUpgradeMethod(UpGradeMethod via)
    {
        if (via == UpGradeMethod.rewardAd && !hasUpgradedViaRewardAdThisRound)//Another attempt at trying to stop the multiple upgrade glitch
        {
            hasUpgradedViaRewardAdThisRound = true;
            CalculateUpgradedStartingSize();
            Debug.Log("Upgraded starting size via reward ad");
        }
        if (via == UpGradeMethod.coins)
        {
            CalculateUpgradedStartingSize();
            Debug.Log("Upgraded starting size via coins");
        }
    }

    public void CalculateUpgradedStartingSize()
    {
        PlayerPrefs.SetFloat("StartingSize", PlayerPrefs.GetFloat("StartingSize") + 0.3f);
        PlayerPrefs.SetInt("StartingSizeUpgradeCost", Mathf.RoundToInt(PlayerPrefs.GetInt("StartingSizeUpgradeCost") * 1.25f));

        if (PlayerPrefs.GetInt("StartingSizeUpgradeLevel") == 0)
        {
            SplashManager.instance.CreatePanel(new TextPanelData { Title = Strings.StartSizeTitle, Body = Strings.StartSizeBody });

        }

        PlayerPrefs.SetInt("StartingSizeUpgradeLevel", PlayerPrefs.GetInt("StartingSizeUpgradeLevel") + 1);

        PointersControl.Instance.Init();

        Debug.Log("Starting size increased to: " + PlayerPrefs.GetFloat("StartingSize"));

        PlayerPrefs.SetInt("UnusedUpgradeCounter", 0);

        CalculateCombinedUpgradeCost();

        AudioManager.instance.UpgradeSuccessful();
    }

    public void AttemptSatiationUpgrade()
    {
        if (satiationCanRewardAdUpgrade && !hasUpgradedViaRewardAdThisRound)
        {
                toBeUpgraded = ToBeUpgraded.satiation;
                AdManager.instance.ShowRewardAd();
        }
        else if (PlayerPrefs.GetInt("Coins") >= PlayerPrefs.GetInt("SatiationUpgradeCost"))
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - PlayerPrefs.GetInt("SatiationUpgradeCost"));
            DetermineSatiationUpgradeMethod(UpGradeMethod.coins);
        }
        else
        {
            Debug.Log("Not enough coins");
            AudioManager.instance.CantAffordUpgrade();
        }
    }

    public void DetermineSatiationUpgradeMethod(UpGradeMethod via)
    {
        if (via == UpGradeMethod.rewardAd && !hasUpgradedViaRewardAdThisRound)//Another attempt at trying to stop the multiple upgrade glitch
        {
            hasUpgradedViaRewardAdThisRound = true;
            CalculateUpgradedSatiation();
            Debug.Log("Upgraded satiation via reward ad");
        }
        if (via == UpGradeMethod.coins)
        {
            CalculateUpgradedSatiation();
            Debug.Log("Upgraded satiation via coins");
        }
    }

    public void CalculateUpgradedSatiation()
    {
        PlayerPrefs.SetFloat("1MinusAdditionalSatiation", 0.9f * PlayerPrefs.GetFloat("1MinusAdditionalSatiation")); // create diminishing returns for satiation upgrade

        PlayerPrefs.SetFloat("Satiation", PlayerPrefs.GetFloat("DefaultSatiation") + (1 - PlayerPrefs.GetFloat("1MinusAdditionalSatiation")));// try to think of a better name for it

        if(PlayerPrefs.GetInt("Max hp") < 270)
        {
            PlayerPrefs.SetInt("Max hp", PlayerPrefs.GetInt("Max hp") + 1);
        }

        PlayerPrefs.SetInt("SatiationUpgradeCost", Mathf.RoundToInt(PlayerPrefs.GetInt("SatiationUpgradeCost") * 1.33f));


        if (PlayerPrefs.GetInt("SatiationUpgradeLevel") == 0)
        {
            SplashManager.instance.CreatePanel(new TextPanelData { Title = Strings.ApetiteTitle, Body = Strings.ApetiteBody });
        }
        PlayerPrefs.SetInt("SatiationUpgradeLevel", PlayerPrefs.GetInt("SatiationUpgradeLevel") + 1);

        Debug.Log("Max health increased to : " + PlayerPrefs.GetInt("Max hp"));
        Debug.Log("Satiation intcreased to : " + PlayerPrefs.GetFloat("Satiation"));

        PlayerPrefs.SetInt("UnusedUpgradeCounter", 0);

        CalculateCombinedUpgradeCost();

        AudioManager.instance.UpgradeSuccessful();
    }

    public void IncrementUnusedUpgradeCounter()
    {
        if (disintegrateAvaliable || satiationAvaliable || startingSizeAvaliable)
        {
            PlayerPrefs.SetInt("UnusedUpgradeCounter", PlayerPrefs.GetInt("UnusedUpgradeCounter") + 1);
        }
        else
        {
            PlayerPrefs.SetInt("UnusedUpgradeCounter", 0);
        }
    }

    void CalculateCombinedUpgradeCost()
    {
        CombinedUpgradeCost =
            PlayerPrefs.GetInt("DisintegrateUpgradeCost") +
            PlayerPrefs.GetInt("StartingSizeUpgradeCost") +
            PlayerPrefs.GetInt("SatiationUpgradeCost");

    }

    public bool CanAffordAnUpgrade()
    {
        CheckIfAnyUpgradesAvaliable();
        return disintegrateAvaliable || satiationAvaliable || startingSizeAvaliable ? true : false;        
    }

    public void ResetUpgrades()
    {
        PlayerPrefs.SetFloat("TurdMass", 0.1f);
        PlayerPrefs.SetFloat("TurdTime", 1.5f);
        PlayerPrefs.SetInt("DisintegrateUpgradeCost", 20);
        PlayerPrefs.SetInt("DisintegrateUpgradeLevel", 0);

        PlayerPrefs.SetFloat("StartingSize", 1f);
        PlayerPrefs.SetInt("StartingSizeUpgradeCost", 15);
        PlayerPrefs.SetInt("StartingSizeUpgradeLevel", 0);

        PlayerPrefs.SetFloat("DefaultSatiation", 0.7f);
        PlayerPrefs.SetFloat("Satiation", PlayerPrefs.GetFloat("DefaultSatiation"));
        PlayerPrefs.SetFloat("1MinusAdditionalSatiation", 1);
        PlayerPrefs.SetInt("SatiationUpgradeCost", 22);
        PlayerPrefs.SetInt("SatiationUpgradeLevel", 0);
    }
}
