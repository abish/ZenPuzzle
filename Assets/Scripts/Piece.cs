using UniRx;
using UniRx.Triggers; // need UniRx.Triggers namespace for extend gameObejct
using UnityEngine;
using System;

public class Piece : MonoBehaviour
{

    // true if this piece is draggable
    private bool canDrag = true;

    //private bool hasLock = false;
    // true if this piece is correctly placed and next player should place next piece
    //private bool isFixed = false;

    // position
    private float z = 1f;
    //distance between tap point and visible position of piece 
    private float yPositionBias;
    private float thresholdDistanceToMove = 0.1f; // if position is moved more than this distance, dragged object is moved

    private bool isTouching = false;
    private Vector3 lastValidPosition = Vector3.zero;

    // fix check
    private float velocityCheckDelay = 0.5f; // [sec] wait this time and start velocity check
    private float maxAllowedVelocity = 0f; // will be fixed when velocity is below this value

    // sound
    private int minVelocityForSound = 1;
    private float playSoundInterval = 0.5f;
    private float lastPlaySoundAt = 0f;
    public AudioClip hitSound;
    private AudioSource _audio;

    // threshold to move camera
    private float yThresholdViewport = 0.60f;
    private float yThresholdPosition;

    void Start () {
        yPositionBias = - Screen.height * 0.1f;

        // DragAndDropOnce
        // TODO: OnMouse**AsObservable is not available for iOS,Andriod build.
        // Should find way to use Input.Touches instead
        this.gameObject.AddComponent<ObservableTouchStartTrigger>().TouchStartAsObservable()
        .Where(x => canDrag && Pieces.Instance.GetLock()) // GetLock returns true if success. only one piece is draggable at a time
        .Subscribe(touch =>
        {
            SubscribeDragStream(touch.fingerId);
        });
    }

    // TODO should find way to pass finger id
    void SubscribeDragStream(int fingerId)
    {
        this.UpdateAsObservable()
        .TakeUntil(Drag.TouchUpStream(fingerId))
        .Select(_ => 
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.fingerId == fingerId)
                     return touch.position;
            }
            return Vector2.zero;//exception
        })
        .Scan((prev, current) => Vector2.Distance(prev, current) < thresholdDistanceToMove ? prev : current)
        .DistinctUntilChanged() // change position only when moved
        .Subscribe(
            position => {
                Debug.Log(position);
                TryUpdateLastValidPosition(position);
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y + yPositionBias, z));
            },
            () => { OnEndDragAndDrop(); }// OnComplete
        );
    }

    void TryUpdateLastValidPosition (Vector2 position)
    {
        if (lastValidPosition.z != z || isTouching == false)
            lastValidPosition = new Vector3(position.x, position.y + yPositionBias, z);
    }

    void OnEndDragAndDrop ()
    {
        transform.position = Camera.main.ScreenToWorldPoint(lastValidPosition);
        canDrag = false; // for PieceSpawner

        GetComponent<Rigidbody2D>().isKinematic = false;
        Collider2D[] colliders  = GetComponents<Collider2D>();
        foreach (Collider2D col in colliders) col.isTrigger = false;

        Vector3 positionDiff = Camera.main.ViewportToWorldPoint(new Vector3(0, yThresholdViewport, 0));
        yThresholdPosition = positionDiff.y;

        // For velocity observation
        this.gameObject.UpdateAsObservable()
        .SkipUntil( Observable.Timer(TimeSpan.FromSeconds(velocityCheckDelay)) )// timer
        .Where(x => GetComponent<Rigidbody2D>().velocity.magnitude <= maxAllowedVelocity)// velocity is small enough
        .First()
        .Subscribe(x => {
            Debug.Log(x);
            Pieces.Instance.UnLock(); // Unlock when fixed
            HeightManager.Instance.UpdateCurrentHeight(transform.position.y);

            // move camera and background image if piece is high enough
            if (transform.position.y > yThresholdPosition)
                Camera.main.GetComponent<CameraController>().MoveUpward();

            GameManager.Instance.GoToNextTurn();
        });
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if(other.tag == "Piece")
            isTouching = true;
    }

    void OnTriggerExit2D (Collider2D other)
    {
        if(other.tag == "Piece")
            isTouching = false;
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
                _audio.PlayOneShot(hitSound);
                lastPlaySoundAt = Time.time;
            }
        }
    }

    public bool CanDrag ()
    {
        return canDrag;
    }
}
