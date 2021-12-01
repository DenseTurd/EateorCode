using UnityEngine;

public class RockTargetsParent : MonoBehaviour
{
    public void ScaleToPlayerStartingSize()
    {
        float scale = PlayerPrefs.GetFloat("StartingSize");
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
