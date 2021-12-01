using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class UiController : MonoBehaviour
{
    public ToBeUpgraded toBeUpgraded;

    GameObject uiCanvas;
    public GameObject uiPanel;
    public GameObject hUDCanvas;
    public GameObject storePanel;
    public GameObject achievemntBrowserPanel;
    public GameObject statsPanel;
    public GameObject characterChangePanel;
    public GameObject settingsPanel;
    public GameObject guidePanel;

    public GameObject noAdsButton;

    public List<GameObject> panels;
   
    public TMP_Text coinsText;

    public TMP_Text disintegrateUpgradeText;
    public TMP_Text disintegrateUpgradeCostText;
    public TMP_Text disintegrateRewardAdText;
    public TMP_Text disintegrateLevelText;

    public TMP_Text startingSizeUpgradeText;
    public TMP_Text startingSizeUpgradeCostText;
    public TMP_Text startingSizeRewardAdText;
    public TMP_Text startingSizeLevelText;

    public TMP_Text satiationUpgradeText;
    public TMP_Text satiationUpgradeCostText;
    public TMP_Text satiationRewardAdText;
    public TMP_Text satiationLevelText;

    Color32 orange = new Color32(0xFC, 0x44, 0x0f, 0xFF);

    public static UiController instance;

    float buttonInfoSetTimer = 0.1f;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        if(PlayerPrefs.GetInt("noads") == 1)
        {
            noAdsButton.SetActive(false);
        }

        uiCanvas = this.gameObject;
        UpdateCoinsText();
        UpdateDisintegrateUpgradeCostText();
        UpdateStartingSizeUpgradeCostText();
        UpdateSatiationUpgradeCostText();

        panels = new List<GameObject>()
        {
            uiPanel,
            storePanel,
            achievemntBrowserPanel,
            statsPanel,
            characterChangePanel,
            settingsPanel,
            guidePanel
        };
    }

    private void Update()
    {
        buttonInfoSetTimer -= Time.deltaTime;

        if (buttonInfoSetTimer <= 0)
        {
            SetUpgradeButtonInfo();
            SetColors();
            buttonInfoSetTimer = 0.1f;
        }
    }

    public void PlayNoTutorial()
    {
        PlayerPrefs.SetInt("Tutorial complete", 1);
        Play();
    }
    public void Play()
    {
        uiCanvas.SetActive(false);
        hUDCanvas.SetActive(true);
        AudioManager.instance.Click();
        GameManager.instance.StartRound();
        UpgradeManager.instance.IncrementUnusedUpgradeCounter();

        AudioManager.instance.RoundStart();

        if(PlayerPrefs.GetInt("Tutorial complete") == 1)
        {
            AudioManager.instance.PlayGameplayMusic();
            AudioManager.instance.StopMenuMusic();
        }
    }

    void SetColors()
    {
        if (PlayerPrefs.GetInt("Coins") >= PlayerPrefs.GetInt("DisintegrateUpgradeCost") || disintegrateRewardAdText.IsActive())
        {
            disintegrateUpgradeCostText.color = orange;
            disintegrateUpgradeText.color = orange;
            disintegrateLevelText.color = orange;
        }
        else
        {
            disintegrateUpgradeCostText.color = Color.gray;
            disintegrateUpgradeText.color = Color.gray;
            disintegrateLevelText.color = Color.gray;
        }
        if (PlayerPrefs.GetInt("Coins") >= PlayerPrefs.GetInt("StartingSizeUpgradeCost") || startingSizeRewardAdText.IsActive())
        {
            startingSizeUpgradeCostText.color = orange;
            startingSizeUpgradeText.color = orange;
            startingSizeLevelText.color = orange;
        }
        else
        {
            startingSizeUpgradeText.color = Color.gray;
            startingSizeUpgradeCostText.color = Color.gray;
            startingSizeLevelText.color = Color.gray;
        }
        if (PlayerPrefs.GetInt("Coins") >= PlayerPrefs.GetInt("SatiationUpgradeCost") || satiationRewardAdText.IsActive())
        {
            satiationUpgradeCostText.color = orange;
            satiationUpgradeText.color = orange;
            satiationLevelText.color = orange;
        }
        else
        {
            satiationUpgradeCostText.color = Color.gray;
            satiationUpgradeText.color = Color.gray;
            satiationLevelText.color = Color.gray;
        }
    }

    void SetUpgradeButtonInfo()
    {
        if (UpgradeManager.instance.disintegrateCanRewardAdUpgrade && !UpgradeManager.instance.hasUpgradedViaRewardAdThisRound)
        {
            disintegrateRewardAdText.gameObject.SetActive(true);
            disintegrateUpgradeCostText.gameObject.SetActive(false);
        }
        else
        {
            disintegrateUpgradeCostText.gameObject.SetActive(true);
            disintegrateRewardAdText.gameObject.SetActive(false);
            UpdateDisintegrateUpgradeCostText();
        }
        if (UpgradeManager.instance.startingSizeCanRewardAdUpgrade && !UpgradeManager.instance.hasUpgradedViaRewardAdThisRound)
        {
            startingSizeRewardAdText.gameObject.SetActive(true);
            startingSizeUpgradeCostText.gameObject.SetActive(false);
        }
        else
        {
            startingSizeUpgradeCostText.gameObject.SetActive(true);
            startingSizeRewardAdText.gameObject.SetActive(false);
            UpdateStartingSizeUpgradeCostText();
        }
        if (UpgradeManager.instance.satiationCanRewardAdUpgrade && !UpgradeManager.instance.hasUpgradedViaRewardAdThisRound)
        {
            satiationRewardAdText.gameObject.SetActive(true);
            satiationUpgradeCostText.gameObject.SetActive(false);
        }
        else
        {
            satiationUpgradeCostText.gameObject.SetActive(true);
            satiationRewardAdText.gameObject.SetActive(false);
            UpdateSatiationUpgradeCostText();
        }

        disintegrateLevelText.text = "Level: " + PlayerPrefs.GetInt("DisintegrateUpgradeLevel");
        startingSizeLevelText.text = "Level: " + PlayerPrefs.GetInt("StartingSizeUpgradeLevel");
        satiationLevelText.text = "Level: " + PlayerPrefs.GetInt("SatiationUpgradeLevel");
    }

    public void UpdateCoinsText()
    {
        coinsText.text = PlayerPrefs.GetInt("Coins").ToString();
    }

    public void UpdateDisintegrateUpgradeCostText()
    {
        disintegrateUpgradeCostText.text = PlayerPrefs.GetInt("DisintegrateUpgradeCost").ToString();
    }

    public void UpdateStartingSizeUpgradeCostText()
    {
        startingSizeUpgradeCostText.text = PlayerPrefs.GetInt("StartingSizeUpgradeCost").ToString();
    }

    public void UpdateSatiationUpgradeCostText()
    {
        satiationUpgradeCostText.text = PlayerPrefs.GetInt("SatiationUpgradeCost").ToString();
    }

    public void DisintegrateUpgrade()
    {
        UpgradeManager.instance.AttemptDisintegrateUpgrade();
        UpdateCoinsText();
        UpdateDisintegrateUpgradeCostText();
    }

    public void StartingSizeUpgrade()
    {
        UpgradeManager.instance.AttemptStartingSizeUpgrade();
        UpdateCoinsText();
        UpdateStartingSizeUpgradeCostText();
    }

    public void SatiationUpgrade()
    {
        UpgradeManager.instance.AttemptSatiationUpgrade();
        UpdateCoinsText();
        UpdateSatiationUpgradeCostText();
    }

    public void ClosePanel()
    {
        foreach(GameObject panel in panels)
        {
            panel.SetActive(false);
        }

        uiPanel.SetActive(true);

        AudioManager.instance.Close();
    }

    public void OpenPanel(GameObject panel)
    {
        foreach(GameObject p in panels)
        {
            p.SetActive(false);
        }

        panel.SetActive(true);

        AudioManager.instance.Click();
        AudioManager.instance.AddElementToMenuMusic();
    }
}
