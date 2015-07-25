using UnityEngine;
using System.Collections;

public class DebugPassButton : MonoBehaviour {


	public void ExecPass ()
	{
		PassCountManager.Instance.ExecPass();
	}
}
