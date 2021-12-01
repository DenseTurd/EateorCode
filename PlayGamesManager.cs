using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class PlayGamesManager : MonoBehaviour
{
    public static PlayGamesManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        SignIn();
    }

    void SignIn()
    {
        Social.localUser.Authenticate(success => { });
    }

    public static void UnlockAchievement(string id)
    {
        Debug.Log("Attempting GPG achievement unlock: " + id);
        Social.ReportProgress(id, 100, success => { Debug.Log("Unlocked " + id); });
    }

    public static void IncrementAchievement(string id, int stepsToIncrement)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
    }

    public static void ShowAchievementsUI()
    {
        Social.ShowAchievementsUI();
        Debug.Log("Open PlayGames achievements.");
    }

    public static void AddScoreToLeaderboard(string leaderboardId, long score)
    {
        Social.ReportScore(score, leaderboardId, success => { Debug.Log("Added score to leaderboard"); });
    }

    public static void ShowLeaderboardUI()
    {
        Social.ShowLeaderboardUI();
        Debug.Log("Show PlayGames leaderboard");
    }
}
