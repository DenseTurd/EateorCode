using UnityEngine;

public class SplashScores : MonoBehaviour
{
    public static SplashScores Instance { get; private set; }

    public GameObject scorePrefab;

    RectTransform rectTransform;

    string prefix = "+";

    void Awake()
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

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SplashScore(float score, Vector3 pos)
    {
        var splish = SplashScorePool.Instance.Get();
        splish.transform.SetParent(this.transform);

        if(score < 0)
        {
            prefix = "-";
        }
        else
        {
            prefix = "+";
        }

        splish.GetComponent<TMPro.TMP_Text>().text = prefix + score.ToString("F0");

        Vector2 canvasPos;
        Vector2 screenPos = Camera.main.WorldToViewportPoint(pos);

        Vector2 offset = new Vector2(-500, -500);

        canvasPos = (screenPos * 1000) + offset;

        splish.initialScale = 1.25f;
        splish.currentTimer = 0;

        splish.transform.localPosition = canvasPos;
        splish.gameObject.SetActive(true);
    }
}
