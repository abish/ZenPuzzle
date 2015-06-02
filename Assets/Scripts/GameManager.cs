using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {

	[SerializeField]
	private int currentTurn = 1;
	private bool isGameOver      = false;
	private bool isInitialized   = false;
	private bool isPlayerGoFirst = true;// flg which player goes first
	[SerializeField]
	private string nextPlayerPieceName = null;
	[SerializeField]
	private string nextEnemyPieceName = null;
	[SerializeField]
	private string currentPieceName = null;// both enemy and player use this variable


	void Start ()
	{
		Init ();
	}

    void Update ()
    {
        if (this.isGameOver == false && this.isInitialized == false)
        {
            Init();
        }
    }

    // Note: If gameObject is not found (in case scene is not fully loaded), this function is called multiple times
	public void Init () {
        this.currentTurn = 1;

		// check if gameObject exists
		GameObject spawner = GameObject.FindWithTag("Spawner");
        if (spawner == null) return;

		// Lot Initial Piece
		this.currentPieceName    = Pieces.LotPieceName();
		this.nextPlayerPieceName = Pieces.LotPieceName();
		this.nextEnemyPieceName  = Pieces.LotPieceName();

		// Lot first player

		// Instantiate first piece
		spawner.GetComponent<PieceSpawner>().Spawn(this.currentPieceName);

		// prepare unityads
		UnityAdsManager unityAdsManager = UnityAdsManager.GetInctance();

        // initialize flg
        this.isInitialized = true;
	}

	public bool IsPlayerTurn () {
		bool isPlayerTurn = this.currentTurn % 2 == 1;
		if (this.isPlayerGoFirst == false) isPlayerTurn = !isPlayerTurn;

		return isPlayerTurn;
	}

	public int CurrentTurn () {
		return this.currentTurn;
	}
	// TODO rewrite with get,set
	public string NextPlayerPieceName () {
		return this.nextPlayerPieceName;
	}
	public string NextEnemyPieceName () {
		return this.nextEnemyPieceName;
	}
	public string CurrentPieceName () {
		return this.currentPieceName;
	}

	public void GoToNextTurn ()
    {
		//  set Current Piece for next turn
		bool isPlayerTurn = IsPlayerTurn ();
		this.currentPieceName = (isPlayerTurn) ? nextEnemyPieceName : nextPlayerPieceName;

		// spawn next piece
		GameObject spawner = GameObject.FindWithTag("Spawner");
		spawner.GetComponent<PieceSpawner>().Spawn(this.currentPieceName);
		Debug.Log ("isFixed");

		//  lot Next Piece
		if (isPlayerTurn)
		{
			this.nextEnemyPieceName = Pieces.LotPieceName();
		}
		else
		{
			this.nextPlayerPieceName = Pieces.LotPieceName();
		}

		// TODO event to notify ugui

		// next turn has come
		currentTurn++;
	}

    public void GoToGameOver ()
    {
        this.isGameOver    = true;
        this.isInitialized = false;
		Application.LoadLevel("Result");
    }

    public void GoToGameStart ()
    {
        this.isGameOver = false;

		Application.LoadLevel("MainGame");
        Init();
    }

    public void ExecPlayerPass ()
    {
        this.nextPlayerPieceName = Pieces.LotPieceName();
    }
}
