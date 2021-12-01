using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    List<GameObject> stars = new List<GameObject>();

    public GameObject starPrefab;
    int starCount;

    public void SpawnStars()
    {
        stars.Add(starPrefab);
        stars.Add(starPrefab);
        stars.Add(starPrefab);
        stars.Add(starPrefab);
        stars.Add(starPrefab);
        stars.Add(starPrefab);
        stars.Add(starPrefab);
        stars.Add(starPrefab);
        stars.Add(starPrefab);
        stars.Add(starPrefab);
        stars.Add(starPrefab);
        stars.Add(starPrefab);
        stars.Add(starPrefab);
        stars.Add(starPrefab);
        stars.Add(starPrefab);
        stars.Add(starPrefab);
        StartCoroutine(Spawning());
    }

    IEnumerator Spawning()
    {
        foreach (GameObject star in stars)
        {
            Instantiate(star);
            Vector3 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 820));
            star.transform.position = new Vector3(spawnPosition.x, spawnPosition.y, 820);
            starCount++;

            AudioManager.instance.RoundStartingPop();

            yield return new WaitForSeconds(0.08f);
        }
        if (starCount >= stars.Count)
        {
            GameManager.instance.planetSpawner.SpawnFirstPlanetsOnScreen();
            StopAllCoroutines();
        }
    }
}
