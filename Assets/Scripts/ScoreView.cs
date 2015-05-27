using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour {
	int previousTurn = 0; // turn = stone num = karma

	void Start () {
		int currentTurn = GameManager.Instance.CurrentTurn();
		previousTurn = currentTurn;
		updateText();
	}
	// Update is called once per frame
	void Update () {
		int currentTurn = GameManager.Instance.CurrentTurn();
		if (currentTurn == previousTurn) return;

		// update if turn changed
		previousTurn = currentTurn;
		updateText();
	}

	void updateText()
	{
		string _text = "Karma:" + previousTurn;
		GetComponent<Text>().text = _text;
	}
}