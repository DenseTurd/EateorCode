using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawning : MonoBehaviour
{
    List<GameObject> rocks = new List<GameObject>();
    List<GameObject> disposableRocks = new List<GameObject>();
    public GameObject rockPrefab;
    public GameObject disposableRockPrefab;
    public GameObject helperRockPrefab;

    public RockTargetsParent rockTargetsParent;
    public RockTargetsParent disposableRockTargetsParent;
    
    public List<Transform> rockTargets;
    public List<Transform> disposableRockTargets;

    int rockCount;
    int disposableRockCount;

    public static RockSpawning Instance { get; private set; }
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        rockTargets = new List<Transform>();
        foreach (Transform child in rockTargetsParent.transform)
        {
            rockTargets.Add(child);
        }
        disposableRockTargets = new List<Transform>();
        foreach (Transform child in disposableRockTargetsParent.transform)
        {
            disposableRockTargets.Add(child);
        }
    }

    public void SpawnRocks()
    {
        rockTargetsParent.ScaleToPlayerStartingSize();
        disposableRockTargetsParent.ScaleToPlayerStartingSize();

        rocks.Add(rockPrefab);
        rocks.Add(rockPrefab);
        rocks.Add(rockPrefab);
        rocks.Add(rockPrefab);
        rocks.Add(rockPrefab);
        rocks.Add(rockPrefab);
        rocks.Add(rockPrefab);
        disposableRocks.Add(disposableRockPrefab);
        disposableRocks.Add(disposableRockPrefab);
        disposableRocks.Add(disposableRockPrefab);
        disposableRocks.Add(disposableRockPrefab);
        StartCoroutine(Spawning());
    }

    IEnumerator Spawning()
    {
        for (int i = 0; i < rocks.Count; i++)
        {
            var rock = Instantiate(rocks[i]);
            rock.transform.position = rockTargets[i].position;
            float randomScale = Random.Range(0.2f, 0.5f) * PlayerPrefs.GetFloat("StartingSize");
            rock.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            rockCount++;

            AudioManager.instance.RoundStartingPop();

            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i < disposableRocks.Count; i++)
        {
            var dRock = Instantiate(disposableRocks[i]);
            dRock.transform.position = disposableRockTargets[i].position;
            float randomScale = Random.Range(0.2f, 0.5f) * PlayerPrefs.GetFloat("StartingSize");
            dRock.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            disposableRockCount++;

            AudioManager.instance.RoundStartingPop();

            yield return new WaitForSeconds(0.08f);
        }

        if (rockCount >= rocks.Count)
        {
            GameManager.instance.coinSpawner.SpawnCoins();
            StopAllCoroutines();
        }
    }

    public void SpawnHelperRock()
    {
        var rock = Instantiate(helperRockPrefab);
        rock.transform.position = SelectSpawnPosition();
        float scale = 0.6f * PlayerController.PlayerScale;
        rock.transform.localScale = new Vector3(scale, scale, scale);

        //Debug.Log("Spawned helper rock");
    }

    Vector3 SelectSpawnPosition()
    {
        Vector3 spawnPosition = new Vector3(0, 0, 0);
        int spawnLocationSelector = Random.Range(0, 2);
        if (spawnLocationSelector == 0 ) // spawn to the left
        {
            spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(-0.6f, -0.4f), Random.Range(-0.2f, 0.6f), PlayerController.instance.camZOffset));
        }
        
        if (spawnLocationSelector == 1 ) // spawn to the right
        {
            spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(1.6f, 1.4f), Random.Range(-0.2f, 0.6f), PlayerController.instance.camZOffset));
        }

        return spawnPosition;
    }
}
