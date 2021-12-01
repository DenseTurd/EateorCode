using UnityEngine;
public class DisposableRockController : BaseRockController
{
    public override void Respawn()
    {
        Destroy(gameObject);
    }

    // disable random motion
    public override void MoveMe(Vector3 direction, float speed)
    {
        return;
    }

    // disable out of bounds respawn
    public override void OutOfBoundsRespawn()
    {
        return;
    }
}
