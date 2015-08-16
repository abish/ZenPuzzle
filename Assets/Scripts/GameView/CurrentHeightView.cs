using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UniRx;

public class CurrentHeightView : MonoBehaviour {
    private RectTransform _rectTransform;

    void Start () {
        _rectTransform = GetComponent<RectTransform>();

        HeightManager.Instance.CurrentHeight
        .DistinctUntilChanged()
        .Subscribe(height => {
            Debug.Log("update position!!!");
            Vector3 currentHeightPosition = Camera.main.WorldToViewportPoint(new Vector3(0, height, 0));

            _rectTransform.anchorMin = new Vector2(0f, currentHeightPosition.y);
            _rectTransform.anchorMax = new Vector2(1f, currentHeightPosition.y);
        });
    }

}
