using System.Collections.Generic;
using UnityEngine;

public class Reminders : MonoBehaviour
{
    Queue<BasePanelData> postFirstRoundMessagesPanels;
    Queue<BasePanelData> unusedUpgradePanels;

    public static Reminders Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        if (PlayerPrefs.GetInt("Rounds played") == 1)
        {
            PostFirstRoundMessages();
        }

        if (PlayerPrefs.GetInt("UnusedUpgradeCounter") > 0)
        {
            PlayerPrefs.SetInt("RoundsSinceLastUpgradeReminder", PlayerPrefs.GetInt("RoundsSinceLastUpgradeReminder") + 1);

            if (PlayerPrefs.GetInt("UnusedUpgradeCounter") > 2)
            {
                if (PlayerPrefs.GetInt("RoundsSinceLastUpgradeReminder") > 2)
                    UnusedUpgradeReminder();
            }
        }
    }

    public void PostFirstRoundMessages()
    {
        postFirstRoundMessagesPanels = new Queue<BasePanelData>();
        postFirstRoundMessagesPanels.Enqueue(new TextPanelData { Title = Strings.PostFirstTitle, Body = Strings.PostFirstBody1 });

        SplashManager.instance.QueuePanels(postFirstRoundMessagesPanels);
        SplashManager.instance.DisplayNextGeneralPanel();
        Debug.Log("Post first round messages created");
    }

    public void UnusedUpgradeReminder()
    {
        unusedUpgradePanels = new Queue<BasePanelData>();
        unusedUpgradePanels.Enqueue(new TextPanelData { Title = Strings.UpgradeRreminderTitle, Body = Strings.UpgradeReminderBody });

        SplashManager.instance.QueuePanels(unusedUpgradePanels);
        SplashManager.instance.DisplayNextGeneralPanel();
        PlayerPrefs.SetInt("RoundsSinceLastUpgradeReminder", 0);
        Debug.Log("Upgrade reminder created");
    }
}
