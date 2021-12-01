using UnityEngine;

public class PowerupTrailController : MonoBehaviour
{
    float scalingTime = 1f;
    float scale;
    float _startingScale;

    MeshRenderer renderer;

    private void Start()
    {
        renderer = GetComponentInChildren<MeshRenderer>();
    }

    public void Init(float startingScale, Material mat)
    {
        if(renderer == null)
        {
            renderer = GetComponentInChildren<MeshRenderer>();
        }
        scale = startingScale;
        _startingScale = startingScale;

        renderer.material = mat;
    }

    void Update()
    {
        scale -= (_startingScale / scalingTime) * Time.deltaTime;

        transform.localScale = new Vector3(scale, scale, scale);

        if(scale <= 0)
        {
            PowerupTrailPool.Instance.ReturnToPool(this);
        }
    }
}
