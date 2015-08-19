using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LastScoreView : MonoBehaviour
{
    void Start ()
    {
        int height = HeightManager.Instance.GetCurrentHeightForLeaderboard();
        string text = (height >= 100) ? ((height - height % 100) / 100) + "." : 0 + ".";
        text += (height >= 100) ? height % 100 : height;
        GetComponent<Text>().text = text + "m";
    }
}
