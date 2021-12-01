using UnityEngine.UI;

public class Peckish : BaseAchievement
{
    public Peckish()
    {
        Name = "Peckish";
        Description = "I feel a munch approaching..";
        Requirements = "Eat 5000 rocks total.";
        RewardCoins = 50;
        RequirementsVisible = true;
        Icon = Icons.instance.ThatlLearnYa;
        ID = GPGSIds.achievement_peckish;
    }
}
