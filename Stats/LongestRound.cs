using UnityEngine;
public class LongestRound : Stat
{
    public LongestRound()
    {
        Name = "Longest round";
        IntValue = PlayerPrefs.GetInt(Name);
        StatType = StatType.Int;
    }
}
