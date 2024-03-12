using System;
using YG;

namespace UI
{
    public static class GameDataManager
    {
        public static int SceneIndex { get; private set; }
        public static int Coins { get; private set; }
        public static int[] BoughtItems { get; private set; }
        public static int PutOnItem { get; private set; }
        
        public static void AddCoin(int count)
        {
            YandexGame.savesData.score += count;
            Coins = YandexGame.savesData.score;
            CoinsUI.Instance.UpdateCoinsText();
            if (count > 0)
            {
                YandexGame.savesData.allScore += count;
                PlayerRatingShower.Instance.BestScoreUpdate();
            }
            YandexGame.SaveProgress();
        }

        public static void SetSceneIndex(int index)
        {
            SceneIndex = index;
            YandexGame.savesData.levelNumber = SceneIndex;
            YandexGame.SaveProgress();
        }

        public static void InitDataAndStartGame()
        {
            SceneIndex = YandexGame.savesData.levelNumber;
            Coins = YandexGame.savesData.score;
            PutOnItem = YandexGame.savesData.putOnId; 
            if(string.IsNullOrEmpty(YandexGame.savesData.boughtItems))
            {
                BoughtItems = Array.Empty<int>();
            }
            else
            {
                var boughtItemsString = YandexGame.savesData.boughtItems.Split(";");
                if (boughtItemsString.Length != 0)
                {
                    BoughtItems = new int[boughtItemsString.Length - 1];
                    for (int i = 0; i < BoughtItems.Length; i++)
                    {
                        BoughtItems[i] = int.Parse(boughtItemsString[i]);
                    }
                }
            }
            LevelControl.Instance.RestartLevel();
            ShopUI.Instance.SpawnItems();
        }

        public static void AddBoughtItem(int index)
        {
            YandexGame.savesData.boughtItems += index + ";";
            YandexGame.SaveProgress();
        }

        public static void SetPutOn(int index)
        {
            YandexGame.savesData.putOnId = index;
            YandexGame.SaveProgress();
        }
    }
}