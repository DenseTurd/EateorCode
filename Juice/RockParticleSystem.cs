using UnityEngine;

public class RockParticleSystem : MonoBehaviour
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
        var particle = RockParticlePool.Instance.Get();
        particle.gameObject.transform.position = transform.position;
        particle.transform.Rotate(Random.Range(0, 359), Random.Range(0, 359), Random.Range(0, 359), Space.World);

        float randomScale = transform.lossyScale.x * Random.Range(0.3f, 0.6f);
        Vector3 dir = Random.insideUnitCircle.normalized;
        float speed = Random.Range(PlayerController.instance.maxSpeed * 1f, PlayerController.instance.maxSpeed * 4f);

        particle.gameObject.SetActive(true);
        particle.Init(randomScale, dir, speed, meshIndex);
    }

    public void SpawnBonkParticles(int meshIndex)
    {
        for (int i = 0; i < 10; i++)
        {
            SpawnBonkParticle(meshIndex);
        }
    }

    void SpawnBonkParticle(int meshIndex)
    {
        var particle = RockParticlePool.Instance.Get();

        Vector3 dirToCollision = (transform.position - PlayerController.instance.transform.position).normalized;
        Vector3 collisionPosition = PlayerController.instance.transform.position + (dirToCollision * PlayerController.PlayerScale);

        particle.gameObject.transform.position = collisionPosition;
        particle.transform.Rotate(Random.Range(0, 359), Random.Range(0, 359), Random.Range(0, 359), Space.World);

        float randomScale = PlayerController.PlayerScale * Random.Range(0.1f, 0.2f);
        Vector3 dir = Random.insideUnitCircle.normalized;
        float speed = Random.Range(PlayerController.instance.maxSpeed * 1f, PlayerController.instance.maxSpeed * 4f);

        particle.gameObject.SetActive(true);
        particle.Init(randomScale, dir, speed, meshIndex);
    }
}
