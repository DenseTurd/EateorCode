using UnityEngine.UI;

public class LongWinder : BaseAchievement
{
    public LongWinder()
    {
        Name = "Long wind-er";
        Description = "Thanks friend!";
        Requirements = "Play for 24 hours total.";
        RewardCoins = 1999;
        RequirementsVisible = true;
        Icon = Icons.instance.ThatlLearnYa;
        ID = GPGSIds.achievement_long_winder;
    }
}
