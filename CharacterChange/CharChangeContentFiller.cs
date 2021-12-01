using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharChangeContentFiller : MonoBehaviour
{
    private void OnEnable()
    {
        CharacterChanger.instance.PopulateCharacters();
        PlayerPrefs.SetInt("Character change opened", 1);
        PointersControl.Instance.Init();
    }
}
