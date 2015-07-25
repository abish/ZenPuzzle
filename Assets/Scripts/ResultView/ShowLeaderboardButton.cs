using UnityEngine;
using System.Collections;

public class ShowLeaderboardButton : MonoBehaviour {

	void Start () {
		// GetInstance for initialization
		SocialPlatformsManager socialPlatformsManager = SocialPlatformsManager.Instance;
	}

    public void ShowLeaderboardUI ()
    {
        Debug.Log("ShowLeaderboardUI");
        SocialPlatformsManager.Instance.ShowLeaderboardUI();
    }
}
