using UnityEngine.UI;
using UnityEngine;

public class Flashing : MonoBehaviour
{
    float flashTimer = 0.3f;
    Image myImage;

    private void Start()
    {
        myImage = GetComponent<Image>();
    }
    void Update()
    {
        flashTimer -= Time.deltaTime;
        if (flashTimer <= 0)
        {
            myImage.enabled = !myImage.enabled;
            flashTimer = 0.3f;
        }
    }
}
