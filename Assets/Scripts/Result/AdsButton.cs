﻿using UnityEngine;
using System.Collections;

public class AdsButton : MonoBehaviour {

	void Start () {
		// GetInstance for initialization
		UnityAdsManager unityAdsManager = UnityAdsManager.GetInctance();
	}
	

	public void ShowAds ()
	{
		UnityAdsManager unityAdsManager = UnityAdsManager.GetInctance();
		unityAdsManager.ShowAds();
	}
}