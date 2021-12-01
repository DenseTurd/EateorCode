using UnityEngine.UI;

public class TakeYerTime : BaseAchievement
{
    public TakeYerTime()
    {
        Name = "Take yer time";
        Description = "Nice one human!";
        Requirements = "Play for 2 hours total.";
        RewardCoins = 321;
        RequirementsVisible = true;
        Icon = Icons.instance.ThatlLearnYa;
        ID = GPGSIds.achievement_take_yer_time;
    }
}
