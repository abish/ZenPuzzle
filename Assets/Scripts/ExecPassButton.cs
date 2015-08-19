using UnityEngine;
using System.Collections;

public class ExecPassButton : MonoBehaviour {
    public void ExecPass ()
    {
        if (Pieces.Instance.IsLocked()) return;

        bool result = PassCountManager.Instance.ExecPass();

        if (result == true)
            GameManager.Instance.ExecPlayerPass();
    }
}
