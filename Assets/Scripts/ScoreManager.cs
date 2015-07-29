using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreManager : Singleton<ScoreManager> {

    // TODO validation
	[SerializeField]
	private int _bestScore   = -1;
	private int _secondScore = -1;
	private int _thirdScore  = -1;

    public int GetBestScore () {
        return this._bestScore;
    }
    public int GetSecondScore () {
        return this._secondScore;
    }
    public int GetThirdScore () {
        return this._thirdScore;
    }

    // return true if score is updated
    public bool UpdateScore (int newScore)
    {
        Reload();

        // update is not necessary
        if (newScore < this._thirdScore) return false;

        if (newScore > this._bestScore)
        {
            this._thirdScore  = this._secondScore;
            this._secondScore = this._bestScore;
            this._bestScore   = newScore;
            PlayerPrefs.SetInt("thirdScore",  this._thirdScore);
            PlayerPrefs.SetInt("secondScore", this._secondScore);
            PlayerPrefs.SetInt("bestScore",   this._bestScore);
        }
        else if (newScore > this._secondScore)
        {
            this._thirdScore  = this._secondScore;
            this._secondScore = newScore;
            PlayerPrefs.SetInt("thirdScore",  this._thirdScore);
            PlayerPrefs.SetInt("secondScore", this._secondScore);
        }
        else
        {
            this._thirdScore = newScore;
            PlayerPrefs.SetInt("thirdScore", this._thirdScore);
        }

        PlayerPrefs.Save();
        return true;
    }

    public void DeleteAll ()
    {
        PlayerPrefs.DeleteKey("bestScore");
        PlayerPrefs.DeleteKey("secondScore");
        PlayerPrefs.DeleteKey("thirdScore");
    }

    public void Reload ()
    {
        this._bestScore   = PlayerPrefs.GetInt("bestScore");
        this._secondScore = PlayerPrefs.GetInt("secondScore");
        this._thirdScore  = PlayerPrefs.GetInt("thirdScore");
    }
}
