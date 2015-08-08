using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LastScoreView : MonoBehaviour {

    // Use this for initialization
    void Start () {
        // CurrentTurn is not initialized until next game starts
        //int previousTurn = GameManager.Instance.CurrentTurn();
        //GetComponent<Text>().text = "" + previousTurn;

        int height = HeightManager.Instance.GetCurrentHeightForLeaderboard();
        string text = (height >= 100) ? ((height - height % 100) / 100) + "." : 0 + ".";
        text += (height >= 100) ? height % 100 : height;
        GetComponent<Text>().text = text + "m";
    }
}
