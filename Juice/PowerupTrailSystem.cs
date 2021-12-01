using UnityEngine;

public class PowerupTrailSystem : MonoBehaviour
{
    public Material mat;
    public float trailTimer = 0.5f;
    float timer;

    private void Start()
    {
        timer = trailTimer;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = trailTimer;
            SpawnTrail();
        }
    }
    public void SpawnTrail()
    {
        var trail = PowerupTrailPool.Instance.Get();
        trail.gameObject.transform.position = transform.position;
        trail.transform.Rotate(Random.Range(0, 359), Random.Range(0, 359), Random.Range(0, 359), Space.World);

        float randomScale = transform.lossyScale.x * Random.Range(0.3f, 0.6f);

        trail.gameObject.SetActive(true);
        trail.Init(randomScale, mat);
    }
}
