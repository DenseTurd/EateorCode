using UnityEngine;

public class DangerSpawner : MonoBehaviour
{
    public static DangerSpawner instance;

    public GameObject meteorPrefab;
    public GameObject sploogePrefab;

    Vector3 spawnPosition;
    float randomisedScale;
    public bool meteorAvaliable;
    public bool sploogeAvaliable;

    float firstMeteorDelay;
    public float meteorAvaliableTime;
    public float sploogeAvaliableTime;

    public float nextMeteorTime = 13f;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        firstMeteorDelay = Random.Range(13, 19);
    }

    private void Update()
    {
        if (!RoundOverManager.instance.roundOver)
        {
            // Spawn a meteor after random delay 13 - 19 sec
            if (PlayerController.instance != null && Time.time > PlayerController.instance.roundStartTime + firstMeteorDelay && meteorAvaliable)
            {
                meteorAvaliable = false;
                meteorAvaliableTime = Time.time + Random.Range(nextMeteorTime -1, nextMeteorTime +1);
                if (Random.Range(0, 10) < 9)
                {
                    nextMeteorTime -= Random.Range(0.5f, 0.75f);
                }
                nextMeteorTime = Mathf.Clamp(nextMeteorTime, 4, 13);
                //Debug.Log("Next meteor time; " + nextMeteorTime);

                SpawnMeteor();
            }
            if (Time.time > meteorAvaliableTime && !meteorAvaliable && GameManager.play)
            {
                meteorAvaliable = true;
            }

            // Spawn splooge if something !!!!
            if (PlayerController.instance != null && Time.time > PlayerController.instance.roundStartTime + 15 && sploogeAvaliable)
            {
                sploogeAvaliable = false;
                sploogeAvaliableTime = Time.time + Random.Range(13f, 24f);
                SpawnSplooge();
            }
            if (Time.time > sploogeAvaliableTime && !sploogeAvaliable && GameManager.play)
            {
                sploogeAvaliable = true;
            }
        }
    }

    public void SpawnMeteor()   // always spawn below so player has to dodge
    {
        GameObject danger = Instantiate(meteorPrefab);
        
        spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(-0.1f, 1.1f), Random.Range(-1.4f, -1.7f), PlayerController.instance.camZOffset));
        randomisedScale = Random.Range(PlayerController.instance.meanScale * 0.9f, PlayerController.instance.meanScale * 1.6f);

        GameObject meteor = danger.GetComponent<MeteorRef>().meteor;
        meteor.transform.position = new Vector3(spawnPosition.x, spawnPosition.y, 0);
        meteor.transform.localScale = new Vector3(randomisedScale, randomisedScale, randomisedScale);

        //Debug.Log("Spawned meteor");
    }

    public void SpawnAssassin()
    {
        GameObject danger = Instantiate(meteorPrefab);

        spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, Random.Range(-0.9f, -1.1f), PlayerController.instance.camZOffset));
        randomisedScale = Random.Range(PlayerController.instance.meanScale * 1.1f, PlayerController.instance.meanScale * 1.9f);

        GameObject meteor = danger.GetComponent<MeteorRef>().meteor;
        meteor.transform.position = new Vector3(spawnPosition.x, spawnPosition.y, 0);
        meteor.transform.localScale = new Vector3(randomisedScale, randomisedScale, randomisedScale);

        danger.GetComponentInChildren<MeteorController>().assassin = true;

        //Debug.Log("Spawned assassin");
    }

    public GameObject SpawnTutorialMeteor()
    {
        GameObject danger = Instantiate(meteorPrefab);

        spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, -0.5f, PlayerController.instance.camZOffset));
        randomisedScale = PlayerController.instance.meanScale * 1.9f;

        GameObject meteor = danger.GetComponent<MeteorRef>().meteor;
        meteor.transform.position = new Vector3(spawnPosition.x, spawnPosition.y, 0);
        meteor.transform.localScale = new Vector3(randomisedScale, randomisedScale, randomisedScale);

        //Debug.Log("Spawned tutorial meteor");

        return danger;  
    }

    public void SpawnSplooge()
    {
        GameObject danger = Instantiate(sploogePrefab);

        spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(-0.1f, 1.1f), Random.Range(-0.9f, -1.1f), PlayerController.instance.camZOffset));
        randomisedScale = Random.Range(PlayerController.instance.meanScale * 0.9f, PlayerController.instance.meanScale * 1.6f);

        GameObject meteor = danger.GetComponent<MeteorRef>().meteor;
        meteor.transform.position = new Vector3(spawnPosition.x, spawnPosition.y, 0);
        meteor.transform.localScale = new Vector3(randomisedScale, randomisedScale, randomisedScale);

        //Debug.Log("Spawned splooge");
    }
}
