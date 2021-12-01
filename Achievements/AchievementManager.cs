using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public List<BaseAchievement> achievements;
    public Queue<BaseAchievement> achievementQueue;
    public static AchievementManager instance;

    int meteorHitCount;
    public static bool sploogeOnScreen;

    int achievementsUnlocked;

    public int xRaySpecsCount;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        achievementsUnlocked = 0;
        meteorHitCount = 0;

        achievements = new List<BaseAchievement>
        {
            new ThatlLearnYa(),
            new MoonageDavedream(),
            new Plutonians(),
            new Martian(),
            new Earth(),
            new Uranus(),
            new Saturn(),
            new Jupiter(),
            new Sun(),
            new Doh(),
            new SirVivor(),
            new XRaySpecs(),
            new MegaSpace(),
            new CosmicBurger(),
            new Pebble(),
            new Boulder(),
            new TheRock(),
            new JustASec(),
            new LongHaul(),
            new Marathon(),
            new JustAMin(),
            new TakeYerTime(),
            new LongWinder(),
            new Peckish(),
            new Hungry(),
            new FerociousAppetite(),
            new SecretCoins()
        };
        AchievementBrowser.instance.PopulateAchievementBrowser();

        achievementQueue = new Queue<BaseAchievement>();
        foreach (BaseAchievement achievement in achievements)
        {
            if(PlayerPrefs.GetInt(achievement.Name + " achieved") == 1)
            {
                achievementsUnlocked++;
                if(PlayerPrefs.GetInt(achievement.Name + " confirmed") == 0)
                {
                    achievementQueue.Enqueue(achievement);
                }
            }
        }
        PlayerPrefs.SetInt("Achievements unlocked", achievementsUnlocked);

        if (achievementQueue.Count == 0)
        {
            PlayerPrefs.SetInt("Rounds without achievement", PlayerPrefs.GetInt("Rounds without achievement") + 1);
            CharacterUnlocker.Instance.FillerUnlock();
        }

        while (achievementQueue.Count != 0)
        {
            SplashManager.instance.DisplayAchievemet(achievementQueue.Dequeue());
            Debug.Log("Dequed achievement");
            PlayerPrefs.SetInt("Rounds without achievement", 0);
        }
    }

    public void ConfirmAchievement(BaseAchievement achievement)
    {
        PlayerPrefs.SetInt(achievement.Name + " confirmed", 1);
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + achievement.RewardCoins);

        PlayGamesManager.UnlockAchievement(achievement.ID);

        UiController.instance.UpdateCoinsText();

        AchievementBrowser.instance.PopulateAchievementBrowser();
    }

    public void ThatlLearnYa()
    {
        SetAchievementPlayerPrefs("That'l learn ya");
    }

    public void Doh()
    {
        SetAchievementPlayerPrefs("Doh!");
    }

    public void MegaSpace()
    {
        SetAchievementPlayerPrefs("Mega space!");
    }

    public void CosmicBurger()
    {
        SetAchievementPlayerPrefs("Cosmic burger");
    }

    public void SortPlanetAchievements(BaseRockController rockController)
    {
        if (rockController.activeMesh == rockController.rockMesh)
        {
            SetBiggestPlanetEaten(0);
        }
        if (rockController.activeMesh == rockController.moonMesh)
        {
            MoonageDavedream();
            SetBiggestPlanetEaten(1);
        }
        if (rockController.activeMesh == rockController.plutoMesh)
        {
            Plutonians();
            SetBiggestPlanetEaten(2);
        }
        if (rockController.activeMesh == rockController.marsMesh)
        {
            Martian();
            SetBiggestPlanetEaten(3);
        }
        if (rockController.activeMesh == rockController.earthMesh)
        {
            Earth();
            SetBiggestPlanetEaten(4);
        }
        if (rockController.activeMesh == rockController.uranusMesh)
        {
            Uranus();
            SetBiggestPlanetEaten(5);
        }
        if (rockController.activeMesh == rockController.saturnMesh)
        {
            Saturn();
            SetBiggestPlanetEaten(6);
        }
        if (rockController.activeMesh == rockController.jupiterMesh)
        {
            Jupiter();
            SetBiggestPlanetEaten(7);
        }
        if (rockController.activeMesh == rockController.sunMesh)
        {
            Sun();
            SetBiggestPlanetEaten(8);
        }
    }

    void SetBiggestPlanetEaten(int dictionaryIndex)
    {
        if(dictionaryIndex > PlayerPrefs.GetInt("Biggest planet eaten"))
        {
            PlayerPrefs.SetInt("Biggest planet eaten", dictionaryIndex);
        }
    }

    public void MoonageDavedream()
    {
        SetAchievementPlayerPrefs("Moonage Davedream");
    }

    public void Plutonians()
    {
        SetAchievementPlayerPrefs("Plutonians");
    }

    public void Martian()
    {
        SetAchievementPlayerPrefs("Martian");
    }

    public void Earth()
    {
        SetAchievementPlayerPrefs("Earth");
    }

    public void Uranus()
    {
        SetAchievementPlayerPrefs("Uranus");
    }

    public void Saturn()
    {
        SetAchievementPlayerPrefs("Saturn");
    }

    public void Jupiter()
    {
        SetAchievementPlayerPrefs("Jupiter");
    }

    public void Sun()
    {
        SetAchievementPlayerPrefs("Sun");
    }

    void SirVivor()
    {
        SetAchievementPlayerPrefs("Sir Vivor");
    }

    void XRaySpecs()
    {
        SetAchievementPlayerPrefs("X ray specs");
    }

    public void MeteorHit()
    {
        meteorHitCount++;
        if (meteorHitCount > 4)
            SirVivor();
    }

    public void HandleXRaySpecs()
    {
        if (sploogeOnScreen)
        {
            xRaySpecsCount++;

            if(xRaySpecsCount > 1)
                XRaySpecs();
        }
        else
        {
            xRaySpecsCount = 0;
        }
    }

    public void SizeAchievements(int newMaxSize)
    {
        if (newMaxSize > 999 && newMaxSize < 5000)
        {
            Pebble();
        }
        else if (newMaxSize > 4999 && newMaxSize < 10000)
        {
            Boulder();
        }
        else if (newMaxSize > 10000)
        {
            TheRock();
        }
    }

    void Pebble()
    {
        SetAchievementPlayerPrefs("Pebble");
    }

    void Boulder()
    {
        SetAchievementPlayerPrefs("Boulder");
    }

    void TheRock()
    {
        SetAchievementPlayerPrefs("The Rock");
    }

    public void RoundTimeAchievements(int roundTime)
    {
        if (roundTime >= 90 && roundTime < 180)
        {
            JustASec();
        }
        else if (roundTime >= 180 && roundTime < 300)
        {
            LongHaul();
        }
        else if (roundTime >= 300)
        {
            Marathon();
        }
    }

    void JustASec()
    {
        SetAchievementPlayerPrefs("Just a sec");
    }

    void LongHaul()
    {
        SetAchievementPlayerPrefs("Long haul");
    }

    void Marathon()
    {
        SetAchievementPlayerPrefs("Marathon");
    }

    public void TotalPlayTimeAchievements()
    {
        int totalPlayTime = PlayerPrefs.GetInt("Total play time");
        if (totalPlayTime >= 1800 && totalPlayTime < 7200)
        {
            JustAMin();
        }
        else if (totalPlayTime >= 7200 && totalPlayTime < 86400)
        {
            TakeYerTime();
        }
        else if (totalPlayTime >= 86400)
        {
            LongWinder();
        }
    }

    void JustAMin()
    {
        SetAchievementPlayerPrefs("Just a minute");
    }

    void TakeYerTime()
    {
        SetAchievementPlayerPrefs("Take yer time");
    }

    void LongWinder()
    {
        SetAchievementPlayerPrefs("Long wind-er");
    }

    public void TotalRocksEatenAchievements()
    {
        int rocksEaten = PlayerPrefs.GetInt("Rocks eaten");
        if(rocksEaten >= 5000 && rocksEaten < 10000)
        {
            Peckish();
        }
        if(rocksEaten >= 10000 && rocksEaten < 20000)
        {
            Hungry();
        }
        if(rocksEaten >= 20000)
        {
            FerociousAppetite();
        }
    }

    void Peckish()
    {
        SetAchievementPlayerPrefs("Peckish");
    }

    void Hungry()
    {
        SetAchievementPlayerPrefs("Hungry");
    }

    void FerociousAppetite()
    {
        SetAchievementPlayerPrefs("Ferocious appetite");
    }

    public void SecretCoins()
    {
        SetAchievementPlayerPrefs("Secret coins!");
        Start();
    }

    void SetAchievementPlayerPrefs(string name)
    {
        if (PlayerPrefs.GetInt(name + " confirmed") == 0)
        {
            if (PlayerPrefs.GetInt(name + " achieved") == 0)
            {
                PlayerPrefs.SetInt(name + " achieved", 1);
            }
        }
    }

    public void ResetAchievements() // probably remove this
    {
        foreach (BaseAchievement ach in achievements)
        {
            PlayerPrefs.SetInt(ach.Name + " confirmed", 0);
            PlayerPrefs.SetInt(ach.Name + " achieved", 0);
        }
        PlayerPrefs.SetInt("Rounds without achievement", 0);
    }
}
