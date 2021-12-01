public class Plutonians : BaseAchievement
{
    public Plutonians()
    {
        Name = "Plutonians";
        Description = "Make you go cross eyed.";
        Requirements = "Eat pluto.";
        RewardCoins = 15;
        RequirementsVisible = true;
        Icon = Icons.instance.Pluto;
        ID = GPGSIds.achievement_plutonians;
    }
}
