using UnityEngine;
using System.Collections;

public class Piece : MonoBehaviour {

    // true if this piece is draggable
    private bool canDrag = true;
    private bool hasLock = false;
    // true if this piece is correctly placed and next player should place next piece
    private bool isFixed = false;
    private float z = 1f;
    private float tapEndAt;
    private bool isTouching = false;
    private Vector3 lastValidPosition = Vector3.zero;
    //distance between tap point and visible position of piece 
    private float yPositionBias = - 50f;

    // sound
    private int minVelocityForSound = 1;
    private float playSoundInterval = 0.5f;
    private float lastPlaySoundAt = 0f;
    public AudioClip hitSound;
    private AudioSource _audio;

    // threshold to move camera
    private float yThresholdViewport = 0.5f;
    private float yThresholdPosition;

    void Start () {
        Vector3 positionDiff = Camera.main.ViewportToWorldPoint(new Vector3(0, yThresholdViewport, 0));
        yThresholdPosition = positionDiff.y;

        yPositionBias = - Screen.height * 0.1f;
    }

    // Update is called once per frame
    void Update () {
        if (isFixed == false && canDrag == false)
        {
            //TODO magic number
            if (GetComponent<Rigidbody2D>().velocity.magnitude == 0 && tapEndAt + 1 < Time.time)
            {
                isFixed = true;
                if (hasLock) Pieces.Instance.UnLock();

                // move camera and background image if piece is high enough
                if (transform.position.y > yThresholdPosition)
                    Camera.main.GetComponent<CameraController>().MoveUpward();

                GameManager.Instance.GoToNextTurn();
            }

        }
    }

    void OnMouseDrag()
    {
        if (canDrag == false ) return;

        if (hasLock == false)
        {
            bool isSuccess = Pieces.Instance.GetLock();
            if (isSuccess == false) return;
            hasLock = true;

            // set initial position
            lastValidPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y + yPositionBias, z);
        }

        if (isTouching == false )
        {
            lastValidPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y + yPositionBias, z);
        }

        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y + yPositionBias, z));
    }

    void OnMouseUp()
    {
        if (canDrag == false ) return;
        if (hasLock == false) return;

        transform.position = Camera.main.ScreenToWorldPoint(lastValidPosition);
        canDrag = false;
        tapEndAt = Time.time;

        GetComponent<Rigidbody2D>().isKinematic = false;
        Collider2D[] colliders  = GetComponents<Collider2D>();
        foreach (Collider2D col in colliders) col.isTrigger=false;
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

    void OnCollisionEnter2D (Collision2D other)
    {
        if(other.gameObject.tag == "Piece" && hitSound != null)
        {
            if (_audio == null) _audio = GetComponent<AudioSource>();

            if (_audio != null
                && Time.time > lastPlaySoundAt + playSoundInterval
                && GetComponent<Rigidbody2D>().velocity.magnitude >= minVelocityForSound)
            {
        Debug.Log ("play!");
                _audio.PlayOneShot(hitSound);
                lastPlaySoundAt = Time.time;
            }

        Debug.Log (_audio);
        }
    }

    public bool CanDrag ()
    {
        return canDrag;
    }
}
