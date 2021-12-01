using UnityEngine;

public class AmountHealed : Stat
{
    public AmountHealed()
    {
        Name = "Amount healed";
        IntValue = PlayerPrefs.GetInt(Name);
        StatType = StatType.Int;
    }
}
