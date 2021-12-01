using UnityEngine;

public class PlanetController : MonoBehaviour //Eateor
{
    public float startingScale;
    public float scale;

    public int index;
    public PlanetSpawner planetSpawner;

    Vector3 respawnPosition;

    int respawnAttempts;

    private void Start()
    {
        transform.Rotate(Random.Range(0, 359), Random.Range(0, 359), Random.Range(0, 359), Space.World);
        scale = startingScale;
        respawnAttempts = 0;
    }

    public void Respawn()
    {   // use scale to push respawns away from the middle of the screen
        respawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(-0.3f, 1.3f),-0.25f - (scale * 0.0003f), PlayerController.instance.camZOffset + (scale * 3)));
        transform.position = respawnPosition;

        QueryDistToOtherPlanets();
    }

    private void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        bool onScreen = screenPoint.x > -( 1.1f + (scale * 0.0003f)) && screenPoint.x < 2.1f + (scale * 0.0003f) && screenPoint.y > -0.7f - (scale * 0.0003f) && screenPoint.y < 1.6 + (scale * 0.0003f);
        if (!onScreen)
        {
            if (planetSpawner.activePlanetDictionary[index] == false)
            {
                //Debug.Log("Planet " + index + " not in dictionary. returning to pool.");
                planetSpawner.planetsInScene.Remove(gameObject);
                PlanetPool.Instance.ReturnPlanetToPool(gameObject);
                return;
            }
            Respawn();
        }

        transform.Rotate(Vector3.up, 3 * Time.deltaTime, Space.World);
        scale = startingScale - PlayerController.PlayerScale;
        transform.localScale = new Vector3(scale, scale, scale);

        if (scale <= 0)
            PlanetPool.Instance.ReturnPlanetToPool(gameObject);

        if (transform.position.z < 150)
        {
            PlanetPool.Instance.ReturnPlanetToPool(gameObject);
        }
    }

    public void QueryDistToOtherPlanets()
    {
        if (TooClose() && respawnAttempts < 10)
        {
            Respawn();
        }
    }

    bool TooClose()
    {
        if (planetSpawner != null)
        {
            foreach (GameObject planet in planetSpawner.planetsInScene)
            {
                if (planet.GetComponent<PlanetController>().index != index)
                {
                    if (Vector3.Distance(planet.transform.position, transform.position) < planet.transform.localScale.x + scale)
                    {
                        respawnAttempts++;
                        return true;
                    }
                }
            }
        }
        respawnAttempts = 0;
        return false;
    }
}
