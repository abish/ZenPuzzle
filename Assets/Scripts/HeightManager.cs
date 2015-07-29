using System;
using UnityEngine;
using System.Collections;

public class HeightManager : MonoBehaviour {


    // this object should be destroyed after each game
    static HeightManager _instance;

    //private float geta = 4.5f; // startPoint.transform.position.y * -1

    public float _bestHeight    = -5.0f;
    public float _lastHeight    = -5.0f;
    public float _currentHeight = -5.0f;

    public static HeightManager GetInstance()
    {
        if ( _instance == null )
        {
            GameObject go = new GameObject("HeightManager");
            _instance = go.AddComponent<HeightManager>();

            _instance.Init();
        }
        return _instance;
    }

    public void Init () {
        _instance.Reload();
    }

    public float GetBestHeight () {
        return this._bestHeight;
    }
    public float GetLastHeight () {
        return this._lastHeight;
    }
    public float GetCurrentHeight () {
        return this._currentHeight;
    }
    public void DeleteAll ()
    {
        PlayerPrefs.DeleteKey("bestHeight");
        PlayerPrefs.DeleteKey("lastHeight");
    }

    public void Reload ()
    {
        if (PlayerPrefs.HasKey("bestHeight"))
            this._bestHeight = PlayerPrefs.GetFloat("bestHeight");
        if (PlayerPrefs.HasKey("lastHeight"))
            this._lastHeight = PlayerPrefs.GetFloat("lastHeight");
    }

    // called when piece is fixed
    public void UpdateCurrentHeight(float originalHeight)
    {
        float height = originalHeight;
        if (this._currentHeight >= height) return;

        this._currentHeight = height;
    }

    // called when game ended
    public bool UpdateHeight ()
    {
        _instance.Reload();

        if (this._currentHeight > this._bestHeight)
        {
            this._bestHeight = this._currentHeight;
            PlayerPrefs.SetFloat("bestHeight", this._bestHeight);
        }
        this._lastHeight = this._currentHeight;
        PlayerPrefs.SetFloat("lastHeight", this._lastHeight);

        PlayerPrefs.Save();
        return true;
    }

}
