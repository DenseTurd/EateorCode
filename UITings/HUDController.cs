using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public static HUDController instance;

    public Image hungerVal;
    public Image healthVal;
    public TMP_Text sizeValText;
    public TMP_Text scoreValText;

    public GameObject splooge;
    public GameObject megaSpacePanel;

    public TMP_Text cosmicBurgerSplash;
    Animator burgerAnim;
    bool burgerSplash;
    float burgerSplashTimer = 1.2f;
    public GameObject cosmicBurgerMajesty;

    public TMP_Text hungerBGoneSplashText;
    Animator HungerBGoneAnim;
    bool hungerBGoneSplash;
    float hungerBGoneTimer = 1.2f;
    public GameObject hungerBGoneMajesty;

    public TMP_Text gravitySplashText;
    Animator gravityAnim;
    bool gravitySplash;
    float gravityTimer = 1.2f;
    public GameObject gravityMajesty;

    public TMP_Text slowMoSplashText;
    Animator slowMoAnim;
    bool slowMoSplash;
    float slowMoTimer = 1f;
    public GameObject slowMoMajesty;

    float sploogeTimer = 3f;
    float sploogeMaxScale;
    float sploogeScale;
    float sploogeScaleUpTime = 0.2f;
    float sploogeRandomRotate;
    bool shrinkingSplooge;

    float megaSpaceTimer = 0.4f;

    void Awake()
    {
        if(instance == null)
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
        burgerAnim = cosmicBurgerSplash.GetComponent<Animator>();
        HungerBGoneAnim = hungerBGoneSplashText.GetComponent<Animator>();
        gravityAnim = gravitySplashText.GetComponent<Animator>();
        slowMoAnim = slowMoSplashText.GetComponent<Animator>();
        sploogeMaxScale = splooge.transform.localScale.x;
    }

    public void Init()
    {
        UpdateHungerBar();
        UpdateScoreText();
        UpdateSizeText();
    }

    void Update()
    {

        if (splooge.activeSelf == true && !shrinkingSplooge)
        {
            sploogeTimer -= Time.deltaTime;
            if (splooge.transform.localScale.x < sploogeMaxScale)
            {
                splooge.transform.Rotate(0, 0, sploogeRandomRotate, Space.Self);
                sploogeScale += Time.deltaTime * (sploogeMaxScale / sploogeScaleUpTime);
                splooge.transform.localScale = new Vector3(sploogeScale, sploogeScale, sploogeScale);
            }
        }

        if (sploogeTimer <= 0)
        {
            shrinkingSplooge = true;
        }

        if (shrinkingSplooge)
        {
            sploogeScale -= Time.deltaTime * (sploogeMaxScale / sploogeScaleUpTime) * 2;
            splooge.transform.localScale = new Vector3(sploogeScale, sploogeScale, sploogeScale);
            if (sploogeScale <= 0)
            {
                shrinkingSplooge = false;
                splooge.SetActive(false);
                sploogeTimer = 3f;
                AchievementManager.sploogeOnScreen = false;
            }
        }

        HandleMegaSpaceSplash();

        HandleBurgerSplash();

        HandleHungerBGoneSplash();

        HandleGravitySplash();

        HandleSlowMoSplash();
    }

    public void MegaSpace()
    {
        megaSpacePanel.SetActive(true);
    }

    void HandleMegaSpaceSplash()
    {
        if (megaSpacePanel.activeSelf == true)
        {
            megaSpaceTimer -= Time.deltaTime;
        }

        if (megaSpaceTimer <= 0)
        {
            megaSpacePanel.SetActive(false);
            megaSpaceTimer = 0.3f;
        }
    }

    public void CosmicBurgerSplash()
    {
        burgerSplash = true;
        burgerAnim.SetBool("Splash", burgerSplash);
        cosmicBurgerSplash.alpha = 255;
        cosmicBurgerMajesty.GetComponent<MajestyParent>().StartMajesty();
    }

    void HandleBurgerSplash()
    {
        if (burgerSplash)
        {
            burgerSplashTimer -= Time.deltaTime;
        }

        if (burgerSplashTimer <= 0)
        {
            burgerSplash = false;
            burgerAnim.SetBool("Splash", burgerSplash);
            cosmicBurgerSplash.alpha = 0;
            burgerSplashTimer = 1.2f;
            cosmicBurgerMajesty.GetComponent<MajestyParent>().StopMajesty();
        }
    }

    public void HungerBGoneSplash()
    {
        hungerBGoneSplash = true;
        HungerBGoneAnim.SetBool("Splash", hungerBGoneSplash);
        hungerBGoneSplashText.alpha = 255;
        hungerBGoneMajesty.GetComponent<MajestyParent>().StartMajesty();
    }

    void HandleHungerBGoneSplash()
    {
        if (hungerBGoneSplash)
        {
            hungerBGoneTimer -= Time.deltaTime;
        }

        if (hungerBGoneTimer <= 0)
        {
            hungerBGoneSplash = false;
            HungerBGoneAnim.SetBool("Splash", hungerBGoneSplash);
            hungerBGoneSplashText.alpha = 0;
            hungerBGoneTimer = 1.2f;
            hungerBGoneMajesty.GetComponent<MajestyParent>().StopMajesty();
        }
    }

    public void GravitySplash()
    {
        gravitySplash = true;
        gravityAnim.SetBool("Splash", gravitySplash);
        gravitySplashText.alpha = 255;
        gravityMajesty.GetComponent<MajestyParent>().StartMajesty();
    }

    void HandleGravitySplash()
    {
        if (gravitySplash)
        {
            gravityTimer -= Time.deltaTime;
        }

        if (gravityTimer <= 0)
        {
            gravitySplash = false;
            gravityAnim.SetBool("Splash", gravitySplash);
            gravitySplashText.alpha = 0;
            gravityTimer = 1.2f;
            gravityMajesty.GetComponent<MajestyParent>().StopMajesty();
        }
    }

    public void SlowMoSplash()
    {
        slowMoSplash = true;
        slowMoAnim.SetBool("Splash", slowMoSplash);
        slowMoSplashText.alpha = 255;
        slowMoMajesty.GetComponent<MajestyParent>().StartMajesty();
    }

    void HandleSlowMoSplash()
    {
        if (slowMoSplash)
        {
            slowMoTimer -= Time.deltaTime;
        }

        if (slowMoTimer <= 0)
        {
            slowMoSplash = false;
            slowMoAnim.SetBool("Splash", slowMoSplash);
            slowMoSplashText.alpha = 0;
            slowMoTimer = 1f;
            slowMoMajesty.GetComponent<MajestyParent>().StopMajesty();
        }
    }

    public void UpdateSizeText()
    {
        sizeValText.text = "Size: " + (PlayerController.instance.transform.localScale.x * 10).ToString("F0");
    }

    public void UpdateHungerBar()
    {
        hungerVal.fillAmount = PlayerController.instance.hunger / PlayerController.instance.satiation;
    }

    public void UpdateHealthBar(float val)
    {
        healthVal.fillAmount = val;
    }

    public void UpdateScoreText()
    {
        scoreValText.text = "Score: " + (ScoreManager.instance.score).ToString("F0");
    }

    public void Splooge()
    {
        splooge.transform.Rotate(0, 0, Random.Range(0, 359), Space.World);
        splooge.transform.localScale = (new Vector3(0, 0, 0));
        sploogeScale = 0;

        sploogeRandomRotate = Random.Range(-3, 3);
        splooge.SetActive(true);
        AchievementManager.sploogeOnScreen = true;
    }

    public void ClearHUD()
    {
        megaSpacePanel.SetActive(false);

        if (splooge.activeSelf)
        {
            shrinkingSplooge = true;
        }
    }
}
