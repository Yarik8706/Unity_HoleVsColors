using System;
using UnityEngine;

namespace UI
{
    public class SetShopItemManager : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer holeSkin;

        public static SetShopItemManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void SetSkin(int index)
        {
            holeSkin.color = ShopUI.Instance.shopItemsData.shopItemsData[index].color;
        }
    }
}