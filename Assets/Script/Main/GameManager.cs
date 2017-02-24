using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static int scoreP1 = 0;
	public static int scoreP2 = 0;

	public static void ResetScore()
	{
	}

	public static int getPlayerScore(int playerID)
	{
		if(playerID == 1) {
			return scoreP1;
		} else {
			return scoreP2;
		}
	}


	public static int getWinScore(GameBoard.Result result)
	{
		switch(result) {
			case GameBoard.Result.P1Win: 
			case GameBoard.Result.P2Win: 
				return 2;
			case GameBoard.Result.DrawGame:
				return 1;
			default:
				return 0;
		}
	}

	public static void UpdatePlayerScore(GameBoard.Result result)
	{
		int score = getWinScore(result);

		int addScore1 = 0;
		int addScore2 = 0;
		if(result == GameBoard.Result.DrawGame) {
			addScore1 = score;
			addScore2 = score;
		} else if(result == GameBoard.Result.P1Win) {
			addScore1 = score;
		} else if(result == GameBoard.Result.P2Win) {
			addScore2 = score;
		}

		increaseScore(1, addScore1);
		increaseScore(2, addScore2);
	}

	public static void increaseScore(int playerID, int score){
		if(playerID == 1) {
			scoreP1 += score;
		} else {
			scoreP2 += score;
		}
	}
}

