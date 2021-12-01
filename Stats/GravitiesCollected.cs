using UnityEngine;

public class GravitiesCollected : Stat
{
    public GravitiesCollected()
    {
        Name = "Gravities Collected";
        IntValue = PlayerPrefs.GetInt(Name);
        StatType = StatType.Int;
    }
}
