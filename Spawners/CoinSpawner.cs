using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    List<GameObject> coins = new List<GameObject>();

    public GameObject coinPrefab;
    
    Vector3 playerScale;
    Vector3 playerPosition;

    int coinCount;
    public void SpawnCoins()
    {
        coins.Add(coinPrefab);
        coins.Add(coinPrefab);
        coins.Add(coinPrefab);
        coins.Add(coinPrefab);

        playerScale = PlayerController.instance.gameObject.transform.localScale;
        playerPosition = PlayerController.instance.transform.position;
        StartCoroutine(Spawning());
    }

    IEnumerator Spawning()
    {
        foreach (GameObject coin in coins)
        {
            Instantiate(coin);
            coin.transform.position = SelectSpawnPosition();
            coin.transform.localScale = playerScale * 0.5f;
            coinCount++;

            AudioManager.instance.RoundStartingPop();

            yield return new WaitForSeconds(0.08f);
        }
        if(coinCount >= coins.Count)
        {
            GameManager.instance.doneSpawning = true;
            StopAllCoroutines();
        }
    }

    Vector3 SelectSpawnPosition()
    {
        Vector3 spawnPosition = Vector3.zero;
        int spawnLocationSelector = Random.Range(0, 4); // coins wont spawn beneath the player
        if (spawnLocationSelector == 0 || spawnLocationSelector == 2) // spawn above
        {
            spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.3f, 0.6f), Random.Range(0.6f, 0.9f), 10));
        }
        if (spawnLocationSelector == 1) // spawn to the left
        {
            spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.1f, 0.3f), Random.Range(0.3f, 0.9f), 10));
        }
        if (spawnLocationSelector == 3) // spawn to the right
        {
            spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.6f, 0.9f), Random.Range(0.3f, 0.9f), 10));
        }

        return spawnPosition;
    }
}
