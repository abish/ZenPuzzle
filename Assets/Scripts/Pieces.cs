using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pieces : Singleton<Pieces> {

	// TODO create piece name accessor if necessary
	public string[] pieceList = new string[] {
		//"Stick", "Cross", "Circle", "Zigzag", "Square", "Pawn"
		"Zen1", "Zen2", "Zen3", "Zen4", "Zen5", "Zen6", "Zen7"
	};

	public Dictionary<string,float> weightMap = new Dictionary<string,float>() {
		{"Zen1",  20f},
		{"Zen2",  20f},
		{"Zen3",  20f},
		{"Zen4",  20f},
		{"Zen5",  5f},
		{"Zen6",  5f},
		{"Zen7",  5f},
		//{"Stick",  2f},
		//{"Cross",  1f},
		//{"Circle", 1f},
		//{"Zigzag", 2f},
		//{"Square", 1f},
		//{"Pawn",   1f},
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
		foreach ( float weight in instance.weightMap.Values )
		{
			weightSum += weight;
		}

		float rnd = Random.Range(0, weightSum); 
		foreach( KeyValuePair<string, float> kvp in instance.weightMap )
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
        {
            this._prefabCache.Clear();
        }
        base.OnDestroy();
    }
}
