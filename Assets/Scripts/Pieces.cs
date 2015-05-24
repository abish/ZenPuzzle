using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pieces : Singleton<Pieces> {

	// TODO create piece name accessor if necessary
	public string[] pieceList = new string[] {
		"Stick", "Cross", "Circle", "Zigzag", "Square"
	};

	public Dictionary<string,int> weightMap = new Dictionary<string,int>() {
		{"Stick", 1},
		{"Cross", 1},
		{"Circle", 1},
		{"Zigzag", 1},
		{"Square", 1},
	};
	private string defaultPiece = "Stick";

	private Dictionary<string, GameObject> _prefabCache;
	public void Awake ()
	{
		this._prefabCache = new Dictionary<string, GameObject>();
	}

	public static string LotPieceName ()
	{
	    float weightSum = 0f;
        Pieces instance = Pieces.Instance as Pieces;
		foreach ( int weight in instance.weightMap.Values )
		{
			weightSum += (float)weight;
		}

		float rnd = Random.Range(0, weightSum); 
		foreach( KeyValuePair<string, int> kvp in instance.weightMap )
		{
			if (rnd <= kvp.Value)
			{
				return kvp.Key;
			}

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
        Debug.Log(pieceName);
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
        {
            this._prefabCache.Clear();
        }
        base.OnDestroy();
    }
}
