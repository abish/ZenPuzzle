using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UniRx;

public class BestHeightView : MonoBehaviour
{
    void Start ()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        // Initial value
        float bestHeight = HeightManager.Instance.GetBestHeight();
        Vector3 bestHeightPosition = Camera.main.WorldToViewportPoint(new Vector3(0, bestHeight, 0));
        SetAnchor(rectTransform, bestHeightPosition.y);

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
