using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using YG;

namespace UI
{
    public class YandexControl : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(YandexSDKEnabledCoroutine());
        }

        public IEnumerator YandexSDKEnabledCoroutine()
        {
            yield return new WaitUntil(() => YandexGame.SDKEnabled);
            Debug.Log("VAR");
            GameDataManager.InitDataAndStartGame();
        }
    }
}