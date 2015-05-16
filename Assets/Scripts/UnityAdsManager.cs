using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class UnityAdsManager : MonoBehaviour {

	[SerializeField]
	string appId = "39251";// GameID
	[SerializeField]
	bool isDebugMode = true;// should be false when shipped

	static UnityAdsManager _instance;

	public static UnityAdsManager GetInctance()
	{
		if ( _instance == null )
		{
			GameObject go = new GameObject("UnityAdsManager");
			_instance = go.AddComponent<UnityAdsManager>();

			_instance.Init();
		}
		return _instance;
	}

	public void Init () {
		if (Advertisement.isSupported)
		{
			Advertisement.Initialize(appId, isDebugMode);
		}
	}
	
	// Update is called once per frame
	public void ShowAds () {
		if (Advertisement.isSupported && !Advertisement.isReady())
		{
			Advertisement.Initialize(appId);
		}

		if (Advertisement.isReady())
		{
			Advertisement.Show();
		}
	}
}
