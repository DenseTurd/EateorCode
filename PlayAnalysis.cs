using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayAnalysis : MonoBehaviour
{
    public bool initialised;

    float averageRotationRate;
    float turnRateTimer;
    List<float> deltaRotations;
    List<float> averageRotationRates;
    float averageTakerTimer = 1f;


    float prevRotation;
    float currentRotation;
    float deltaRotation;

    PlayerController playerController;

    bool canIncrementNotTurningEnoughMessages;

    public static PlayAnalysis Instance { get; private set; }
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        //Extensions.PlayerPrefsIncrementInt("testInt"); << something like an extension method but not quite
       // Debug.Log(PlayerPrefs.GetInt("testInt"));

        //transform.position = transform.position.With(y : 0); << testing extension methods

        canIncrementNotTurningEnoughMessages = false;
        if(PlayerPrefs.GetInt("NotTurningEnough") == 1 && PlayerPrefs.GetInt("Rounds played") > 1)
        {
            SplashManager.instance.CreatePanel(new TextPanelData { Title = Strings.TurnMoreTitle, Body = Strings.TurnMoreBody1 });
            canIncrementNotTurningEnoughMessages = true;
        }
        if (PlayerPrefs.GetInt("NotTurningEnough") == 2 && PlayerPrefs.GetInt("Rounds played") > 1)
        {
            SplashManager.instance.CreatePanel(new TextPanelData { Title = Strings.TurnMoreTitle, Body = Strings.TurnMoreBody2 });
            canIncrementNotTurningEnoughMessages = true;
        }
        if (PlayerPrefs.GetInt("NotTurningEnough") == 3 && PlayerPrefs.GetInt("Rounds played") > 1)
        {
            SplashManager.instance.CreatePanel(new TextPanelData { Title = Strings.TurnMoreTitle, Body = Strings.TurnMoreBody3 });
            canIncrementNotTurningEnoughMessages = true;
        }
        if (PlayerPrefs.GetInt("NotTurningEnough") == 4  && PlayerPrefs.GetInt("Rounds played") > 1)
        {
            SplashManager.instance.CreatePanel(new TextPanelData { Title = Strings.TurnMoreTitle, Body = Strings.TurnMoreBody4 });
            canIncrementNotTurningEnoughMessages = true;
        }
        if (PlayerPrefs.GetInt("NotTurningEnough") == 5 && PlayerPrefs.GetInt("Rounds played") > 1)
        {
            SplashManager.instance.CreatePanel(new TextPanelData { Title = Strings.TurnMoreTitle, Body = Strings.TurnMoreBody5 });
            canIncrementNotTurningEnoughMessages = true;
        }
        if (PlayerPrefs.GetInt("NotTurningEnough") >= 6 && PlayerPrefs.GetInt("Rounds played") > 1)
        {
            SplashManager.instance.CreatePanel(new TextPanelData { Title = Strings.TurnMoreTitle, Body = Strings.TurnMoreBody6 });
            canIncrementNotTurningEnoughMessages = true;
        }
    }

    public void Init()
    {
        deltaRotations = new List<float>();
        averageRotationRates = new List<float>();
        playerController = PlayerController.instance;
        initialised = true;
    }

    private void Update()
    {
        if (initialised)
        {
            turnRateTimer -= Time.deltaTime;
            if (turnRateTimer <= 0)
            {
                turnRateTimer = 0.03f;
                prevRotation = currentRotation;
                currentRotation = playerController.transform.rotation.eulerAngles.z;
                CalculateDirectionChange();
            }

            averageTakerTimer -= Time.deltaTime;
            if(averageTakerTimer <= 0)
            {
                averageRotationRates.Add(averageRotationRate);
                averageTakerTimer = 1f;
            }
        }
    }

    void CalculateDirectionChange()
    {
        deltaRotation = Mathf.Abs(prevRotation - currentRotation);

        if(deltaRotation > 180)
        {
            if(prevRotation > 180)
            {
                prevRotation -= 360;
            }
            else
            {
                prevRotation += 360;
            }
            deltaRotation = Mathf.Abs(prevRotation - currentRotation);

            if(deltaRotation > 180)
            {
                return;
            }
        }
        AddToList(deltaRotation);
    }

    void AddToList(float toAdd)
    {
        deltaRotations.Add(toAdd);
        if (deltaRotations.Count > 30)
        {
            deltaRotations.Remove(deltaRotations[0]);
        }
        CalculateAverageDeltaRotation();
    }

    void CalculateAverageDeltaRotation()
    {
        float summedValues = 0;
        foreach (var val in deltaRotations)
        {
            summedValues += val;
        }
        averageRotationRate = summedValues / deltaRotations.Count;
    }

    public void CalculateTheRoundsAverageRotationRate()
    {
        float summedValues = 0;
        foreach (var val in averageRotationRates)
        {
            summedValues += val;
        }

        float roundRotationRate = summedValues / averageRotationRates.Count;
        if (roundRotationRate < 2.4f)
        {
            Debug.Log("Not turning enough");

            if (PlayerPrefs.GetInt("NotTurningEnough") == 0)
            {
                PlayerPrefs.SetInt("NotTurningEnough", 1);
            }

            if (canIncrementNotTurningEnoughMessages)
            {
                Extensions.PlayerPrefsIncrementInt("NotTurningEnough");
            }
            
        }
        else
        {
            PlayerPrefs.SetInt("NotTurningEnough", 0);
        }

        Debug.Log("round rotation rate: " + roundRotationRate);
        //Debug.Log("Summed values: " + summedValues);
        //Debug.Log("averageRotationRatesCount: " + averageRotationRates.Count);
    }

    public void ResetPlayAnalysis()
    {
        PlayerPrefs.SetInt("NotTurningEnough", 0);
    }
}
