using System;
using DG.Tweening;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;
using Random = UnityEngine.Random;

public class LevelControl : MonoBehaviour
{
    public AudioSource buyItem;
    public AudioSource putOnItem;
    
    [SerializeField] private ParticleSystem winEffect;
    [SerializeField] private Level[] levels;
    [SerializeField] private Level[] randomLevels;
    [SerializeField] private Transform blackHoleStartPosition;
    [SerializeField] private HoleMovement blackHole;
    [SerializeField] private SpriteRenderer groundBorderSprite;
    [SerializeField] private SpriteRenderer groundSideSprite;
    [SerializeField] private Image progressFillImage;
    [SerializeField] private SpriteRenderer bgFadeSprite;
    [SerializeField] private Theme[] themes;
    
    public Level ActiveLevel { get; private set; }

    public static LevelControl Instance;

    private void Awake()
    {
#if UNITY_EDITOR
        YandexGame.ResetSaveProgress();
#endif
        Instance = this;
        YandexGame.InitLang();
        MultiTextUI.lang = YandexGame.lang;
    }

    public void RestartLevel()
    {
        LoadLevel(GameDataManager.SceneIndex);
    }

    public void LoadNextLevel()
    {
        GameDataManager.SetSceneIndex(GameDataManager.SceneIndex+1);
        LoadLevel(GameDataManager.SceneIndex);
    }

    private void LoadLevel(int index)
    {
        var nextLevel = GameDataManager.SceneIndex >= levels.Length 
            ? randomLevels[Random.Range(0, randomLevels.Length)] : levels[GameDataManager.SceneIndex];
        YandexGame.FullscreenShow();
        YandexMetrica.Send(index.ToString());
        GameDataManager.SetSceneIndex(index);
        blackHole.transform.position = blackHoleStartPosition.position;
        blackHole.UpdateHoleVerticesPosition();
        SceneBlackoutControlUI.Instance.StartBlackoutOverTime();
        if(ActiveLevel != null) Destroy(ActiveLevel.gameObject);
        ActiveLevel = Instantiate(nextLevel, Vector3.zero, Quaternion.identity);
        var newTheme = themes[Random.Range(0, themes.Length)];
        ActiveLevel.UpdateLevelColors(newTheme);
        UIManager.Instance.InitUI(index);
        UIManager.Instance.SetLoseScreenState(false);
        GameStateProperty.IsGameOver = false;
    }

    public void PlayWinEffect()
    {
        winEffect.Play();
    }

    public void GameOver()
    {
        GameStateProperty.IsGameOver = true;
        UIManager.Instance.SetLoseScreenState(true);
        Camera.main.transform
            .DOShakePosition (1f, .2f, 20, 90f)
            .OnComplete (() => {
                Instance.RestartLevel();
            }).SetLink(gameObject);
    }

    public void SetGameObjectsColors(Color sideColor, Color bordersColor, 
        Color progressFillColor, Color fadeColor)
    {
        groundSideSprite.color = sideColor;
        groundBorderSprite.color = bordersColor;
        progressFillImage.color = progressFillColor;
        bgFadeSprite.color = fadeColor;
    }
}