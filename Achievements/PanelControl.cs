using UnityEngine;
using TMPro;

public class PanelControl : MonoBehaviour
{
    public BaseAchievement achievement;

    public TMP_Text achivementNameText;
    public TMP_Text achivementDescriptionText;
    public TMP_Text achivementRequiredText;
    public TMP_Text achievementRewardText;

    private void Start()
    {
        AudioManager.instance.Achievement();
    }

    public void Init()
    {
        achivementNameText.text = achievement.Name;
        achivementDescriptionText.text = achievement.Description;
        achivementRequiredText.text = "Required: " + achievement.Requirements;
        achievementRewardText.text = "Reward: " + achievement.RewardCoins.ToString() + " coins!";
        Debug.Log(achievement.Name + " panel created");
    }
    
    public void Confirm()
    {
        AchievementManager.instance.ConfirmAchievement(achievement);
        AudioManager.instance.Click();

        if (AchievementManager.instance.achievementQueue.Count == 0)
        {
            AudioManager.instance.StopMajesticWhooshing();
        }

        Destroy(this.gameObject);
    }
}
