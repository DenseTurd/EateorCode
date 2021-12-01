using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    public List<GameObject> planetsInScene;

    float scale;

    public Dictionary<int, bool> activePlanetDictionary;

    Vector3 spawnPosition;

    GameObject mainMenuPlanet;

    private void Start()
    {
        planetsInScene = new List<GameObject>();

        activePlanetDictionary = new Dictionary<int, bool>();
        activePlanetDictionary.Add(0, false);
        activePlanetDictionary.Add(1, false);
        activePlanetDictionary.Add(2, false);
        activePlanetDictionary.Add(3, false);
        activePlanetDictionary.Add(4, false);
        activePlanetDictionary.Add(5, false);
        activePlanetDictionary.Add(6, false);
        activePlanetDictionary.Add(7, false);

        SpawnMainMenuPlanet();
    }

    private void Update()
    {
        if (GameManager.play)
        {
            DecideActivePlanet();
        }
    }

    public void SpawnFirstPlanetsOnScreen()
    {
        SpawnPlanetOnScreen(1);
        UpdateDictionary(1);
        DisableMainMenuPlanet();
        GameManager.instance.rockSpawning.SpawnRocks();
    }

    public void DecideActivePlanet()
    {
        SelectPlanetAccordingToPlayerSize(0, 2, 0);
        SelectPlanetAccordingToPlayerSize(2, 6, 1);
        SelectPlanetAccordingToPlayerSize(6, 20, 2);
        SelectPlanetAccordingToPlayerSize(20, 70, 3);
        SelectPlanetAccordingToPlayerSize(70, 230, 4);
        SelectPlanetAccordingToPlayerSize(230, 600, 5);
        SelectPlanetAccordingToPlayerSize(600, 1000, 6);
        SelectPlanetAccordingToPlayerSize(1000, 1500, 7);
    }

    void SelectPlanetAccordingToPlayerSize(int minSize, int maxSize, int planetIndex)
    {
        if (PlayerController.PlayerScale > minSize && PlayerController.PlayerScale <= maxSize)
        {
            if (activePlanetDictionary[planetIndex] == false)
            {
                if (!PlanetInScene(planetIndex))
                {
                    SpawnPlanet(planetIndex);
                }
                UpdateDictionary(planetIndex);
            }
        }
    }

    void SpawnPlanet(int index)
    {
        GameObject planet = PlanetPool.Instance.GetPlanet(index);
        PlanetController planetController = planet.GetComponent<PlanetController>();
        Vector3 spawnPosition;

        // use scale to push bigger planets further off screen when spawning
        scale = planetController.startingScale - PlayerController.PlayerScale;
        planet.transform.localScale = new Vector3(scale, scale, scale);

        spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(-0.3f, 1.3f), -0.25f - (scale * 0.0003f), PlayerController.instance.camZOffset + (scale * 3)));
        planet.transform.position = spawnPosition;

        planetController.index = index;
        planetController.planetSpawner = this;

        planetController.QueryDistToOtherPlanets();

        planetsInScene.Add(planet);

        planet.SetActive(true);

        //Debug.Log("Planet " + index + " spawned");
    }

    void SpawnPlanetOnScreen(int index)
    {
        GameObject planet = PlanetPool.Instance.GetPlanet(index);
        PlanetController planetController = planet.GetComponent<PlanetController>();

        // use scale to push respawns away from the middle of the screen
        scale = planetController.scale;
        planet.transform.localScale = new Vector3(scale, scale, scale);

        spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 158));
        planet.transform.position =  new Vector3(spawnPosition.x, spawnPosition.y, 158);

        planetController.index = index;
        planetController.planetSpawner = this;

        planetController.QueryDistToOtherPlanets();

        planetsInScene.Add(planet);

        planet.SetActive(true);

        //Debug.Log("planet " + index + " spawned onscreen");
    }

    bool PlanetInScene(int index)
    {
        foreach(GameObject planet in planetsInScene)
        {
            if (planet.GetComponent<PlanetController>().index == index)
                return true;
        }
        return false;
    }

    void SpawnMainMenuPlanet()
    {
        if(PlayerPrefs.GetInt("Biggest planet eaten") > 0)
        {
            mainMenuPlanet = PlanetPool.Instance.GetPlanet(PlayerPrefs.GetInt("Biggest planet eaten") -1);
        }
        else
        {
            mainMenuPlanet = PlanetPool.Instance.GetPlanet(0);
        }
        mainMenuPlanet.SetActive(true);
        var planetController = mainMenuPlanet.GetComponent<PlanetController>();
        planetController.planetSpawner = FindObjectOfType<PlanetSpawner>();
        spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 136 + (planetController.startingScale * 3)));
        mainMenuPlanet.transform.position = spawnPosition;
    }

    void DisableMainMenuPlanet()
    {
        mainMenuPlanet.SetActive(false);
    }

    void UpdateDictionary(int key)
    {
        activePlanetDictionary.Remove(key-1);
        activePlanetDictionary.Remove(key);
        activePlanetDictionary.Remove(key+1);
        activePlanetDictionary.Add(key-1, false);
        activePlanetDictionary.Add(key, true);
        activePlanetDictionary.Add(key+1, false);
    }
}
