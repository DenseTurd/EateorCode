using UnityEngine;

public class Pointer : MonoBehaviour
{
    float t;
    Vector3 startPos;
    Vector3 adjustedPosition;
    public bool horizontal;
    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        float offset = Mathf.Sin(t) * 10;
        if (horizontal)
        {
            adjustedPosition = new Vector3(startPos.x + offset, startPos.y, startPos.z);
        }
        else
        {
            adjustedPosition = new Vector3(startPos.x, startPos.y + offset, startPos.z);
        }
        transform.position = adjustedPosition;
        t += Time.deltaTime * 16;
        if (t > 360)
            t = 0;
    }
}
