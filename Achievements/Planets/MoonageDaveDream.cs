public class MoonageDavedream : BaseAchievement
{
    public MoonageDavedream()
    {
        Name = "Moonage Davedream";
        Description = "Freak out, it's Dave tha moon!";
        Requirements = "Eat tha moon.";
        RewardCoins = 10;
        RequirementsVisible = true;
        Icon = Icons.instance.MoonageDaveDream;
        ID = GPGSIds.achievement_moonage_davedream;
    }
}
