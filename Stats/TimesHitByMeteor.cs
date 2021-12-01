using UnityEngine;

public class TimesHitByMeteor : Stat
{
    public TimesHitByMeteor()
    {
        Name = "Times Hit By Meteor";
        IntValue = PlayerPrefs.GetInt(Name);
        StatType = StatType.Int;
    }
}
