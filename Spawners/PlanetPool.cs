using System.Collections.Generic;
using UnityEngine;

public class PlanetPool : MonoBehaviour
{

    public GameObject planet1Prefab;
    public GameObject planet2Prefab;
    public GameObject planet3Prefab;
    public GameObject planet4Prefab;
    public GameObject planet5Prefab;
    public GameObject planet6Prefab;
    public GameObject planet7Prefab;
    public GameObject planet8Prefab;

    Dictionary<int, GameObject> planets;

    public static PlanetPool Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        var planet1 = Instantiate(planet1Prefab);
        var planet2 = Instantiate(planet2Prefab);
        var planet3 = Instantiate(planet3Prefab);
        var planet4 = Instantiate(planet4Prefab);
        var planet5 = Instantiate(planet5Prefab);
        var planet6 = Instantiate(planet6Prefab);
        var planet7 = Instantiate(planet7Prefab);
        var planet8 = Instantiate(planet8Prefab);

        planets = new Dictionary<int, GameObject>();
        planets.Add(0, planet1);
        planets.Add(1, planet2);
        planets.Add(2, planet3);
        planets.Add(3, planet4);
        planets.Add(4, planet5);
        planets.Add(5, planet6);
        planets.Add(6, planet7);
        planets.Add(7, planet8);

        for (int i = 0; i < planets.Count; i++)
        {
            planets[i].SetActive(false);
        }
    }

    public GameObject GetPlanet(int index)
    {
        return planets[index];
    }

    public void ReturnPlanetToPool(GameObject planet)
    {
        planet.SetActive(false);
    }
}
