using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
	[Space]
	//remaining objects
	[HideInInspector] public int objectsInScene;
	//total objects at the beginning
	[HideInInspector] public int totalObjects;
	[SerializeField] private bool needChangeObjectsOnObstacles;
	[SerializeField] private int maxObstaclesCount;

	//the Objects parent
	[SerializeField] Transform objectsParent;

	[Space]
	[Header ("Materials & Sprites")]
	[SerializeField] Material groundMaterial;
	[SerializeField] Material objectMaterial;
	[SerializeField] Material obstacleMaterial;

	private Transform _obstaclesParent;

	private void Start ()
	{
		if(needChangeObjectsOnObstacles) ChangeSomeObjectsOnObstacles();
		CountObjects ();
	}

	public void ChangeSomeObjectsOnObstacles()
	{
		_obstaclesParent = objectsParent.parent.GetChild(1);

		float countObstacles;
		if (maxObstaclesCount == 0)
		{
			countObstacles = Random.Range(0, Mathf.Round(objectsParent.childCount * 0.25f));
		}
		else
		{
			countObstacles = maxObstaclesCount;
		}
		for (int i = 0; i < countObstacles; i++)
		{
			var randomObjects = objectsParent.GetChild(Random.Range(0, objectsParent.childCount)).GetComponent<MeshRenderer>();
			randomObjects.materials[0] = obstacleMaterial;
			randomObjects.material = obstacleMaterial;
			randomObjects.gameObject.tag = "Obstacle";
			randomObjects.transform.parent = _obstaclesParent;
		}
	}

	private void CountObjects ()
	{
		totalObjects = objectsParent.childCount;
		objectsInScene = totalObjects;
	}

	public void UpdateLevelColors (Theme theme)
	{
		groundMaterial.color = theme.groundColor;
		if (Camera.main != null) Camera.main.backgroundColor = theme.cameraColor;
		obstacleMaterial.color = theme.obstacleColor;
		objectMaterial.color = theme.objectColor;
		LevelControl.Instance.SetGameObjectsColors(theme.sideColor, theme.bordersColor, theme.progressFillColor, theme.fadeColor);
	}
}
