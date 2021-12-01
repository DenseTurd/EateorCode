using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementData : MonoBehaviour
{
    public TMP_Text achievementName;
    public TMP_Text reward;
    public TMP_Text description;
    public TMP_Text requirement;

    public Image icon;

    public void OpenPlayGamesAchievements()
    {
        PlayGamesManager.ShowAchievementsUI();
    }
}
