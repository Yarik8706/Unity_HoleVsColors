using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
	[SerializeField] private TMP_Text nextLevelText;
	[SerializeField] private TMP_Text currentLevelText;
	[SerializeField] private Image progressFillImage;
	[SerializeField] private TMP_Text levelCompletedText;
	[SerializeField] private GameObject loseScreen;
	
	public static UIManager Instance;
	
	private void Awake ()
	{
		Instance = this;
	}

	public void SetLoseScreenState(bool state)
	{
		loseScreen.SetActive(state);
	}
	
	public void InitUI(int activeSceneIndex)
	{
		progressFillImage.fillAmount = 0f;
		int level = activeSceneIndex + 1;
        currentLevelText.text = level.ToString ();
        nextLevelText.text = (level + 1).ToString ();
        HideLevelCompletedUI();
	}

	public void UpdateLevelProgress ()
	{
		float val = 1f - ((float)LevelControl.Instance.ActiveLevel.objectsInScene 
		                  / LevelControl.Instance.ActiveLevel.totalObjects);
		if(val == progressFillImage.fillAmount) return;
		progressFillImage.DOFillAmount (val, .4f);
	}

	public void ShowLevelCompletedUI ()
	{
		levelCompletedText.DOFade (1f, .6f).From (0f);
	}

	public void HideLevelCompletedUI()
	{
		levelCompletedText.DOFade(0, .4f);
	}
}
