using UnityEngine;

public class TurdController : MonoBehaviour
{
    public float distanceToPlayer;

    public float canBeEatenTimer;
    public bool canBeEaten;

    public Vector3 direction;
    public float speed;

    public GameObject activeMesh;
    public GameObject cloudOne;
    public float cloudTimer;

    public SphereCollider collider;

    bool exitCloud;

    public void Init(Vector3 scale)
    {
        if (activeMesh == null)
        {
            activeMesh = Instantiate(MeshSwapper.instance.characterDictionary.characters[PlayerPrefs.GetInt("CharIndex")]);
            activeMesh.transform.position = transform.position;
            activeMesh.transform.parent = transform;
            activeMesh.AddComponent<PlayerGraphicsRotation>();
        }
        activeMesh.SetActive(false);
        cloudOne.transform.localScale = new Vector3(0, 0, 0);
        cloudOne.SetActive(true);
        cloudTimer = 0f;
        exitCloud = false;
        transform.localScale = scale;
    }

    private void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        bool inBounds = screenPoint.x > -0.7 && screenPoint.x < 1.7 && screenPoint.y > -0.7 && screenPoint.y < 1.2;
        if (!inBounds)
        {
            DeSpawn();
        }

        if(canBeEatenTimer <=0 && !canBeEaten)
        {
            canBeEaten = true;
        }
        
        canBeEatenTimer -= Time.deltaTime;

        MoveMe(direction, speed);

        if (cloudTimer <= 0.3f)
        {
            cloudTimer += Time.deltaTime;
            var scale = Mathf.Sin(cloudTimer * 12);
            cloudOne.transform.localScale = new Vector3(scale, scale, scale);
        }
        else if (cloudOne.activeSelf == true)
        {
            cloudOne.SetActive(false);
            activeMesh.SetActive(true);

            if (exitCloud)
                DeSpawn();
        }
    }

    void MoveMe(Vector3 direction, float speed)
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    public void DeSpawnCloud()
    {
        cloudTimer = 0;
        cloudOne.SetActive(true);
        cloudOne.transform.localScale = new Vector3(0, 0, 0);
        activeMesh.SetActive(false);
        exitCloud = true;
    }

    public void DeSpawn()
    {
        TurdPool.Instance.ReturnToPool(this);
    }
}
