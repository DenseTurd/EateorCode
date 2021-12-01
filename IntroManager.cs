using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class IntroManager : MonoBehaviour
{
    public TMP_Text tapToStart;
    float displayTapToStartTimer = 1f;

    void Update()
    {
        if (Input.touchCount > 0)
        {
                GoToMain();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
                GoToMain();
        }

        displayTapToStartTimer -= Time.deltaTime;

        if (displayTapToStartTimer <= 0)
        {
            tapToStart.enabled = !tapToStart.enabled;
            displayTapToStartTimer = 0.4f;
        }
    }

    void GoToMain()
    {
        SceneManager.LoadScene(1);
    }
}
