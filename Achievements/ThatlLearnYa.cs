using UnityEngine.UI;

public class ThatlLearnYa : BaseAchievement
{
    public ThatlLearnYa()
    {
        Name = "That'l learn ya";
        Description = "You got the achievement that lets you know achievements are a thing :D have 5 coins :)";
        Requirements = "Play some Eateor.";
        RewardCoins = 5;
        RequirementsVisible = true;
        Icon = Icons.instance.ThatlLearnYa;
        ID = GPGSIds.achievement_thatl_learn_ya;
    }
}
