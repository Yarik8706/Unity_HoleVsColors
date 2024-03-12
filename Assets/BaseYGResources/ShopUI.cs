using System;
using UnityEngine;
using UnityEngine.Events;
using YG;

namespace UI
{
    public class ShopUI : MonoBehaviour
    {
        public ShopItemsData shopItemsData;
        
        [SerializeField] private ShopItem shopItemPrefab;
        [SerializeField] private Transform itemsForBuyingContainer;
        [SerializeField] private Transform itemsForPutOnContainer;

        public static ShopUI Instance { get; private set; }

        public UnityEvent RewardVideoEvent { get; set; } = new();

        private void Awake()
        {
            Instance = this;
            gameObject.SetActive(false);
        }

        private void OnEnable() => YandexGame.RewardVideoEvent += AdsRewarded;
            
        private void OnDisable() => YandexGame.RewardVideoEvent -= AdsRewarded;
        
        public void SpawnItems()
        {
            foreach (var shopItemData in shopItemsData.shopItemsData)
            {
                shopItemData.alreadyPutOn = false;
            }
            shopItemsData.shopItemsData[GameDataManager.PutOnItem].alreadyPutOn = true;
            foreach (var i in GameDataManager.BoughtItems)
            {
                shopItemsData.shopItemsData[i].shopItemState = ShopItemState.AlreadyBuying;
            }
            foreach (var shopItemData in shopItemsData.shopItemsData)
            {
                Transform container = shopItemData.shopItemState == ShopItemState.AlreadyBuying
                    ? itemsForPutOnContainer : itemsForBuyingContainer;
                var shopItem = Instantiate(shopItemPrefab, container);
                shopItem.InitItem(shopItemData);
            }
        }

        public void BuyItem(int id)
        {
            shopItemsData.shopItemsData[id].shopItemState = ShopItemState.AlreadyBuying;
            GameDataManager.AddBoughtItem(id);
            var itemData = shopItemsData.shopItemsData[id];
            var shopItem = Instantiate(shopItemPrefab, itemsForPutOnContainer);
            shopItem.InitItem(itemData);
        }

        private void AdsRewarded(int id)
        {
            if(id != (int) VideoAdsId.AdsForShop) return;
            RewardVideoEvent.Invoke();
        }
    }
}