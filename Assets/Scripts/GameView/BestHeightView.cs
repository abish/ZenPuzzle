using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BestHeightView : MonoBehaviour {
    void Start () {
        float bestHeight = HeightManager.GetInstance().GetBestHeight();
        Vector3 bestHeightPosition = Camera.main.WorldToViewportPoint(new Vector3(0, bestHeight, 0));
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0f, bestHeightPosition.y);
        rectTransform.anchorMax = new Vector2(1f, bestHeightPosition.y);
    }

}
