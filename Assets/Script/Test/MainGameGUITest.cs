using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameGUITest : MonoBehaviour {

	private bool mShowEndGameDialog = false;
	public MainGameGUI mainGameGUI;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TestShowEndGameDialog()
	{
		mShowEndGameDialog = ! mShowEndGameDialog;
		GameObject.FindObjectOfType<MainGameGUI>().SetGameEndDialogVisible(mShowEndGameDialog);
//		GetComponentInParent<MainGameGUI>().
	}

	public void TestEndGameDialog()
	{
		GameBoard.Result result = GameBoard.Result.DrawGame;
		mainGameGUI.ShowEndGameDialog(result);
		//mainGameGUI.
	}
}
