using UnityEngine;
using System.Collections;

public class PieceSpawner : MonoBehaviour {

	public void Spawn (string pieceName)
	{
		//TODO can spawn check 
		//TODO spawn position check if possible 
		// Spawn 
		GameObject piecePrefab = Pieces.GetPiecePrefab(pieceName);
        //Debug.Log (piecePrefab);
		GameObject Piece = Instantiate(piecePrefab, transform.position, Quaternion.identity) as GameObject;
		// spawned piece shouldn't fall
		Rigidbody2D rigidbody2D = Piece.GetComponent<Rigidbody2D>();
		rigidbody2D.isKinematic = true;

		Collider2D[] colliders  = Piece.GetComponents<Collider2D>();
		foreach (Collider2D col in colliders) col.isTrigger=true;

		//Debug.Log("Spawn!");
	}
}
