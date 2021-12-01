using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    List<Stat> stats;

    public GameObject statPrefab;

    public static StatManager instance;

    public GameObject leaderboardsButton;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        stats = new List<Stat>
        {
            new HighScore(),
            new MaxSize(),
            new LongestRound(),
            new RocksEaten(),
            new BiggestPlanetEaten(),
            new CosmicBurgersEaten(),
            new TimesBonked(),
            new TimesHitByMeteor(),
            new TimesHitByNebula(),
            new HungerBGonesCollected(),
            new SlowmosCollected(),
            new GravitiesCollected(),
            new DamageTaken(),
            new AmountHealed(),
            new TotalPlayTime(),
            new RoundsPlayed(),
            new CharactersUnlocked(),
            new AchievementsUnlocked()
        };
    }

    public void PopulateStats(Transform content)
    {
        foreach(Transform child in content)
        {
            Destroy(child.gameObject);
        }

        Instantiate(leaderboardsButton, content);

        foreach(Stat stat in stats)
        {
            var s = Instantiate(statPrefab, content);
            var statData = s.GetComponent<StatData>();
            statData.statName.text = stat.Name;

            DisplayCorrectStatType(stat, statData);
        }
    }

    void DisplayCorrectStatType(Stat stat, StatData statData)
    {
        if (stat.StatType == StatType.Int)
        {
            statData.statValue.text = stat.IntValue.ToString();
        }
        if (stat.StatType == StatType.Float)
        {
            statData.statValue.text = stat.FloatValue.ToString();
        }
        if (stat.StatType == StatType.String)
        {
            statData.statValue.text = stat.StringValue;
        }
        if (stat.StatType == StatType.Bool)
        {
            if (stat.BoolValue == true)
            {
                statData.statValue.text = "Yeah!";
            }
            else
            {
                statData.statValue.text = "Nope.";
            }
        }
    }

    public void ResetStats()
    {
        foreach(Stat stat in stats)
        {
            PlayerPrefs.SetInt(stat.Name, 0);
        }
    }

    public void SetMaxSize()
    {
        if (PlayerPrefs.GetInt("Max size") < PlayerController.instance.maxSize * 10)
        {
            int newMaxSize = Mathf.RoundToInt(PlayerController.instance.maxSize * 10);
            PlayerPrefs.SetInt("Max size", newMaxSize);
            AchievementManager.instance.SizeAchievements(newMaxSize);
        }
        else
        {
            PlayerPrefs.SetInt("Rounds without beating max size", PlayerPrefs.GetInt("Rounds without beating max size") + 1);
            Debug.Log("Rounds without beating max size: " + PlayerPrefs.GetInt("Rounds without beating max size"));
        }
    }

    public void SetStat(Stat stat, int val)
    {
        if (PlayerPrefs.GetInt(stat.Name) < val)
            PlayerPrefs.SetInt(stat.Name, val);
    }
}
