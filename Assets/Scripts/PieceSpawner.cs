using UnityEngine;
using System.Collections;

public class PieceSpawner : MonoBehaviour {

    GameObject lastSpawnedObject;
	public void Spawn (string pieceName)
	{
        if (CanSpawn() == false) return;

		// Spawn 
		GameObject piecePrefab = Pieces.GetPiecePrefab(pieceName);
        //Debug.Log (piecePrefab);
		GameObject Piece = Instantiate(piecePrefab, transform.position, Quaternion.identity) as GameObject;
		// spawned piece shouldn't fall
		Rigidbody2D rigidbody2D = Piece.GetComponent<Rigidbody2D>();
		rigidbody2D.isKinematic = true;

		Collider2D[] colliders  = Piece.GetComponents<Collider2D>();
		foreach (Collider2D col in colliders) col.isTrigger = true;

        lastSpawnedObject = Piece;
		//Debug.Log("Spawn!");
	}

    public GameObject GetNotMovedPiece ()
    {
        if (lastSpawnedObject == null) return null;

        Piece piece = lastSpawnedObject.GetComponent<Piece>();
        if (piece == null) return null;
        if (piece.CanDrag() == false) return null;

        return lastSpawnedObject;
    }

    public bool CanSpawn ()
    {
        if (GetNotMovedPiece() != null)
            return false;
        return true;
    }

    public void DestroyNotMovedPiece ()
    {
        if (lastSpawnedObject == null) return;

        Destroy(lastSpawnedObject);
        lastSpawnedObject = null;
    }

    public void MoveUpward (float yPosition)
    {
        Vector3 spawnerPosition = new Vector3(transform.position.x, yPosition, transform.position.z);
        transform.position = spawnerPosition;
        if (GetNotMovedPiece() == null) return;

        lastSpawnedObject.transform.position = spawnerPosition;
    }
}
