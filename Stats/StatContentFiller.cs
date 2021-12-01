using UnityEngine;

public class StatContentFiller : MonoBehaviour
{
    private void OnEnable()
    {
        StatManager.instance.PopulateStats(transform);
    }
}
