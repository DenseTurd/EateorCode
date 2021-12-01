using UnityEngine;

public class RocksEaten : Stat
{
    public RocksEaten()
    {
        Name = "Rocks eaten";
        IntValue = PlayerPrefs.GetInt(Name);
        StatType = StatType.Int;
    }
}
