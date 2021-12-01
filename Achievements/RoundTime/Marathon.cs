using UnityEngine.UI;

public class Marathon : BaseAchievement
{
    public Marathon()
    {
        Name = "Marathon";
        Description = "Cosmic chompin long time.";
        Requirements = "Play a 300 second round.";
        RewardCoins = 300;
        RequirementsVisible = true;
        Icon = Icons.instance.ThatlLearnYa;
        ID = GPGSIds.achievement_marathon;
    }
}
