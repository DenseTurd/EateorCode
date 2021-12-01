using UnityEngine;

public class StarController : MonoBehaviour
{
    Vector3 respawnPosition;
    float twinkleSpeed;
    float scale;
    float maxScale;
    float minScale;
    float lerpVal;
    bool growing;

    private void Start()
    {
        RandomRotation();
        twinkleSpeed = Random.Range(0.8f, 3f);
        scale = 4;
        maxScale = Random.Range(scale, scale * 1.6f);
        minScale = Random.Range(scale * 0.3f, scale * 0.9f);
        lerpVal = 0;
        growing = true;
    }

    public void Respawn()
    {
        if (PlayerController.instance != null)
        {
            int spawnLocationSelector = Random.Range(0, 3);
            if (spawnLocationSelector == 0) // spawn to the left
            {
                respawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(-0.2f, -0.1f), UnityEngine.Random.Range(-0.1f, 0.7f), 800 + PlayerController.instance.camZOffset + (PlayerController.PlayerScale * 4)));
            }
            if (spawnLocationSelector == 1) // spawn to the bottom
            {
                respawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(-0.1f, 1.1f), UnityEngine.Random.Range(-0.1f, -0.2f), 800 + PlayerController.instance.camZOffset + (PlayerController.PlayerScale * 4)));
            }
            if (spawnLocationSelector == 2) // spawn to the right
            {
                respawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(1.1f, 1.2f), UnityEngine.Random.Range(-0.1f, 0.7f), 800 + PlayerController.instance.camZOffset + (PlayerController.PlayerScale * 4)));
            }
            transform.position = respawnPosition;
            scale = PlayerController.PlayerScale * 2;
        }
        RandomRotation();
    }

    void RandomRotation()
    {
        transform.Rotate(Random.Range(0, 359), Random.Range(0, 359), Random.Range(0, 359), Space.World);
    }

    private void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        bool onScreen = screenPoint.x > -0.2f && screenPoint.x < 1.2f && screenPoint.y > -0.2f && screenPoint.y < 1.2f;
        if (!onScreen)
        {
            Respawn();
        }
        Twinkle();
    }

    void Twinkle()
    {
        if (growing)
        {
            scale = Mathf.Lerp(minScale, maxScale, lerpVal);
            lerpVal += Time.deltaTime * twinkleSpeed;
        }
        else
        {
            scale = Mathf.Lerp(minScale, maxScale, lerpVal);
            lerpVal -= Time.deltaTime * twinkleSpeed;
        }
        if(lerpVal >= 1)
        {
            growing = false;
        }
        if (lerpVal <= 0)
        {
            growing = true;
        }
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
