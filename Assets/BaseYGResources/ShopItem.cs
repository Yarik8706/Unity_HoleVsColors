using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace UI
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private Button buyItemButton;
        [SerializeField] private TMP_Text priceText;
        [SerializeField] private Button showAdsButton;
        [SerializeField] private Button putOnButton;
        [SerializeField] private Image _image;
        [SerializeField] private GameObject alreadyPutOn;

        private static ShopItem _alreadyPutOnItem;

        private int _price;
        private int _itemId;
        private bool _isRewVideo;

        public void InitItem(ShopItemData shopItemData)
        {
            _price = shopItemData.price;
            _itemId = shopItemData.id;
            _image.color = shopItemData.color;
            switch (shopItemData.shopItemState) 
            {
                case ShopItemState.NeedBuy:
                    showAdsButton.gameObject.SetActive(false);
                    putOnButton.gameObject.SetActive(false);
                    alreadyPutOn.gameObject.SetActive(false);
                    buyItemButton.onClick.AddListener(BuyItem);
                    priceText.text = _price.ToString();
                    break;
                case ShopItemState.NeedShowAds:
                    putOnButton.gameObject.SetActive(false);
                    buyItemButton.gameObject.SetActive(false);
                    alreadyPutOn.gameObject.SetActive(false);
                    ShopUI.Instance.RewardVideoEvent.AddListener(EndRewVideo);
                    showAdsButton.onClick.AddListener(StartRewVideo);
                    break;
                case ShopItemState.AlreadyBuying:
                    alreadyPutOn.gameObject.SetActive(false);
                    buyItemButton.gameObject.SetActive(false);
                    showAdsButton.gameObject.SetActive(false);
                    putOnButton.onClick.AddListener(PutOn);
                    break;
                default:
                    return;
            }

            if (shopItemData.alreadyPutOn)
            {
                PutOn();
            }
        }

        private void PutOn()
        {
            GameDataManager.SetPutOn(_itemId);
            putOnButton.gameObject.SetActive(false);
            alreadyPutOn.gameObject.SetActive(true);
            LevelControl.Instance.putOnItem.Play();
            if(_alreadyPutOnItem != null) _alreadyPutOnItem.TakeOff();
            SetShopItemManager.Instance.SetSkin(_itemId);
            _alreadyPutOnItem = this;
        }

        private void TakeOff()
        {
            putOnButton.gameObject.SetActive(true);
            alreadyPutOn.gameObject.SetActive(false);
        }

        private void BuyItem()
        {
            if(GameDataManager.Coins < _price) return;
            YandexMetrica.Send("Buy Item "+_itemId);
            ShopUI.Instance.BuyItem(_itemId);
            GameDataManager.AddCoin(-_price);
            LevelControl.Instance.buyItem.Play();
            Destroy(gameObject);
        }

        private void StartRewVideo()
        {
            YandexGame.RewVideoShow((int)VideoAdsId.AdsForShop);
            _isRewVideo = true;
        }

        private void EndRewVideo()
        {
            if(!_isRewVideo) return;
            _isRewVideo = false;
            BuyItem();
        }
    }
}