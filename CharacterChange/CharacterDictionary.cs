using System.Collections.Generic;
using UnityEngine;

public class CharacterDictionary : MonoBehaviour
{
    public Dictionary<int, GameObject> characters;

    public GameObject eateor;
    public GameObject meatemor;
    public GameObject seateor;
    public GameObject heateor;
    public GameObject beatleor;
    public GameObject beeteor;
    public GameObject feeteor;
    public GameObject sheteor;
    public GameObject sheeteor;
    public GameObject teateor;
    public GameObject animeteor;

    void Start()
    {
        characters = new Dictionary<int, GameObject>();
        characters.Add(0, eateor);
        characters.Add(1, meatemor);
        characters.Add(2, seateor);
        characters.Add(3, heateor);
        characters.Add(4, beatleor);
        characters.Add(5, beeteor);
        characters.Add(6, feeteor);
        characters.Add(7, sheteor);
        characters.Add(8, sheeteor);
        characters.Add(9, teateor);
        characters.Add(10, animeteor);

        MeshSwapper.instance.MeshSwap(PlayerPrefs.GetInt("CharIndex"));
    }
}
