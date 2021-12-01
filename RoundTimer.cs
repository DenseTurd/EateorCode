
using UnityEngine;

public class RoundTimer : MonoBehaviour
{
    public float roundTime;
    bool roundStarted;

    public static RoundTimer Instance { get; private set; }
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

    public void StartTimer()
    {
        roundStarted = true;
    }

    private void Update()
    {
        if (roundStarted)
            roundTime += Time.deltaTime;
    }

    public void EndRound()
    {
        roundStarted = false;
        StatManager.instance.SetStat(new LongestRound(), (int)roundTime);
        StatManager.instance.SetStat(new TotalPlayTime(), PlayerPrefs.GetInt("Total play time") + (int)roundTime);
        AchievementManager.instance.RoundTimeAchievements((int)roundTime);
        AchievementManager.instance.TotalPlayTimeAchievements();

        Debug.Log("Round Time: " + roundTime);
    }
}
