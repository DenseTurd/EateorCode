using UnityEngine;
public class TotalPlayTime : Stat
{
    public TotalPlayTime()
    {
        Name = "Total play time";
        IntValue = PlayerPrefs.GetInt(Name);
        StatType = StatType.Int;
    }
}
