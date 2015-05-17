using UnityEngine;
using System.Collections;

public class Piece : MonoBehaviour {

	// true if this piece is draggable
	private bool canDrag = true;
	// true if this piece is correctly placed and next player should place next piece
	private bool isFixed = false;
	private float z = 1f;
	private float tapEndAt;
	private bool isTouching = false;
	private Vector3 lastValidPosition = Vector3.zero;
	private float yPositionBias = - 40f;

	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (isFixed == false && canDrag == false)
		{
			//TODO magic number
			if (GetComponent<Rigidbody2D>().velocity.magnitude < 1 && tapEndAt + 3 < Time.time)
			{
				isFixed = true;
				// spawn next piece
				GameObject spawner = GameObject.FindWithTag("Spawner");
				spawner.GetComponent<PieceSpawner>().Spawn();
				Debug.Log ("isFixed");
			}

		}
	}

	void OnMouseDrag()
	{
		if (canDrag == false ) return;

		//TODO check rest time
		if (isTouching == false )
		{
			lastValidPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y + yPositionBias, z);
		}

		transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y + yPositionBias, z));
	}

	void OnMouseUp()
	{
		if (canDrag == false ) return;

		transform.position = Camera.main.ScreenToWorldPoint(lastValidPosition);
		canDrag = false;
		tapEndAt = Time.time;

		GetComponent<Rigidbody2D>().isKinematic = false;
		GetComponent<Collider2D>().isTrigger = false;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if(other.tag == "Piece")
		{
			isTouching = true;
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if(other.tag == "Piece")
		{
			isTouching = false;
		}
	}
}
