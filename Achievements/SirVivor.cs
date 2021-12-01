using UnityEngine.UI;

public class SirVivor : BaseAchievement
{
    public SirVivor()
    {
        Name = "Sir Vivor";
        Description = "Entropy! No thankyou.";
        Requirements = "Survive 5 hits from meteor in a single round";
        RewardCoins = 24;
        RequirementsVisible = true;
        Icon = Icons.instance.ThatlLearnYa;
        ID = GPGSIds.achievement_sir_vivor;
    }
}
