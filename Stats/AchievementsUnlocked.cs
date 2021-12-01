using UnityEngine;
public class AchievementsUnlocked : Stat
{
    public AchievementsUnlocked()
    {
        Name = "Achievements unlocked";
        IntValue = PlayerPrefs.GetInt(Name);
        StatType = StatType.Int;
    }
}
