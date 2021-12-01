using UnityEngine;
using TMPro;

public class CharacterUnlockPanelControl : MonoBehaviour
{
    public BaseCharacter character;

    public TMP_Text characterNameText;
    public TMP_Text characterDescriptionText;

    private void Start()
    {
        AudioManager.instance.Achievement();
    }

    public void Init()
    {
        characterNameText.text = character.Name;
        characterDescriptionText.text = character.Description;
        Debug.Log(character.Name + " panel created");
    }
    
    public void Confirm()
    {
        CharacterUnlocker.Instance.ConfirmUnlock(character);
        AudioManager.instance.Click();

        if (AchievementManager.instance.achievementQueue.Count == 0)
        {
            AudioManager.instance.StopMajesticWhooshing();
        }

        Destroy(this.gameObject);
    }
}
