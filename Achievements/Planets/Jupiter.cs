public class Jupiter : BaseAchievement
{
    public Jupiter()
    {
        Name = "Jupiter";
        Description = "There's a storm in ya teacup.";
        Requirements = "Eat Jupiter.";
        RewardCoins = 55;
        RequirementsVisible = true;
        Icon = Icons.instance.Jupiter;
        ID = GPGSIds.achievement_jupiter;
    }
}
