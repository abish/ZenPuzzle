using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BestScoreView : MonoBehaviour
{
    void Start ()
    {
        int bestScore   = ScoreManager.Instance.GetBestScore();
        int secondScore = ScoreManager.Instance.GetSecondScore();
        int thirdScore  = ScoreManager.Instance.GetThirdScore();

        if (bestScore < 0)   bestScore   = 0;
        if (secondScore < 0) secondScore = 0;
        if (thirdScore < 0)  thirdScore  = 0;

        string _text = formatText(bestScore) + "\n";    
        _text += formatText(secondScore) + "\n";    
        _text += formatText(thirdScore);    
        GetComponent<Text>().text = _text;
    }

    string formatText (int score)
    {
        string text = (score >= 100) ? ((score - score % 100) / 100) + "." : 0 + ".";
        text += (score >= 100) ? score % 100 : score;
        text += "m";

        return text;
    }
}
