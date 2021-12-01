using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretCoinsButton : MonoBehaviour
{
    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("Secret coins! achieved") != 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void SecretCoins()
    {
        AchievementManager.instance.SecretCoins();
        AudioManager.instance.Secret();
        gameObject.SetActive(false);
    }
}
