using UnityEngine;

public enum Prize
{
    Constellation,
    Cosmic,
    Galactic
}
public struct PrizeStruct
{
    public string Title { get; set; }
    public string PrizeName { get; set; }
    public int Value { get; set; }
}

public class RoundOverPrize
{
    int prizeScore = 0;
    public Prize CalculatePrize()
    {
        Prize prize = Prize.Constellation;
        if (RoundTimer.Instance.roundTime > 120)
        {
            prizeScore++;
        }

        if (RoundTimer.Instance.roundTime > 220)
        {
            prizeScore++;
        }
        
        if (PlayerController.instance.maxSize > 200) // Sizes are in code size not in game size
        {
            prizeScore++;
        }

        if (PlayerController.instance.maxSize > 1100) // Sizes are in code size not in game size
        {
            prizeScore++;
        }

        if (PlayerController.instance.maxSize * 10 > PlayerPrefs.GetInt("Max size") * 0.8f) // if within 20% of max size
        {
            prizeScore++;
            Debug.Log("prizescore++ from max size");
        }

        if (ScoreManager.instance.score > PlayerPrefs.GetInt("HighScore") * 0.8f) // if within 20% of high score
        {
            prizeScore++;
            Debug.Log("prizescore++ from high score");
        }

        if (PlayerPrefs.GetInt("NotTurningEnough") > 0)
        {
            prizeScore = 0;
            Debug.Log("prizescore reset due to not turning enough");
        }
           
        if (prizeScore >= 2)
            prize = Prize.Cosmic;

        if (prizeScore >= 5)
            prize = Prize.Galactic;

        return prize;
    }
}
