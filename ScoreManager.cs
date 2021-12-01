using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float score;
    public int bonkCount;
    public bool bonkCheck;

    public static ScoreManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if (GameManager.play)
        {
            score += Time.deltaTime * 3f;
        }

        if (!bonkCheck)
        {
            if (bonkCount >= 50)
            {
                AchievementManager.instance.Doh();
                bonkCheck = true;
            }
        }
    }

    public void AddScore(float scoreToAdd)
    {
        if (GameManager.play)
        {
            score += scoreToAdd;
            HUDController.instance.UpdateScoreText();
        }
    }

    public void AddScore(float scoreToAdd, Vector3 pos)
    {
        if (!RoundOverManager.instance.roundOver)
        {
            AddScore(scoreToAdd);
            SplashScores.Instance.SplashScore(scoreToAdd, pos);
        }
    }

    public void SetHighScore()
    {
        if(score > PlayerPrefs.GetFloat("HighScore"))
        {
            StatManager.instance.SetStat(new HighScore(), (int)score);
            PlayerPrefs.SetFloat("HighScore", score);
            PlayGamesManager.AddScoreToLeaderboard(GPGSIds.leaderboard_leaderboard, (long)score);
            Debug.Log("New high score");
        }
    }
}
