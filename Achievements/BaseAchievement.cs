using UnityEngine.UI;

public class BaseAchievement 
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Requirements { get; set; }
    public int RewardCoins { get; set; }
    public Image Icon { get; set; }
    public bool RequirementsVisible { get; set; }
    public int Achieved { get; set; } // use ints so we can save in playerprefs
    public int Confirmed { get; set; } // use ints so we can save in playerprefs
    public string ID { get; set; }
}
