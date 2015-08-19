using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class UnityAdsManager : MonoBehaviour
{
    [SerializeField]
    string iosGameID = "39251";// GameID
    //string andriodGameID = "39251";// GameID
    [SerializeField]
    bool isDebugMode = true;// should be false when shipped

    static UnityAdsManager _instance;

    private static Action _handleFinished;
    private static Action _handleSkipped;
    private static Action _handleFailed;
    private static Action _onContinue;

    public static UnityAdsManager GetInstance()
    {
        if ( _instance == null )
        {
            GameObject go = new GameObject("UnityAdsManager");
            _instance = go.AddComponent<UnityAdsManager>();

            _instance.Init();
        }
        return _instance;
    }

    public void Init ()
    {
        if (Advertisement.isSupported)
        {
            string gameID = iosGameID;
            Advertisement.Initialize(gameID, isDebugMode);
        }
    }

    public void ShowAds ()
    {
        ShowAds(null,null,null,null,null);
    }
    public void ShowAds (string zoneID)
    {
        ShowAds(zoneID,null,null,null,null);
    }
    public void ShowAds (string zoneID, Action handleFinished)
    {
        ShowAds(zoneID,handleFinished,null,null,null);
    }
    public void ShowAds (string zoneID, Action handleFinished, Action handleSkipped)
    {
        ShowAds(zoneID,handleFinished,handleSkipped,null,null);
    }
    public void ShowAds (string zoneID, Action handleFinished, Action handleSkipped, Action handleFailed)
    {
        ShowAds(zoneID,handleFinished,handleSkipped,handleFailed,null);
    }
    public void ShowAds (string zoneID, Action handleFinished, Action handleSkipped, Action handleFailed, Action onContinue)
    {
        if (string.IsNullOrEmpty(zoneID)) zoneID = null;

        _handleFinished = handleFinished;
        _handleSkipped  = handleSkipped;
        _handleFailed   = handleFailed;
        _onContinue     = onContinue;

        if (Advertisement.isSupported && !Advertisement.isReady())
            Init();

        if (Advertisement.isReady(zoneID))
        {
            ShowOptions options = new ShowOptions();
            options.resultCallback = HandleShowResult;
            options.pause = true;
            Advertisement.Show(zoneID, options);
        }
    }

    private void HandleShowResult (ShowResult result)
    {
        switch (result)
        {
        case ShowResult.Finished:
            Debug.Log("The ad was successfully shown.");
            if (!object.ReferenceEquals(_handleFinished,null)) _handleFinished();
            break;
        case ShowResult.Skipped:
            Debug.LogWarning("The ad was skipped before reaching the end.");
            if (!object.ReferenceEquals(_handleSkipped,null)) _handleSkipped();
            break;
        case ShowResult.Failed:
            Debug.LogError("The ad failed to be shown.");
            if (!object.ReferenceEquals(_handleFailed,null)) _handleFailed();
            break;
        }

        if (!object.ReferenceEquals(_onContinue,null)) _onContinue();
    }

}
