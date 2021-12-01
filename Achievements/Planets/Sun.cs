public class Sun : BaseAchievement
{
    public Sun()
    {
        Name = "Sun";
        Description = "Get a bit of a thermonuclear warm on.";
        Requirements = "Eat a Sun.";
        RewardCoins = 66;
        RequirementsVisible = true;
        Icon = Icons.instance.Sun;
        ID = GPGSIds.achievement_sun;
    }
}
