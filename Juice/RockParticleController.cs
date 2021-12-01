using System.Collections.Generic;
using UnityEngine;

public class RockParticleController : MonoBehaviour
{
    float despawnTimer = 0.1f;
    float startingDespawnTimer = 0.1f;
    Vector3 _dir;
    float _speed;
    float t = 0;

    int matIndex;
    public Material rockMat;
    public Material moonMat;
    public Material plutoMat;
    public Material marsMat;
    public Material earthMat;
    public Material uranusMat;
    public Material saturnMat;
    public Material jupiterMat;
    public Material sunMat;

    Dictionary<int, Material> matDict;

    public MeshRenderer renderer;


    private void Start()
    {
        matIndex = 0;
        matDict = new Dictionary<int, Material> 
        { 
            { 0, rockMat }, 
            { 1, moonMat }, 
            { 2, plutoMat }, 
            { 3, marsMat }, 
            { 4, earthMat }, 
            { 5, uranusMat }, 
            { 6, saturnMat },
            { 7, jupiterMat },
            { 8, sunMat }
        };
    }

    public void Init(float scale, Vector3 dir, float speed, int incMatIndex)
    {
        if (matDict == null)
        {
            matDict = new Dictionary<int, Material>
            {
                { 0, rockMat },
                { 1, moonMat },
                { 2, plutoMat },
                { 3, marsMat },
                { 4, earthMat },
                { 5, uranusMat },
                { 6, saturnMat },
                { 7, jupiterMat },
                { 8, sunMat }
            };
        }

        transform.localScale = (new Vector3(scale, scale, scale));
        _dir = dir;
        _speed = speed;
        matIndex = incMatIndex;
        renderer.material = matDict[matIndex];
    }

    void Update()
    {
        despawnTimer -= Time.deltaTime;
        if (despawnTimer <= 0)
        {
            t = 0;
            despawnTimer = Random.Range(0.09f, 0.22f);
            startingDespawnTimer = despawnTimer;
            RockParticlePool.Instance.ReturnToPool(this);
        }

        t += (1/startingDespawnTimer) * Time.deltaTime;
        float speedAdjust = _speed * (1 - (t * t * t));
        transform.position = transform.position + _dir * speedAdjust * Time.deltaTime;

    }
}
