using UnityEngine.UI;

public class LongHaul : BaseAchievement
{
    public LongHaul()
    {
        Name = "Long haul";
        Description = "Be sure to hydrate.";
        Requirements = "Play a 180 second round.";
        RewardCoins = 49;
        RequirementsVisible = true;
        Icon = Icons.instance.ThatlLearnYa;
        ID = GPGSIds.achievement_long_haul;
    }
}
