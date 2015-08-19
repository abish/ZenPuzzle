using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Required only in VS game
public class EnemyNextPieceImageView : MonoBehaviour
{

    private string _pieceName;

    // TODO use UniRx
    void Update ()
    {
        string pieceName = GameManager.Instance.NextEnemyPieceName();
        if (pieceName == null || pieceName == _pieceName) return;

        // update image if changed
        GameObject piecePrefab = Pieces.GetPiecePrefab(pieceName);
        //Debug.Log (piecePrefab);
        if (piecePrefab == null) return;

        _pieceName = pieceName;
        Sprite sprite = piecePrefab.GetComponent<SpriteRenderer>().sprite;

        GetComponent<Image>().sprite = sprite;
    }
}
