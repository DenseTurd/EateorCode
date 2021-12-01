public class Uranus : BaseAchievement
{
    public Uranus()
    {
        Name = "Uranus";
        Description = "Not going there.";
        Requirements = "Eat uranus.";
        RewardCoins = 30;
        RequirementsVisible = true;
        Icon = Icons.instance.Uranus;
        ID = GPGSIds.achievement_uranus;
    }
}
