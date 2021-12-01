using System;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    Camera mainCam;
    public Camera backgroundCam;
    float linearOffset = 8f;

    private void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        if (GameManager.play)
        {
            float playerScale = PlayerController.PlayerScale;

            // Additional camera work to stop player filling more of the screen after size 500
            float scaledOffset = playerScale * ((float)Math.PI) * 2f;
            float additionalOffsetWithLimiter = Mathf.Min(playerScale / 1000 * playerScale, playerScale / 2 );

            mainCam.transform.position = new Vector3
                (mainCam.transform.position.x, 
               mainCam.transform.position.y, 
                -(linearOffset + scaledOffset + additionalOffsetWithLimiter));

            var farClipPlaneDist = ((playerScale / (float)Math.PI) * 1000) + 1000;
            mainCam.farClipPlane = farClipPlaneDist;
            backgroundCam.farClipPlane = farClipPlaneDist;

            if (!CameraShake.instance.shaking)
            {
                if(mainCam.transform.localPosition.x != 0)
                mainCam.transform.localPosition = new Vector3(0, 0, mainCam.transform.localPosition.z);
            }
        }
    }
}
