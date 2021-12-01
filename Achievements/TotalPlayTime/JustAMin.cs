using UnityEngine.UI;

public class JustAMin : BaseAchievement
{
    public JustAMin()
    {
        Name = "Just a minute";
        Description = "Your really doing it!";
        Requirements = "Play for 30 minutes total.";
        RewardCoins = 186;
        RequirementsVisible = true;
        Icon = Icons.instance.ThatlLearnYa;
        ID = GPGSIds.achievement_just_a_minute;
    }
}
