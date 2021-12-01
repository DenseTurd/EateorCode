using System.Collections.Generic;
using UnityEngine;

public class MajestyStarSystem : MonoBehaviour
{
    float majestyTimer = 0.1f;

    float scale;

    Vector2 randomStartPos;

    private void Update()
    {
        majestyTimer -= Time.deltaTime;

        if (majestyTimer <= 0)
        {
            majestyTimer = Random.Range(0.2f, 0.5f);
            MakeStar();
        }
    }

    public void MakeStar()
    {
        var star = MajestyStarsPool.Instance.Get();
        star.transform.SetParent(transform, false);

        randomStartPos = Random.insideUnitCircle * 30;
        star.gameObject.transform.position = transform.position + (Vector3)randomStartPos;
        star.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(-22, 22)));

        float randomScale = Random.Range(0.1f, 0.5f);

        star.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        star.gameObject.SetActive(true);
        star.Init(randomScale);
    }
}
