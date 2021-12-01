using UnityEngine;

public class SlowmosCollected : Stat
{
    public SlowmosCollected()
    {
        Name = "Slowmos Collected";
        IntValue = PlayerPrefs.GetInt(Name);
        StatType = StatType.Int;
    }
}
