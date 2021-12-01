using UnityEngine;

public class DangerTrailController : MonoBehaviour
{
    float scalingTime = 1.5f;
    float scale;
    float _startingScale;

    MeshRenderer renderer;

    Vector3 dir;
    float speed;

    public void Init(float startingScale, Material mat, Vector3 startPos)
    {
        if (renderer == null)
        {
            renderer = GetComponentInChildren<MeshRenderer>();
        }

        scale = startingScale;
        _startingScale = startingScale;

        transform.position = startPos;

        dir = Random.insideUnitSphere;
        speed = Random.Range(PlayerController.instance.maxSpeed * 0.1f, PlayerController.instance.maxSpeed * 0.3f);

        renderer.material = mat;
    }

    void Update()
    {
        scale += (_startingScale / scalingTime) * Time.deltaTime;

        transform.localScale = new Vector3(scale, scale, scale);

        if(scale >= _startingScale * 2)
        {
            DangerTrailPool.Instance.ReturnToPool(this);
        }

        transform.Translate(dir * speed * Time.deltaTime, Space.World);
    }
}
