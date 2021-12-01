using UnityEngine;
public class ProductBeeteor : Eat.Product
{
    public ProductBeeteor()
    {
        Name = "Beeteor";
        Price = Purchaser.instance.GetLocalisedPrice(Purchaser.beeteor);
        Icon = Icons.instance.beeteor;
        Description = "Unlocks Beeteor as a playable character";

        if (PlayerPrefs.GetInt("Beeteor unlocked") == 0)
        {
            IsAvaliable = true;
        }
        else if (PlayerPrefs.GetInt("Beeteor unlocked") == 1)
        {
            IsAvaliable = false;
        }
    }

    public override void Purchase()
    {
        if (IsAvaliable)
            Purchaser.instance.BuyBeeteor();
    }
}
