using UnityEngine;

public class ComedySplashPanels : MonoBehaviour
{
    public static ComedySplashPanels Instance { get; private set; }
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

    private void Start()
    {
        if (PlayerPrefs.GetInt("Rounds without achievement") > 4 && PlayerPrefs.GetInt("ApplesQuestionPosed") == 0)
        {
            SplashManager.instance.CreatePanel
                ( new ChoicePanelData
                {
                    Title = "Question!",
                    Body = "Tell me what you think about apples?",
                    Yes = delegate ()
                    {
                        Debug.Log("Pressed yes for apple question");
                    },
                    No = delegate ()
                    {
                        Debug.Log("Pressed no for apple question");
                    }
                }
                );

            PlayerPrefs.SetInt("ApplesQuestionPosed", 1);
        }
    }

    public void ResetComedy()
    {
        PlayerPrefs.SetInt("ApplesQuestionPosed", 0);
    }
}
