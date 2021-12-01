using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public static Settings instance;

    public Slider swipeSensitivity;
    public Slider sfxVolume;
    public Slider musicVolume;

    public Toggle invertSwipeToggle;
    public static bool InvertSwipe { get; private set; }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InvertSwipeToggle(Toggle toggle)
    {
        InvertSwipe = toggle.isOn;

        if (InvertSwipe == false)
        {
            PlayerPrefs.SetInt("InvertSwipe", 0);
        }
        else
        {
            PlayerPrefs.SetInt("InvertSwipe", 1);
        }
        
    }

    public void Tutorial()
    {
        PlayerPrefs.SetInt("Tutorial complete", 0);
        UiController.instance.ClosePanel();
        UiController.instance.Play();
    }

    public void ChangeSwipeSensitivity()
    {
        PlayerPrefs.SetFloat("RotateSpeed", swipeSensitivity.value);
    }

    public void DisplayCorrectValues()
    {
        swipeSensitivity.value = PlayerPrefs.GetFloat("RotateSpeed");

        sfxVolume.value = PlayerPrefs.GetFloat("sfxVolume");
        musicVolume.value = PlayerPrefs.GetFloat("musicVolume");

        if (PlayerPrefs.GetInt("InvertSwipe") == 0)
        {
            InvertSwipe = false;
            invertSwipeToggle.isOn = false;
        }
        else
        {
            InvertSwipe = true;
            invertSwipeToggle.isOn = true;
        }
    }

    public void ChangeSFXVol()
    {
        AudioManager.instance.ChangeSFXVol(sfxVolume.value);
    }

    public void ChangeMusicVol()
    {
        AudioManager.instance.ChangeMusicVolume(musicVolume.value);
    }

    public void DefaultSettings()
    {
        PlayerPrefs.SetFloat("RotateSpeed", 0.15f);
        PlayerPrefs.SetInt("InvertSwipe", 0);
        PlayerPrefs.SetFloat("sfxVolume", 0.7f);
        PlayerPrefs.SetFloat("musicVolume", 0.7f);
    }

}
