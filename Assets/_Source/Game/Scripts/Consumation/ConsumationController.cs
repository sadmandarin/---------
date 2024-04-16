using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PersistentData;
using ShopPage;
using System.Linq;
using Agava.YandexGames;
using System;

namespace Legion
{
    [CreateAssetMenu(menuName = "Shop/ConsumationController")]
    public class ConsumationController : ScriptableObject
    {
        public Action OnConsumingFinished;

        [SerializeField] private List<YandexProduct> _products;
        public void HandleConsumation(List<PurchasedProduct> products, out bool consumedSomething)
        {
            Debug.Log("Trying to consume products:");
            foreach (var product in products)
            {
                Debug.Log(product.productID.ToString());
            }
            consumedSomething = false;
            foreach (var product in products)
            {
                YandexProduct yandexProduct = _products.FirstOrDefault(n => n.YandexId == product.productID);
                if (product == null)
                {
                    Debug.LogError("matching product id not found");
                    continue;
                }

                Billing.ConsumeProduct(product.purchaseToken, () =>
                {
                    yandexProduct.GetRewardForProduct();
                    Debug.Log("Succesfully consumed " + product);
                }, (data) =>
                {
                    Debug.Log("Error trying to consume " + product + ". Error: " + data);
                });
                consumedSomething = true;
            }
            OnConsumingFinished?.Invoke();
        }
    }
}
