using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UniRx;

// move camera
public class CameraController : MonoBehaviour {

    public GameObject backgoundImage;
    public GameObject[] pieceSpawners;

    private float yThresholdViewport = 0.6f; // when piece piled up to 60% from the bottom of screen
    private float yCameraViewport = 0.6f; // 0.5f is default position. move 10% of window size toward y direction

    void Start ()
    {
        pieceSpawners = GameObject.FindGameObjectsWithTag("Spawner");

        HeightManager.Instance.CurrentHeight
        .Where(height => height > Camera.main.ViewportToWorldPoint(new Vector3(0, yThresholdViewport, 0)).y)
        .Subscribe(height => MoveUpward());
    }

    public void MoveUpward ()
    {
        Vector3 cameraPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, yCameraViewport, 0));

        // Move Spawner
        if (pieceSpawners != null)
        {
            float yPositionDiff = cameraPosition.y - Camera.main.transform.position.y;
            foreach (GameObject spawner in pieceSpawners)
            {
                spawner.GetComponent<PieceSpawner>().MoveUpward(yPositionDiff);
                //iTween.MoveBy(spawner, iTween.Hash("y", cameraPosition.y, "easeType", "easeInOutExpo", "time", .1));
            }
        }

        // Move Camera
        iTween.MoveTo(gameObject, iTween.Hash("y", cameraPosition.y, "easeType", "easeInOutExpo", "time", .3));
        // Move background image
        if (backgoundImage != null)
            iTween.MoveTo(backgoundImage, iTween.Hash("y", cameraPosition.y, "easeType", "easeInOutExpo", "time", .3));
    }
}
