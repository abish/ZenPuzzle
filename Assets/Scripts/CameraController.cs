using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// move camera
public class CameraController : MonoBehaviour {

    public GameObject backgoundImage;

    // TODO shouldn't move background image here
    // move 10% of window size toward y direction
    public void MoveUpward () {
        // Initialize

        Vector3 positionDiff = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.6f, 0));

        //iTween.MoveBy(gameObject, iTween.Hash("y", -1, "easeType", "easeInOutExpo", "time", .3));
        iTween.MoveTo(gameObject, iTween.Hash("y", positionDiff.y, "easeType", "easeInOutExpo", "time", .3));
        if (backgoundImage != null)
        {
            iTween.MoveTo(backgoundImage, iTween.Hash("y", positionDiff.y, "easeType", "easeInOutExpo", "time", .3));
        }
    }
}
