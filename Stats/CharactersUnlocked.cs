using UnityEngine;
public class CharactersUnlocked : Stat
{
    public CharactersUnlocked()
    {
        Name = "Characters unlocked";
        IntValue = PlayerPrefs.GetInt(Name);
        StatType = StatType.Int;
    }
}
