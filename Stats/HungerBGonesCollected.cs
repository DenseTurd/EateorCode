using UnityEngine;

public class HungerBGonesCollected : Stat
{
    public HungerBGonesCollected()
    {
        Name = "Hunger-B-Gone collected";
        IntValue = PlayerPrefs.GetInt(Name);
        StatType = StatType.Int;
    }
}
