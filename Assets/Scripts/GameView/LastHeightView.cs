using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UniRx;

public class LastHeightView : MonoBehaviour
{
    void Start ()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        float lastHeight = HeightManager.Instance.GetLastHeight();
        Vector3 lastHeightPosition = Camera.main.WorldToViewportPoint(new Vector3(0, lastHeight, 0));
        SetAnchor(rectTransform, lastHeightPosition.y);

        Camera.main.ObserveEveryValueChanged(x => x.transform.position.y)
        .Select(height => Camera.main.WorldToViewportPoint(new Vector3(0, HeightManager.Instance.BestHeight.Value, 0)).y)
        .Subscribe(viewportHeight => SetAnchor(rectTransform, viewportHeight));
    }

    void SetAnchor (RectTransform rectTransform, float viewportHeight)
    {
        rectTransform.anchorMin = new Vector2(0f, viewportHeight);
        rectTransform.anchorMax = new Vector2(1f, viewportHeight);
    }
}
