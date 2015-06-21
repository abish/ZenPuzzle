using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// move camera
public class CameraController : MonoBehaviour {

    public GameObject backgoundImage;
    public GameObject PieceSpawner;

    // TODO shouldn't move background image here
    // TODO use position diff
    // move 10% of window size toward y direction
    public void MoveUpward () {
        // Initialize

        Vector3 cameraPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.6f, 0));

        //iTween.MoveBy(gameObject, iTween.Hash("y", -1, "easeType", "easeInOutExpo", "time", .3));
        if (PieceSpawner != null)
        {
            Vector3 spawnerPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.95f, 0));
            iTween.MoveTo(PieceSpawner, iTween.Hash("y", spawnerPosition.y, "easeType", "easeInOutExpo", "time", .1));
        }

        iTween.MoveTo(gameObject, iTween.Hash("y", cameraPosition.y, "easeType", "easeInOutExpo", "time", .3));
        if (backgoundImage != null)
        {
            iTween.MoveTo(backgoundImage, iTween.Hash("y", cameraPosition.y, "easeType", "easeInOutExpo", "time", .3));
        }
    }
}
