using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreOpenDetector : MonoBehaviour
{
    private void OnEnable()
    {
        Extensions.PlayerPrefsSetBool("Store opened", true);
        PointersControl.Instance.Init();
    }
}
