using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour {
	public enum WinLine {
		LineV1 = 0,
		LineV2 = 1,
		LineV3 = 2,
		LineH1 = 3,
		LineH2 = 4,
		LineH3 = 5,
		LineD1 = 6,
		LineD2 = 7,

		None = 1000,
	}

	public enum Result
	{
		NotYet,
		P1Win,				// Circle
		P2Win,				// Cross
		DrawGame
	}

	private bool isGameStarted;

	private Tile.Type currentTileType;
	private GameObject[] winLines;	// Array of the lines, index = enum.WinLine
	private BoardLogic boardLogic;

	public GameBoard.WinLine winLine = GameBoard.WinLine.None;
	public int winningPlayer = 0;
	public GameBoard.Result gameResult = GameBoard.Result.NotYet;
	public int currentPlayer = 0;

	// Use this for initialization
	void Start () {
		currentTileType = Tile.Type.TypeCircle;

		boardLogic = new BoardLogic();

		ResetTiles();

		HideAllWinLine();

		isGameStarted = false;
		winningPlayer = 0;
		currentPlayer = 0;
		winLine = GameBoard.WinLine.None;
		// SetupWinLine();
	}

	public void StartGame()
	{
		SetPlayerTurn(1);

		boardLogic.Reset();

		ResetTiles();

		HideAllWinLine();

		isGameStarted = true;

		gameResult = GameBoard.Result.NotYet;
	}

	private void ChangePlayerTurn()
	{
		if(currentPlayer == 1) {
			SetPlayerTurn(2);
		} else if(currentPlayer == 2) {
			SetPlayerTurn(1);
		}
	}

	private void SetPlayerTurn(int playerID) 
	{
		currentPlayer = playerID;

		if(playerID == 1) {
			currentTileType = Tile.Type.TypeCircle;
		} else if(playerID == 2) {
			currentTileType = Tile.Type.TypeCross;
		} else {
			currentTileType = Tile.Type.TypeEmpty;
		}
	}

	void ResetTiles()
	{
		Tile[] tileArray = GetComponentsInChildren<Tile>();
		foreach(Tile tile in tileArray) {
			//Debug.Log("Reseting tile=" + tile);
			tile.Reset();
		}
	}

	public void HideAllWinLine()
	{
		GameObject winLineParent = GameObject.Find("GameBoard/WinLine");
		SpriteRenderer[] rendererList = winLineParent.GetComponentsInChildren<SpriteRenderer>();
		//Debug.Log("Hide ShowLine. child=" + rendererList.Length);
		foreach(SpriteRenderer myLine in rendererList) {
			//myLine.enabled = false;
			myLine.gameObject.SetActive(false);
		}
	}

	public void ShowLine(WinLine line) {
		GameObject winLineParent = GameObject.Find("GameBoard/WinLine");

		string lineName = line.ToString();
		for(int i=0; i<winLineParent.transform.childCount; i++){
			GameObject myLine = winLineParent.transform.GetChild(i).gameObject;

			string objectName = myLine.gameObject.name;
			bool visible = objectName.Equals(lineName);
			myLine.gameObject.SetActive(visible);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			if(isGameStarted) {
				CheckForTileMove();
			}
		}
	}

	void OnChessPlaced(Tile tile, Tile.Type chessType)
	{
		// Change the tile
		tile.ChangeType(chessType);
		int player = (int) chessType;

		boardLogic.PlaceChess(player, tile.gridX, tile.gridY);

		gameResult = boardLogic.Check();
		if(gameResult != Result.NotYet)
		{
			winningPlayer = boardLogic.winningPlayer;
			winLine = boardLogic.winLine;

			if(winLine != WinLine.None) {
				ShowLine(winLine);
			}

			isGameStarted = false;
		}
	}

	bool CheckForTileMove()
	{
		Vector3 mousePos = Input.mousePosition;
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
		Vector3 relativePos = worldPos;	// - transform.position;
		// Debug.Log("worldPos=" + worldPos + " relativePos=" + relativePos);

		// Find the Tile being touched 
		Tile[] tileArray = GetComponentsInChildren<Tile>();
		Tile foundTile = null;
		foreach(Tile tile in tileArray) {
			if(tile.ContainsPoint(relativePos)){
				foundTile = tile;
			}
		}

		// Checking 
		if(foundTile == null) {
			Debug.Log("Not Found");
			return false;
		}

		if(foundTile.HavePlaced()) {
			Debug.Log("Have Placed");
			return false;
		}

		// Change the tile
		OnChessPlaced(foundTile, currentTileType);


		// Set next time 
		ChangePlayerTurn();

		return true;
	}
}
