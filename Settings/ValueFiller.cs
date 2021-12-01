using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueFiller : MonoBehaviour
{
    private void OnEnable()
    {
        Settings.instance.DisplayCorrectValues();
    }
}
