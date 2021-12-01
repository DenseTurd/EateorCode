public class CosmicBurger : BaseAchievement
{
    public CosmicBurger()
    {
        Name = "Cosmic burger";
        Description = "Galactic gourmet.";
        Requirements = "Chow down on a cosmic burger.";
        RewardCoins = 49;
        RequirementsVisible = true;
        Icon = Icons.instance.MegaSpace;
        ID = GPGSIds.achievement_cosmic_burger;
    }
}
