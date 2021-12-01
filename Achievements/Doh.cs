public class Doh : BaseAchievement
{
    public Doh()
    {
        Name = "Doh!";
        Description = "Bonked off bigger tings 50 times in one round.";
        Requirements = "Bonk off bigger tings 50 times in one round.";
        RewardCoins = 11;
        RequirementsVisible = true;
        Icon = Icons.instance.Doh;
        ID = GPGSIds.achievement_doh;
    }
}
