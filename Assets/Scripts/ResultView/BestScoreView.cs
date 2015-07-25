using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BestScoreView : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int bestScore   = ScoreManager.Instance.GetBestScore();
		int secondScore = ScoreManager.Instance.GetSecondScore();
		int thirdScore  = ScoreManager.Instance.GetThirdScore();

        if (bestScore < 0) bestScore = 0;

        string _text = bestScore + "\n";    
        if (secondScore > 0) _text += + secondScore + "\n";    
        if (thirdScore  > 0) _text += + thirdScore;    
		GetComponent<Text>().text = _text;
	}
}
