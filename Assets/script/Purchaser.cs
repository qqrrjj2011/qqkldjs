using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppStoresSupport;

using System;
 

using UnityEngine.UI;
using UnityEngine.Purchasing;
using UnityEngine.Store; // UnityChannel
                     
#if RECEIPT_VALIDATION
using UnityEngine.Purchasing.Security;
#endif

public class Purchaser : MonoBehaviour,IStoreListener{
	private IStoreController m_Controller;  
	private IExtensionProvider m_StoreExtensionProvider;
	Action<int> payAtion;
	private UnityEngine.Purchasing.Security.CrossPlatformValidator validator;  
	// Use this for initialization
	// 月川id
	string[] payIDs = {"jige01","","jige02","jige03","jige05","",""};

	//string[] payIDs = {"tanqiu01","","tanqiu02","tanqiu03","tanqiu05","tanqiu06","tanqiu07"};
	int curId;
	void Start () {

		 var module = StandardPurchasingModule.Instance();  
		var builder = ConfigurationBuilder.Instance (module);  
	
		//添加计费点  
		#if UNITY_ANDROID
			builder.AddProduct(payIDs[0], ProductType.Consumable);  
		#elif UNITY_IOS
			builder.AddProduct(payIDs[0], ProductType.NonConsumable);  
		#endif
		
		builder.AddProduct(payIDs[2], ProductType.Consumable);  
		builder.AddProduct(payIDs[3], ProductType.Consumable);  
		builder.AddProduct(payIDs[4], ProductType.Consumable);  


		// #if UNITY_ANDROID
		// 	builder.AddProduct("tanqiu01", ProductType.Consumable);  
		// #elif UNITY_IOS
		// 	builder.AddProduct("tanqiu01", ProductType.NonConsumable);  
		// #endif
		
		// builder.AddProduct("tanqiu02", ProductType.Consumable);  
		// builder.AddProduct("tanqiu03", ProductType.Consumable);  
		// builder.AddProduct("tanqiu05", ProductType.Consumable);  

 
 
		    // builder.AddProduct("", ProductType.NonConsumable);
            // // And finish adding the subscription product. Notice this uses store-specific IDs, illustrating
            // // if the Product ID was configured differently between Apple and Google stores. Also note that
            // // one uses the general kProductIDSubscription handle inside the game - the store-specific IDs 
            // // must only be referenced here. 
            // builder.AddProduct("kProductIDSubscription", ProductType.Subscription, new IDs(){
            //     { kProductNameAppleSubscription, AppleAppStore.Name },
            //     { kProductNameGooglePlaySubscription, GooglePlay.Name },
            // });

	
		UnityPurchasing.Initialize (this, builder);  
	}
	
	// Update is called once per frame
	void Update () {
		
	}



public void OnInitialized (IStoreController controller, IExtensionProvider extensions) {  
    m_Controller = controller;  

	m_StoreExtensionProvider =extensions;
	
	if(m_Controller != null)
	{
		 // On Apple platforms we need to handle deferred purchases caused by Apple's Ask to Buy feature.  
		// On non-Apple platforms this will have no effect; OnDeferred will never be called.  
		var m_AppleExtensions = extensions.GetExtension<IAppleExtensions> ();  
		m_AppleExtensions.RegisterPurchaseDeferredListener(OnDeferred);  
	
		// var product = m_Controller.products.WithID("jige01");  
		// //价格 (带货币单位的字符串)  
		// var priceString = product.metadata.localizedPriceString;  
		// //价格 （换算汇率后的价格）  
		// var price = product.metadata.localizedPrice;  
	}
   
}  


//初始化失败（没有网络的情况下并不会调起，而是一直等到有网络连接再尝试初始化）  
public void OnInitializeFailed (InitializationFailureReason error) {  
      
    Debug.Log("Billing failed to initialize!");  
    switch (error) {  
        case InitializationFailureReason.AppNotKnown:  
            Debug.LogError("Is your App correctly uploaded on the relevant publisher console?");  
            break;  
        case InitializationFailureReason.PurchasingUnavailable:  
            // Ask the user if billing is disabled in device settings.  
            Debug.Log("Billing disabled!");  
            break;  
        case InitializationFailureReason.NoProductsAvailable:  
            // Developer configuration error; check product metadata.  
            Debug.Log("No products available for purchase!");  
            break;  
    }  
}  

