using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class TutorialVideoControl : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    public GameObject continueButton;
    public GameObject startGameButton;
    public GameObject ReplayTutorialButton;

    public GameObject welcomeParent;
    public GameObject welcomePanel;
    public GameObject welcomeHighlightRectangle;

    public GameObject eatSmall;

    public GameObject turns;

    public GameObject bonk;

    public GameObject danger;

    public GameObject tryNotToGetHit;

    public GameObject also;

    public float welcomeTimer;
    public float welcomePlayTimer;
    public float eatSmallTimer;
    public float eatSmallPlayTimer;
    public float turnsTimer;
    public float turnsPlayTimer;
    public float bonkTimer;
    public float bonkPlayTimer;
    public float dangerTimer;
    public float dangerPlayTimer;
    public float tryNotToGetHitTimer;
    public float tryNotToGetHitPlayTimer;
    public float alsoTimer;


    enum TutorialStage
    {
        welcomePaused,
        welcomePlay,
        eatSmallPaused,
        eatSmallPlay,
        turnsPaused,
        turnsPlay,
        bonkPaused,
        bonkPlay,
        dangerPaused,
        dangerPlay,
        tryNotToGetHitPaused,
        tryNotToGetHitPlay,
        also
    }

    TutorialStage currentStage;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.Prepare();
        videoPlayer.Pause();
        currentStage = TutorialStage.welcomePaused;
    }

    private void Update()
    {
        //KeyboardInputs();
        HandleWelcome();
        HandleEatSmall();
        HandleTurns();
        HandleBonk();
        HandleDanger();
        HandlyTryNotToGetHit();
        HandleAlso();
    }

    private void HandleAlso()
    {
        if (currentStage == TutorialStage.also)
        {
            alsoTimer -= Time.deltaTime;
        }
        if (alsoTimer <= 0)
        {
            if (!ReplayTutorialButton.activeSelf)
                ReplayTutorialButton.SetActive(true);

            if (!startGameButton.activeSelf)
                startGameButton.SetActive(true);
        }
    }

    private void HandlyTryNotToGetHit()
    {
        if (currentStage == TutorialStage.tryNotToGetHitPaused)
        {
            tryNotToGetHitTimer -= Time.deltaTime;
        }
        if (tryNotToGetHitTimer <= 0)
        {
            if (!continueButton.activeSelf)
                continueButton.SetActive(true);
        }

        if (currentStage == TutorialStage.tryNotToGetHitPlay)
        {
            tryNotToGetHitPlayTimer -= Time.deltaTime;
        }
        if (tryNotToGetHitPlayTimer <= 0)
        {
            PauseVid();
            tryNotToGetHit.SetActive(false);
            tryNotToGetHitPlayTimer = 1;
            currentStage = TutorialStage.also;
            also.SetActive(true);
        }
    }

    private void HandleDanger()
    {
        if (currentStage == TutorialStage.dangerPaused)
        {
            dangerTimer -= Time.deltaTime;
        }
        if (dangerTimer <= 0)
        {
            if (!continueButton.activeSelf)
                continueButton.SetActive(true);
        }

        if (currentStage == TutorialStage.dangerPlay)
        {
            dangerPlayTimer -= Time.deltaTime;
        }
        if (dangerPlayTimer <= 0)
        {
            PauseVid();
            danger.SetActive(false);
            dangerPlayTimer = 1;
            currentStage = TutorialStage.tryNotToGetHitPaused;
            tryNotToGetHit.SetActive(true);
        }
    }

    private void HandleBonk()
    {
        if (currentStage == TutorialStage.bonkPaused)
        {
            bonkTimer -= Time.deltaTime;
        }
        if (bonkTimer <= 0)
        {
            if (!continueButton.activeSelf)
                continueButton.SetActive(true);
        }

        if (currentStage == TutorialStage.bonkPlay)
        {
            bonkPlayTimer -= Time.deltaTime;
        }
        if (bonkPlayTimer <= 0)
        {
            PauseVid();
            bonk.SetActive(false);
            bonkPlayTimer = 1;
            currentStage = TutorialStage.dangerPaused;
            danger.SetActive(true);
        }
    }

    private void HandleTurns()
    {
        if (currentStage == TutorialStage.turnsPaused)
        {
            turnsTimer -= Time.deltaTime;
        }
        if (turnsTimer <= 0)
        {
            if (!continueButton.activeSelf)
                continueButton.SetActive(true);
        }

        if (currentStage == TutorialStage.turnsPlay)
        {
            turnsPlayTimer -= Time.deltaTime;
        }
        if (turnsPlayTimer <= 0)
        {
            PauseVid();
            turns.SetActive(false);
            turnsPlayTimer = 1;
            currentStage = TutorialStage.bonkPaused;
            bonk.SetActive(true);
        }
    }

    private void HandleEatSmall()
    {
        if (currentStage == TutorialStage.eatSmallPaused)
        {
            eatSmallTimer -= Time.deltaTime;
        }
        if (eatSmallTimer <= 0)
        {
            if (!continueButton.activeSelf)
                continueButton.SetActive(true);
        }

        if (currentStage == TutorialStage.eatSmallPlay)
        {
            eatSmallPlayTimer -= Time.deltaTime;
        }
        if (eatSmallPlayTimer <= 0)
        {
            PauseVid();
            turns.SetActive(true);
            eatSmallPlayTimer = 1;
            currentStage = TutorialStage.turnsPaused;
        }
    }

    private void HandleWelcome()
    {
        if (currentStage == TutorialStage.welcomePaused)
        {
            welcomeTimer -= Time.deltaTime;
        }
        if (welcomeTimer <= 0)
        {
            if (!continueButton.activeSelf)
                continueButton.SetActive(true);
        }

        if (currentStage == TutorialStage.welcomePlay)
        {
            welcomePlayTimer -= Time.deltaTime;
        }
        if (welcomePlayTimer <= 0)
        {
            PauseVid();
            welcomeParent.SetActive(false);
            eatSmall.SetActive(true);
            currentStage = TutorialStage.eatSmallPaused;
            welcomePlayTimer = 1;
        }
    }

    public void Continue()
    {
        if(currentStage == TutorialStage.welcomePaused)
        {
            welcomeHighlightRectangle.SetActive(true);
            welcomePanel.SetActive(false);
            continueButton.SetActive(false);
            welcomeTimer = 1;
            videoPlayer.playbackSpeed = 0.75f;
            PlayVid();
            currentStage = TutorialStage.welcomePlay;
        }

        if(currentStage == TutorialStage.eatSmallPaused)
        {
            eatSmall.SetActive(false);
            continueButton.SetActive(false);
            eatSmallTimer = 1;
            videoPlayer.playbackSpeed = 0.75f;
            PlayVid();
            currentStage = TutorialStage.eatSmallPlay;
        }

        if(currentStage == TutorialStage.turnsPaused)
        {
            turns.SetActive(false);
            continueButton.SetActive(false);
            turnsTimer = 1;
            PlayVid();
            currentStage = TutorialStage.turnsPlay;
        }

        if (currentStage == TutorialStage.bonkPaused)
        {
            bonk.SetActive(false);
            continueButton.SetActive(false);
            bonkTimer = 1;
            PlayVid();
            currentStage = TutorialStage.bonkPlay;
        }

        if (currentStage == TutorialStage.dangerPaused)
        {
            danger.SetActive(false);
            continueButton.SetActive(false);
            dangerTimer = 1;
            PlayVid();
            currentStage = TutorialStage.dangerPlay;
        }

        if (currentStage == TutorialStage.tryNotToGetHitPaused)
        {
            tryNotToGetHit.SetActive(false);
            continueButton.SetActive(false);
            tryNotToGetHitTimer = 1;
            PlayVid();
            currentStage = TutorialStage.tryNotToGetHitPlay;
        }
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("Tutorial complete", 1);
        SceneManager.LoadScene(1);
    }

    public void ReplayTutorial()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name.ToString());
    }

    void PlayVid()
    {
        videoPlayer.Play();
    }

    void PauseVid()
    {
        videoPlayer.Pause();
    }

    //void KeyboardInputs()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        if (videoPlayer.isPlaying)
    //        {
    //            PauseVid();
    //            Debug.Log("Pause");
    //            return;
    //        }


    //        if (videoPlayer.isPaused)
    //        {
    //            PlayVid();
    //            Debug.Log("Play");
    //            return;
    //        }
    //    }

    //    if (Input.GetKeyDown(KeyCode.N))
    //        SceneManager.LoadScene(1);
    //}
}
