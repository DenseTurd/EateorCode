using UnityEngine;
using UnityEngine.SceneManagement;

public enum TutorialStages
{
    Start,
    EatRocks,
    EatCoins,
    Powerup,
    BonkRocks,
    DodgeMeteor
}
public interface ITutorialStage 
{
    void Process(TutorialManager manager);
    void Reset(TutorialManager manager);
}

public class StartStage : ITutorialStage
{
    public void Process(TutorialManager manager)
    {
        if (!manager.started)
        {
            manager.tutorialRocksParent.SetActive(true);
            manager.gameplayTutorialPanels.Enqueue(new ShortHighTextPanelData { Body = Strings.TutorialBody2 });
            manager.gameplayTutorialPanels.Enqueue(new TextPanelData { Body = Strings.TutorialBody2point5 });
            manager.gameplayTutorialPanels.Enqueue(new ShortLowTextPanelData { Body = Strings.TutorialBody3 });
            manager.gameplayTutorialPanels.Enqueue(new SwipePanelData());

            SplashManager.instance.QueuePanels(manager.gameplayTutorialPanels);
            SplashManager.instance.DisplayNextGeneralPanel();

            manager.roundStartRockCount = PlayerPrefs.GetInt("Rocks eaten");
            manager.roundStartCoinCount = PlayerPrefs.GetInt("Coins");
            manager.roundStartSatiationCount = PlayerPrefs.GetInt("Hunger-B-Gone collected");
            manager.roundStartBonkCount = PlayerPrefs.GetInt("Times bonked");
            manager.roundStartMeteorsHit = PlayerPrefs.GetInt("Times Hit By Meteor");

            manager.playSpeed = 0.5f;

            PlayerController.instance.rb.isKinematic = true;

            manager.currentStage = TutorialStages.EatRocks;

            manager.started = true;
        }
    }
    public void Reset(TutorialManager manager)
    {
        
    }
}

public class EatRockStage : ITutorialStage
{
    public void Process(TutorialManager manager)
    {
        if (SplashManager.instance.panelsClosedQueueEmpty && !manager.failTimerActive)
        {
            manager.failTimerActive = true;
            manager.failTimer = 7f;
        }

        if (!manager.firstRockEaten && manager.roundStartRockCount < PlayerPrefs.GetInt("Rocks eaten"))
        {
            manager.postRockEatenTimer -= Time.deltaTime;
            if (manager.postRockEatenTimer <= 0)
            {
                manager.tutorialRocksParent.SetActive(false);
                manager.tutorialCoinsParent.SetActive(true);
                manager.tutorialCoinsParent.transform.SetParent(null);
                manager.gameplayTutorialPanels.Enqueue(new TextPanelData { Body = Strings.TutorialBody4 });
                manager.gameplayTutorialPanels.Enqueue(new TextPanelData { Body = Strings.TutorialBody5 });
                manager.gameplayTutorialPanels.Enqueue(new ShortLowTextPanelData { Body = Strings.TutorialBody6 });
                manager.gameplayTutorialPanels.Enqueue(new SwipePanelData());

                SplashManager.instance.QueuePanels(manager.gameplayTutorialPanels);
                SplashManager.instance.DisplayNextGeneralPanel();
                manager.firstRockEaten = true;

                PlayerController.instance.rb.isKinematic = true;

                Time.timeScale = 0;
                manager.playSpeed = 0.75f;
                manager.currentStage = TutorialStages.EatCoins;

                manager.failTimerActive = false;
            }
            manager.failTimer = 10f;
        }
    }

    public void Reset(TutorialManager manager)
    {
        manager.roundStartRockCount = PlayerPrefs.GetInt("Rocks eaten");
        manager.firstRockEaten = false;
        manager.postRockEatenTimer = 1.5f;
        manager.failTimer = 7f;
        manager.failTimerActive = false;
        manager.tutorialRocksParent.GetComponent<TutorialObjectsParent>().ResetObjects();

        manager.currentStage = TutorialStages.Start;
        manager.started = false;

        manager.gameplayTutorialPanels.Enqueue(new TextPanelData { Title = "Doh!", Body = "Shall we try that bit again?" });

        PlayerController.instance.rb.isKinematic = true;
    }
}

public class EatCoinStage : ITutorialStage
{
    public void Process(TutorialManager manager)
    {
        if (SplashManager.instance.panelsClosedQueueEmpty)
        {
            manager.failTimerActive = true;
        }

        if (!manager.firstCoinEaten && manager.roundStartCoinCount < PlayerPrefs.GetInt("Coins"))
        {
            manager.postCoinEatenTimer -= Time.deltaTime;
            if (manager.postCoinEatenTimer <= 0)
            {
                manager.tutorialCoinsParent.SetActive(false);
                manager.gameplayTutorialPanels.Enqueue(new TextPanelData { Body = Strings.TutorialBody7 });
                manager.gameplayTutorialPanels.Enqueue(new ShortLowTextPanelData { Body = Strings.TutorialBody8 });

                SplashManager.instance.QueuePanels(manager.gameplayTutorialPanels);
                SplashManager.instance.DisplayNextGeneralPanel();
                manager.firstCoinEaten = true;

                PlayerController.instance.rb.isKinematic = true;
                Time.timeScale = 0;
                manager.playSpeed = 1f;
                manager.currentStage = TutorialStages.Powerup;

                manager.failTimerActive = false;
            }
            manager.failTimer = 10f;
        }
    }

