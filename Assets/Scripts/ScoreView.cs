using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour {
	int previousTurn    = 0; // turn = stone num = karma
	int _validPassCount = 0; // turn = stone num = karma

	void Start () {
		int currentTurn    = GameManager.Instance.CurrentTurn();
		int validPassCount = PassCountManager.Instance.GetValidPassCount();
		previousTurn    = currentTurn;
        _validPassCount = validPassCount;

		updateText();
	}
	// Update is called once per frame
	void Update () {
		int currentTurn    = GameManager.Instance.CurrentTurn();
		int validPassCount = PassCountManager.Instance.GetValidPassCount();
		if (currentTurn == previousTurn && validPassCount == _validPassCount)
            return;

		// update if turn changed
		previousTurn    = currentTurn;
        _validPassCount = validPassCount;
		updateText();
	}

	void updateText()
	{
		string _text = "Karma:" + previousTurn + "\n";
        _text += _validPassCount + " passes left";
		GetComponent<Text>().text = _text;
	}
}
