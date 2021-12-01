using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
// Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
public class Purchaser : MonoBehaviour, IStoreListener
{
    public static Purchaser instance;

    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

    // Product identifiers for all products capable of being purchased: 
    // "convenience" general identifiers for use with Purchasing, and their store-specific identifier 
    // counterparts for use with and outside of Unity Purchasing. Define store-specific identifiers 
    // also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)

    // General product identifiers for the consumable, non-consumable, and subscription products.
    // Use these handles in the code to reference which product to purchase. Also use these values 
    // when defining the Product Identifiers on the store. Except, for illustration purposes, the 
    // kProductIDSubscription - it has custom Apple and Google identifiers. We declare their store-
    // specific mapping to Unity Purchasing's AddProduct, below.
    public static string coins = "coins";
    public static string removeAds = "noads";
    public static string beeteor = "beeteor";
    public static string donateSmall = "donatesmall";
    public static string donateMedium = "donatemedium";
    public static string donateLarge = "donatelarge";

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        // If we haven't set up the Unity Purchasing reference << setting up the purchasing reference
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }
    }
    void Start()
    {
        // may need to move setting up the purchasing reference back here if you have issues
    }

    public void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }

        // Create a builder, first passing in a suite of Unity provided stores.
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Add a product to sell / restore by way of its identifier, associating the general identifier
        // with its store-specific identifiers.
        builder.AddProduct(coins, ProductType.Consumable);
        builder.AddProduct(donateSmall, ProductType.Consumable);
        builder.AddProduct(donateMedium, ProductType.Consumable);
        builder.AddProduct(donateLarge, ProductType.Consumable);
        // Continue adding the non-consumable products.
        builder.AddProduct(removeAds, ProductType.NonConsumable);
        builder.AddProduct(beeteor, ProductType.NonConsumable);

        // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
        // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void BuyNoAds()
    {
        BuyNonConsumable(removeAds);
    }

    public void BuyBeeteor()
    {
        BuyNonConsumable(beeteor);
    }

    public void BuyCoins()
    {
        BuyConsumable(coins);
    }

    public void DonateSmall()
    {
        BuyConsumable(donateSmall);
    }

    public void DonateMedium()
    {
        BuyConsumable(donateMedium);
    }

    public void DonateLarge()
    {
        BuyConsumable(donateLarge);
    }

    public void BuyConsumable(string id)
    {
        // Buy the consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(id);
    }

    public void BuyNonConsumable(string id)
    {
        // Buy the non-consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(id);
    }

    void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);
            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }
    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
    public void RestorePurchases()
    {
        // If Purchasing has not yet been set up ...
        if (!IsInitialized())
        {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }


    //  
    // --- IStoreListener
    //

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        // A consumable product has been purchased by this user.
        if (String.Equals(args.purchasedProduct.definition.id, coins, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // The consumable item has been successfully purchased, add 1000 coins to the player's in-game score.
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + UpgradeManager.instance.CombinedUpgradeCost);
            UiController.instance.UpdateCoinsText();
            SplashManager.instance.CreatePanel(new TextPanelData { Title = Strings.Thankyou, Body = Strings.CoinsThankBody });
            PlayerPrefs.SetInt("Purchase made", 1);
            PointersControl.Instance.Init();

            AudioManager.instance.PurchaseSuccessful();
            Debug.Log("Coins Purchased");
        }
        // Or ... a non-consumable product has been purchased by this user.
        else if (String.Equals(args.purchasedProduct.definition.id, removeAds, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // TODO: The non-consumable item has been successfully purchased, grant this item to the player.
            PlayerPrefs.SetInt("noads", 1);
            AdManager.instance.HideBanner();
            UiController.instance.noAdsButton.SetActive(false);
            SplashManager.instance.CreatePanel(new TextPanelData { Title = Strings.Thankyou, Body = Strings.NoAdsThankBody });

            AudioManager.instance.PurchaseSuccessful();
            Debug.Log("Ads removed");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, beeteor, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // TODO: The non-consumable item has been successfully purchased, grant this item to the player.
            CharacterUnlocker.Instance.Beeteor();
            SplashManager.instance.CreatePanel(new TextPanelData { Title = Strings.Thankyou, Body = Strings.BeeteorThankBody });
            PlayerPrefs.SetInt("Purchase made", 1);
            PointersControl.Instance.Init();

            AudioManager.instance.PurchaseSuccessful();
            Debug.Log("Beeteor purchased");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, donateSmall, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // TODO: The non-consumable item has been successfully purchased, grant this item to the player.
            SplashManager.instance.CreatePanel(new TextPanelData { Title = Strings.Thankyou, Body = Strings.DonationSmallThankBody });
            PlayerPrefs.SetInt("Purchase made", 1);
            PlayerPrefs.SetInt("Donated small", 1);
            StoreManager.Instance.Init();
            PointersControl.Instance.Init();

            AudioManager.instance.PurchaseSuccessful();
            Debug.Log("Donation small recieved!");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, donateMedium, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // TODO: The non-consumable item has been successfully purchased, grant this item to the player.
            SplashManager.instance.CreatePanel(new TextPanelData { Title = Strings.Thankyou, Body = Strings.DonationMedThankBody });
            PlayerPrefs.SetInt("Purchase made", 1);
            PlayerPrefs.SetInt("Donated medium", 1);
            StoreManager.Instance.Init();
            PointersControl.Instance.Init();

            AudioManager.instance.PurchaseSuccessful();
            Debug.Log("Donation medium recieved!");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, donateLarge, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // TODO: The non-consumable item has been successfully purchased, grant this item to the player.
            SplashManager.instance.CreatePanel(new TextPanelData { Title = Strings.Thankyou, Body = Strings.DonationLargeThankBody });
            PlayerPrefs.SetInt("Purchase made", 1);
            PlayerPrefs.SetInt("Donated large", 1);
            StoreManager.Instance.Init();
            PointersControl.Instance.Init();

            AudioManager.instance.PurchaseSuccessful();
            Debug.Log("Donation large recieved!");
        }

        // Or ... an unknown product has been purchased by this user. Fill in additional products here....
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
            AudioManager.instance.PurchaseFailed();
        }

        // Return a flag indicating whether this product has completely been received, or if the application needs 
        // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
        // saving purchased products to the cloud, and when that save is delayed. 
        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }

    public string GetLocalisedPrice(string productID)
    {
        string price;

        Product product = m_StoreController.products.WithID(productID);
        price =  product.metadata.localizedPriceString;

        return price;
    }
}
