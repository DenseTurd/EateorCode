using UnityEngine.UI;

public class SecretCoins : BaseAchievement
{
    public SecretCoins()
    {
        Name = "Secret coins!";
        Description = "You found the secret coins!";
        Requirements = "Read all of the guide.";
        RewardCoins = 20;
        RequirementsVisible = false;
        Icon = Icons.instance.ThatlLearnYa;
        ID = GPGSIds.achievement_secret_coins;
    }
}
