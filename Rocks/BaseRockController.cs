using UnityEngine;
public abstract class BaseRockController : MonoBehaviour
{
    public float distanceToPlayer;
    public float randomisedScale;

    public Vector3 respawnPosition;

    public bool hasMotion = false;
    public Vector3 direction;
    public float hasMotionSpeed;

    public float gravitySpeed;

    public bool respawnInitiated;
    float respawnTime;
    public SphereCollider collider;
    public RockDictionary rockDictionary;

    public GameObject activeMesh;

    public GameObject rockMesh;
    public GameObject moonMesh;
    public GameObject plutoMesh;
    public GameObject marsMesh;
    public GameObject earthMesh;
    public GameObject uranusMesh;
    public GameObject saturnMesh;
    public GameObject jupiterMesh;
    public GameObject sunMesh;

    public GameObject splosion1;
    public GameObject splosion2;
    public float nextSplosionMeshTime;
    public PlayerGraphicsRotation graphicsRotation;

    public RockParticleSystem rockParticleSystem;

    public int currentMeshIndex;

    bool eatenOnce;

    public bool selected;

    void Start()
    {
        collider = GetComponent<SphereCollider>();
        respawnInitiated = false;
        graphicsRotation = GetComponent<PlayerGraphicsRotation>();
        rockDictionary = GetComponent<RockDictionary>();
        AssignMeshes(0);

        rockParticleSystem = GetComponent<RockParticleSystem>();
        currentMeshIndex = 0;
    }

    public virtual void Respawn()
    {
        MeshSwap();

        DecideIfHasMotion();

        SelectRespawnPosition();

        transform.position = respawnPosition;
        randomisedScale = Random.Range(PlayerController.instance.meanScale * 0.6f, PlayerController.instance.meanScale * 1.4f);

        float playerScale = PlayerController.PlayerScale;
        randomisedScale = Mathf.Min(Random.Range(playerScale * 2, playerScale * 2.5f), randomisedScale);
        transform.localScale = new Vector3(randomisedScale, randomisedScale, randomisedScale);

        collider.enabled = true;
        activeMesh.SetActive(true);
        graphicsRotation.enabled = true;

        ClearSelected();
    }

    public void MeshSwap()
    {
        // change mesh based on player size
        if (PlayerController.PlayerScale >= 2 && PlayerController.PlayerScale < 6) // Moon
        {
            ChooseAppropriateYetRandomMesh(0, 0, 1);
        }
        if (PlayerController.PlayerScale >= 6 && PlayerController.PlayerScale < 20) //pluto
        {
            ChooseAppropriateYetRandomMesh(0, 1, 2);
        }
        if (PlayerController.PlayerScale >= 20 && PlayerController.PlayerScale < 70) //mars
        {
            ChooseAppropriateYetRandomMesh(1, 2, 3);
        }
        if (PlayerController.PlayerScale >= 70 && PlayerController.PlayerScale < 230) //earth
        {
            ChooseAppropriateYetRandomMesh(2, 3, 4);
        }
        if (PlayerController.PlayerScale >= 230 && PlayerController.PlayerScale < 600) //uranus
        {
            ChooseAppropriateYetRandomMesh(3, 4, 5);
        }
        if (PlayerController.PlayerScale >= 600 && PlayerController.PlayerScale < 1000) //saturn
        {
            ChooseAppropriateYetRandomMesh(4, 5, 6);
        }
        if (PlayerController.PlayerScale >= 1000 && PlayerController.PlayerScale < 1500) //jupiter
        {
            ChooseAppropriateYetRandomMesh(5, 6, 7);
        }
        if (PlayerController.PlayerScale >= 1500 && PlayerController.PlayerScale < 3000) //sun
        {
            ChooseAppropriateYetRandomMesh(6, 7, 8);
        }
        else if (PlayerController.PlayerScale < 2) // rock
        {
            ChooseAppropriateYetRandomMesh(0, 0, 0);
        }
    }

    void ChooseAppropriateYetRandomMesh(int index1, int index2, int index3)
    {
        if (Random.Range(0, 2) == 1)
        {
            DeactivateMeshes();
            var indexes = new int[] { index1, index2, index3 };
            AssignMeshes(indexes[Random.Range(0, 3)]);
        }
    }

    void AssignMeshes(int index)
    {
        activeMesh = rockDictionary.RocksDictionary[index].Rok;
        splosion1 = rockDictionary.RocksDictionary[index].Pop1;
        splosion2 = rockDictionary.RocksDictionary[index].Pop2;
        currentMeshIndex = index;
    }

    void DeactivateMeshes()
    {
        rockMesh.SetActive(false);
        moonMesh.SetActive(false);
        plutoMesh.SetActive(false);
        marsMesh.SetActive(false);
        earthMesh.SetActive(false);
        uranusMesh.SetActive(false);
        saturnMesh.SetActive(false);
        jupiterMesh.SetActive(false);
        sunMesh.SetActive(false);
    }

