using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class HUDAlarms : MonoBehaviour
{
    public GameObject hungerPanel;
    public TMP_Text hungerText;
    public Image hungerBar;
    public GameObject hungerExclamation;
    float hungerExclamationTimer;

    public GameObject hPPanel;
    public TMP_Text hPText;
    public Image hPBar;
    public GameObject hPExclamation;
    float hPExclamationTimer;

    public GameObject sizePanel;
    public TMP_Text sizeText;
    public GameObject sizeExclamation;
    float sizeExclamationTimer;

    public GameObject exclamationParent;

    float t;

    Color32 orange = new Color32(0xFC, 0x44, 0x0f, 0xFF);

    float satiation;

    public static HUDAlarms Instance { get; private set; }
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
        satiation = PlayerPrefs.GetFloat("Satiation");
    }

    private void Update()
    {
        if (!RoundOverManager.instance.roundOver)
        {
            t += Time.deltaTime * 15;
            if (t > 360)
                t = 0;

            float scale = 1 + (Mathf.Sin(t/3.5f) * 0.1f);
            exclamationParent.transform.localScale = new Vector3(scale, scale, scale);

            if (PlayerController.PlayerScale < 0.8f)
            {
                SizeAlarm();
            }
            else if (sizePanel.transform.localScale.x != 1)
            {
                StopSizeAlarm();
            }

            if (PlayerController.instance.hunger > satiation * 0.7f)
            {
                HungerAlarm();
            }
            else if (hungerPanel.transform.localScale.x != 1)
            {
                StopHungerAlarm();
            }

            if (PlayerHealth.Instance.GetHealth() < PlayerHealth.Instance.GetMaxHealth() * 0.3f)
            {
                HPAlarm();
            }
            else if (hPPanel.transform.localScale.x != 1)
            {
                StopHPAlarm();
            }
        }
    }

    void SizeAlarm()
    {
        float scale = 1 + (Mathf.Sin(t) * 0.1f);
        sizePanel.transform.localScale = new Vector3(scale,scale,scale);

        if(sizeText.color != Color.red)
        {
            sizeText.color = Color.red;
        }

        sizeExclamationTimer -= Time.deltaTime;

        if(sizeExclamationTimer  < 0)
        {
            sizeExclamationTimer = 0.4f;
            sizeExclamation.SetActive(!sizeExclamation.activeSelf);
        }
    }

    void StopSizeAlarm()
    {
        sizePanel.transform.localScale = new Vector3(1, 1, 1);
        sizeText.color = orange;
        sizeExclamation.SetActive(false);
    }

    void HungerAlarm()
    {
        float scale = 1 + (Mathf.Sin(t) * 0.1f);
        hungerPanel.transform.localScale = new Vector3(scale, scale, scale);

        if (hungerBar.color != Color.red)
        {
            hungerBar.color = Color.red;
            hungerText.color = Color.red;
        }

        hungerExclamationTimer -= Time.deltaTime;

        if (hungerExclamationTimer < 0)
        {
            hungerExclamationTimer = 0.4f;
            hungerExclamation.SetActive(!hungerExclamation.activeSelf);
        }
    }

    void StopHungerAlarm()
    {
        hungerPanel.transform.localScale = new Vector3(1, 1, 1);
        hungerBar.color = orange;
        hungerText.color = orange;
        hungerExclamation.SetActive(false);
    }

    void HPAlarm()
    {
        float scale = 1 + (Mathf.Sin(t) * 0.1f);
        hPPanel.transform.localScale = new Vector3(scale, scale, scale);

        if (hPBar.color != Color.red)
        {
            hPBar.color = Color.red;
            hPText.color = Color.red;
        }

        hPExclamationTimer -= Time.deltaTime;

        if (hPExclamationTimer < 0)
        {
            hPExclamationTimer = 0.4f;
            hPExclamation.SetActive(!hPExclamation.activeSelf);
        }
    }

    void StopHPAlarm()
    {
        hPPanel.transform.localScale = new Vector3(1, 1, 1);
        hPBar.color = orange;
        hPText.color = orange;
        hPExclamation.SetActive(false);
    }

    public void StopAllAlarms()
    {
        StopSizeAlarm();
        StopHungerAlarm();
        StopHPAlarm();
    }
}
