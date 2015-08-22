using UnityEngine;
using System;
using System.Collections;
using UniRx;
using UniRx.Triggers; // need UniRx.Triggers namespace for extend gameObejct

public class TutorialManager : MonoBehaviour
{
    public GameObject dragArrow;
    public int animationStartAt = 3;

    void Start () {
        // For velocity observation
        this.gameObject.UpdateAsObservable()
        .SkipUntil( Observable.Timer(TimeSpan.FromSeconds(animationStartAt)) )// timer
        .FirstOrDefault()
        .Subscribe(x  =>
        {
            dragArrow.SetActive(true);

            // stop animation when turn > 1
            this.gameObject.UpdateAsObservable()
            .Where(s => GameManager.Instance.CurrentTurn() > 1)// show tutorial only in first turn
            .FirstOrDefault()
            .Subscribe(s => {dragArrow.SetActive(false);});
        });
    }
}
