using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PassCountView : MonoBehaviour {

    // Note: not required to call every frame
	void Update () {
		int validPassCount    = PassCountManager.Instance.GetValidPassCount();
		int restTimeToRecover = PassCountManager.Instance.RestTimeToRecoverOne();

        string _text = "You can Pass:" + validPassCount + " times \n";
        _text += "Recover in:" + restTimeToRecover + " seconds";
		GetComponent<Text>().text = _text;
	}
}
