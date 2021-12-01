using UnityEngine;
public class ProductDonateSmall : Eat.Product
{
    public ProductDonateSmall()
    {
        Name = "Donate";
        Price = Purchaser.instance.GetLocalisedPrice(Purchaser.donateSmall);
        Description = "Donate a little :)";
        Icon = Icons.instance.thumb;

        IsAvaliable = true;
    }

    public override void Purchase()
    {
        if(IsAvaliable)
            Purchaser.instance.DonateSmall();
    }
}
