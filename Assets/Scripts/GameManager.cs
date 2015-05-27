using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {

	[SerializeField]
	private int currentTurn = 1;
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

	public void Init () {
		// Lot Initial Piece
		this.currentPieceName    = Pieces.LotPieceName();
		this.nextPlayerPieceName = Pieces.LotPieceName();
		this.nextEnemyPieceName  = Pieces.LotPieceName();

		// Lot first player

		// Instantiate first piece
		GameObject spawner = GameObject.FindWithTag("Spawner");
		spawner.GetComponent<PieceSpawner>().Spawn(this.currentPieceName);

		// prepare unityads
		UnityAdsManager unityAdsManager = UnityAdsManager.GetInctance();
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

	public void GoToNextTurn () {
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
	
}
