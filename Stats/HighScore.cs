using UnityEngine;

public class HighScore : Stat
{
    public HighScore()
    {
        Name = "High score";
        IntValue = PlayerPrefs.GetInt(Name);
        StatType = StatType.Int;
    }
}
