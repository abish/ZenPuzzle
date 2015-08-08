using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LastHeightView : MonoBehaviour {
    void Start () {
        float lastHeight = HeightManager.Instance.GetLastHeight();
        Vector3 lastHeightPosition = Camera.main.WorldToViewportPoint(new Vector3(0, lastHeight, 0));
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0f, lastHeightPosition.y);
        rectTransform.anchorMax = new Vector2(1f, lastHeightPosition.y);
    }

}