    public void SelectRespawnPosition()
    {
        int spawnLocationSelector = Random.Range(0, 5); // rocks more likely to spawn to the left or right to reward interaction
        if (spawnLocationSelector == 0 || spawnLocationSelector == 3) // spawn to the left
        {
            respawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(-0.6f, -0.4f), Random.Range(-0.2f, 0.6f), PlayerController.instance.camZOffset));
        }
        if (spawnLocationSelector == 1) // spawn to the bottom
        {
            respawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(-0.1f, 1.1f), Random.Range(-0.2f, -0.6f), PlayerController.instance.camZOffset));
        }
        if (spawnLocationSelector == 2 || spawnLocationSelector == 4) // spawn to the right
        {
            respawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(1.6f, 1.4f), Random.Range(-0.2f, 0.6f), PlayerController.instance.camZOffset));
        }
    }

    public void DecideIfHasMotion()
    {
        if (Random.Range(0, 4) == 0)
        {
            hasMotion = true;
            direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            hasMotionSpeed = Random.Range(1f, PlayerController.instance.maxSpeed / 2);
        }
        else
        {
            hasMotion = false;
        }
    }

    private void Update()
    {
        OutOfBoundsRespawn();

        if (hasMotion)
        {
            MoveMe(direction, hasMotionSpeed);
        }

        if (respawnInitiated && Time.time > respawnTime)
        {
            respawnInitiated = false;
            Respawn();
        }

        if (PlayerController.instance != null)
        {
            HandleGravity();
            HandleEmergencyGravity();

        }

        if (collider.enabled == false)
        {
            if (Time.time > nextSplosionMeshTime && splosion1.activeSelf == true)
            {
                splosion1.SetActive(false);
                splosion2.SetActive(true);
                nextSplosionMeshTime = Time.time + 0.13f;
            }
            if (Time.time > nextSplosionMeshTime && splosion2.activeSelf == true)
            {
                splosion2.SetActive(false);
                InitiateRespawn();
            }
        }

        if (!eatenOnce)
        {
            if(RoundTimer.Instance.roundTime > 3)
            {
                eatenOnce = true;
            }
        }
    }

    void HandleEmergencyGravity()
    {
        if (PlayerController.PlayerScale < 1)
        {
            float playerMaxSpeed = PlayerController.instance.maxSpeed;
            gravitySpeed = Random.Range(playerMaxSpeed / 10, playerMaxSpeed / 5);
            MoveTowardPlayer();
        }
    }

    public virtual void HandleGravity()
    {
        if (PlayerController.instance.gravity == true)
        {
            float playerMaxSpeed = PlayerController.instance.maxSpeed;
            gravitySpeed = Random.Range(playerMaxSpeed / 10, playerMaxSpeed / 2);
            MoveTowardPlayer();
        }
    }

    public virtual void OutOfBoundsRespawn()
    {
        if (eatenOnce)
        {
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
            bool inBounds = screenPoint.x > -0.7 && screenPoint.x < 1.7 && screenPoint.y > -0.7 && screenPoint.y < 1.2;
            if (!inBounds)
            {
                if (!respawnInitiated)
                {
                    InitiateRespawn();
                }
            }
        }
    }

    public void Splode()
    {
        if (!eatenOnce)
            eatenOnce = true;

        collider.enabled = false;
        activeMesh.SetActive(false);
        splosion1.SetActive(true);
        graphicsRotation.enabled = false;
        nextSplosionMeshTime = Time.time + 0.09f;
        FunFactor.instance.OnARoll();

        AudioManager.instance.Crunch();

        if(!RoundOverManager.instance.roundOver)
            AudioManager.instance.Chomp();

        rockParticleSystem.SpawnParticles(currentMeshIndex);

        ClearSelected();
    }

    public void Bonk()
    {
        rockParticleSystem.SpawnBonkParticles(currentMeshIndex);
    }

    public void InitiateRespawn()
    {
        respawnInitiated = true;
        respawnTime = Time.time + 0.1f;

    }

    public virtual void MoveMe(Vector3 direction, float speed)
    {
        if (PlayerController.PlayerScale < 1)
        {
            speed = 10;
        }
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    public void MoveTowardPlayer() // smaller rocks should move towards player much quicker.
    {
        float step = gravitySpeed * Time.deltaTime * Mathf.Pow((PlayerController.PlayerScale / transform.localScale.x), 2);
        transform.position = Vector3.MoveTowards(transform.position, PlayerController.instance.transform.position, step);
    }

    public void ClearSelected()
    {
        if (selected)
        {
            Autoteor.Instance.selectedARock = false;
            selected = false;
        }
    }
}
