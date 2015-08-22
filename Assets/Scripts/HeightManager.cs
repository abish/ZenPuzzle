using System;
using UnityEngine;
using System.Collections;
using UniRx;

public class HeightManager : Singleton<HeightManager>
{
    // for leaderboard score type is To 2 decimals
    public float geta = 4.5f;// startPoint.transform.position.y * -1
    public int multiplier = 100;

    public ReactiveProperty<float> BestHeight { get; private set; }
    public ReactiveProperty<float> LastHeight { get; private set; }
    public ReactiveProperty<float> CurrentHeight { get; private set; }
    private bool isInitialized  = false;
    public float initialHeight = -4.5f;

    public override void Init ()
    {
        BestHeight    = new ReactiveProperty<float>(initialHeight);
        LastHeight    = new ReactiveProperty<float>(initialHeight);
        CurrentHeight = new ReactiveProperty<float>(initialHeight);
        Reload();

        this.isInitialized = true;
    }

    public float GetBestHeight ()
    {
        if (this.isInitialized == false) this.Init();
        return BestHeight.Value;
    }

    public float GetLastHeight ()
    {
        if (this.isInitialized == false) this.Init();
        return LastHeight.Value;
    }

    public int GetCurrentHeightForLeaderboard ()
    {
        return (int)(this.multiplier * (CurrentHeight.Value + this.geta));
    }
    public float GetHeightForView (float height)
    {
        return height + this.geta;
    }

    public void DeleteAll ()
    {
        PlayerPrefs.DeleteKey("bestHeight");
        PlayerPrefs.DeleteKey("lastHeight");
    }

    public void Reload ()
    {
        if (PlayerPrefs.HasKey("bestHeight"))
            BestHeight.Value = PlayerPrefs.GetFloat("bestHeight");

        if (PlayerPrefs.HasKey("lastHeight"))
            LastHeight.Value = PlayerPrefs.GetFloat("lastHeight");
    }

    // called when piece is fixed
    public void UpdateCurrentHeight(float height)
    {
        if (CurrentHeight.Value >= height) return;

        CurrentHeight.Value = height;
    }

    // called when game ended
    public void UpdateHeight ()
    {
        if (CurrentHeight.Value > BestHeight.Value)
        {
            BestHeight.Value = CurrentHeight.Value;
            PlayerPrefs.SetFloat("bestHeight", BestHeight.Value);
        }

        LastHeight.Value = CurrentHeight.Value;
        PlayerPrefs.SetFloat("lastHeight", LastHeight.Value);

        PlayerPrefs.Save();

        // set to be uninitialized
        this.isInitialized = false;
    }

}
