using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace UI
{
    public enum VideoAdsId
    {
        AdsForMoney,
        AdsForShop
    }
    public class AdsMoneyButton : MonoBehaviour
    {
        private Button _saveScoreButton;
        
        private void Start()
        {
            _saveScoreButton = GetComponent<Button>();
            DOTween.Sequence().Append(_saveScoreButton.transform.DOScale(1.2f, 0.7f))
                .Append(_saveScoreButton.transform.DOScale(1f, 0.7f))
                .SetLoops(-1);
            _saveScoreButton.onClick.AddListener(ShowRewVideo);
        }
        
        private void OnEnable() => YandexGame.RewardVideoEvent += RewVideoEnd;
    
        private void OnDisable() => YandexGame.RewardVideoEvent -= RewVideoEnd;
        
        private void ShowRewVideo()
        {
            YandexGame.RewVideoShow((int)VideoAdsId.AdsForMoney);
        }

        private void RewVideoEnd(int i)
        {
            if(i != (int)VideoAdsId.AdsForMoney) return;
            GameDataManager.AddCoin(GameStateProperty.AdsMoney);
        }
    }
}