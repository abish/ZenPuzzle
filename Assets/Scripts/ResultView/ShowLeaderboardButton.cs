using UnityEngine;
using System.Collections;

public class ShowLeaderboardButton : MonoBehaviour {

	void Start () {
		// GetInstance for initialization
		SocialPlatformsManager socialPlatformsManager = SocialPlatformsManager.Instance;

        if (socialPlatformsManager.IsAuthenticated() == false)
        {
            gameObject.SetActive(false);

        }
	}

    public void ShowLeaderboardUI ()
    {
        Debug.Log("ShowLeaderboardUI");
        SocialPlatformsManager.Instance.ShowLeaderboardUI();
    }
}
