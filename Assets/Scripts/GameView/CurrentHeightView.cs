using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CurrentHeightView : MonoBehaviour {
    private RectTransform _rectTransform;
    private float _currentHeight = -5f;

    void Awake () {
        _rectTransform = GetComponent<RectTransform>();
    }

    void Update () {
        // should be event driven
        float currentHeight = HeightManager.GetInstance().GetCurrentHeight();
        // no need to change
        if (_currentHeight == currentHeight) return;

        _currentHeight = currentHeight;
        Vector3 currentHeightPosition = Camera.main.WorldToViewportPoint(new Vector3(0, currentHeight, 0));

        _rectTransform.anchorMin = new Vector2(0f, currentHeightPosition.y);
        _rectTransform.anchorMax = new Vector2(1f, currentHeightPosition.y);
    }

}
