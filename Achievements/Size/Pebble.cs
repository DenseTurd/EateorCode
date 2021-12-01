using UnityEngine.UI;

public class Pebble : BaseAchievement
{
    public Pebble()
    {
        Name = "Pebble";
        Description = "Do you even LEO bro.";
        Requirements = "Reach size 1000.";
        RewardCoins = 15;
        RequirementsVisible = true;
        Icon = Icons.instance.ThatlLearnYa;
        ID = GPGSIds.achievement_pebble;
    }
}
