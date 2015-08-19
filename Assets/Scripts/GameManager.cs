using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO: create GameRuleManager class for single/vs game to remove if/else
public class GameManager : Singleton<GameManager> {

    [SerializeField]
    private int currentTurn = 1;// == score
    private bool isGameOver      = false;
    private bool isInitialized   = false;
    private bool isSingleMode = true;

    private GameObject[] spawners;
    //
    // SINGLE GAME

    // SINGLE GAME
    //

    //
    // VS GAME
    private bool   isPlayerGoFirst     = true;// flg which player goes first
    private string nextPlayerPieceName = null;
    private string nextEnemyPieceName  = null;
    private string currentPieceName    = null;// both enemy and player use this variable
    // VS GAME
    //

    void Update ()
    {
        if (this.isGameOver == false && this.isInitialized == false)
            Init();
    }

    // Note: If gameObject is not found at first call (in case scene is not fully loaded), this function is called multiple times
    public override void Init ()
    {
        this.currentTurn = 1;

        spawners = GameObject.FindGameObjectsWithTag("Spawner");
        if (spawners == null) return;
        if (spawners.Length == 0) return;

        // Instantiate initial pieces
        if (this.isSingleMode)
        {
            foreach (GameObject spawner in spawners)
            {
                string pieceName = Pieces.LotPieceName();
                spawner.GetComponent<PieceSpawner>().Spawn(pieceName);
            }
        }
        else
        {
            // Lot Initial Piece
            this.currentPieceName    = Pieces.LotPieceName();
            this.nextPlayerPieceName = Pieces.LotPieceName();
            this.nextEnemyPieceName  = Pieces.LotPieceName();

            foreach (GameObject spawner in spawners)
            {
                spawner.GetComponent<PieceSpawner>().Spawn(this.currentPieceName);
            }
        }

        // prepare unityads
        UnityAdsManager.GetInstance();

        // unlock pieces
        Pieces.Instance.UnLock();

        // initialize flg
        this.isInitialized = true;

        SocialPlatformsManager.Instance.Init();
    }

    public int CurrentTurn () {
        return this.currentTurn;
    }

    public void GoToNextTurn ()
    {
        if (this.isSingleMode)
        {
            bool canSpawn = true;        
            foreach (GameObject spawner in spawners)
            {
                if (spawner.GetComponent<PieceSpawner>().CanSpawn() == false)
                    canSpawn = false;
            }

            if (canSpawn)
            {
                foreach (GameObject spawner in spawners)
                {
                    string pieceName = Pieces.LotPieceName();
                    spawner.GetComponent<PieceSpawner>().Spawn(pieceName);
                }
            }
        }
        else
        {
            //  set Current Piece for next turn
            bool isPlayerTurn = IsPlayerTurn ();
            this.currentPieceName = (isPlayerTurn) ? nextEnemyPieceName : nextPlayerPieceName;

            // only one spawner exists 
            // spawn next piece
            foreach (GameObject spawner in spawners)
            {
                spawner.GetComponent<PieceSpawner>().Spawn(this.currentPieceName);
            }

            //  lot Next Piece
            if (isPlayerTurn)
            {
                this.nextEnemyPieceName = Pieces.LotPieceName();
            }
            else
            {
                this.nextPlayerPieceName = Pieces.LotPieceName();
            }
        }

        // next turn has come
        currentTurn++;
    }

    public void GoToGameOver ()
    {
        // return if isGameOver flg is already true
        if (this.isGameOver == true) return;

        int biasedHeight = HeightManager.Instance.GetCurrentHeightForLeaderboard();
        Debug.Log(biasedHeight);
        SocialPlatformsManager.Instance.ReportScore(biasedHeight);
        ScoreManager.Instance.UpdateScore(biasedHeight);
        HeightManager.Instance.UpdateHeight();

        this.isGameOver    = true;
        this.isInitialized = false;
        Application.LoadLevel("Result");
    }

    public void GoToGameStart ()
    {
        this.isGameOver = false;

        if (this.isSingleMode)
        {
            Application.LoadLevel("SingleGame");
        }
        else
        {
            Application.LoadLevel("MainGame");
        }
    }

    public void ExecPlayerPass ()
    {
        if (this.isSingleMode)
        {
            foreach (GameObject spawner in spawners)
            {
                // delete current pieces
                spawner.GetComponent<PieceSpawner>().DestroyNotMovedPiece();

                // create next pieces
                string pieceName = Pieces.LotPieceName();
                spawner.GetComponent<PieceSpawner>().Spawn(pieceName);
                //Debug.Log("spawn");
                //Debug.Log(pieceName);
            }
        }
        else
        {
            this.nextPlayerPieceName = Pieces.LotPieceName();
        }
    }

    //
    // SINGLE GAME

    // SINGLE GAME
    //

    //
    // VS GAME
    public bool IsPlayerTurn ()
    {
        bool isPlayerTurn = this.currentTurn % 2 == 1;
        if (this.isPlayerGoFirst == false) isPlayerTurn = !isPlayerTurn;

        return isPlayerTurn;
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
    // VS GAME
    //
}
