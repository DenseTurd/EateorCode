using UnityEngine;

public class AutoRocksParent : MonoBehaviour
{
    public static AutoRocksParent Instance { get; private set; }
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void DestroyAutoRocks()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        Destroy(gameObject);
    }
}
