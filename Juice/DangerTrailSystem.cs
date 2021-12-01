using UnityEngine;

public class DangerTrailSystem : MonoBehaviour
{
    float trailTimer = 0.15f;

    public Material _mat;

    Vector3 trailStartPosition;

    public Transform parentTransform;

    float scale;

    private void Update()
    {
        trailTimer -= Time.deltaTime;

        if (trailTimer <= 0)
        {
            trailTimer = 0.08f;
            MakeTrail();
        }

        trailStartPosition = new Vector3(
            transform.position.x, 
            transform.position.y, 
            transform.position.z + (transform.localScale.z * 0.5f)
            );
    }

    public void Kaboom()
    {
        MakeTrail();
        MakeTrail();
        MakeTrail();
        MakeTrail();
    }

    public void MakeTrail()
    {
        var trail = DangerTrailPool.Instance.Get();
        trail.gameObject.transform.position = transform.position;
        trail.transform.Rotate(Random.Range(0, 359), Random.Range(0, 359), Random.Range(0, 359), Space.World);

        if(parentTransform != null)
        {
            scale = parentTransform.localScale.x;
        }
        float randomScale = scale * Random.Range(0.25f, 0.5f);

        trail.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        trail.gameObject.SetActive(true);
        trail.Init(randomScale, _mat, trailStartPosition);
    }
}
