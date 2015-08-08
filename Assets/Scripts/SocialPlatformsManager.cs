using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;
using System.Collections.Generic;

public class SocialPlatformsManager : Singleton<SocialPlatformsManager>
{
    private string leaderboardID = "1";
    private bool isAuthenticated = false;

	public void Awake ()
    {
        Init();
    }

	public void Init()
    {
        if (this.isAuthenticated == true) return;

        Social.localUser.Authenticate((success) =>
        {
            if (success)
            {
                Debug.Log("Auth success");
                Debug.Log(Social.localUser.userName);
                Debug.Log(Social.localUser.id);
                this.isAuthenticated = true;
            }
            else
            {
                Debug.Log("Auth fail");
                this.isAuthenticated = false;
            }
        });
	}

    public bool IsAuthenticated ()
    {
        return this.isAuthenticated;
    }

    public void ShowLeaderboardUI()
    {
        if (this.isAuthenticated == false) return;

        Social.LoadScores(this.leaderboardID, scores => {
            if (scores.Length > 0)
                Debug.Log("score length:" + scores.Length);
        });

        Social.ShowLeaderboardUI();
    }

    public void ReportScore (int score)
    {
        if (this.isAuthenticated == false) return;
        Debug.Log("ReportScore:" + score);

        Social.ReportScore((long)score, this.leaderboardID, success => {
            Debug.Log(success ? "success!" : "failed"); 
        });
    }
}
