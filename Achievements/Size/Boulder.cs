using UnityEngine.UI;

public class Boulder : BaseAchievement
{
    public Boulder()
    {
        Name = "Boulder";
        Description = "Boulderly go where no boulder has bouldly bebouldered.";
        Requirements = "Reach size 5000.";
        RewardCoins = 35;
        RequirementsVisible = true;
        Icon = Icons.instance.ThatlLearnYa;
        ID = GPGSIds.achievement_boulder;
    }
}
