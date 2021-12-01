using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurdSystem : MonoBehaviour
{
    private void Start()
    {
        PlayerController.instance.turdsystem = this;
    }
    public void Turd()
    {
        var turd = TurdPool.Instance.Get();
        turd.gameObject.transform.position = PlayerController.instance.transform.position;
        turd.direction = -PlayerController.instance.rb.velocity.normalized;
        turd.speed = PlayerController.instance.rb.velocity.magnitude / 3;
        turd.transform.Rotate(Random.Range(0, 359), Random.Range(0, 359), Random.Range(0, 359), Space.World);
        turd.canBeEaten = false;
        turd.canBeEatenTimer = 1f;

        float randomScale = Random.Range(0.3f, 0.5f);
        Vector3 scale = new Vector3(PlayerController.PlayerScale * randomScale, PlayerController.PlayerScale * randomScale, PlayerController.PlayerScale * randomScale);

        turd.gameObject.SetActive(true);
        turd.Init(scale);
    }
}
