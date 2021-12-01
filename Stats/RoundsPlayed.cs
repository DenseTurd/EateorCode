using UnityEngine;
public class RoundsPlayed : Stat
{
    public RoundsPlayed()
    {
        Name = "Rounds played";
        IntValue = PlayerPrefs.GetInt(Name);
        StatType = StatType.Int;
    }
}
