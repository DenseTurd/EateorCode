public class Saturn : BaseAchievement
{
    public Saturn()
    {
        Name = "Saturn";
        Description = "Ringy ringy ringy.";
        Requirements = "Eat Saturn.";
        RewardCoins = 42;
        RequirementsVisible = true;
        Icon = Icons.instance.Saturn;
        ID = GPGSIds.achievement_saturn;
    }
}
