using UnityEngine;

public class MeshSwapper : MonoBehaviour
{
    public static MeshSwapper instance;

    public CharacterDictionary characterDictionary;

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

    public void MeshSwap(int characterIndex)
    {
        for (int i = 0; i < characterDictionary.characters.Count; i++)
        {
            characterDictionary.characters[i].SetActive(false);
        }
        characterDictionary.characters[characterIndex].SetActive(true);
        Chara.Name = characterDictionary.characters[characterIndex].name;
        PlayerPrefs.SetInt("CharIndex", characterIndex);
    }

}
