using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUnlocker : MonoBehaviour
{
    List<BaseCharacter> characters;
    public List<BaseCharacter> avaliableCharacters;
    public Queue<BaseCharacter> charactersToUnlockQueue;
    public static CharacterUnlocker Instance { get; private set; }

    public Image eateorIcon;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Init();
    }

    void CreateCharList()
    {
        characters = new List<BaseCharacter>
        {
            new Eateor(),
            new Sheteor(),
            new Meatemor(),
            new Seateor(),
            new Heateor(),
            new Beatleor(),
            new Beeteor(),
            new Feeteor(),
            new Sheeteor(),
            new Teateor(),
            new Animeteor()
        };
    }

    void Init()
    {
        CreateCharList();

        //foreach (var debugUnlock in characters)
        //{
        //    SetPlayerPrefs(debugUnlock.Name);
        //}

        UnlockCharactersFromPlayerPrefs();

        CharacterChanger.instance.Init(characters);

        charactersToUnlockQueue = new Queue<BaseCharacter>();
        foreach (BaseCharacter charact in avaliableCharacters)
        {
            if (PlayerPrefs.GetInt(charact.Name + " confirmed") == 0)
            {
                charactersToUnlockQueue.Enqueue(charact);
            }
        }

        while (charactersToUnlockQueue.Count != 0)
        {
            SplashManager.instance.DisplayCharacterUnlock(charactersToUnlockQueue.Dequeue());
            Debug.Log("Dequed character unlock");
        }
    }

    void UnlockCharactersFromPlayerPrefs()
    {
        avaliableCharacters = new List<BaseCharacter>();

        foreach (var character in characters)
        {
            if (PlayerPrefs.GetInt(character.Name + " unlocked") == 1)
            {
                avaliableCharacters.Add(character);
            }
        }
        PlayerPrefs.SetInt("Characters unlocked", avaliableCharacters.Count);
    }

    public void ConfirmUnlock(BaseCharacter unlockedCharacter)
    {
        PlayerPrefs.SetInt(unlockedCharacter.Name + " confirmed", 1);

        CharacterChanger.instance.Init(characters);
    }


    public void Beeteor()
    {
        SetPlayerPrefs("Beeteor");
        Init();
        StoreManager.Instance.Init();
    }

    public void Meatemor() // Filler unlocks do not need to re initialise, this avoids splash screen duping
    {
        SetPlayerPrefs("Meatemor");
    }

    void Seateor() // Filler unlocks do not need to re initialise, this avoids splash screen duping
    {
        SetPlayerPrefs("Seateor");
    }

    void Beatleor() // Filler unlocks do not need to re initialise, this avoids splash screen duping
    {
        SetPlayerPrefs("Beatleor");
    }

    void Heateor() // Filler unlocks do not need to re initialise, this avoids splash screen duping
    {
        SetPlayerPrefs("Heateor");
    }

    void Feeteor() // Filler unlocks do not need to re initialise, this avoids splash screen duping
    {
        SetPlayerPrefs("Feeteor");
    }

    void Sheteor() // Filler unlocks do not need to re initialise, this avoids splash screen duping
    {
        SetPlayerPrefs("Sheteor");
    }

    void Sheeteor() // Filler unlocks do not need to re initialise, this avoids splash screen duping
    {
        SetPlayerPrefs("Sheeteor");
    }

    void Teateor() // Filler unlocks do not need to re initialise, this avoids splash screen duping
    {
        SetPlayerPrefs("Dense-T");
    }

    public void FillerUnlock() // unlock characters if no achievements have happened recently
    {
        if (PlayerPrefs.GetInt("Rounds without achievement") > 0)
        {
            if (PlayerPrefs.GetInt("Meatemor unlocked") == 0)
            {
                Meatemor();
                PlayerPrefs.SetInt("Rounds without achievement", 0);
            }
        }

        if (PlayerPrefs.GetInt("Rounds without achievement") > 1)
        {
            if (PlayerPrefs.GetInt("Seateor unlocked") == 0)
            {
                Seateor();
                PlayerPrefs.SetInt("Rounds without achievement", 0);
            }
        }

        if (PlayerPrefs.GetInt("Rounds without achievement") > 2)
        {
            if (PlayerPrefs.GetInt("Rounds played") > 7)
            {
                if (PlayerPrefs.GetInt("Heateor unlocked") == 0)
                {
                    Heateor();
                    PlayerPrefs.SetInt("Rounds without achievement", 0);
                }
            }
        }

        if (PlayerPrefs.GetInt("Rounds without achievement") > 3)
        {
            if (PlayerPrefs.GetInt("Rounds played") > 11)
            {
                if (PlayerPrefs.GetInt("Beatleor unlocked") == 0)
                {
                    Beatleor();
                    PlayerPrefs.SetInt("Rounds without achievement", 0);
                }
            }
        }

        if (PlayerPrefs.GetInt("Rounds without achievement") > 3)
        {
            if (PlayerPrefs.GetInt("Rounds played") > 16)
            {
                if (PlayerPrefs.GetInt("Feeteor unlocked") == 0)
                {
                    Feeteor();
                    PlayerPrefs.SetInt("Rounds without achievement", 0);
                }
            }
        }

        if (PlayerPrefs.GetInt("Rounds without achievement") > 3)
        {
            if (PlayerPrefs.GetInt("Rounds played") > 21)
            {
                if (PlayerPrefs.GetInt("Sheteor unlocked") == 0)
                {
                    Sheteor();
                    PlayerPrefs.SetInt("Rounds without achievement", 0);
                }
            }
        }

        if (PlayerPrefs.GetInt("Rounds without achievement") > 3)
        {
            if (PlayerPrefs.GetInt("Rounds played") > 26)
            {
                if (PlayerPrefs.GetInt("Sheeteor unlocked") == 0)
                {
                    Sheeteor();
                    PlayerPrefs.SetInt("Rounds without achievement", 0);
                }
            }
        }

        if (PlayerPrefs.GetInt("Rounds without achievement") > 3)
        {
            if (PlayerPrefs.GetInt("Rounds played") > 31)
            {
                if (PlayerPrefs.GetInt("Dense-T unlocked") == 0)
                {
                    Teateor();
                    PlayerPrefs.SetInt("Rounds without achievement", 0);
                }
            }
        }
        Debug.Log("Rounds without achievement " + PlayerPrefs.GetInt("Rounds without achievement"));
    }

    void SetPlayerPrefs(string name)
    {
        if (PlayerPrefs.GetInt(name + " confirmed") == 0)
        {
            if (PlayerPrefs.GetInt(name + " unlocked") == 0)
            {
                PlayerPrefs.SetInt(name + " unlocked", 1);
            }
        }
    }

    public void ResetCharacters() // probably remove this
    {
        CreateCharList();
        foreach (BaseCharacter ch in characters)
        {
            PlayerPrefs.SetInt(ch.Name + " confirmed", 0);
            PlayerPrefs.SetInt(ch.Name + " unlocked", 0);
        }
        UnlockDefaultCharacters();
    }

    void UnlockDefaultCharacters()
    {
        PlayerPrefs.SetInt(new Eateor().Name + " confirmed", 1);
        PlayerPrefs.SetInt(new Eateor().Name + " unlocked", 1);
        UnlockCharactersFromPlayerPrefs();
    }
}
