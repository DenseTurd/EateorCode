using UnityEngine;

public class TutorialObjectsParent : MonoBehaviour
{
    public GameObject prefab;

    public void ResetObjects()
    {
        transform.position = PlayerController.instance.transform.position;
        transform.rotation = PlayerController.instance.transform.rotation;
        foreach (Transform child in transform)
        {
            if (child.childCount == 0)
            {
            var obj = Instantiate(prefab, child);
            }
        }
    }
}
