using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// move camera
public class CameraController : MonoBehaviour {

    public GameObject backgoundImage;
    public GameObject[] pieceSpawners;

    // TODO shouldn't move background image here
    // TODO use position diff
    // move 10% of window size toward y direction
    void Start () {
        pieceSpawners = GameObject.FindGameObjectsWithTag("Spawner");
    }

    public void MoveUpward () {
        // Initialize

        // initial position is Vector3.zero
        Vector3 cameraPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.6f, 0));

        if (pieceSpawners != null)
        {
            foreach (GameObject spawner in pieceSpawners)
            {
                float yPosition = cameraPosition.y + spawner.transform.position.y - Camera.main.transform.position.y;
                // spawner is invisible iTween is not required
                spawner.GetComponent<PieceSpawner>().MoveUpward(yPosition);
                //iTween.MoveBy(spawner, iTween.Hash("y", cameraPosition.y, "easeType", "easeInOutExpo", "time", .1));
                //iTween.MoveTo(spawner, iTween.Hash("y", spawnerPosition.y, "easeType", "easeInOutExpo", "time", .1));
            }
        }

        iTween.MoveTo(gameObject, iTween.Hash("y", cameraPosition.y, "easeType", "easeInOutExpo", "time", .3));
        if (backgoundImage != null)
        {
            iTween.MoveTo(backgoundImage, iTween.Hash("y", cameraPosition.y, "easeType", "easeInOutExpo", "time", .3));
        }
    }
}
