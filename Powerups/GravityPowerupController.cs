using UnityEngine;

public class GravityPowerupController : MonoBehaviour, Ipowerup
{
    PowerupParticleSystem powerupParticleSystem;
    void Start()
    {
        powerupParticleSystem = GetComponent<PowerupParticleSystem>();
    }

    private void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        bool inBounds = screenPoint.x > -1 && screenPoint.x < 2 && screenPoint.y > -1 && screenPoint.y < 2;
        if (!inBounds)
        {
            Destroy(this.gameObject);
            //Debug.Log("Powerup destroyed as not in bounds");
        }

        MoveTowardPlayer();
    }

    void MoveTowardPlayer()
    {
        if(PlayerController.instance != null)
        {
            float step = (PlayerController.instance.maxSpeed / 2)  * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, PlayerController.instance.transform.position, step);
        }
    }

    public void Powerup()
    {
        PlayerController.instance.gravity = true;
        PlayerController.instance.gravityCharges = 2;

        powerupParticleSystem.SpawnParticles(2);

        HUDController.instance.GravitySplash();

        AudioManager.instance.Gravity();

        ScoreManager.instance.AddScore(Random.Range(16, 23), transform.position);

        PlayerPrefs.SetInt("Gravities Collected", PlayerPrefs.GetInt("Gravities Collected") + 1);

        Destroy(this.gameObject);
    }
}
