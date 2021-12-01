using UnityEngine;

public class PointersControl : MonoBehaviour
{
    public GameObject storePointer;
    public GameObject upgradePointer;
    public GameObject charChangePointer;
    public GameObject disintegrateUpgradePointer;

    int roundsPlayed;
    public bool storeOpened;

    public bool upgraded;

    public bool charChangeOpened;

    float storePointerTimer = 3f;
    float upgradePointerTimer = 0.05f;
    float charChangePointerTimer = 0.5f;
    float disintegrateUpgradeTimer = 0.1f;

    bool postFirstRoundStartingSizePointer;
    bool anyRoundStartingSizePointer;


    public static PointersControl Instance { get; private set; }

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
        Init();
    }

    public void Init()
    {
        roundsPlayed = PlayerPrefs.GetInt("Rounds played");
        if (Extensions.PlayerPrefsGetBool("Store opened"))
        {
            storeOpened = true;
            storePointer.SetActive(false);
        }
        if (PlayerPrefs.GetInt("StartingSizeUpgradeLevel") != 0)
        {
            upgraded = true;
            upgradePointer.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Character change opened") != 0)
        {
            charChangeOpened = true;
            charChangePointer.SetActive(false);
        }
    }

    void Update()
    {
        if (roundsPlayed > 3 && !storeOpened)
        {
            storePointerTimer -= Time.deltaTime;

            if (storePointerTimer <= 0)
            {
                storePointer.SetActive(!storePointer.activeSelf);
                storePointerTimer = 3f;
            }
        }
        else
        {
            storePointer.SetActive(false);
        }

        DetermineStartingSizePointerActiveConditions();
        if (postFirstRoundStartingSizePointer || anyRoundStartingSizePointer)
        {
            upgradePointerTimer -= Time.deltaTime;

            if (upgradePointerTimer <= 0)
            {
                upgradePointer.SetActive(!upgradePointer.activeSelf);
                upgradePointerTimer = 2f;
            }
        }
        else
        {
            upgradePointer.SetActive(false);
        }

        if (roundsPlayed > 3 && !charChangeOpened)
        {
            charChangePointerTimer -= Time.deltaTime;

            if (charChangePointerTimer <= 0)
            {
                charChangePointer.SetActive(!charChangePointer.activeSelf);
                charChangePointerTimer = 2.5f;
            }
        }
        else
        {
            charChangePointer.SetActive(false);
        }

        if (PlayerPrefs.GetInt("UnusedUpgradeCounter") > 1 && UpgradeManager.instance.disintegrateAvaliable)
        {
            disintegrateUpgradeTimer -= Time.deltaTime;

            if (disintegrateUpgradeTimer <= 0)
            {
                disintegrateUpgradePointer.SetActive(!disintegrateUpgradePointer.activeSelf);
                disintegrateUpgradeTimer = 2.8f;
            }
        }
        else
        {
            disintegrateUpgradePointer.SetActive(false);
        }

    }

    void DetermineStartingSizePointerActiveConditions()
    {
        if (PlayerPrefs.GetInt("UnusedUpgradeCounter") > 1 && UpgradeManager.instance.startingSizeAvaliable)
        {
            anyRoundStartingSizePointer = true;
        }
        else
        {
            anyRoundStartingSizePointer = false;
        }


        if (roundsPlayed > 0 && !upgraded)
        {
            postFirstRoundStartingSizePointer = true;
        }
        else
        {
            postFirstRoundStartingSizePointer = false;
        }
    }
}
