using UnityEngine.UI;

public class XRaySpecs : BaseAchievement
{
    public XRaySpecs()
    {
        Name = "X ray specs";
        Description = "Even in the face of adversity your spirit cannot be broken.";
        Requirements = "Collect 2 powerups while nebula is on screen.";
        RewardCoins = 28;
        RequirementsVisible = true;
        Icon = Icons.instance.ThatlLearnYa;
        ID = GPGSIds.achievement_x_ray_specs;
    }
}
