using UnityEngine;

public class MeteorController : MonoBehaviour
{
    Vector3 dir;

    public DangerIndicator dangerIndicator;

    public bool assassin;

    DangerTrailSystem dangerTrailSystem;
    RockParticleSystem rockParticleSystem;

    SphereCollider collider;

    public GameObject cometMesh;
    public GameObject splosion1;
    public GameObject splosion2;
    float nextSplosionMeshTimer;

    bool enteredPlayZone;

    float twistDir;

    private void Start()
    {
        enteredPlayZone = false;
        dir = ((Vector2)PlayerController.instance.transform.position - (Vector2)transform.position);
        dangerIndicator.danger = gameObject;
        dangerIndicator.dangerOnScreen = true;

        transform.rotation = Quaternion.LookRotation(dir);
        dangerTrailSystem = GetComponentInChildren<DangerTrailSystem>();
        rockParticleSystem = GetComponentInChildren<RockParticleSystem>();

        collider = GetComponent<SphereCollider>();

        twistDir = Random.Range(-360, 361);

        AudioManager.instance.MeteorRumble();
    }
    private void Update()
    {
        if (PlayerController.instance != null)
        {
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
            dangerIndicator.dangerDistToPlayer = Vector3.Distance(Camera.main.WorldToViewportPoint(PlayerController.instance.transform.position), screenPoint);

            HandleRumbleVolume();

            bool inPlayZone = screenPoint.x > -2 && screenPoint.x < 3 && screenPoint.y > -1.1 && screenPoint.y < 1.7f;
            if (!enteredPlayZone && inPlayZone)
            {
                enteredPlayZone = true;
            }

            bool inBounds = screenPoint.x > -4 && screenPoint.x < 5 && screenPoint.y > -4 && screenPoint.y < 3.7f;
            if (!inBounds && enteredPlayZone)
            {
                DestroyThem();
                //Debug.Log("Meteor destroyed as not in bounds");
            }

            bool inBigBounds = screenPoint.x > -5 && screenPoint.x < 6 && screenPoint.y > -6 && screenPoint.y < 5.7f;
            if (!inBigBounds)
            {
                DestroyThem();
                //Debug.Log("Meteor destroyed as not in big bounds");
            }


            if (GetComponent<MeshRenderer>().isVisible)
            {
                dangerIndicator.dangerOnScreen = true;
            }
            else
            {
                dangerIndicator.dangerOnScreen = false;
            }

            MoveTowardPlayer();

            if (collider.enabled == false)
            {
                nextSplosionMeshTimer -= Time.deltaTime;
                if (nextSplosionMeshTimer <= 0 && splosion1.activeSelf)
                {
                    splosion1.SetActive(false);
                    splosion2.SetActive(true);
                    nextSplosionMeshTimer = 0.09f;
                }
                if (nextSplosionMeshTimer <= 0 && splosion2.activeSelf)
                {
                    Destroy(transform.parent.gameObject);
                    Destroy(transform.GetChild(0));
                    Destroy(gameObject);
                }
            }

            Twist();
        }
    }

    void HandleRumbleVolume()
    {
        if (!AudioManager.instance.meteorRumble.isPlaying && collider.enabled)
        {
            AudioManager.instance.MeteorRumble();
        }
        float rumbleVol = Mathf.Clamp(((1 - dangerIndicator.dangerDistToPlayer) + 0.5f), 0, 1);
        AudioManager.instance.MeteorRumbleVolume(rumbleVol);
    }

    void MoveTowardPlayer()
    {
        if (PlayerController.instance != null)
        {
            float speed;
            if (assassin)
            {
                speed = PlayerController.instance.maxSpeed * Random.Range(2.9f, 3.7f);
            }
            else
            {
                speed = PlayerController.instance.maxSpeed * Random.Range(0.7f, 1.1f);
            }
            transform.Translate((Vector3)dir.normalized * speed * Time.deltaTime, Space.World);
        }
    }

    public void Splode()
    {
        DangerSpawner.instance.nextMeteorTime += 1f; // surpress meteor frequency increase when hit, wait for the 4th meteor to guarantee the full second is added

        collider.enabled = false;
        cometMesh.SetActive(false);
        splosion1.SetActive(true);
        nextSplosionMeshTimer = 0.09f;
        dangerTrailSystem.Kaboom();
        rockParticleSystem.SpawnParticles(3);
        rockParticleSystem.SpawnParticles(3);

        if (assassin)
        {
            AudioManager.instance.AssassinExplosion();
            AudioManager.instance.ShrinkLong();
        }
        else
        {
            AudioManager.instance.MeteorExplosion();
            AudioManager.instance.ShrinkShort();
        }

        AudioManager.instance.StopMeteorRumble();
        Destroy(dangerIndicator.gameObject);
    }

    public void DestroyThem()
    {

        AudioManager.instance.StopMeteorRumble();
        if (dangerIndicator != null)
        {
            Destroy(dangerIndicator.gameObject);
        }
        
        Destroy(transform.parent.gameObject);
        Destroy(transform.GetChild(0));
        Destroy(gameObject);
    }

    void Twist()
    {
        transform.Rotate(0, 0, twistDir * Time.deltaTime);
    }
}
