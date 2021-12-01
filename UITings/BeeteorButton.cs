using UnityEngine;
using UnityEngine.UI;

public class BeeteorButton : MonoBehaviour
{
    public GameObject purchasedSash;
    void Start()
    {
        Init();
    }

    void Init()
    {
        if (PlayerPrefs.GetInt("Beeteor unlocked") != 0)
        {
            gameObject.GetComponent<Button>().enabled = false;
            purchasedSash.SetActive(true);
        }
    }

    public void OnClick()
    {
        Purchaser.instance.BuyBeeteor();
        Init();
    }
}
