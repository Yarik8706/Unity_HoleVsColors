using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace UI
{
    public class LevelCompletedInfoControl : MonoBehaviour
    {
        [SerializeField] private GameObject levelCompletedInfoObject;
        [SerializeField] private Button nextLevelButton;
        
        public static LevelCompletedInfoControl Instance { get; private set; }

        private void Start()
        {
            Instance = this;
            nextLevelButton.onClick.AddListener(NextClickEvent);
        }

        public void Init()
        {
            nextLevelButton.gameObject.SetActive(false);
            levelCompletedInfoObject.SetActive(true);
            GameDataManager.AddCoin(50);
            StartCoroutine(InitCoroutine());
        }

        public IEnumerator InitCoroutine()
        {
            yield return new WaitForSeconds(2f);
            nextLevelButton.gameObject.SetActive(true);
        }

        public void NextClickEvent()
        {
            levelCompletedInfoObject.SetActive(false);
            LevelControl.Instance.LoadNextLevel();
        }
    }
}