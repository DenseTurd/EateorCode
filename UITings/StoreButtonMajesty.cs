using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreButtonMajesty : MonoBehaviour
{
    float timer;
    float scale;
    float t;
    bool swelling;
    Vector3 defaultScale;

    private void Start()
    {
        defaultScale = transform.localScale;
        transform.localScale = defaultScale * 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            swelling = true;
            timer = 6f;
        }

        if (swelling)
        {
            Swell();

            t += 2 * Time.deltaTime;
            if (Mathf.Rad2Deg * t > 180)
            {
                t = 0;
                swelling = false;
            }
        }
        else
        {
            t = 0;
            transform.localScale = defaultScale * 0.1f;
        }
    }

    void Swell()
    {
        scale = Mathf.Sin(t);
        transform.localScale = new Vector3(defaultScale.x * scale, defaultScale.y * scale, defaultScale.z);
    }
}
