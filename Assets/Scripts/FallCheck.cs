using UnityEngine;
using System.Collections;

public class FallCheck : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D other)
    {
        // If a piece enters the trigger zone...
        if(other.tag == "Piece")
        {
            // Destroy
            Destroy(other.gameObject);
            Debug.Log ("GameOver");

            GameManager.Instance.GoToGameOver();
        }
    }
}
