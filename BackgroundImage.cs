using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundImage : MonoBehaviour
{
    
    void Update()
    {
        transform.rotation = GameManager.instance.gameObject.transform.rotation;
    }
}
