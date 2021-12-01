using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct RockData
{
    public BaseRockController controller;
    public int rayAngle;
    public float Dist;
}
public class Autoteor : MonoBehaviour
{
    Transform playerTransform;
    float distToViewportEdgeTop;
    float distToViewpotEdgeRight;
    float maxHypotenuse;
    RaycastHit hit;

    List<int> angleList;

    RockData selectedRock;

    List<RockData> detectedRocks;

    public bool selectedARock;
    float reselectTimer = 2;
    float t;

    public static Autoteor Instance { get; private set; }
    void Awake()
    {
        if(Instance == null)
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
        Init();
    }

    public void Init()
    {
        playerTransform = PlayerController.instance.transform;
        angleList = new List<int> { 0, 12, 22, 34, 45, 57, 68, 79, 90, -12, -22, -34, -45, -57, -68, -79, -90 };
        detectedRocks = new List<RockData>();
        selectedARock = false;
    }

    void FixedUpdate()
    {
        distToViewportEdgeTop = (Camera.main.ViewportToWorldPoint(new Vector3(0,0, PlayerController.instance.camZOffset)) - Camera.main.ViewportToWorldPoint(new Vector3(0, 1, PlayerController.instance.camZOffset))).magnitude;
        distToViewpotEdgeRight = (Camera.main.ViewportToWorldPoint(new Vector3(0, 0, PlayerController.instance.camZOffset)) - Camera.main.ViewportToWorldPoint(new Vector3(1, 0, PlayerController.instance.camZOffset))).magnitude;
        maxHypotenuse = Mathf.Sqrt((distToViewportEdgeTop * distToViewportEdgeTop) + (distToViewpotEdgeRight * distToViewpotEdgeRight));

        //Debug.Log("Adjacent = " + distToViewportEdgeTop + "\nOpposite = " + distToViewpotEdgeRight + "\nHypotenuse = " + maxHypotenuse);
        CastRays();
    }

    void Update()
    {
        if (!selectedARock)
        {
            reselectTimer = 2;
            t = 0;
            SelectRock();
        }

        SteerPlayer();

        if (selectedRock.Dist > (maxHypotenuse * 0.65f))
        {
            selectedARock = false;
        }

        if (selectedARock)
        {
            reselectTimer -= Time.deltaTime;
        }

        if (reselectTimer <= 0)
        {
            selectedARock = false;
        }
    }

    void SteerPlayer()
    {
        if (selectedARock)
        {
            if (selectedRock.controller != null)
            {
                t += Time.deltaTime * 0.1f;
                Vector3 desiredDir = (selectedRock.controller.transform.position - playerTransform.position).normalized;

                Vector3 dir = Vector3.Slerp(playerTransform.up, -desiredDir, Mathf.Min(1, t));

                playerTransform.rotation = Quaternion.LookRotation(playerTransform.forward, dir);
            }
            else
            {
                IdleSteer();
            }
        }
        else
        {
            IdleSteer();
        }
    }

    void IdleSteer()
    {
        {
            Vector3 dir = Vector3.Lerp(playerTransform.up, playerTransform.right, 2f * Time.deltaTime);

            playerTransform.rotation = Quaternion.LookRotation(playerTransform.forward, dir);
        }
    }

    void SelectRock()
    {
        if (detectedRocks.Count > 0)
        {
            selectedRock = detectedRocks[0];

            for (int i = 0; i < detectedRocks.Count; i++)
            {
                if (detectedRocks[i].rayAngle < selectedRock.rayAngle)
                {
                    selectedRock = detectedRocks[i];
                }
            }

            selectedRock.controller.selected = true;
            selectedARock = true;
            detectedRocks.Clear();
        }
    }

    void CastRays()
    {
        for (int i = 0; i < angleList.Count; i++)
        {
            CastRayAtAngle(angleList[i]);
        }
    }

    void CastRayAtAngle(int angle)
    {
        Color color = Color.red;

        var dir = Quaternion.Euler(0, 0, angle) * playerTransform.up;

        var dist = Mathf.Abs(distToViewportEdgeTop / Mathf.Cos(Mathf.Deg2Rad * angle));
        if (dist >= maxHypotenuse)
        {
            dist = Mathf.Abs(distToViewpotEdgeRight / Mathf.Cos(Mathf.Deg2Rad * (90 - angle)));
        }

        float nearScreenEdgeDistance = dist * 0.45f;

        if (Physics.Raycast(playerTransform.position, dir, out hit, nearScreenEdgeDistance))
        {
            if(hit.transform.gameObject.GetComponent<BaseRockController>() != null)
            {
                if (!selectedARock)
                {
                    RockData data = new RockData
                    {
                        controller = hit.transform.gameObject.GetComponent<BaseRockController>(),
                        rayAngle = Mathf.Abs(angle),
                        Dist = hit.distance
                    };
                    detectedRocks.Add(data);
                }
                color = Color.green;
            }
        }

        DrawRayAtAngle(angle, color);
    }

    void DrawRayAtAngle(int angle, Color color)
    {
        var dir = Quaternion.Euler(0, 0, angle) * playerTransform.up;

        var dist = Mathf.Abs(distToViewportEdgeTop / Mathf.Cos(Mathf.Deg2Rad *angle));
        if (dist >= maxHypotenuse)
        {
            dist = Mathf.Abs(distToViewpotEdgeRight / Mathf.Cos(Mathf.Deg2Rad * (90 - angle)));
        }

        float nearScreenEdgeDistance = dist * 0.45f;

        Debug.DrawRay(playerTransform.position, dir * nearScreenEdgeDistance, color);
    }
}
