using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class HoleMovement : MonoBehaviour
{
	[SerializeField] private Joystick joystick;
	[SerializeField] private MeshFilter meshFilter;
	[SerializeField] private MeshCollider meshCollider;
	[SerializeField] private Vector2 moveLimits;
	//Hole vertices radius from the hole's center
	[SerializeField] private float radius;
	[SerializeField] private Transform holeCenter;
	//rotating circle arround hole (animation)
	[SerializeField] private Transform rotatingCircle;
	[SerializeField] private float moveSpeed;
	
	private Mesh _mesh;
	private List<int> _holeVertices;
	private List<Vector3> _offsets;
	private int _holeVerticesCount;
	private Vector3 _touch, _targetPos;

	private void Start ()
	{
		RotateCircleAnim ();

		GameStateProperty.IsMoving = false;
		GameStateProperty.IsGameOver = false;

		_holeVertices = new List<int> ();
		_offsets = new List<Vector3> ();
		_mesh = meshFilter.mesh;
		FindHoleVertices ();
	}

	private void RotateCircleAnim ()
	{
		rotatingCircle
			.DORotate (new Vector3 (90f, 0f, -90f), .2f)
			.SetEase (Ease.Linear)
			.From (new Vector3 (90f, 0f, 0f))
			.SetLoops (-1, LoopType.Incremental);
	}

	private void Update ()
	{
		GameStateProperty.IsMoving = Input.GetMouseButton (0);

		if (!GameStateProperty.IsGameOver && GameStateProperty.IsMoving) {
			MoveHole ();
			UpdateHoleVerticesPosition ();
		}

		GameStateProperty.IsMoving = Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved;

		if (!GameStateProperty.IsGameOver && GameStateProperty.IsMoving) { 
			MoveHole (); 
			UpdateHoleVerticesPosition ();
		}
	}

	private void MoveHole ()
	{
		_touch = holeCenter.transform.position +
		         new Vector3(joystick.Horizontal, 0f, joystick.Vertical) * Time.deltaTime * moveSpeed;

		_targetPos = new Vector3 (
			//Clamp: to prevent hole from going outside of the ground
			Mathf.Clamp (_touch.x, -moveLimits.x, moveLimits.x),//limit X
			_touch.y,
			Mathf.Clamp (_touch.z, -moveLimits.y, moveLimits.y)//limit Z
		);

		holeCenter.position = _targetPos;
	}

	public void UpdateHoleVerticesPosition ()
	{
		//Move hole vertices
		Vector3[] vertices = _mesh.vertices;
		for (int i = 0; i < _holeVerticesCount; i++) {
			vertices [_holeVertices [i]] = holeCenter.position + _offsets [i];
		}

		//update mesh vertices
		_mesh.vertices = vertices;
		//update meshFilter's mesh
		meshFilter.mesh = _mesh;
		//update collider
		meshCollider.sharedMesh = _mesh;
	}

	void FindHoleVertices ()
	{
		for (int i = 0; i < _mesh.vertices.Length; i++) {
			//Calculate distance between holeCenter & each Vertex
			float distance = Vector3.Distance (holeCenter.position, _mesh.vertices [i]);

			if (distance < radius) {
				//this vertex belongs to the Hole
				_holeVertices.Add (i);
				//offset: how far the Vertex from the HoleCenter
				_offsets.Add (_mesh.vertices [i] - holeCenter.position);
			}
		}
		//save hole vertices count
		_holeVerticesCount = _holeVertices.Count;
	}


	//Visualize Hole vertices Radius in the Scene view
	void OnDrawGizmos ()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (holeCenter.position, radius);
	}
}
