using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers; // need UniRx.Triggers namespace for extend gameObejct

public class PassCountView : MonoBehaviour
{
    void Start () {
        this.gameObject.UpdateAsObservable()
        .Select(x => PassCountManager.Instance.GetValidPassCount())
        .DistinctUntilChanged()
        .Subscribe(passCount => { updateText(passCount); }
        );
    }

    void updateText(int passCount)
    {
        int maxPassCount = PassCountManager.Instance.maxPassCount;
        string _text = passCount + "/" + maxPassCount;
        GetComponent<Text>().text = _text;
    }
}
