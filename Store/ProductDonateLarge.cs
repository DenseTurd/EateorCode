using UnityEngine;
public class ProductDonateLarge : Eat.Product
{
    public ProductDonateLarge()
    {
        Name = "Donate\nloads!";
        Price = Purchaser.instance.GetLocalisedPrice(Purchaser.donateLarge);
        Description = "Donate a whole heap! XD";
        Icon = Icons.instance.thumb;

        IsAvaliable = true;
    }

    public override void Purchase()
    {
        if(IsAvaliable)
            Purchaser.instance.DonateLarge();
    }
}
