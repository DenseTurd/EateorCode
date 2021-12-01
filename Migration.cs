using UnityEngine;

public class Migration : MonoBehaviour
{
    int boundary = 300000;
    public virtual void Migrate()
    {
        if(transform.position.x < -boundary)
        {
            transform.position = new Vector3(transform.position.x + boundary, transform.position.y, transform.position.z);
            Debug.Log("wrapped to the right");
            AchievementManager.instance.MegaSpace();
            CameraShake.Shake(0.4f, 0.5f);
            HUDController.instance.MegaSpace();
        }

        if (transform.position.x > boundary)
        {
            transform.position = new Vector3(transform.position.x - boundary, transform.position.y, transform.position.z);
            Debug.Log("wrapped to the left");
            AchievementManager.instance.MegaSpace();
            CameraShake.Shake(0.4f, 0.5f);
            HUDController.instance.MegaSpace();
        }

        if (transform.position.y < -boundary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + boundary, transform.position.z);
            Debug.Log("wrapped up");
            AchievementManager.instance.MegaSpace();
            CameraShake.Shake(0.4f, 0.5f);
            HUDController.instance.MegaSpace();
        }

        if (transform.position.y > boundary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - boundary, transform.position.z);
            Debug.Log("wrapped down");
            AchievementManager.instance.MegaSpace();
            CameraShake.Shake(0.4f, 0.5f);
            HUDController.instance.MegaSpace();
        }
    }
}
