using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class AchievementBrowser : MonoBehaviour
{
    public static AchievementBrowser instance;

    public GameObject achievementPrefab;
    public GameObject content;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PopulateAchievementBrowser()
    {
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (BaseAchievement achievement in AchievementManager.instance.achievements)
        {
            if (PlayerPrefs.GetInt(achievement.Name + " achieved") == 1)
            {
                GameObject ach = Instantiate(achievementPrefab, content.transform);

                AchievementData data = ach.GetComponent<AchievementData>();

                data.achievementName.text = achievement.Name;
                data.reward.text = "Reward: " + achievement.RewardCoins.ToString() + " coins";
                data.description.text = achievement.Description;
                data.requirement.text = "Requirements: " + achievement.Requirements;

                data.icon.sprite = achievement.Icon.sprite;
            }
            else // grey out unachieved ones
            {
                if (achievement.RequirementsVisible)
                {
                    GameObject ach = Instantiate(achievementPrefab, content.transform);

                    AchievementData data = ach.GetComponent<AchievementData>();

                    data.achievementName.text = achievement.Name;
                    data.achievementName.color = Color.grey;
                    data.reward.text = "Reward: " + achievement.RewardCoins.ToString() + " coins";
                    data.reward.color = Color.grey;
                    data.description.text = "???";
                    data.description.color = Color.grey;
                    data.requirement.text = "Requirements: " + achievement.Requirements;
                    data.requirement.color = Color.grey;

                    data.icon.sprite = Icons.instance.QuestionMark.sprite;
                }
                else
                {
                    GameObject ach = Instantiate(achievementPrefab, content.transform);

                    AchievementData data = ach.GetComponent<AchievementData>();

                    data.achievementName.text = "???";
                    data.achievementName.color = Color.grey;
                    data.reward.text = "Reward: ???";
                    data.reward.color = Color.grey;
                    data.description.text = "???";
                    data.description.color = Color.grey;
                    data.requirement.text = "Requirements: ???";
                    data.requirement.color = Color.grey;

                    data.icon.sprite = Icons.instance.QuestionMark.sprite;
                }
            }
        }
    }
}
