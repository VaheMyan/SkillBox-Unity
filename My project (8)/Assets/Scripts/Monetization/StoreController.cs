using System.Collections.ObjectModel;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class StoreController : IStore
{
    public void FinishTransaction(ProductDefinition product, string transactionId)
    {
        throw new System.NotImplementedException();
    }

    public void Initialize(IStoreCallback callback)
    {
        throw new System.NotImplementedException();
    }

    public void Purchase(ProductDefinition product, string developerPayload)
    {
        throw new System.NotImplementedException();
    }

    public void RetrieveProducts(ReadOnlyCollection<ProductDefinition> products)
    {
        throw new System.NotImplementedException();
    }
}

public class StoreListener : IStoreListener
{
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        throw new System.NotImplementedException();
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        throw new System.NotImplementedException();
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        throw new System.NotImplementedException();
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        throw new System.NotImplementedException();
    }
}
