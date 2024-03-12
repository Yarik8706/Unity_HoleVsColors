using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.Utils.LB;

public class PlayerRatingShower : MonoBehaviour
{
    [SerializeField] private TMP_Text playerRatingText;
    [SerializeField] private Image ratingImage;

    private int playerRank;
    
    public static PlayerRatingShower Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        YandexGame.onGetLeaderboard += OnUpdateLB;
    }

    public void BestScoreUpdate()
    {
        YandexGame.NewLeaderboardScores("Score", YandexGame.savesData.allScore);
        YandexGame.GetLeaderboard("Score",
            Int32.MaxValue, Int32.MaxValue, Int32.MaxValue, "nonePhoto");
    }

    private void OnUpdateLB(LBData lb)
    {
        var playerRank = 0;
        foreach (var player in lb.players)
        {
            if (player.uniqueID == YandexGame.playerId)
            {
                playerRank = player.rank;
                UpdateRating(playerRank);
                return;
            }
        }

        playerRank = lb.players.Length + 1;
        UpdateRating(playerRank);
    }

    public void UpdateRating(int newPlayerRating)
    {

        switch (newPlayerRating)
        {
            case 1:
                ratingImage.color = Color.yellow;
                break;
            case 2:
                ratingImage.color = Color.gray;
                break;
            case 3:
                ratingImage.color = new Color(0.59f, 0.29f, 0f);
                break;
            default:
                ratingImage.color = new Color(0.92f, 0.42f, 0f);
                break;
        }
        if (playerRank == newPlayerRating)
        {
            return;
        }

        playerRank = newPlayerRating;
        playerRatingText.transform.DOScale(playerRatingText.transform.localScale * 1.2f, 0.5f).OnComplete(() =>
        {
            playerRatingText.text = "" + newPlayerRating;
            playerRatingText.transform.DOScale(playerRatingText.transform.localScale * 0.8f, 0.5f);
        }).SetDelay(0.4f);
    }
}