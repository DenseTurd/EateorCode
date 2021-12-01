using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    float _sfxVolume;
    float _musicVolume;

    public AudioSource roundStart;
    public AudioSource gameplayMusic;
    public AudioSource cometRumble;
    public AudioSource cometExplosion;
    public AudioSource cometExplosionBass;
    public AudioSource meteorRumble;
    public AudioSource meteorNoise;
    public AudioLowPassFilter meteorLowFilter;
    public AudioHighPassFilter meteorHighFilter;
    public AudioSource meteorExplosion;
    public AudioSource meteorExplosionBass;
    public AudioSource assassinExplosion;
    public AudioSource assassinExplosionBass;
    public AudioSource niceTry;
    public AudioSource greatJob;
    public AudioSource incredible;
    public AudioSource crunch;
    public AudioSource chomp1;
    public AudioSource chomp2;
    public AudioSource chomp3;
    public AudioSource chomp4;
    public AudioSource chomp5;
    public AudioSource chomp6;
    public AudioSource chomp7;
    public AudioSource chomp8;
    public AudioSource chomp9;
    public AudioSource bonk0;
    public AudioSource bonk1;
    public AudioSource bonk2;
    public AudioSource bonk3;
    public AudioSource bonk4;
    public AudioSource bonk5;
    public AudioSource bonk6;
    public AudioSource bonk7;
    public AudioSource bonk8;
    public AudioSource bonk9;
    public AudioSource pop0;
    public AudioSource pop1;
    public AudioSource pop2;
    public AudioSource pop3;
    public AudioSource pop4;
    public AudioSource coin1;
    public AudioSource coin2;
    public AudioSource coin3;
    public AudioSource slowMo;
    public AudioSource hungerBGone;
    public AudioSource gravity;
    public AudioSource cosmicBurger;
    public AudioSource splat;
    public AudioSource growShort;
    public AudioSource growLong;
    public AudioSource shrinkShort;
    public AudioSource shrinkLong;
    public AudioSource click;
    public AudioSource close;
    public AudioSource upgradeSuccessful;
    public AudioSource cantAffordUpgrade;
    public AudioSource purchaseSuccessful;
    public AudioSource purchaseFailed;
    public AudioSource roundOver;
    public AudioSource achievement;
    public AudioSource majesticWhoosing;
    public AudioSource menuDrums;
    public AudioSource menuLead;
    public AudioSource secret;

    float risingPitch;

    Dictionary<int, AudioSource> chompDict;
    Dictionary<int, AudioSource> coinDict;
    Dictionary<int, AudioSource> bonkDict;
    Dictionary<int, AudioSource> popDict;

    public static AudioManager instance;
    bool leadAdded;
    float stopWhooshingTimer;
    bool stoppingWhooshing;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        _sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
        _musicVolume = PlayerPrefs.GetFloat("musicVolume");

        risingPitch = 0.1f;

        chompDict = new Dictionary<int, AudioSource>() { { 0, chomp1 }, { 1, chomp2 }, { 2, chomp3 }, { 3, chomp4 }, { 4, chomp5 }, { 5, chomp6 }, { 6, chomp7 }, { 7, chomp8 }, { 8, chomp9 }, };
        coinDict = new Dictionary<int, AudioSource>() { { 0, coin1}, { 1, coin2 }, { 2, coin3} };
        bonkDict = new Dictionary<int, AudioSource>() { { 0, bonk0 }, { 1, bonk1 }, { 2, bonk2 }, { 3, bonk3 }, { 4, bonk4 }, { 5, bonk5 }, { 6, bonk6 }, { 7, bonk7 }, { 8, bonk8 }, { 9, bonk9 }, };
        popDict = new Dictionary<int, AudioSource>() { { 0, pop0 }, { 1, pop1 }, { 2, pop2 }, { 3, pop3 }, { 4, pop4 }, };

        meteorHighFilter = meteorNoise.gameObject.GetComponent<AudioHighPassFilter>();
        meteorLowFilter = meteorNoise.gameObject.GetComponent<AudioLowPassFilter>();

        PlayMenuMusic();
    }

    void PlayMenuMusic()
    {
        menuDrums.volume = _musicVolume;
        menuLead.volume = 0;
        menuDrums.Play();
        menuLead.Play();
    }

    public void MenuMusicVolume()
    {
        menuDrums.volume = _musicVolume;
        if(leadAdded)
        {
            menuLead.volume = _musicVolume;
        }
    }

    public void StopMenuMusic()
    {
        menuDrums.Stop();
        menuLead.Stop();
    }

    public void RoundStart()
    {
        roundStart.volume = _sfxVolume;
        roundStart.Play();
    }

    public void PrizeAudio(Prize prize)
    {
        if(prize == Prize.Constellation)
        {
            NiceTry();
        }
        if(prize == Prize.Cosmic)
        {
            GreatJob();
        }
        if(prize == Prize.Galactic)
        {
            Incredible();
        }
    }

    void NiceTry()
    {
        niceTry.volume = _sfxVolume;
        niceTry.Play();
    }

    void GreatJob()
    {
        greatJob.volume = _sfxVolume;
        greatJob.Play();
    }

    void Incredible()
    {
        incredible.volume = _sfxVolume;
        incredible.Play();
    }

    public void AddElementToMenuMusic()
    {
        if (!leadAdded)
        {
            menuLead.volume = _musicVolume;
            leadAdded = true;
        }
    }

    public void PlayGameplayMusic()
    {
        gameplayMusic.volume = _musicVolume;
        gameplayMusic.Play();
    }

    public void StopGameplayMusic()
    {
        gameplayMusic.Stop();
    }

    public void RoundStartingPop()
    {
        pop0.pitch = risingPitch;
        pop0.volume = _sfxVolume;
        pop0.Play();
        risingPitch += 0.1f;
    }

    public void Turd()
    {
        Pop();
    }

    public void ChangeSFXVol(float volume)
    {
        _sfxVolume = volume;
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    public void ChangeMusicVolume(float volume)
    {
        _musicVolume = volume;
        PlayerPrefs.SetFloat("musicVolume", volume);
        MenuMusicVolume();
    }

    public void CometRumble()
    {
        cometRumble.pitch = UnityEngine.Random.Range(0.4f, 0.6f);
        cometRumble.volume = 0;
        cometRumble.Play();
    }

    public void CometRumbleVolume(float volume)
    {
        cometRumble.volume = Mathf.Clamp((_sfxVolume * volume * 1.3f), 0, 1);
    }

    public void StopCometRumble()
    {
        cometRumble.Stop();
    }

    public void CometExplosion()
    {
        cometExplosion.pitch = UnityEngine.Random.Range(0.7f, 1.5f);
        cometExplosion.volume = Mathf.Clamp((_sfxVolume * 1.2f), 0, 1);
        cometExplosion.Play();
        cometExplosionBass.pitch = UnityEngine.Random.Range(0.2f, 0.3f);
        cometExplosionBass.volume = _sfxVolume;
        cometExplosionBass.Play();
    }

    public void MeteorRumble()
    {
        meteorRumble.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        meteorRumble.volume = 0;
        meteorRumble.Play();

        meteorNoise.pitch = UnityEngine.Random.Range(0.2f, 0.3f);
        meteorNoise.volume = 0;
        meteorNoise.Play();

    }

    public void MeteorRumbleVolume(float volume)
    {
        float adjustedVol = Mathf.Clamp((_sfxVolume * volume * 1.4f), 0, 1);
        meteorRumble.volume = adjustedVol;
        meteorNoise.volume = (volume * volume) * 0.4f;
        float freq = ((volume * volume) * 5500 ) + 180;
        meteorLowFilter.cutoffFrequency = freq;
        meteorHighFilter.cutoffFrequency = freq;
    }

    public void StopMeteorRumble()
    {
        meteorRumble.Stop();
        meteorNoise.Stop();
    }

    public void MeteorExplosion()
    {
        meteorExplosion.pitch = UnityEngine.Random.Range(0.7f, 1.5f);
        meteorExplosion.volume = Mathf.Clamp((_sfxVolume * 1.2f), 0, 1); 
        meteorExplosion.Play();
        meteorExplosionBass.pitch = UnityEngine.Random.Range(0.2f, 0.3f);
        meteorExplosionBass.volume = _sfxVolume;
        meteorExplosionBass.Play();
    }

    public void AssassinExplosion()
    {
        assassinExplosion.pitch = UnityEngine.Random.Range(0.7f, 1.5f);
        assassinExplosion.volume = Mathf.Clamp((_sfxVolume * 1.2f), 0, 1);
        assassinExplosion.Play();
        assassinExplosionBass.pitch = UnityEngine.Random.Range(0.2f, 0.3f);
        assassinExplosionBass.volume = _sfxVolume;
        assassinExplosionBass.Play();
    }

    public void SlowMo()
    {
        slowMo.pitch = UnityEngine.Random.Range(0.4f, 0.6f);
        slowMo.volume = _sfxVolume;
        slowMo.Play();
    }

    public void HungerBGone()
    {
        hungerBGone.pitch = UnityEngine.Random.Range(0.7f, 1.3f);
        hungerBGone.volume = _sfxVolume;
        hungerBGone.Play();
    }

    public void Gravity()
    {
        gravity.pitch = UnityEngine.Random.Range(0.7f, 1.3f);
        gravity.volume = _sfxVolume;
        gravity.Play();
    }

    public void CosmicBurger()
    {
        cosmicBurger.pitch = UnityEngine.Random.Range(1.1f, 1.5f);
        cosmicBurger.volume = Mathf.Clamp((_sfxVolume * 1.2f), 0, 1);
        cosmicBurger.Play();
    }

    public void Crunch()
    {
        crunch.pitch = UnityEngine.Random.Range(0.7f, 1f);
        crunch.volume = _sfxVolume;
        crunch.Play();
    }

    public void Chomp()
    {
        int j;
        int i = UnityEngine.Random.Range(0, 2);
        if(i == 0)
        {
            j = UnityEngine.Random.Range(0, 4);
        }
        else
        {
            j = UnityEngine.Random.Range(0, 9);
        }

        chompDict[j].pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        chompDict[j].volume = _sfxVolume * 0.8f;
        chompDict[j].Play();
    }

    public void Bonk()
    {
        int i = UnityEngine.Random.Range(0, 10);
        
        bonkDict[i].pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        bonkDict[i].volume = _sfxVolume * 0.8f;
        bonkDict[i].Play();
    }

    public void Coin()
    {
        int i = UnityEngine.Random.Range(0, 3);

        coinDict[i].volume = _sfxVolume;
        coinDict[i].Play();
    }

    public void Pop()
    {
        int i = UnityEngine.Random.Range(0, 5);

        popDict[i].volume = _sfxVolume;
        popDict[i].Play();
    }

    public void Splat()
    {
        splat.pitch = UnityEngine.Random.Range(0.7f, 1.4f);
        splat.volume = _sfxVolume;
        splat.Play();
    }

    public void GrowShort()
    {
        growShort.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        growShort.volume = _sfxVolume * 1.5f;
        growShort.Play();
    }

    public void GrowLong()
    {
        growLong.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        growLong.volume = _sfxVolume * 1.5f;
        growLong.Play();
    }

    public void ShrinkShort()
    {
        shrinkShort.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        shrinkShort.volume = _sfxVolume * 1.5f;
        shrinkShort.Play();
    }

    public void ShrinkLong()
    {
        shrinkLong.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        shrinkLong.volume = _sfxVolume * 1.5f;
        shrinkLong.Play();
    }

    public void Click()
    {
        click.volume = _sfxVolume;
        click.Play();
    }

    public void Close()
    {
        close.volume = _sfxVolume;
        close.Play();
    }

    public void UpgradeSuccessful()
    {
        upgradeSuccessful.volume = _sfxVolume;
        upgradeSuccessful.Play();
    }

    public void CantAffordUpgrade()
    {
        cantAffordUpgrade.volume = _sfxVolume;
        cantAffordUpgrade.Play();
    }

    public void PurchaseSuccessful()
    {
        purchaseSuccessful.volume = _sfxVolume;
        purchaseSuccessful.Play();
    }

    public void PurchaseFailed()
    {
        purchaseFailed.volume = _sfxVolume;
        purchaseFailed.Play();
    }

    public void RoundOver()
    {
        roundOver.volume = _musicVolume;
        roundOver.Play();
    }

    public void Achievement()
    {
        if (!majesticWhoosing.isPlaying)
        {
            achievement.volume = _sfxVolume;
            achievement.Play();
            MajesticWhooshing();
        }
    }

    void MajesticWhooshing()
    {
        majesticWhoosing.volume = _sfxVolume * 0.1f;
        majesticWhoosing.Play();
    }

    public void StopMajesticWhooshing()
    {
        stopWhooshingTimer = 0.1f;
        stoppingWhooshing = true;
    }

    public void Secret()
    {
        secret.volume = _sfxVolume;
        secret.Play();
    }

    private void Update()
    {
        if(stoppingWhooshing)
        {
            stopWhooshingTimer -= Time.deltaTime;
            if(stopWhooshingTimer <= 0)
            {
                if (FindObjectOfType<PanelControl>() == null)
                {
                    majesticWhoosing.Stop();
                    stoppingWhooshing = false;
                }
            }
        }
    }
}

