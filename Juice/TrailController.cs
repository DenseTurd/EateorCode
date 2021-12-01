using UnityEngine;

public class TrailController : MonoBehaviour
{
    float scalingTime = 1f;
    float scale;
    float _startingScale;

    public void Init(float startingScale)
    {
        scale = startingScale;
        _startingScale = startingScale;
        transform.localScale = new Vector3(scale, scale, scale);
        gameObject.SetActive(true);
    }

    void Update()
    {
        scale -= (_startingScale / scalingTime) * Time.deltaTime;

        transform.localScale = new Vector3(scale, scale, scale);

        if(scale <= 0)
        {
            TrailPool.Instance.ReturnToPool(this);
        }
    }
}
