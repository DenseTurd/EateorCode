using UnityEngine.UI;

public class Hungry : BaseAchievement
{
    public Hungry()
    {
        Name = "Hungry";
        Description = "Food coma imminent.";
        Requirements = "Eat 10,000 rocks total.";
        RewardCoins = 75;
        RequirementsVisible = true;
        Icon = Icons.instance.ThatlLearnYa;
        ID = GPGSIds.achievement_hungry;
    }
}
