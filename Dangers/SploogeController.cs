using UnityEngine;

public class SploogeController : MonoBehaviour
{
    Vector3 dir;

    public DangerIndicator dangerIndicator;

    DangerTrailSystem dangerTrailSystem;
    RockParticleSystem rockParticleSystem;

    SphereCollider collider;

    public GameObject cometMesh;
    public GameObject splosion1;
    public GameObject splosion2;
    float nextSplosionMeshTimer;

    float twistDir;

    private void Start()
    {
        dir = ((Vector2)PlayerController.instance.transform.position - (Vector2)transform.position);
        dangerIndicator.danger = gameObject;
        dangerIndicator.dangerOnScreen = true;

        transform.rotation = Quaternion.LookRotation(dir);

        dangerTrailSystem = GetComponentInChildren<DangerTrailSystem>();
        rockParticleSystem = GetComponentInChildren<RockParticleSystem>();

        collider = GetComponent<SphereCollider>();

        twistDir = Random.Range(-360, 361);

        AudioManager.instance.CometRumble();

    }
    private void Update()
    {
        if (PlayerController.instance != null)
        {
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
            dangerIndicator.dangerDistToPlayer = Vector3.Distance(Camera.main.WorldToViewportPoint(PlayerController.instance.transform.position), screenPoint);
            HandleRumbleVolume();

            bool inBounds = screenPoint.x > -3 && screenPoint.x < 4 && screenPoint.y > -4 && screenPoint.y < 3.7f;
            if (!inBounds)
            {
                DestroyThem();
                //Debug.Log("Comet destroyed as not in bounds");
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
                    Destroy(gameObject);
                }
            }

            Twist();
        }
    }

    void HandleRumbleVolume()
    {
        if (!AudioManager.instance.cometRumble.isPlaying && collider.enabled)
        {
            AudioManager.instance.CometRumble();
        }
        float rumbleVol = Mathf.Clamp(((1 - dangerIndicator.dangerDistToPlayer) + 0.2f), 0, 1);
        AudioManager.instance.CometRumbleVolume(rumbleVol);
    }

    void MoveTowardPlayer() // Make a hybrid movement ting, homing then straight, maybe a couple of cycles
    {
        if (PlayerController.instance != null)
        {
            float speed;
            
            speed = PlayerController.instance.maxSpeed * 0.9f;
            
            transform.Translate((Vector3)dir.normalized * speed * Time.deltaTime, Space.World);
        }
    }

    public void SploogeScreen()
    {
        if (!RoundOverManager.instance.roundOver)
        {
            HUDController.instance.Splooge();
        }

        Splode();

        AchievementManager.instance.xRaySpecsCount = 0;
    }

    public void Splode()
    {
        collider.enabled = false;
        cometMesh.SetActive(false);
        splosion1.SetActive(true);
        nextSplosionMeshTimer = 0.09f;
        dangerTrailSystem.Kaboom();
        rockParticleSystem.SpawnParticles(4);
        rockParticleSystem.SpawnParticles(4);

        AudioManager.instance.CometExplosion();
        AudioManager.instance.Splat();
        AudioManager.instance.StopCometRumble();
        Destroy(dangerIndicator.gameObject);
    }

    void DestroyThem()
    {
        AudioManager.instance.StopCometRumble();
        Destroy(dangerIndicator.gameObject);
        Destroy(transform.parent.gameObject);
        Destroy(gameObject);
    }

    void Twist()
    {
        transform.Rotate(0, 0 , twistDir * Time.deltaTime);
    }
}
