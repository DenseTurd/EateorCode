using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class AdManager : MonoBehaviour, IUnityAdsListener
{

    // be sure to check out the enable testing on the developer console to avoid being flagged for fraud.

    public static AdManager instance;

    string gameID = "3700457";
    bool testMode = true;
    string roundOverId = "roundOver";
    string bannerID = "banner";
    public string rewardAdID = "rewardedVideo";
    public bool rewardAdReady;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        rewardAdReady = false;
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameID, testMode);

        if(PlayerPrefs.GetInt("noads") ==0)
        StartCoroutine(ShowBannerWhenReady());
    }
    public void OnUnityAdsDidFinish(string placementID, ShowResult showResult)
    {
        if (placementID == rewardAdID) // stops regular ad upgrading 
        {
            if (showResult == ShowResult.Finished)
            {
                if (UpgradeManager.instance.toBeUpgraded == ToBeUpgraded.disintegrate)
                {
                    UpgradeManager.instance.DetermineDisintegrationUpgradeMethod(UpGradeMethod.rewardAd);
                }
                if (UpgradeManager.instance.toBeUpgraded == ToBeUpgraded.startingSize)
                {
                    UpgradeManager.instance.DetermineStartingSizeUpgradeMethod(UpGradeMethod.rewardAd);
                }
            }
            if (showResult == ShowResult.Skipped)
            {
                Debug.Log("Ad skipped, no reward given");
                // Dont reward a skip
            }
        }
        if(showResult == ShowResult.Failed)
        {
            Debug.Log("Ad failed");
        }
    }
    public void OnUnityAdsReady(string placementID)
    {
        if (!rewardAdReady)
        {
            if (placementID == rewardAdID)
            {
                Debug.Log("Reward Ad ready");
                rewardAdReady = true;
            }
        }
    }
    public void OnUnityAdsDidError(string placementID)
    {
        Debug.Log("Ad error");
    }
    public void OnUnityAdsDidStart(string placementID)
    {
        // optional
    }
    public void ShowRewardAd()
    {
        Advertisement.Show(rewardAdID);
    }
    public IEnumerator ShowBannerWhenReady()
    {
        if (PlayerPrefs.GetInt("noads") == 0)
        {
            if (!GameManager.play)
            {
                while (!Advertisement.IsReady(bannerID))
                {
                    yield return new WaitForSeconds(0.5f);
                }
                Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
                Advertisement.Banner.Show(bannerID);
                Debug.Log("Banner up");
            }
            else
            {
                Advertisement.Banner.Hide();
            }
        }
        else
        {
            Advertisement.Banner.Hide();
        }
    }

    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }
    public void ShowRoundOverAd()
    {
        if (PlayerPrefs.GetInt("noads") == 0)
        {
            Debug.Log("Attempting ad");
            if (Advertisement.IsReady())
            {
                Advertisement.Show(roundOverId);
            }
            else
            {
                Debug.Log("not ready");
            }
        }
    }

}
