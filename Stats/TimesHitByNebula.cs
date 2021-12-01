using UnityEngine;

public class TimesHitByNebula : Stat
{
    public TimesHitByNebula()
    {
        Name = "Times Hit By Nebula";
        IntValue = PlayerPrefs.GetInt(Name);
        StatType = StatType.Int;
    }
}
