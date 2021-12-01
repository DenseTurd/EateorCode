using UnityEngine;

public class Thumb : MonoBehaviour
{
    public Vector3 stopPosition;
    void Update()
    {
        if (transform.position.y < stopPosition.y)
            transform.position = new Vector3(transform.position.x, transform.position.y + 2400 * Time.deltaTime, transform.position.z);
    }
}
