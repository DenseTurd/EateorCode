public class MegaSpace : BaseAchievement
{
    public MegaSpace()
    {
        Name = "Mega space!";
        Description = "Its a feature, honest.";
        Requirements = "Teleport Eateor after drifting into deep space.";
        RewardCoins = 100;
        RequirementsVisible = false;
        Icon = Icons.instance.MegaSpace;
        ID = GPGSIds.achievement_mega_space;
    }
}
