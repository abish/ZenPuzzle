using UnityEngine;
using System.Collections;

public class PieceSpawner : MonoBehaviour {

	// TODO will enable prefab selection
	public GameObject samplePrefab;

	public void Spawn ()
	{
		//TODO can spawn check 
		//TODO spawn position check if possible 
		// Spawn 
		GameObject Piece = Instantiate(samplePrefab, transform.position, Quaternion.identity) as GameObject;
		Rigidbody2D rigidbody2D = Piece.GetComponent<Rigidbody2D>();
		Collider2D collider = Piece.GetComponent<Collider2D>();
		// spawned piece shouldn't fall
		rigidbody2D.isKinematic = true;
		collider.isTrigger = true;

		Debug.Log("Spawn!");
	}
}
