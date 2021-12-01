using UnityEngine.UI;

public class TheRock : BaseAchievement
{
    public TheRock()
    {
        Name = "The Rock";
        Description = "Dinners ready.";
        Requirements = "Reach size 10,000.";
        RewardCoins = 99;
        RequirementsVisible = true;
        Icon = Icons.instance.ThatlLearnYa;
        ID = GPGSIds.achievement_the_rock;
    }
}
