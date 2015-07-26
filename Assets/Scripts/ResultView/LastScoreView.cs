using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LastScoreView : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // CurrentTurn is not initialized until next game starts
		int previousTurn = GameManager.Instance.CurrentTurn();
	
		GetComponent<Text>().text = "" + previousTurn;
	}
}
