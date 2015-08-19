using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pieces : Singleton<Pieces>
{
    private bool isLocked = false;

    // TODO create piece name accessor if necessary
    public string[] pieceList = new string[] {
        "Zen1", "Zen2", "Zen3", "Zen4", "Zen5", "Zen6", "Zen7", "Zen8", "Zen9", "Zen10"
    };

    public Dictionary<string,float> weightMap = new Dictionary<string,float>() {
        {"Zen1",  20f},
        {"Zen2",  10f},
        {"Zen3",  20f},
        {"Zen4",  20f},
        {"Zen5",  10f},
        {"Zen6",  20f},
        {"Zen7",  10f},
        {"Zen8",  5f},
        {"Zen9",  10f},
        {"Zen10", 20f},
    };
    private string defaultPiece = "Zen1";

    private Dictionary<string, GameObject> _prefabCache;
    public void Awake ()
    {
        this._prefabCache = new Dictionary<string, GameObject>();
    }

    public static string LotPieceName ()
    {
        float weightSum = 0f;
        Pieces instance = Pieces.Instance as Pieces;
        foreach ( float weight in instance.weightMap.Values )
        {
            weightSum += weight;
        }

        float rnd = Random.Range(0, weightSum); 
        foreach( KeyValuePair<string, float> kvp in instance.weightMap )
        {
            if (rnd <= kvp.Value)
                return kvp.Key;

            rnd -= kvp.Value;
        }

        return instance.defaultPiece;
    }

    public static GameObject LotPiece ()
    {
        string pieceName = LotPieceName();
        return GetPiecePrefab(pieceName);
    }

    public static GameObject GetPiecePrefab (string pieceName)
    {
        //Debug.Log(pieceName);
        GameObject result = null;

        Pieces instance = Pieces.Instance as Pieces;
        if (instance == null) return result;

        // TODO validation
        if (!instance._prefabCache.TryGetValue(pieceName, out result))
        {
            result = Resources.Load("Prefabs/Piece/" + pieceName) as GameObject;;

            instance._prefabCache.Add(pieceName, result);
        }

        return result;
    }

    public override void OnDestroy()
    {
        if (this._prefabCache != null)
            this._prefabCache.Clear();

        base.OnDestroy();
    }

    // Lock function
    // Only one piece is draggable at a time
    // 
    public bool IsLocked()
    {
        return this.isLocked;
    }

    public bool GetLock()
    {
        // cannot get lock if already locked
        // TODO: avoid multiple locks
        if (this.isLocked == true) return false;

        this.isLocked = true;
        return true;
    }
    public bool UnLock()
    {
        // cannot unlock when not locked
        if (this.isLocked == false) return false;

        this.isLocked = false;
        return true;
    }
    // 
    // Lock function
}
