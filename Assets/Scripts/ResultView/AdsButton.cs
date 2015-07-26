using UnityEngine;
using System.Collections;

public class AdsButton : MonoBehaviour {

	void Start () {
		// GetInstance for initialization
		UnityAdsManager unityAdsManager = UnityAdsManager.GetInstance();
	}
	

	public void ShowAds ()
	{
		UnityAdsManager unityAdsManager = UnityAdsManager.GetInstance();
		unityAdsManager.ShowAds(null, RecoverPassCount);
	}

    private void RecoverPassCount ()
    {
        Debug.Log("RecoverPassCount");
        PassCountManager.Instance.RecoverPassCount(PassCountManager.Instance.maxPassCount);
    }
}
