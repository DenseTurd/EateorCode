using UnityEngine;

public class ComsicBurgerPowerup : MonoBehaviour, Ipowerup
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
            Debug.Log("Powerup destroyed as not in bounds");
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
        if (!RoundOverManager.instance.roundOver)
        {
            PlayerController.instance.EatCosmicBurger();
            AchievementManager.instance.CosmicBurger();
            HUDController.instance.CosmicBurgerSplash();

            powerupParticleSystem.SpawnParticles(3);

            AudioManager.instance.CosmicBurger();
            AudioManager.instance.GrowLong();

            ScoreManager.instance.AddScore(Random.Range(267, 300), transform.position);

            PlayerPrefs.SetInt("Cosmic burgers eaten", PlayerPrefs.GetInt("Cosmic burgers eaten") + 1);

            Destroy(this.gameObject);
        }
    }
}