    public void Reset(TutorialManager manager)
    {
        manager.roundStartCoinCount = PlayerPrefs.GetInt("Coins");
        manager.firstCoinEaten = false;
        manager.postCoinEatenTimer = 1.5f;
        manager.failTimer = 10f;
        manager.failTimerActive = false;
        manager.tutorialCoinsParent.GetComponent<TutorialObjectsParent>().ResetObjects();

        manager.currentStage = TutorialStages.EatRocks;
        manager.firstRockEaten = false;

        manager.gameplayTutorialPanels.Enqueue(new TextPanelData { Title = "Doh!", Body = "Shall we try that bit again?" });

        PlayerController.instance.rb.isKinematic = true;
    }
}

public class PowerupStage : ITutorialStage
{
    public void Process(TutorialManager manager)
    {
        if (SplashManager.instance.panelsClosedQueueEmpty)
        {
            manager.failTimerActive = true;
        }

        if (!manager.satiationSpawned && manager.firstRockEaten && manager.firstCoinEaten && SplashManager.instance.panelsClosedQueueEmpty)
        {
            PowerupSpawner.Instance.SpawnSatiationPowerup();
            manager.satiationSpawned = true;
            Time.timeScale = 1;
        }

        if (!manager.satiationCollected && manager.roundStartSatiationCount < PlayerPrefs.GetInt("Hunger-B-Gone collected"))
        {
            manager.satiationSplashTimer -= Time.deltaTime;

            if (manager.satiationSplashTimer <= 0)
            {
                manager.tutorialBonksParent.SetActive(true);
                manager.tutorialBonksParent.transform.SetParent(null);
                manager.gameplayTutorialPanels.Enqueue(new TextPanelData { Body = Strings.TutorialBody9 });
                manager.gameplayTutorialPanels.Enqueue(new ShortHighTextPanelData { Body = Strings.TutorialBody10 });
                manager.gameplayTutorialPanels.Enqueue(new SwipePanelData());

                SplashManager.instance.QueuePanels(manager.gameplayTutorialPanels);
                SplashManager.instance.DisplayNextGeneralPanel();

                Time.timeScale = 0;
                manager.satiationCollected = true;
                manager.currentStage = TutorialStages.BonkRocks;

                manager.failTimerActive = false;
            }
            manager.failTimer = 10f;
        }
    }

    public void Reset(TutorialManager manager)
    {

    }
}

public class BonkStage : ITutorialStage
{
    public void Process(TutorialManager manager)
    {
        if (SplashManager.instance.panelsClosedQueueEmpty)
        {
            manager.failTimerActive = true;
        }

        if (!manager.bonked2Times && manager.roundStartBonkCount < PlayerPrefs.GetInt("Times bonked") - 1)
        {
            manager.postBonkTimer -= Time.deltaTime;
            if (manager.postBonkTimer <= 0)
            {
                manager.tutorialBonksParent.SetActive(false);
                manager.tutorialMeteor = DangerSpawner.instance.SpawnTutorialMeteor();
                manager.gameplayTutorialPanels.Enqueue(new ShortHighTextPanelData { Body = Strings.TutorialBody11 });
                manager.gameplayTutorialPanels.Enqueue(new ShortHighTextPanelData { Body = Strings.TutorialBody12 });
                manager.gameplayTutorialPanels.Enqueue(new SwipePanelData());

                SplashManager.instance.QueuePanels(manager.gameplayTutorialPanels);
                SplashManager.instance.DisplayNextGeneralPanel();

                Time.timeScale = 0;
                manager.bonked2Times = true;
                manager.meteorSpawned = true;
                manager.currentStage = TutorialStages.DodgeMeteor;

                manager.failTimerActive = false;
            }
            manager.failTimer = 10f;
        }
    }

    public void Reset(TutorialManager manager)
    {

    }
}

public class DodgeStage : ITutorialStage
{
    public void Process(TutorialManager manager)
    {
        if (SplashManager.instance.panelsClosedQueueEmpty && !manager.failTimerActive)
        {
            manager.failTimerActive = true;
            manager.meteorDodgeTimer = 4f;
        }

        if (manager.roundStartMeteorsHit < PlayerPrefs.GetInt("Times Hit By Meteor"))
        {
            Reset(manager);
        }

        if (!manager.meteorDodged && manager.meteorSpawned)
        {
            manager.meteorDodgeTimer -= Time.deltaTime;
            if (manager.meteorDodgeTimer <= 0)
            {
                manager.gameplayTutorialPanels.Enqueue(new ShortHighTextPanelData { Body = Strings.TutorialBody13 });
                manager.gameplayTutorialPanels.Enqueue(new ShortHighTextPanelData { Body = Strings.TutorialBody14 });


                SplashManager.instance.QueuePanels(manager.gameplayTutorialPanels);
                SplashManager.instance.DisplayNextGeneralPanel();
                Time.timeScale = 0.01f;
                manager.meteorDodged = true;

                manager.failTimerActive = false;
            }
            manager.failTimer = 10f;
        }

        if (manager.meteorDodged && SplashManager.instance.panelsClosedQueueEmpty)
        {
            Extensions.PlayerPrefsSetBool("Tutorial complete", true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name.ToString());
        }
    }

    public void Reset(TutorialManager manager)
    {
        Vector3 pos = PlayerController.instance.transform.position;
        PlayerController.instance.transform.position = new Vector3(pos.x + 50, pos.y, pos.z);
        manager.roundStartMeteorsHit = PlayerPrefs.GetInt("Times Hit By Meteor");
        manager.meteorDodged = false;
        manager.meteorDodgeTimer = 4.5f;
        manager.failTimer = 10f;
        manager.failTimerActive = false;

        manager.currentStage = TutorialStages.BonkRocks;
        manager.bonked2Times = false;

        manager.gameplayTutorialPanels.Enqueue(new TextPanelData { Title = "Doh!", Body = "Shall we try that bit again?" });

        PlayerController.instance.rb.isKinematic = true;
    }
}
