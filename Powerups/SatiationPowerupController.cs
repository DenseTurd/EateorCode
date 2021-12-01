using UnityEngine;

public class SatiationPowerupController : MonoBehaviour, Ipowerup
{
    Vector3 dir;
    int catchAttempts;
    bool makingPass;
    float randomisedScale;
    float viewportDistFromPlayer;
    PowerupParticleSystem powerupParticleSystem;

    void Start()
    { 
        dir = ((Vector2)PlayerController.instance.transform.position - (Vector2)transform.position);
        catchAttempts = 3;
        powerupParticleSystem = GetComponent<PowerupParticleSystem>();
    }

    private void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        bool inBounds = screenPoint.x > -1 && screenPoint.x < 2 && screenPoint.y > -1 && screenPoint.y < 2;
        if (!inBounds)
        {
            if (makingPass)
            {
                catchAttempts -= 1;
                makingPass = false;
                Respawn();
                dir = ((Vector2)PlayerController.instance.transform.position - (Vector2)transform.position);
            }
        }

        // used to increase speed of star when outside view
        viewportDistFromPlayer = (new Vector3(0f, 0f, 0) - new Vector3(screenPoint.x -0.5f, screenPoint.y -0.5f, 0)).magnitude;
        
        if (inBounds)
        {
            makingPass = true;
        }

        if (catchAttempts < 1)
        {
            Destroy(this.gameObject);
            //Debug.Log("Powerup destroyed after too many passes");
        }

        MoveTowardPlayer();
    }

    void MoveTowardPlayer()
    {
        if(PlayerController.instance != null)
        {
            var speedAdjust = (viewportDistFromPlayer * viewportDistFromPlayer * viewportDistFromPlayer) + 0.4f;
            transform.Translate((Vector3)dir.normalized * (PlayerController.instance.maxSpeed * 1.6f * speedAdjust) * Time.deltaTime, Space.World);
        }
    }

    public void Respawn()
    {
        randomisedScale = Random.Range(PlayerController.instance.meanScale * 0.4f, PlayerController.instance.meanScale * 1.2f);
        transform.localScale = new Vector3(randomisedScale, randomisedScale, randomisedScale);
        //Debug.Log("Respawned satiation powerup");
    }

    public void Powerup()
    {
        PlayerController.instance.hunger = 0;

        powerupParticleSystem.SpawnParticles(0);

        HUDController.instance.UpdateHungerBar();

        HUDController.instance.HungerBGoneSplash();

        AudioManager.instance.HungerBGone();

        ScoreManager.instance.AddScore(Random.Range(19, 28), transform.position);

        PlayerPrefs.SetInt("Hunger-B-Gone collected", PlayerPrefs.GetInt("Hunger-B-Gone collected") + 1);

        Destroy(this.gameObject);
    }
}
