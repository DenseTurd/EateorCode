using UnityEngine;

public class MajestyStarController : MonoBehaviour
{
    float scalingTime = 0.8f;
    float scale;
    float _startingScale;
    float targetScale;

    Vector3 dir;
    float speed;

    public void Init(float startingScale)
    {
        scale = startingScale;
        _startingScale = startingScale;

        dir = Random.insideUnitCircle.normalized;
        speed = Random.Range(40f, 80f);

        targetScale = Random.Range(startingScale * 1.5f, startingScale * 2f);
    }

    void Update()
    {
        scale += (_startingScale / scalingTime) * Time.deltaTime;

        transform.localScale = new Vector3(scale, scale, scale);
        transform.Translate(dir * speed * Time.deltaTime, Space.World);

        if (scale >= _startingScale * 2)
        {
            MajestyStarsPool.Instance.ReturnToPool(this);
        }
    }
}
