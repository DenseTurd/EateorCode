using UnityEngine;

public class PlayerTrail : MonoBehaviour
{
    float trailTimer = 0.15f;

    private void Update()
    {

        trailTimer -= Time.deltaTime;

        if (trailTimer <= 0)
        {
            trailTimer = 0.07f;
            MakeTrail();
        }

        transform.position = new Vector3(
            PlayerController.instance.transform.position.x, 
            PlayerController.instance.transform.position.y, 
            PlayerController.instance.transform.position.z + (PlayerController.PlayerScale * 0.5f)
            );
    }

    public void MakeTrail()
    {
        var trail = TrailPool.Instance.Get();
        trail.gameObject.transform.position = transform.position;
        trail.transform.Rotate(Random.Range(0, 359), Random.Range(0, 359), Random.Range(0, 359), Space.World);

        float randomScale = PlayerController.PlayerScale * Random.Range(0.2f, 0.4f);

        trail.Init(randomScale);
    }
}
