using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    int health;
    int maxHp;

    public static PlayerHealth Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void Init()
    {
        maxHp = PlayerPrefs.GetInt("Max hp");
        health = maxHp;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHp;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        ClampHealth();
        UpdateHUD();

        PlayerPrefs.SetInt("Damage Taken", PlayerPrefs.GetInt("Damage Taken") + damage);
        if (health <= 0)
        {
            StatManager.instance.SetMaxSize();
            RoundOverManager.instance.RoundOver("HealthDrained");
            GameManager.play = false;
        }
    }

    public void Heal(int hp)
    {
        if (TutorialManager.Instance == null)
        {
            if (health < maxHp)
                PlayerPrefs.SetInt("Amount healed", PlayerPrefs.GetInt("Amount healed") + hp);
        }

        health += hp;
        ClampHealth();
        UpdateHUD();
    }

    void ClampHealth()
    {
        health = Mathf.Clamp(health, 0, maxHp);
    }

    void UpdateHUD()
    {
        if(GameManager.play)
            HUDController.instance.UpdateHealthBar((float)health / maxHp);
    }
}
