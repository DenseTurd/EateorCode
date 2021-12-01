using UnityEngine;

public class CosmicBurgersEaten : Stat
{
    public CosmicBurgersEaten()
    {
        Name = "Cosmic burgers eaten";
        IntValue = PlayerPrefs.GetInt(Name);
        StatType = StatType.Int;
    }
}
