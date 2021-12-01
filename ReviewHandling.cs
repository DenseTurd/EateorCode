using Google.Play.Review;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviewHandling : MonoBehaviour
{
    ReviewManager reviewManager;
    PlayReviewInfo playReviewInfo;


    public static ReviewHandling Instance;
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
        reviewManager = new ReviewManager();
    }

    public IEnumerator RequestReviewFlow()
    {
        var requestFlowOperation = reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;
        if (requestFlowOperation.Error != ReviewErrorCode.NoError)
        {
            Debug.Log(requestFlowOperation.Error.ToString());
            yield break;
        }
        playReviewInfo = requestFlowOperation.GetResult();
        Debug.Log("Review request sucessful");
        StartCoroutine(LaunchFlowOperation());
    }

    public IEnumerator LaunchFlowOperation()
    {
        var launchFlowOperation = reviewManager.LaunchReviewFlow(playReviewInfo);
        yield return launchFlowOperation;
        if (launchFlowOperation.Error != ReviewErrorCode.NoError)
        {
            Debug.Log(launchFlowOperation.Error.ToString());
            yield break;
        }
        Debug.Log("Review flow success");
    }

    public void TriggerReview()
    {
        StartCoroutine(RequestReviewFlow());
    }
}
