using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour
{

    public GameObject dragArrow;
    private int createdAt;
    // should create class for each tutorial
    public int animationStartAt = 3;
    private bool isTutorialRequired = true;
    private bool isTutorialStarted = false;

    void Awake () {
        createdAt = DateUtil.GetEpochTime();
    }

    // UniRxに変えたい
    void Update ()
    {
        if (isTutorialRequired)
            EnableDragArrowAnimation();
    }

    // should use event "goToNextTurn"
    public void EnableDragArrowAnimation ()
    {
        // show animation only when turn <= 1
        if (GameManager.Instance.CurrentTurn() > 1)
        {
            isTutorialRequired = false;
            dragArrow.SetActive(false);
            return;
        }

        if (isTutorialStarted) return;

        int now = DateUtil.GetEpochTime();
        // show animation when time >= createdAt + 3 sec
        if (now >= createdAt + animationStartAt)
        {
            dragArrow.SetActive(true);
            isTutorialStarted = true;
        }
    }

}
