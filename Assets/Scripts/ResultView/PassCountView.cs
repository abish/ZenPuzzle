using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// use namespace for view
namespace Result {
    public class PassCountView : MonoBehaviour {
    
        // Note: not required to call every frame
        void Update () {
            int validPassCount    = PassCountManager.Instance.GetValidPassCount();
            int restTimeToRecover = PassCountManager.Instance.RestTimeToRecoverOne();
    
            string _text = validPassCount + "Pass left\n";
            _text += restTimeToRecover + " sec";
            GetComponent<Text>().text = _text;
        }
    }
}
