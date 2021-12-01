using UnityEngine;
public class HelperRockController : BaseRockController
{
    public override void Respawn()
    {
        Destroy(gameObject);
    }

    public override void HandleGravity() // always gravity
    {
        float playerMaxSpeed = PlayerController.instance.maxSpeed;
        gravitySpeed = Random.Range(playerMaxSpeed / 4, playerMaxSpeed / 3);
        MoveTowardPlayer();
    }
}
