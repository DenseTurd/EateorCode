using UnityEngine;

public class MaxSize : Stat
{
    public MaxSize()
    {
        Name = "Max size";
        IntValue = PlayerPrefs.GetInt(Name);
        StatType = StatType.Int;
    }
}
