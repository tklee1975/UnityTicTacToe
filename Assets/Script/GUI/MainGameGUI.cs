using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameGUI : MonoBehaviour {
	public enum State {
		GameMain,
		InGame,
		EndGame,
	}

	public GameBoard gameBoard = null;

	// The main GUI Component
	private GameObject playButton;
	private GameObject gameEndDialog;	
	private GameObject inGamePanel;

	private State mState;

	// Use this for initialization
	void Start () {
		playButton = GameObject.Find("PlayButton");
		inGamePanel = GameObject.Find("GamePanel");
		gameEndDialog = GameObject.Find("GameEndDialog");

		Debug.Log("Starting MainGameUI");
		Debug.Log("Info:\n" + InfoObjects());

		GameManager.ResetScore();

		ChangeState(State.GameMain);
	}

//	void Awake() {
//		ChangeState(State.GameMain);
//	}

	void ChangeState(State state)
	{
		if(gameBoard == null) {
			Debug.Log("ChangeState: gameBoard is null");
			return;
		}

		if(state == State.GameMain) {
			mState = State.GameMain;

			ShowMainUI();
			UpdatePlayerScore();

		} else if(state == State.InGame) {
			mState = State.InGame;

			gameBoard.StartGame();
			ShowInGameUI();
		} else if(state == State.EndGame) {
			ChangeStateToEndGame();
		}
	}

	void ChangeStateToEndGame()
	{
		mState = State.EndGame;

		// update player score
		GameManager.UpdatePlayerScore(gameBoard.gameResult);
		UpdatePlayerScore();

		// show endGame dialog
		ShowEndGameUI();
	}
	
	// Update is called once per frame
	void Update () {
		if(mState == State.InGame) {
			UpdatePlayerTurn();
		}
	}

	void UpdatePlayerScore()
	{
		Text p1Score = GameObject.Find("P1Score").GetComponent<Text>();
		Text p2Score = GameObject.Find("P2Score").GetComponent<Text>();

		p1Score.text = "P1: " + GameManager.getPlayerScore(1);
		p2Score.text = "P2: " + GameManager.getPlayerScore(2);
	}

	void UpdatePlayerTurn()
	{
		if(gameBoard == null) {
			return;
		}

		if(gameBoard.gameResult != GameBoard.Result.NotYet) {
			ChangeState(State.EndGame);
			return;
		}


		// 
		Text text = GameObject.Find("PlayerTurnText").GetComponent<Text>();
		text.text = "Player " + gameBoard.currentPlayer;

		string imageName = gameBoard.currentPlayer == 1 ? "icon-o" : "icon-x";
		Image image = GameObject.Find("PlayerTurnImage").GetComponent<Image>();
		image.sprite = Resources.Load<Sprite>("Sprite/" + imageName);
	}

	public void StartGame()
	{
		ChangeState(State.InGame);
	}

	public string InfoObjects()
	{
		string info = "";

		info += "playButton=" + playButton + "\n";
		info += "inGamePanel=" + inGamePanel + "\n";
		info += "gameEndDialog=" + gameEndDialog + "\n";

		return info;
	}

	public void ChangeObjectY(GameObject obj, float position)
	{
		Vector3 newPos = obj.transform.localPosition;
		newPos.y = position;

		obj.transform.localPosition = newPos;
	}

	public void ShowMainUI() 
	{
		playButton.SetActive(true);
		SetGameEndDialogVisible(false);
		SetInGamePanelVisible(false);
	}

	public void ShowInGameUI()
	{
		playButton.SetActive(false);
		SetInGamePanelVisible(true);
		SetGameEndDialogVisible(false);
	}

	public void ShowEndGameUI()
	{
		playButton.SetActive(false);

		if(gameBoard) {
			ShowEndGameDialog(gameBoard.gameResult);
		}

		SetInGamePanelVisible(false);
	}

	public void SetInGamePanelVisible(bool flag)
	{
		// Debug.Log("SetGameEndDialogVisible: flag=" + flag);
		inGamePanel.SetActive(true);

		float position = flag ? 0 : -500;		// 500 is out of screen
		GameObjectHelper.ChangeObjectY(inGamePanel, position);
	}

	public void SetGameEndDialogVisible(bool flag)
	{
		// Debug.Log("SetGameEndDialogVisible: flag=" + flag);

		gameEndDialog.SetActive(flag);

		float position = flag ? 0 : 1000;		// 500 is out of screen

		GameObjectHelper.ChangeObjectY(gameEndDialog, position);
	}

	public void ShowEndGameDialog(GameBoard.Result result)
	{
		

		gameEndDialog.GetComponent<GameEndDialog>().SetupContent(result);
		SetGameEndDialogVisible(true);
	}
}