	public void DoIapPurchase(int id,Action<int> ation) {  
		payAtion = ation;
		if (m_Controller != null) {  
			var product = m_Controller.products.WithID (payIDs[id]);  
			curId = id;
			if (product != null && product.availableToPurchase) {  
				Debug.Log(">>>>>>>>>>start pay");
				//调起支付  
				m_Controller.InitiatePurchase(product);  
			}  
			else {  
			// callback (false, "no available product");  
			Debug.Log(">>>>>>>>>>fail 1");
				payAtion(0);
			}  
		}  
		else {  
			Debug.Log(">>>>>>>>>>fail 2  m_Controller = null");
			payAtion(0);
		}  
}  



	public PurchaseProcessingResult ProcessPurchase (PurchaseEventArgs e) {  
		Debug.Log("Purchase OK: " + e.purchasedProduct.definition.id);
        Debug.Log("Receipt: " + e.purchasedProduct.receipt);
			payAtion(1);
			GameMgr.inst().purchaserEvent(curId);
			// try {  
			// 	var result = validator.Validate (e.purchasedProduct.receipt);  
			// 	Debug.Log ("Receipt is valid. Contents:");  
			// 	foreach (IPurchaseReceipt productReceipt in result) {  
			// 		Debug.Log(productReceipt.productID);  
			// 		Debug.Log(productReceipt.purchaseDate);  
			// 		Debug.Log(productReceipt.transactionID);  
		
			// 		AppleInAppPurchaseReceipt apple = productReceipt as AppleInAppPurchaseReceipt;  
			// 		if (null != apple) {  
			// 			Debug.Log(apple.originalTransactionIdentifier);  
			// 			Debug.Log(apple.subscriptionExpirationDate);  
			// 			Debug.Log(apple.cancellationDate);  
			// 			Debug.Log(apple.quantity);  
						
		
			// 			//如果有服务器，服务器用这个receipt去苹果验证。  
			// 			var receiptJson = JSONObject.Parse(e.purchasedProduct.receipt);  
			// 			var receipt = receiptJson.GetString("Payload");  
			// 		}  
					
			// 		GooglePlayReceipt google = productReceipt as GooglePlayReceipt;  
			// 		if (null != google) {  
			// 			Debug.Log(google.purchaseState);  
			// 			Debug.Log(google.purchaseToken);  
			// 		}  
			// 	}  
			// 	return PurchaseProcessingResult.Complete;  
			// } catch (IAPSecurityException) {  
			// 	Debug.Log("Invalid receipt, not unlocking content");  
			// 	return PurchaseProcessingResult.Complete;  
			// }  
			return PurchaseProcessingResult.Complete;  
		}  

	public void OnPurchaseFailed(Product i, PurchaseFailureReason p) {  
    		Debug.Log("purchase failed of reason : " + p.ToString());  
			payAtion(0);
		}  



    private void OnDeferred(Product item)
    {
        Debug.Log("Purchase deferred: " + item.definition.id);
    }


	 public void RestorePurchases(Action<int> ation)
        {
            // If Purchasing has not yet been set up ...
            if (!IsInitialized())
            {
                // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
                Debug.Log("RestorePurchases FAIL. Not initialized.");
				ation(0);
                return;
            }
            
            // If we are running on an Apple device ... 
            if (Application.platform == RuntimePlatform.IPhonePlayer || 
                Application.platform == RuntimePlatform.OSXPlayer)
            {
                // ... begin restoring purchases
                Debug.Log("RestorePurchases started ...");
                
                // Fetch the Apple store-specific subsystem.
                var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
                // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
                // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
                apple.RestoreTransactions((result) => {
                    // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                    // no purchases are available to be restored.
                    Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
					if(result)
					{
						ation(1);
					}
					else
					{
						ation(0);
					}
                });
            }
            // Otherwise ...
            else
            {
                // We are not running on an Apple device. No work is necessary to restore purchases.
                Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
				ation(0);
            }
        }

	   private bool IsInitialized()
        {
            // Only say we are initialized if both the Purchasing references are set.
            return m_Controller != null && m_StoreExtensionProvider != null;
        }
	
}
