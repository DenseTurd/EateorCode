using System.Collections.Generic;
using UnityEngine;

public class CharacterChanger : MonoBehaviour
{
    public static CharacterChanger instance;

    List<BaseCharacter> characters;

    public GameObject content;
    public GameObject characterPrefab;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Init(List<BaseCharacter> chars)
    {
        characters = chars;
    }

    public void PopulateCharacters()
    {
        foreach(Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }

        foreach(var character in characters)
        {
            GameObject charPrefab = Instantiate(characterPrefab, content.transform);
            CharacterData charData = charPrefab.GetComponent<CharacterData>();
            CharacterButton charButton = charPrefab.GetComponent<CharacterButton>();
            charButton.charIndex = character.CharacterIndex;

            charData.characterName.text = character.Name;

            if(PlayerPrefs.GetInt(character.Name + " unlocked") == 1)
            {
                charData.icon.sprite = character.Icon.sprite;
                charData.description.text = character.Description;
                charData.requirement.text = "Requirements: " + character.Requirements;
            }
            else
            {
                charData.icon.sprite = Icons.instance.QuestionMark.sprite;
                charButton.button.enabled = false;
                charData.description.text = "???";
                charData.requirement.text = "Requirements: ???";
                charData.characterName.color = Color.gray;
                charData.description.color = Color.gray;
                charData.requirement.color = Color.gray;
            }
        }
    }
}
