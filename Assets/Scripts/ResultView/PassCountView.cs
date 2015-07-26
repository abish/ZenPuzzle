using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// use namespace for view
namespace Result {
    public class PassCountView : MonoBehaviour {
    
        // Note: not required to call every frame
        void Update () {
            int maxPassCount = PassCountManager.Instance.maxPassCount;
            int validPassCount    = PassCountManager.Instance.GetValidPassCount();
            int restTimeToRecover = PassCountManager.Instance.RestTimeToRecoverOne();
    
            string _text = "Change stones:" + validPassCount + "/" + maxPassCount + "\n";
            _text += restTimeToRecover + " sec";
            GetComponent<Text>().text = _text;
        }
    }
}
