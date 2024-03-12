using System;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public enum ShopItemState
    {
        NeedBuy,
        NeedShowAds,
        AlreadyBuying
    }
    
    [Serializable]
    public class ShopItemData
    {
        public int id;
        public int price;
        public Color color;
        public ShopItemState shopItemState;
        public bool alreadyPutOn;
    }
    
    [Serializable]
    [CreateAssetMenu(fileName = "ShopItemsData", menuName = "ShopItemsData")]
    public class ShopItemsData : ScriptableObject
    {
        public ShopItemData[] shopItemsData;
    }
}