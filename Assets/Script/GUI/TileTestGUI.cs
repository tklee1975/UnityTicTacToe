using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileTestGUI : MonoBehaviour {
	private Text positionText;
	private Text infoText;

	private Tile tile;

	public GameBoard gameBoard = null;
	public GameBoard.WinLine testLine = GameBoard.WinLine.LineD1;

	// Use this for initialization
	void Start () {
		Debug.Log("TileTestGUI: start");	

		Text[] text = GetComponentsInChildren<Text>();
		positionText = text[0];
		infoText = text[1];


		tile = GameObject.FindGameObjectWithTag("TestTile").GetComponent<Tile>();
		infoText.text = "Testing"; // tile.infoBox();
		infoText.text = tile.InfoBox();
		//info
	}

	void Awake() {
		Debug.Log("TileTestGUI: awake");
	}


	public void ShowTestLine()
	{
		if(gameBoard == null) {
			return;
		}

		gameBoard.ShowLine(testLine);
	}

	public void TestReset()
	{
		Debug.Log("TestReset");
		tile.Reset();
	}

	public void TestSetCross()
	{
		Debug.Log("TestSetCross");
		tile.Reset();
		tile.ChangeType(Tile.Type.TypeCross);
	}

	public void TestSetCircle()
	{
		Debug.Log("TestSetCircle");
		tile.Reset();
		tile.ChangeType(Tile.Type.TypeCircle);
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 mousePos = Input.mousePosition;
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);


		positionText.text = "mPos: " + mousePos + "\n"
			+ "wPos: " + worldPos;

		Tile tile = GameObject.Find("Tile").GetComponent<Tile>();
		infoText.text = "Is touching the tile: " + tile.ContainsPoint(worldPos);
	}
}
