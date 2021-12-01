using UnityEngine;

public class StoreContentFiller : MonoBehaviour
{
    public void OnEnable()
    {
        StoreManager.Instance.Init();
    }
}
