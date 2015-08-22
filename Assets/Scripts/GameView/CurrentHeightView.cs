using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UniRx;

public class CurrentHeightView : MonoBehaviour
{
    void Start ()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        Observable.Merge(
            // when CurrentHeight is changed
            HeightManager.Instance.CurrentHeight
            .Select(height => Camera.main.WorldToViewportPoint(new Vector3(0, height, 0)).y),
            // when Camera position is changed
            Camera.main.ObserveEveryValueChanged(x => x.transform.position.y)
            .Select(height => Camera.main.WorldToViewportPoint(new Vector3(0, HeightManager.Instance.CurrentHeight.Value, 0)).y)
        )
        .Subscribe(height => SetAnchor(rectTransform, height))
        .AddTo(this);
    }

    void SetAnchor (RectTransform rectTransform, float viewportHeight)
    {
        rectTransform.anchorMin = new Vector2(0f, viewportHeight);
        rectTransform.anchorMax = new Vector2(1f, viewportHeight);
    }
}
