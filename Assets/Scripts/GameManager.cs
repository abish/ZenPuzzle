using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {

    [SerializeField]
    private int currentTurn = 1;// == score
    private bool isGameOver      = false;
    private bool isInitialized   = false;

    private GameObject[] spawners;

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
        foreach (GameObject spawner in spawners)
        {
            string pieceName = Pieces.LotPieceName();
            spawner.GetComponent<PieceSpawner>().Spawn(pieceName);
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

        Application.LoadLevel("SingleGame");
    }

    public void ExecPlayerPass ()
    {
        foreach (GameObject spawner in spawners)
        {
            // delete current pieces
            spawner.GetComponent<PieceSpawner>().DestroyNotMovedPiece();

            // create next pieces
            string pieceName = Pieces.LotPieceName();
            spawner.GetComponent<PieceSpawner>().Spawn(pieceName);
            //Debug.Log(pieceName);
        }
    }
}
