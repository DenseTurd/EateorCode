public class Earth : BaseAchievement
{
    public Earth()
    {
        Name = "Earth";
        Description = "These guys named their planet after mud.";
        Requirements = "Eat earth.";
        RewardCoins = 25;
        RequirementsVisible = true;
        Icon = Icons.instance.Earth;
        ID = GPGSIds.achievement_earth;
    }
}
