using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    public Vector3 originalPosition;
    Vector3 previousPosition;
    Vector3 targetPosition;

    float slerpVal;
    float variedIntensity;
    float intensityLerp;

    float endTime;

    public bool shaking;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void SetOriginalCameraPosition()
    {
        originalPosition = transform.localPosition;
        targetPosition = originalPosition;
    }

    public static void Shake(float duration, float ammount)
    {
        instance.StopAllCoroutines();
        instance.shaking = false;
        instance.originalPosition = instance.transform.localPosition;
        instance.endTime = Time.time + duration;
        instance.slerpVal = 0;
        instance.variedIntensity = 0.5f;
        instance.intensityLerp = 0;
        instance.StartCoroutine(instance.CShake(duration, ammount * 2.5f));
    }

    public IEnumerator CShake(float duration, float ammount)
    {
        while (Time.time < endTime)
        {
            shaking = true;

            VaryIntensity(duration);

            if (transform.localPosition == originalPosition)
            {
                previousPosition = transform.localPosition;
                targetPosition = RandomTargetPosition(ammount);
            }
            if (transform.localPosition == targetPosition || slerpVal >= 1)
            {
                previousPosition = transform.localPosition;
                targetPosition = RandomTargetPosition(ammount);
                slerpVal = 0;
            }

            transform.localPosition = Vector3.Slerp(previousPosition, targetPosition, slerpVal);

            slerpVal += Time.deltaTime * 50;

            duration -= Time.deltaTime;

            yield return null;
        }
        if(Time.time >= endTime)
        {
            transform.localPosition = originalPosition;
            shaking = false;
            StopAllCoroutines();
        }
        transform.localPosition = originalPosition;
    }

    Vector3 RandomTargetPosition(float ammount)
    {
        float x = Random.insideUnitCircle.x * variedIntensity * ammount;
        float y = Random.insideUnitCircle.y * variedIntensity * ammount;
        return originalPosition + new Vector3(x, y, 0);
    }

    void VaryIntensity(float duration)
    {
        intensityLerp += Time.deltaTime / duration;
        float upDownLerp;
        if (intensityLerp < 0.5)
        {
            upDownLerp = intensityLerp * 2;
        }
        else
        {
            upDownLerp = 2 - (intensityLerp * 2);
        }
        variedIntensity = Mathf.Lerp(0.5f, 1f, upDownLerp);
    }
}
