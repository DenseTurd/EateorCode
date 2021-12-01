public class Martian : BaseAchievement
{
    public Martian()
    {
        Name = "Martian";
        Description = "K-Gnarly";
        Requirements = "Eat mars.";
        RewardCoins = 20;
        RequirementsVisible = true;
        Icon = Icons.instance.Mars;
        ID = GPGSIds.achievement_martian;
    }
}
