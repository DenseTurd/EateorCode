using UnityEngine;

public class CoinController : MonoBehaviour
{
    PlayerController playerController;
    Vector3 playerScale;

    new SphereCollider collider;
    new MeshRenderer renderer;

    Vector3 respawnPosition;
    public float respawnTime;
    public bool respawnInitiated;

    bool rotating;

    float nextPopMeshTime;
    public GameObject pop1;
    public GameObject pop2;


    private void Start()
    {
        playerController = PlayerController.instance;
        transform.Rotate(0, Random.Range(0, 359), 0, Space.World);
        collider = GetComponent<SphereCollider>();
        renderer = GetComponentInChildren<MeshRenderer>();
        respawnInitiated = false;
        rotating = true;
    }
    public void Respawn()
    {
        SelectRespawnPosition();

        playerScale = playerController.gameObject.transform.localScale;
        transform.position = respawnPosition;
        transform.localScale = playerScale * Mathf.Clamp(0.5f - playerScale.x/30, 0.2f, 1f);

        collider.enabled = true;
        renderer.enabled = true;
        rotating = true;
    }

    void SelectRespawnPosition()
    {
        int spawnLocationSelector = Random.Range(0, 4); // coins wont spawn beneath the player
        if (spawnLocationSelector == 0 || spawnLocationSelector == 2) // spawn above
        {
            respawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.3f, 0.6f), Random.Range(0.6f, 0.9f), playerController.camZOffset));
        }
        if (spawnLocationSelector == 1) // spawn to the left
        {
            respawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.1f, 0.3f), Random.Range(0.3f, 0.9f), playerController.camZOffset));
        }
        if (spawnLocationSelector == 3) // spawn to the right
        {
            respawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.6f, 0.9f), Random.Range(0.3f, 0.9f), playerController.camZOffset));
        }
    }
    private void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        bool inBounds = screenPoint.x > -0.2 && screenPoint.x < 1.2 && screenPoint.y > -0.2 && screenPoint.y < 1.2;
        if (!inBounds)
        {
            if (!respawnInitiated)
            {
                InitiateRespawn();
            }
        }

        if(rotating)
        transform.Rotate(Vector3.up, 360 * Time.deltaTime, Space.World);

        if(respawnInitiated && Time.time > respawnTime)
        {
            respawnInitiated = false;
            Respawn();
        }

        if (collider.enabled == false)
        {
            if (Time.time > nextPopMeshTime && pop1.activeSelf == true)
            {
                pop1.SetActive(false);
                pop2.SetActive(true);
                nextPopMeshTime = Time.time + 0.15f;
            }
            if (Time.time > nextPopMeshTime && pop2.activeSelf == true)
            {
                pop2.SetActive(false);
                InitiateRespawn();
            }
        }
    }
    public virtual void InitiateRespawn()
    {
        respawnInitiated = true;
        respawnTime = Time.time + 0.5f;
    }

    public void Pop()
    {
        collider.enabled = false;
        renderer.enabled = false;
        rotating = false;

        pop1.SetActive(true);
        nextPopMeshTime = Time.time + 0.15f;
    }
}
