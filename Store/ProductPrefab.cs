using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProductPrefab : MonoBehaviour
{
    public Eat.Product product;

    public TMP_Text name;
    public TMP_Text price;
    public Image icon;
    public TMP_Text purchased;
    public TMP_Text description;
    public void FillData()
    {
        name.text = product.Name;
        price.text = product.Price;

        if(product.Icon != null)
        {
            icon.sprite = product.Icon.sprite;
        }
        else
        {
            icon.enabled = false;
        }

        description.text = product.Description;

        if (!product.IsAvaliable)
        {
            purchased.gameObject.SetActive(true);
        }
    }

    public void Purchase()
    {
        product.Purchase();
    }
}
