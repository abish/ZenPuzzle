using UnityEngine;
using System.Collections;

public class ResetPlayerPrefsButton : MonoBehaviour {


	public void Reset ()
	{
        //PlayerPrefs.DeleteAll();
        PlayerPrefs.DeleteKey("passCountRecoveredAt");
		PassCountManager.Instance.SetDirty();
	}
}
