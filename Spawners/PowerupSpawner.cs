using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject gravityPowerupPrefab;
    public GameObject satiationPowerupPrefab;
    public GameObject slowmoPowerupPrefab;
    public GameObject cosmicBurgerPrefab;

    Vector3 spawnPosition;
    float randomisedScale;
    bool gravityPowerupAvaliable;
    bool satiationPowerupAvaliable;
    bool slowmoPowerupAvaliable;
    bool initiatedSlowmoPowerup;

    public float gravityPowerupAvaliableTime;
    public float satiationPowerupAvaliableTime;
    float slowmoPowerupAvaliableTime;
    public float slowmoDelay;

    public static PowerupSpawner Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void Init()
    {
        FunFactor.instance.powerupSpawner = this;
    }

    private void Update()
    {
        if (GameManager.play)
        {
            if (PlayerController.instance != null)
            {
                HandleGravityPowerup();
                HandleSatiationPowerup();
                HandleSlowmoPowerup();
            }
        }
    }

    private void HandleSlowmoPowerup()
    {
        // Spawn slowmo powerup if something
        if (slowmoPowerupAvaliable && FunFactor.instance.onARoll)
        {
            DelaySlowmoPowerup();
        }
        if (Time.time > slowmoPowerupAvaliableTime && !slowmoPowerupAvaliable)
        {
            slowmoPowerupAvaliable = true;
        }

        if (slowmoDelay <= 0 && slowmoPowerupAvaliable && initiatedSlowmoPowerup)
        {
            slowmoPowerupAvaliable = false;
            slowmoPowerupAvaliableTime = Time.time + 25f;
            initiatedSlowmoPowerup = false;
            SpawnSlowmoPowerup();
        }
    }

    void DelaySlowmoPowerup()
    {
        if (!initiatedSlowmoPowerup)
        {
            slowmoDelay = 4f;
            initiatedSlowmoPowerup = true;
            return;
        }
        slowmoDelay -= Time.deltaTime;
    }

    private void HandleSatiationPowerup()
    {
        // Spawn a satiation powerup if player gets too hungry
        if (PlayerController.instance.hunger > PlayerController.instance.satiation * 0.5f && satiationPowerupAvaliable)
        {
            satiationPowerupAvaliable = false;
            satiationPowerupAvaliableTime = Time.time + 20f;
            SpawnSatiationPowerup();
        }
        if (Time.time > satiationPowerupAvaliableTime && !satiationPowerupAvaliable)
        {
            satiationPowerupAvaliable = true;
            //Debug.Log("satiation avaliable");
        }
    }

    private void HandleGravityPowerup()
    {
        // Spawn a gravity powerup if player reduces from max size by 25% during a round
        if (PlayerController.instance != null && PlayerController.instance.transform.localScale.x < PlayerController.instance.maxSize * 0.75f && gravityPowerupAvaliable)
        {
            gravityPowerupAvaliable = false;
            gravityPowerupAvaliableTime = Time.time + 20f;
            SpawnGravityPowerup();
        }
        if (Time.time > gravityPowerupAvaliableTime && !gravityPowerupAvaliable)
        {
            gravityPowerupAvaliable = true;
            //Debug.Log("gravityAvaliable");
        }
    }

    public void SpawnGravityPowerup()
    {
        GameObject gravityPowerup = Instantiate(gravityPowerupPrefab);

        int spawnLocationSelector = Random.Range(0, 3);
        if (spawnLocationSelector == 0) // spawn to the left
        {
            spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(-0.6f, -0.4f), Random.Range(-0.2f, 0.6f), PlayerController.instance.camZOffset));
        }
        if (spawnLocationSelector == 1) // spawn to the bottom
        {
            spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(-0.1f, 1.1f), Random.Range(-0.2f, -0.6f), PlayerController.instance.camZOffset));
        }
        if (spawnLocationSelector == 2 || spawnLocationSelector == 4) // spawn to the right
        {
            spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(1.6f, 1.4f), Random.Range(-0.2f, 0.6f), PlayerController.instance.camZOffset));
        }

        gravityPowerup.transform.position = new Vector3(spawnPosition.x, spawnPosition.y, 0);
        randomisedScale = Random.Range(PlayerController.instance.meanScale * 0.4f, PlayerController.instance.meanScale * 1.2f);
        gravityPowerup.transform.localScale = new Vector3(randomisedScale, randomisedScale, randomisedScale);
        //Debug.Log("Spawned gravity powerup");
    }

    public void SpawnSatiationPowerup()
    {
        GameObject satiationPowerup = Instantiate(satiationPowerupPrefab);
        int spawnLocationSelector = Random.Range(0, 4);
        if (spawnLocationSelector == 0) // spawn to the left
        {
            spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(-0.6f, -0.4f), Random.Range(-0.2f, 0.6f), PlayerController.instance.camZOffset));
        }
        if (spawnLocationSelector == 1) // spawn to the bottom
        {
            spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(-0.1f, 1.1f), Random.Range(-0.2f, -0.6f), PlayerController.instance.camZOffset));
        }
        if (spawnLocationSelector == 2 || spawnLocationSelector == 4) // spawn to the right
        {
            spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(1.6f, 1.4f), Random.Range(-0.2f, 0.6f), PlayerController.instance.camZOffset));
        }
        if (spawnLocationSelector == 3) // spawn above
        {
            spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(-0.1f, 1.1f), Random.Range(1.2f, 1.6f), PlayerController.instance.camZOffset));
        }

        satiationPowerup.transform.position = new Vector3(spawnPosition.x, spawnPosition.y, 0);
        randomisedScale = Random.Range(PlayerController.instance.meanScale * 0.4f, PlayerController.instance.meanScale * 1.2f);
        satiationPowerup.transform.localScale = new Vector3(randomisedScale, randomisedScale, randomisedScale);
        //Debug.Log("Spawned satiation powerup");
    }

    public void SpawnSlowmoPowerup()
    {
        GameObject slowmoPowerup = Instantiate(slowmoPowerupPrefab);
        int spawnLocationSelector = Random.Range(0, 4);
        if (spawnLocationSelector == 0) // spawn to the left
        {
            spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(-0.6f, -0.4f), Random.Range(-0.2f, 0.6f), PlayerController.instance.camZOffset));
        }
        if (spawnLocationSelector == 1) // spawn to the bottom
        {
            spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(-0.1f, 1.1f), Random.Range(-0.2f, -0.6f), PlayerController.instance.camZOffset));
        }
        if (spawnLocationSelector == 2 || spawnLocationSelector == 4) // spawn to the right
        {
            spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(1.6f, 1.4f), Random.Range(-0.2f, 0.6f), PlayerController.instance.camZOffset));
        }
        if (spawnLocationSelector == 3) // spawn above
        {
            spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(-0.1f, 1.1f), Random.Range(1.2f, 1.6f), PlayerController.instance.camZOffset));
        }

        slowmoPowerup.transform.position = new Vector3(spawnPosition.x, spawnPosition.y, 0);
        randomisedScale = Random.Range(PlayerController.instance.meanScale * 0.4f, PlayerController.instance.meanScale * 1.2f);
        slowmoPowerup.transform.localScale = new Vector3(randomisedScale, randomisedScale, randomisedScale);
        //Debug.Log("Spawned slowmo powerup");
    }

    public void SpawnCosmicBurgerPowerup()
    {
        GameObject BurgerPowerup = Instantiate(cosmicBurgerPrefab);

        int spawnLocationSelector = Random.Range(0, 3);
        if (spawnLocationSelector == 0) // spawn to the left
        {
            spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(-0.6f, -0.4f), Random.Range(-0.2f, 0.6f), PlayerController.instance.camZOffset));
        }
        if (spawnLocationSelector == 1) // spawn to the bottom
        {
            spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(-0.1f, 1.1f), Random.Range(-0.2f, -0.6f), PlayerController.instance.camZOffset));
        }
        if (spawnLocationSelector == 2 || spawnLocationSelector == 4) // spawn to the right
        {
            spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(1.6f, 1.4f), Random.Range(-0.2f, 0.6f), PlayerController.instance.camZOffset));
        }

        BurgerPowerup.transform.position = new Vector3(spawnPosition.x, spawnPosition.y, 0);
        randomisedScale = Random.Range(PlayerController.instance.meanScale * 0.4f, PlayerController.instance.meanScale * 1.2f);
        BurgerPowerup.transform.localScale = new Vector3(randomisedScale, randomisedScale, randomisedScale);
        //Debug.Log("Spawned cosmic burger powerup");       
    }
}
