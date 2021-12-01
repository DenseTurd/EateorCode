using UnityEngine;

public class SlowmoPowerupController : MonoBehaviour, Ipowerup
{
    Vector3 dir;
    int catchAttempts;
    bool makingPass;
    float randomisedScale;
    float slowmoTimer;
    bool slowmo;

    float viewportDistFromPlayer;

    SphereCollider collider;
    public MeshRenderer renderer;

    PowerupParticleSystem powerupParticleSystem;

    void Start()
    { 
        dir = ((Vector2)PlayerController.instance.transform.position - (Vector2)transform.position);
        catchAttempts = 3;
        collider = GetComponent<SphereCollider>();
        //renderer = GetComponentInChildren<MeshRenderer>();
        slowmoTimer = 1.5f;
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
        viewportDistFromPlayer = (new Vector3(0f, 0f, 0) - new Vector3(screenPoint.x - 0.5f, screenPoint.y - 0.5f, 0)).magnitude;

        if (inBounds)
        {
            makingPass = true;
        }

        if (catchAttempts < 1 && collider.enabled)
        {
            Destroy(this.gameObject);
            //Debug.Log("Powerup destroyed after too many passes");
        }

        MoveTowardPlayer();

        if (slowmo)
            slowmoTimer -= Time.deltaTime;

        if (slowmoTimer <= 0)
            EndSlowmo();
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
        //Debug.Log("Respawned slowmo powerup");
    }

    public void Powerup()
    {
        Time.timeScale = 0.5f;
        slowmo = true;

        collider.enabled = false;
        renderer.enabled = false;

        powerupParticleSystem.SpawnParticles(1);

        HUDController.instance.SlowMoSplash();

        AudioManager.instance.SlowMo();

        ScoreManager.instance.AddScore(Random.Range(13, 29), transform.position);

        PlayerPrefs.SetInt("Slowmos Collected", PlayerPrefs.GetInt("Slowmos Collected") + 1);
    }

    void EndSlowmo()
    {
        Time.timeScale = 1;
        Destroy(this.gameObject);
    }
}
