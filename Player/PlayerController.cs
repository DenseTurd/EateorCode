using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Migration 
{
    //Variables for motion
    public float handling = 17f;
    public float force;
    public float regainControlTimer;
    public Rigidbody rb;

    public float maxSpeed = 5f;
    Vector3 velocity;

    //Variables for turding
    public TurdSystem turdsystem;
    public float firstTurdDelay = 3f;
    public float nextTurdTime;
    public float additionalDisintegration; // increase disintegration with time, less restrictive as starting size is upgraded
    public float roundStartTime;
    public float softSizeRestriction; // increase disintegration rate as player gets bigger, consider reducing with disintegration upgrade
    float turdMass;
    float turdTime;

    //Variables for spawning tings and setting camera
    public float camZOffset;
    public float distanceToScreenEdge;

    //Singleton
    public static PlayerController instance;

    //Variables for adjusting player size
    float previousScale;
    public float desiredScale;
    float interpolationValue;
    bool firstLerp;
    public static float PlayerScale;

    //Variables for calculating size of rock respawns
    List<float> recentScales;
    public float meanScale;

    //Timer for reducing spawn sizes if no eating occours in a while
    float lastEatTime;
    public float autoReduceSpawnSizesDelay = 3f;

    // Hunger tings
    public float hunger;
    public float satiation;

    // Powerups
    public bool gravity;
    public int gravityCharges;

    // Score tings
    public float maxSize;
    public int roundCoins;

    PlayerInput input;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
        Debug.unityLogger.logEnabled = false;
#endif
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        nextTurdTime = Time.time + firstTurdDelay;

        interpolationValue = 0;

        lastEatTime = Time.time + autoReduceSpawnSizesDelay;

        hunger = 0;
        satiation = 0.7f;

        recentScales = new List<float>();

        input = new PlayerInput();
    }

    public void Init()
    {
        FirstRoundBoost();

        PlayerScale = transform.localScale.x;
        previousScale = transform.localScale.x;
        desiredScale = PlayerPrefs.GetFloat("StartingSize");
        firstLerp = true;

        recentScales = new List<float>();
        recentScales.Add(PlayerPrefs.GetFloat("StartingSize"));
        meanScale = PlayerPrefs.GetFloat("StartingSize");

        satiation = PlayerPrefs.GetFloat("Satiation");

        roundStartTime = Time.time;

        lastEatTime = Time.time + autoReduceSpawnSizesDelay;

        maxSize = transform.localScale.x;

        input.Init(this);

        turdMass = PlayerPrefs.GetFloat("TurdMass");
        turdTime = PlayerPrefs.GetFloat("TurdTime");
    }

    void Update()
    {
        // Be kinematic for intro sequence or tutorial intro or tutorial satiation bit
        if (GameManager.instance.initialise && !GameManager.instance.initialised)
        {
            rb.isKinematic = true;
            return;
        }
        else if(TutorialManager.Instance.started && !SplashManager.instance.panelsClosedQueueEmpty)
        {
            rb.isKinematic = true;
            return;
        }
        else if(TutorialManager.Instance.satiationSpawned && !TutorialManager.Instance.satiationCollected)
        {
            rb.isKinematic = true;
            return;
        }
        else
        {
            rb.isKinematic = false;
        }
            

        PlayerScale = transform.localScale.x;
        camZOffset = Vector3.Distance(transform.position, Camera.main.transform.position);
        distanceToScreenEdge = Vector3.Distance(transform.position, Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0, camZOffset)));

        if (!RoundOverManager.instance.roundOver)
        {
            HandleForce();
        }

        HandleSpeed();

        if (GameManager.play)
        {
            
            if (ControlSystem.instance.pcControls)  // for whites controls
            {
                input.HandleMouse();
            }
            else
            {
                input.HandleSwipe();
            }

            //input.HandleDebugging(); // << Take out before release

            Turd();
            LerpScale();
            AutoReduceSpawnSizes();
            CheckLoseCondition(); 
        }

        Migrate();
        ClampHunger();
        SetMaxSize();
    }

    void HandleForce()
    {
        force = (transform.localScale.z * handling) - velocity.magnitude;         // Adjust force, increase acceleration at lower speed
        force = Mathf.Max(force, 7);                                       // minimum force of 7
        rb.AddForce(transform.up * (-force));                              // Add force
    
        if(regainControlTimer > 0)
        {
            regainControlTimer -= Time.deltaTime;
        }
        else if (handling < 17)
        {
            handling = 17;
        }
    }

    void HandleSpeed()
    {
        velocity = rb.velocity;
        maxSpeed = (transform.localScale.x * 5) + 2;                 // Adjust speed cap, Small linear offset to give a small player a boost
        if (rb.velocity.magnitude > maxSpeed)                  // Limit Speed
            rb.velocity = velocity.normalized * maxSpeed;
    }

    void LerpScale() //Smooth out transition when scaling
    {
        if (firstLerp) // Dont limit size change for first lerp
        {
            if (previousScale != desiredScale)
            {
                float scale = Mathf.Lerp(previousScale, desiredScale, interpolationValue);
                interpolationValue += Time.deltaTime;

                transform.localScale = new Vector3(scale, scale, scale);
                HUDController.instance.UpdateSizeText();
            }
        }
        else if (previousScale != desiredScale)   // Limits the maximum size change per lerp to 1.5 times previous scale
        {
            float scale = Mathf.Lerp(previousScale, Mathf.Min(desiredScale, previousScale*1.5f), interpolationValue);
            interpolationValue += Time.deltaTime;

            transform.localScale = new Vector3(scale, scale, scale);
            HUDController.instance.UpdateSizeText();
        }

        if(interpolationValue >= 1)
        {
            previousScale = desiredScale;
            interpolationValue = 0f;

            if (firstLerp)
            {
                firstLerp = false;
            }
        }

    }

    void AutoReduceSpawnSizes()
    {
        if (Time.time > lastEatTime + autoReduceSpawnSizesDelay)
        {
            CalculateMeanScale(transform.localScale.x / 3);
            lastEatTime = Time.time;
            //Debug.Log("Auto reduced spawn sizes due to no eating in a while.");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BaseRockController>() != null)
        {

            if (other.transform.localScale.x > transform.localScale.x * 0.9f)
            {
                BounceOffBiggerRocks(other);
            }
            else
            {
                EatSmallerRocks(other);
            }

            if (gravity)
            {
                gravityCharges--;
                if (gravityCharges <= 0)
                {
                    gravity = false;
                }
            }
        }
        if(other.GetComponent<MeteorController>() != null)
        {
            BeDamagedByMeteor(other);
        }
        if(other.GetComponent<SploogeController>() != null)
        {
            GetSplooged(other);
        }
        if (other.GetComponent<CoinController>() != null)
        {
            CollectCoins(other);
        }
        if (other.CompareTag("Powerup"))
        {
            other.GetComponent<Ipowerup>().Powerup();
            FunFactor.instance.onARoll = true;
            CameraShake.Shake(0.08f, 0.08f);
            AchievementManager.instance.HandleXRaySpecs();
        }
        if (other.GetComponent<TurdController>() != null)
        {
            if (other.GetComponent<TurdController>().canBeEaten)
                EatTurd(other);
        }
    }

    void BounceOffBiggerRocks(Collider other)
    {
        var speed = velocity.magnitude;
        var direction = Vector3.Reflect(velocity.normalized, other.transform.position - transform.position);

        rb.velocity = direction.normalized * Mathf.Min(maxSpeed, speed); // trying to stop the warp on collision

        handling = 4f;
        regainControlTimer = 0.3f;

        BaseRockController rockController = other.GetComponent<BaseRockController>();
        rockController.direction = -direction.normalized; // give the collided with rock motion
        rockController.hasMotionSpeed = Mathf.Min(maxSpeed, speed);
        rockController.hasMotion = true;
        rockController.Bonk();

        AudioManager.instance.Bonk();

        if (GameManager.play)
        {
            CalculateMeanScale(transform.localScale.x / 3);

            ScoreManager.instance.AddScore(3);
            ScoreManager.instance.bonkCount += 1;
            PlayerPrefs.SetInt("Times bonked", PlayerPrefs.GetInt("Times bonked") + 1);

            CameraShake.Shake(0.2f, 0.2f);
        }
    }

    void EatSmallerRocks(Collider other)
    {

        BaseRockController rockController = other.GetComponent<BaseRockController>();
        rockController.Splode();

        if (GameManager.play)
        {
            PlayerHealth.Instance.Heal(1);

            previousScale = transform.localScale.x;
            desiredScale += transform.localScale.x * (0.25f * (other.transform.localScale.x / transform.localScale.x));
            interpolationValue = 0;

            CalculateMeanScale(other.transform.localScale.x);

            AchievementManager.instance.SortPlanetAchievements(rockController);

            lastEatTime = Time.time;

            ScoreManager.instance.AddScore(UnityEngine.Random.Range(8, 13) + (int)RoundTimer.Instance.roundTime / 60, other.transform.position);

            PlayerPrefs.SetInt("Rocks eaten", PlayerPrefs.GetInt("Rocks eaten") + 1);

            hunger -= 0.4f;
            HUDController.instance.UpdateHungerBar();

            CameraShake.Shake(0.1f, 0.15f);
        }
    }

    public void EatCosmicBurger()
    {
        PlayerHealth.Instance.Heal(200);

        previousScale = transform.localScale.x;
        desiredScale += transform.localScale.x * 2;
        interpolationValue = 0;

        CalculateMeanScale(transform.localScale.x * 1.8f);

        lastEatTime = Time.time + 3f;

        hunger -= 1f;
        HUDController.instance.UpdateHungerBar();
    }

    void BeDamagedByMeteor(Collider other)
    {
        PlayerHealth.Instance.TakeDamage(67);

        MeteorController meteorController = other.GetComponent<MeteorController>();

        previousScale = transform.localScale.x;
        desiredScale -= transform.localScale.x * 0.5f;
        interpolationValue = 0;

        if (meteorController.assassin)
        {
            hunger += 0.3f;
            HUDController.instance.UpdateHungerBar();
        }

        CalculateMeanScale(other.transform.localScale.x);

        ScoreManager.instance.AddScore(UnityEngine.Random.Range(-7, -16), other.transform.position);

        PlayerPrefs.SetInt("Times Hit By Meteor", PlayerPrefs.GetInt("Times Hit By Meteor") + 1);

        AchievementManager.instance.MeteorHit();

        //Debug.Log("Meteor hit");

        meteorController.Splode();

        CameraShake.Shake(0.4f, 0.5f);
    }

    void GetSplooged(Collider other)
    {
        SploogeController sploogeController = other.GetComponent<SploogeController>();

        ScoreManager.instance.AddScore(UnityEngine.Random.Range(-6, -11), other.transform.position);

        PlayerPrefs.SetInt("Times Hit By Nebula", PlayerPrefs.GetInt("Times Hit By Nebula") + 1);

        //Debug.Log("Splooge hit");

        sploogeController.SploogeScreen();

        CameraShake.Shake(0.35f, 0.4f);
    }

    void CollectCoins(Collider other)
    {
        if (!RoundOverManager.instance.roundOver)
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1);
            other.GetComponent<CoinController>().Pop();
            UiController.instance.UpdateCoinsText();
            roundCoins++;

            ScoreManager.instance.AddScore(UnityEngine.Random.Range(12, 17), other.transform.position);

            AudioManager.instance.Coin();

            CameraShake.Shake(0.04f, 0.08f);
        }
    }

    void EatTurd(Collider other)
    {
        PlayerHealth.Instance.Heal(1);
        other.GetComponent<TurdController>().DeSpawnCloud();

        AudioManager.instance.Crunch();
        //Debug.Log("ate turd");
    }

    public void CalculateMeanScale(float otherScale)
    {
        if (recentScales.Count > 3)
        {
            float mean = 0;
            for (int i = 0; i < recentScales.Count; i++)
            {
                mean += recentScales[i];
            }
            mean = mean / recentScales.Count;

            recentScales.Clear();
            recentScales.Add(mean);
            recentScales.Add(otherScale);
            recentScales.Add(transform.localScale.x);
        }
        else
        {
            recentScales.Add(otherScale);
            recentScales.Add(transform.localScale.x);
        }
        float tempMean = 0f;
        for (int i = 0; i < recentScales.Count; i++)
        {
            tempMean += recentScales[i];
        }
        meanScale = tempMean / recentScales.Count;
    }

    public void Turd()
    {
        if (Time.time > nextTurdTime)
        {
            turdsystem.Turd();

            previousScale = transform.localScale.x;

            // increase disintegration with time, less restrictive as starting size is upgraded
            additionalDisintegration = (Time.time - roundStartTime) / (PlayerPrefs.GetFloat("StartingSize") * 500);

            float ReduceSoftSizeRestrictionWithDisintegrateUpgrade = turdTime * 1800;

            // increase disintegration rate as player gets bigger, consider reducing with disintegration upgrade
            softSizeRestriction = (transform.localScale.x / (800 + ReduceSoftSizeRestrictionWithDisintegrateUpgrade)); 

            desiredScale -= transform.localScale.x * (turdMass + additionalDisintegration + softSizeRestriction);
            interpolationValue = 0;
            nextTurdTime = Time.time + turdTime;

            float additionalHungerAsRoundProgresses = RoundTimer.Instance.roundTime / 2000;
            hunger += 0.1f + additionalHungerAsRoundProgresses;

            HUDController.instance.UpdateHungerBar();

            AudioManager.instance.Turd();
        }
    }

    void CheckLoseCondition()
    {
        if (transform.localScale.x < 0.4f )
        {
            StatManager.instance.SetMaxSize();
            RoundOverManager.instance.RoundOver("TooSmol");
            GameManager.play = false;
        }
        if (hunger >= satiation)
        {
            StatManager.instance.SetMaxSize();
            RoundOverManager.instance.RoundOver("TooHungry");
            GameManager.play = false;
        }
    }

    void ClampHunger()
    {
        if (hunger < 0f)  
        {
            hunger = 0f;
        }
    }

    void SetMaxSize()
    {
        if (transform.localScale.x > maxSize)
        {
            maxSize = transform.localScale.x;
        }
    }

    void FirstRoundBoost()
    {
        if (PlayerPrefs.GetInt("HasRunOnce") == 0) 
        {
            nextTurdTime = Time.time + firstTurdDelay * 2;
        }
        else
        {
            nextTurdTime = Time.time + firstTurdDelay;
        }
    }
}
