using UnityEngine;
public class ProductDonateMedium : Eat.Product
{
    public ProductDonateMedium()
    {
        Name = "Donate\nlots";
        Price = Purchaser.instance.GetLocalisedPrice(Purchaser.donateMedium);
        Description = "Donate a lot! :D";
        Icon = Icons.instance.thumb;

        IsAvaliable = true;
    }

    public override void Purchase()
    {
        if(IsAvaliable)
            Purchaser.instance.DonateMedium();
    }
}
