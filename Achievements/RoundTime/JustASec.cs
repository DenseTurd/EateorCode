using UnityEngine.UI;

public class JustASec : BaseAchievement
{
    public JustASec()
    {
        Name = "Just a sec";
        Description = "*Insert funny comment here.*";
        Requirements = "Play a 90 second round.";
        RewardCoins = 14;
        RequirementsVisible = true;
        Icon = Icons.instance.ThatlLearnYa;
        ID = GPGSIds.achievement_just_a_sec;
    }
}
