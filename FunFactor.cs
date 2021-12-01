using UnityEngine;

public class FunFactor : MonoBehaviour
{
    public static FunFactor instance;

    public PowerupSpawner powerupSpawner;

    bool initialised;

    [HideInInspector]
    public bool onARoll;
    float noLongerOnARollTimer;
    float onARollTime;
    public float notOnARollTime;
    float assassinDelay = 6f;

    float excessiveGrowthTimer;
    float previousPlayerScale;

    float monstrousGrowthTimer;

    public bool eligibleForRoundBasedBurger;
    float burgerTimer;

    public float helperRock1Cooldown = 4f;
    float helperRock2Cooldown = 0.1f;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (initialised && !RoundOverManager.instance.roundOver)
        {  
            HandleOnARoll();
            HandleExcessiveGrowth();
            HandleMonstrousGrowth();
            HandleCosmicBurger();
            HandleHelperRocks();
            HandleAssassin();
        }
    }

    void HandleOnARoll()
    {
        if (!onARoll)
        {
            notOnARollTime += Time.deltaTime;
        }
        else
        {
            notOnARollTime = 0;
            onARollTime += Time.deltaTime;
        }

        if (Time.time > noLongerOnARollTimer && onARoll)
        {
            onARoll = false;
            //Debug.Log("No longer on a roll");
        }
    }

    void HandleAssassin()
    {
        if (notOnARollTime > assassinDelay && RoundTimer.Instance.roundTime > 20f)
        {
            DangerSpawner.instance.SpawnAssassin();

            if (assassinDelay >= 4f)
                assassinDelay -= 0.5f;

            notOnARollTime = 0;
        }
    }

    void HandleExcessiveGrowth()
    {
        excessiveGrowthTimer -= Time.deltaTime;
        if(excessiveGrowthTimer <= 0)
        {
            if(PlayerController.PlayerScale > previousPlayerScale * 1.5f)
            {
                DangerSpawner.instance.SpawnSplooge();
                AudioManager.instance.GrowShort();
                //Debug.Log("Excessive growth, spawning splooge(nebula).");
            }
            previousPlayerScale = PlayerController.PlayerScale;
            excessiveGrowthTimer = 4f;
        }
    }

    void HandleMonstrousGrowth()
    {
        monstrousGrowthTimer -= Time.deltaTime;
        if (monstrousGrowthTimer <= 0)
        {
            if (PlayerController.PlayerScale > previousPlayerScale * 2f)
            {
                DangerSpawner.instance.SpawnMeteor();
                AudioManager.instance.GrowLong();
                //Debug.Log("Monstrous growth, spawning meteor.");
            }
            previousPlayerScale = PlayerController.PlayerScale;
            monstrousGrowthTimer = 6f;
        }
    }

    void HandleCosmicBurger()
    {
        if (eligibleForRoundBasedBurger)
        {
            burgerTimer -= Time.deltaTime;
            if (PlayerController.PlayerScale * 10 < PlayerPrefs.GetInt("Max size") / 2)
            {
                if(burgerTimer < 0)
                {
                    powerupSpawner.SpawnCosmicBurgerPowerup();
                    PlayerPrefs.SetInt("Rounds without beating max size", 0);
                    eligibleForRoundBasedBurger = false;
                }
            }
        }
    }

    void CheckEligibilityForRoundBasedBurger()
    {
        if (!eligibleForRoundBasedBurger)
        {
            if (PlayerPrefs.GetInt("Rounds without beating max size") > 2)
            {
                eligibleForRoundBasedBurger = true;
                burgerTimer = 11f;
            }
        }
    }

    void HandleHelperRocks()
    {
        if (!onARoll)
        {
            helperRock1Cooldown -= Time.deltaTime;
            if (helperRock1Cooldown <= 0)
            {
                RockSpawning.Instance.SpawnHelperRock();
                helperRock1Cooldown = 4f;
            }
        }

        if (PlayerController.PlayerScale < 1)
        {
            helperRock2Cooldown -= Time.deltaTime;
            if (helperRock2Cooldown <= 0)
            {
                RockSpawning.Instance.SpawnHelperRock();
                helperRock2Cooldown = 3f;
            }
        }
    }

    public void OnARoll()
    {
        if (!onARoll)
        {
            onARoll = true;
        }

        noLongerOnARollTimer = Time.time + 4f;
    }

    public void Init()
    {
        eligibleForRoundBasedBurger = false;
        CheckEligibilityForRoundBasedBurger();

        excessiveGrowthTimer = 6f;
        previousPlayerScale = PlayerPrefs.GetFloat("StartingSize");

        initialised = true;
    }
}
