using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndDialog : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetupContent(GameBoard.Result result)
	{
		Debug.Log("SetupContent. result=" + result);

		// Change the Title
		SetupTitle(result);
		SetupInfo(result);
	}

	void SetupTitle(GameBoard.Result result) {
		string title = "";
		if(GameBoard.Result.DrawGame == result) {
			title = "Draw Game";
		} else if(GameBoard.Result.P1Win == result) {
			title = "Player1 WIN";
		} else if(GameBoard.Result.P2Win == result) {
			title = "Player2 WIN";
		}

		Debug.Log("SetupTitle. title=" + title);

		GameObject titleObj = GameObjectHelper.GetChildObject(gameObject, "TitleText");
		if(titleObj) {
			titleObj.GetComponent<Text>().text = title;
		}

		//GameObject.Find("
	}

	void SetupInfo(GameBoard.Result result) {
		string info = "";
		int score = GameManager.getWinScore(result);

		if(GameBoard.Result.DrawGame == result) {
			info += "Player1 +" + score;
			info += "\n";
			info += "Player2 +" + score;
		} else if(GameBoard.Result.P1Win == result) {
			info += "Player1 +" + score;
		} else if(GameBoard.Result.P2Win == result) {
			info += "Player2 +" + score;
		}

		GameObject textObj = GameObjectHelper.GetChildObject(gameObject, "InfoText");
		if(textObj) {
			textObj.GetComponent<Text>().text = info;
		}

		//GameObject.Find("
	}
}
