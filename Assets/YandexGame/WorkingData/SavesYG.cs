
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public int allScore;
        public int score;
        public int levelNumber;
        public int putOnId;
        public string boughtItems;
        
        public SavesYG()
        {
            
        }

        public int freeSpin;
    }
}
