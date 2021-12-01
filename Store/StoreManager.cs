using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    List<Eat.Product> products;

    public GameObject prefab;

    public Transform content;

    public static StoreManager Instance { get; private set; }
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
        products = new List<Eat.Product>
        {
            new ProductCoins(),
            new ProductBeeteor(),
            new ProductDonateSmall(),
            new ProductDonateMedium(),
            new ProductDonateLarge()
        };

        PopulateStore();
    }

    public void PopulateStore()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        foreach (Eat.Product product in products)
        {
            var p = Instantiate(prefab, content);

            ProductPrefab productPrefab = p.GetComponent<ProductPrefab>();
            productPrefab.product = product;
            productPrefab.FillData();
        }
    }
}
