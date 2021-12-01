using UnityEngine;
public class ProductCoins : Eat.Product
{
    public ProductCoins()
    {
        Name = "Coins";
        Price = Purchaser.instance.GetLocalisedPrice(Purchaser.coins);
        Description = "Recieve enough coins for all 3 upgrades.\nCurrent value: " + UpgradeManager.instance.CombinedUpgradeCost;
        Icon = Icons.instance.coin;

        IsAvaliable = true;
    }

    public override void Purchase()
    {
        if (UpgradeManager.instance.CanAffordAnUpgrade())
        {
            SplashManager.instance.CreatePanel(new BasePanelData { Title = "Unused upgrades!", Body = "You can afford an upgrade. Upgrade first to ensure you get the most coins for your money!" });
        }
        else
        {
            Purchaser.instance.BuyCoins();
        }     
    }
}
