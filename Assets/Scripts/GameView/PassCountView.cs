using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PassCountView : MonoBehaviour {
	int _validPassCount = 0; // turn = stone num = karma

	void Start () {
		int validPassCount = PassCountManager.Instance.GetValidPassCount();
        _validPassCount = validPassCount;

		updateText();
	}
	// Update is called once per frame
	void Update () {
		int validPassCount = PassCountManager.Instance.GetValidPassCount();
		if (validPassCount == _validPassCount)
            return;

		// update if turn changed
        _validPassCount = validPassCount;
		updateText();
	}

	void updateText()
	{
		int maxPassCount = PassCountManager.Instance.maxPassCount;
		string _text = _validPassCount + "/" + maxPassCount;
		GetComponent<Text>().text = _text;
	}
}
