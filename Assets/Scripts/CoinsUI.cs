using System;
using TMPro;
using UnityEngine;
using YG;

namespace UI
{
    public class CoinsUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        public static CoinsUI Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            UpdateCoinsText();
        }

        public void UpdateCoinsText()
        {
            _text.text = YandexGame.savesData.score.ToString();
        }
    }
}