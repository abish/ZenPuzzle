using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UniRx;

public class ScoreView : MonoBehaviour
{
    void Start ()
    {
        HeightManager.Instance.CurrentHeight
        .Select(height => HeightManager.Instance.GetHeightForView(height))// height => height + geta
        .Subscribe(height => updateText(height));
    }

    void updateText(float height)
    {
        GetComponent<Text>().text = height.ToString("F1") + "m";
    }
}
