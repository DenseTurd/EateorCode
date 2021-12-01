using UnityEngine;

public class DamageTaken : Stat
{
    public DamageTaken()
    {
        Name = "Damage Taken";
        IntValue = PlayerPrefs.GetInt(Name);
        StatType = StatType.Int;
    }
}
