using System.Collections.Generic;
using UnityEngine;

public class RockDictionary : MonoBehaviour
{
    public GameObject rockMesh;
    public GameObject moonMesh;
    public GameObject plutoMesh;
    public GameObject marsMesh;
    public GameObject earthMesh;
    public GameObject uranusMesh;
    public GameObject saturnMesh;
    public GameObject jupiterMesh;
    public GameObject sunMesh;

    public GameObject rockPop1;
    public GameObject rockPop2;
    public GameObject moonPop1;
    public GameObject moonPop2;
    public GameObject plutoPop1;
    public GameObject plutoPop2;
    public GameObject marsPop1;
    public GameObject marsPop2;
    public GameObject earthPop1;
    public GameObject earthPop2;
    public GameObject uranusPop1;
    public GameObject uranusPop2;
    public GameObject saturnPop1;
    public GameObject saturnPop2;
    public GameObject jupiterPop1;
    public GameObject jupiterPop2;
    public GameObject sunPop1;
    public GameObject sunPop2;

    public Dictionary<int, Rock> RocksDictionary { get; private set; }


    private void Awake()
    {
        
        RocksDictionary = new Dictionary<int, Rock>()
        {
            {0, new Rock{Rok = rockMesh, Pop1 = rockPop1, Pop2 = rockPop2 } },
            {1, new Rock{Rok = moonMesh, Pop1 = moonPop1, Pop2 = moonPop2} },
            {2, new Rock{Rok = plutoMesh, Pop1 = plutoPop1, Pop2 = plutoPop2 } },
            {3, new Rock{Rok = marsMesh, Pop1 = marsPop1, Pop2 = marsPop2 } },
            {4, new Rock{Rok = earthMesh, Pop1 = earthPop1, Pop2 = earthPop2 } },
            {5, new Rock{Rok = uranusMesh, Pop1 = uranusPop1, Pop2 = uranusPop2 } },
            {6, new Rock{Rok = saturnMesh, Pop1 = saturnPop1, Pop2 = saturnPop2 } },
            {7, new Rock{Rok = jupiterMesh, Pop1 = jupiterPop1, Pop2 = jupiterPop2 } },
            {8, new Rock{Rok = sunMesh, Pop1 = sunPop1, Pop2 = sunPop2 } },
            {9, new Rock{Rok = uranusMesh, Pop1 = uranusPop1, Pop2 = uranusPop2 } },
            {10, new Rock{Rok = uranusMesh, Pop1 = uranusPop1, Pop2 = uranusPop2 } },
            {11, new Rock{Rok = uranusMesh, Pop1 = uranusPop1, Pop2 = uranusPop2 } },
        };

        for (int i = 0; i < RocksDictionary.Count; i++)
        {
            var rokRenderer = RocksDictionary[i].Rok.GetComponent<MeshRenderer>();
            rokRenderer.shadowCastingMode = 0;
            rokRenderer.receiveShadows = false;
            var pop1Renderer = RocksDictionary[i].Pop1.GetComponent<MeshRenderer>();
            pop1Renderer.shadowCastingMode = 0;
            pop1Renderer.receiveShadows = false;
            var pop2Renderer = RocksDictionary[i].Pop2.GetComponent<MeshRenderer>();
            pop2Renderer.shadowCastingMode = 0;
            pop2Renderer.receiveShadows = false;        
        }
    }
}

public class Rock
{
    public GameObject Rok { get; set; }
    public GameObject Pop1 { get; set; }
    public GameObject Pop2 { get; set; }
}
