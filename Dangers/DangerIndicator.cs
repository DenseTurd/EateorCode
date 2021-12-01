using UnityEngine;

public class DangerIndicator : MonoBehaviour
{
    public GameObject danger;

    public bool dangerOnScreen;

    SpriteRenderer rendy;

    public float dangerDistToPlayer;

    private void Start()
    {
        rendy = GetComponentInChildren<SpriteRenderer>();
        rendy.enabled = false;
    }
    void Update()
    {
        if (!dangerOnScreen)
        {
            if (PlayerController.instance != null)
            {
                rendy.enabled = true;
                Vector3 dir = (danger.transform.position - PlayerController.instance.transform.position).normalized;
                transform.position = PlayerController.instance.transform.position + dir * (PlayerController.PlayerScale * 2);
                float scaleAdjustedForDistanceToDanger = PlayerController.PlayerScale * (1-  (dangerDistToPlayer/4));
                transform.localScale = new Vector3(scaleAdjustedForDistanceToDanger, scaleAdjustedForDistanceToDanger, scaleAdjustedForDistanceToDanger);

                transform.rotation = Quaternion.LookRotation(dir);
            }
        }
        else
        {
            rendy.enabled = false;
        }

    }
}
