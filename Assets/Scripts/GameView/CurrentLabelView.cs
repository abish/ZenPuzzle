using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Required only in VS game
public class CurrentLabelView : MonoBehaviour
{
    bool _isPlayerTurn = true;

    void Start ()
    {
        bool isPlayerTurn = GameManager.Instance.IsPlayerTurn();
        _isPlayerTurn = isPlayerTurn;
        updateText();
    }

    // TODO use UniRx
    // Update is called once per frame
    void Update () {
        bool isPlayerTurn = GameManager.Instance.IsPlayerTurn();
        if (isPlayerTurn == _isPlayerTurn) return;

        // update if turn changed
        _isPlayerTurn = isPlayerTurn;
        updateText();
    }

    void updateText()
    {
        string _text = _isPlayerTurn ? "Player Turn\nDrag This" : "Enemy Turn";
        GetComponent<Text>().text = _text;
    }
}
