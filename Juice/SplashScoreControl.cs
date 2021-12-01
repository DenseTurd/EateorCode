using UnityEngine;
using TMPro;

public class SplashScoreControl : MonoBehaviour
{
    TMP_Text text;
    public float destroyTimer = 0.4f;
    public float currentTimer;
    float vspeed = 180;

    public float initialScale;
    public float scale;
    float t;
    void Start()
    {
        text = GetComponent<TMP_Text>();
        scale = transform.localScale.x;
        currentTimer = 0;
        t = 0;
    }

    void Update()
    {
        transform.Translate(new Vector3(0, vspeed * Time.deltaTime, 0));

        currentTimer += Time.deltaTime;
        t = (1 / destroyTimer) * currentTimer;
        scale = (1- (t * t * t *t)) * initialScale;
        transform.localScale = new Vector3(scale, scale, scale);

        if(currentTimer >= destroyTimer)
        {
            SplashScorePool.Instance.ReturnToPool(this);
        }
    }
}
