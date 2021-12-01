using UnityEngine;
public class TimesBonked : Stat
{
    public TimesBonked()
    {
        Name = "Times bonked";
        IntValue = PlayerPrefs.GetInt(Name);
        StatType = StatType.Int;
    }
}
