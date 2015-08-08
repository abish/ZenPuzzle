using System;
using UnityEngine;
using System.Collections;

public class HeightManager : Singleton<HeightManager> {
    //private float geta = 4.5f; // startPoint.transform.position.y * -1

    // for leaderboard score type is To 2 decimals
    public float geta = 4.5f;
    public int multiplier = 100;

    private bool isInitialized  = false;
    public float _bestHeight    = -4.5f;
    public float _lastHeight    = -4.5f;
    public float _currentHeight = -4.5f;

    public void Init () {
        this._bestHeight    = -4.5f;
        this._lastHeight    = -4.5f;
        this._currentHeight = -4.5f;
        Reload();

        this.isInitialized = true;
    }

    public float GetBestHeight () {
        if (this.isInitialized == false) this.Init();
        return this._bestHeight;
    }
    public float GetLastHeight () {
        if (this.isInitialized == false) this.Init();
        return this._lastHeight;
    }
    public float GetCurrentHeight () {
        return this._currentHeight;
    }
    public int GetCurrentHeightForLeaderboard () {
        return (int)(this.multiplier * (this._currentHeight + this.geta));
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
        Reload();

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
