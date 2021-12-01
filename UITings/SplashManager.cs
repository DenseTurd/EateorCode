using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SplashManager : MonoBehaviour
{
    public static SplashManager instance;

    public TMP_Text thumbsReady;
    public TMP_Text go;
    public bool readyGoInitialised;
    public bool readyGoDone;
    float scale;
    float nextTingTime;

    public GameObject achievementPanel;
    public GameObject characterUnlockPanel;
    public GameObject textPanel;
    public GameObject imagePanel;
    public GameObject textAndImagePanel;
    public GameObject swipePanel;
    public GameObject shortHighPanel;
    public GameObject shortLowPanel;
    public GameObject prizePanel;
    public GameObject choicePanel;

    public Queue<BasePanelData> panelQueue = new Queue<BasePanelData>();

    public bool panelsClosedQueueEmpty;
    public bool readyGoDisplayed;

    ThumbsController thumbsController;

    Dictionary<PanelType, GameObject> PanelTypeDictionary = new Dictionary<PanelType, GameObject>();

    float enableControlsTimer = 0.3f;

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

        PanelTypeDictionary.Add(PanelType.Text, textPanel);
        PanelTypeDictionary.Add(PanelType.Image, imagePanel);
        PanelTypeDictionary.Add(PanelType.TextAndImage, textAndImagePanel);
        PanelTypeDictionary.Add(PanelType.Swipe, swipePanel);
        PanelTypeDictionary.Add(PanelType.ShortHighText, shortHighPanel);
        PanelTypeDictionary.Add(PanelType.ShortLowText, shortLowPanel);
        PanelTypeDictionary.Add(PanelType.PrizeText, prizePanel);
        PanelTypeDictionary.Add(PanelType.Choice, choicePanel);
    }
    public void DisplayReadyGo()
    {
        if (!readyGoInitialised)
        {
            thumbsReady.gameObject.SetActive(true);
            thumbsReady.gameObject.transform.localScale = new Vector3(0, 0, 0);
            scale = 0;
            readyGoInitialised = true;
            thumbsController = GetComponent<ThumbsController>();
            thumbsController.SortThumbs();
        }

        if (thumbsReady.isActiveAndEnabled)
        {
            if(scale < 1)
            {
                scale += Time.deltaTime * 7;
                thumbsReady.transform.localScale = new Vector3(scale, scale, scale);
                thumbsReady.alpha = scale * 225;
                nextTingTime = Time.time + 0.4f;
            }
            else
            {
                if (Time.time > nextTingTime)
                {
                    thumbsReady.gameObject.SetActive(false);
                    go.gameObject.SetActive(true);
                    go.gameObject.transform.localScale = new Vector3(0, 0, 0);
                    scale = 0;
                }
            }
        }

        if (go.isActiveAndEnabled)
        {
            enableControlsTimer -= Time.deltaTime;
        }
    }

    private void Update()
    {
        if (enableControlsTimer <= 0)
        {
            readyGoDisplayed = true;
            thumbsController.HideThumbs();
        }

        if (readyGoDisplayed && go.gameObject.activeSelf)
        {
            RemoveGo();
        }
    }

    void RemoveGo()
    {
        if (scale < 1)
        {
            scale += Time.deltaTime * 5;
            go.transform.localScale = new Vector3(scale, scale, scale);
            go.alpha = scale * 225;
            nextTingTime = Time.time + 0.6f;
        }
        else
        {
            if (Time.time > nextTingTime)
            {
                go.gameObject.SetActive(false);
            }
        }
    }

    public void DisplayAchievemet(BaseAchievement achievement)
    {
        GameObject newAcievementPanel = Instantiate(achievementPanel, transform);

        PanelControl panelControl = newAcievementPanel.GetComponent<PanelControl>();
        panelControl.achievement = achievement;
        panelControl.Init();
    }

    public void DisplayCharacterUnlock(BaseCharacter unlockedCharacter)
    {
        GameObject newCharacterUnlockPanel = Instantiate(characterUnlockPanel, transform);

        CharacterUnlockPanelControl characterUnlockPanelControl = newCharacterUnlockPanel.GetComponent<CharacterUnlockPanelControl>();
        characterUnlockPanelControl.character = unlockedCharacter;
        characterUnlockPanelControl.Init();
    }

    public void QueuePanels(Queue<BasePanelData> incomingQueue)
    {
        panelQueue = incomingQueue;
        panelsClosedQueueEmpty = false;
    }

    public void DisplayNextGeneralPanel()
    {
        if (panelQueue.Count > 0)
            CreatePanel(panelQueue.Dequeue());
    }

    public void CreatePanel(BasePanelData data)
    {
        GameObject panel = Instantiate(PanelTypeDictionary[data.Type], transform);
        FillPanelData(data, panel);
        panelsClosedQueueEmpty = false;
    }

    private static void FillPanelData(BasePanelData data, GameObject panel)
    {
        BasePanelControl panelControl = panel.GetComponent<BasePanelControl>();
        panelControl.FillData(data);
    }

    public void CheckLastPanelClosed()
    {
        if (panelQueue.Count == 0)
            panelsClosedQueueEmpty = true;
    }
}
