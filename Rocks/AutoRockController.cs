using UnityEngine;
public class AutoRockController : BaseRockController
{
    public override void OutOfBoundsRespawn()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        bool inBounds = screenPoint.x > -0.7 && screenPoint.x < 1.7 && screenPoint.y > -0.7 && screenPoint.y < 1.2;
        if (!inBounds)
        {
            if (!respawnInitiated)
            {
                InitiateRespawn();
            }
        }  
    }

    public override void Respawn()
    {
        MeshSwap();

        DecideIfHasMotion();

        SelectRespawnPosition();

        transform.position = respawnPosition;
        randomisedScale = Random.Range( 0.4f,  1.2f);

        float playerScale = PlayerController.PlayerScale;
        randomisedScale = Mathf.Min(Random.Range(playerScale * 2, playerScale * 2.5f), randomisedScale);
        transform.localScale = new Vector3(randomisedScale, randomisedScale, randomisedScale);

        collider.enabled = true;
        activeMesh.SetActive(true);
        graphicsRotation.enabled = true;

        ClearSelected();
    }
}
