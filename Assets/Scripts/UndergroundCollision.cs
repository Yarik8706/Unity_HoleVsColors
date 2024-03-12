using UnityEngine;
using DG.Tweening;
using UI;

public class UndergroundCollision : MonoBehaviour
{
	private void OnTriggerEnter (Collider other)
	{
		if (GameStateProperty.IsGameOver) return;
		string tag = other.tag;
		if (tag.Equals ("Object")) { 
			LevelControl.Instance.ActiveLevel.objectsInScene--;
			UIManager.Instance.UpdateLevelProgress ();

			Magnet.Instance.RemoveFromMagnetField (other.attachedRigidbody);

			Destroy (other.gameObject);

			if (LevelControl.Instance.ActiveLevel.objectsInScene == 0) {
				GameStateProperty.IsGameOver = true;
				UIManager.Instance.ShowLevelCompletedUI ();
				LevelControl.Instance.PlayWinEffect();

				Invoke(nameof(NextLevel), 2f);
			}
		}

		if (!tag.Equals("Obstacle")) return;
		LevelControl.Instance.GameOver();
		Destroy (other.gameObject);
	}

	private void NextLevel()
	{
		LevelCompletedInfoControl.Instance.Init();
	}
}
