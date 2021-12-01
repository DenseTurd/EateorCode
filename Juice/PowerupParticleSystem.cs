using UnityEngine;

public class PowerupParticleSystem : MonoBehaviour
{
    public void SpawnParticles(int meshIndex)
    {
        SpawnParticle(meshIndex);
        SpawnParticle(meshIndex);
        SpawnParticle(meshIndex);
        SpawnParticle(meshIndex);
        SpawnParticle(meshIndex);
        SpawnParticle(meshIndex);
        SpawnParticle(meshIndex);
        SpawnParticle(meshIndex);
        SpawnParticle(meshIndex);
    }

    void SpawnParticle(int meshIndex)
    {
        var particle = PowerupParticlePool.Instance.Get();
        particle.gameObject.transform.position = transform.position;
        particle.transform.Rotate(Random.Range(0, 359), Random.Range(0, 359), Random.Range(0, 359), Space.World);

        float randomScale = transform.lossyScale.x * Random.Range(0.3f, 0.6f);
        Vector3 dir = Random.insideUnitCircle.normalized;
        float speed = Random.Range(PlayerController.instance.maxSpeed * 1f, PlayerController.instance.maxSpeed * 4f);

        particle.gameObject.SetActive(true);
        particle.Init(randomScale, dir, speed, meshIndex);
    }
}
