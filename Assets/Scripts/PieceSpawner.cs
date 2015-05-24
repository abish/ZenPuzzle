using UnityEngine;
using System.Collections;

public class PieceSpawner : MonoBehaviour {

	public void Spawn (string pieceName)
	{
		//TODO can spawn check 
		//TODO spawn position check if possible 
		// Spawn 
		GameObject piecePrefab = Pieces.GetPiecePrefab(pieceName);
		Debug.Log (piecePrefab);
		GameObject Piece = Instantiate(piecePrefab, transform.position, Quaternion.identity) as GameObject;
		Rigidbody2D rigidbody2D = Piece.GetComponent<Rigidbody2D>();
		Collider2D collider = Piece.GetComponent<Collider2D>();
		// spawned piece shouldn't fall
		rigidbody2D.isKinematic = true;
		collider.isTrigger = true;

		Debug.Log("Spawn!");
	}
}
