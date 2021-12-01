using UnityEngine.UI;

public class FerociousAppetite : BaseAchievement
{
    public FerociousAppetite()
    {
        Name = "Ferocious appetite";
        Description = "I could eat a ruler!";
        Requirements = "Eat 20,000 rocks total.";
        RewardCoins = 100;
        RequirementsVisible = true;
        Icon = Icons.instance.ThatlLearnYa;
        ID = GPGSIds.achievement_ferocious_appetite;
    }
}
