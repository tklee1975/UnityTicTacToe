using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardLogic
{
	const int kNumGrid = 3;

	private int[,] mChessArray = new int[kNumGrid, kNumGrid];		// value: 0-not fill, 1-Player1, 2-Player2

	public GameBoard.WinLine winLine = GameBoard.WinLine.None;
	public int winningPlayer = 0;
	public GameBoard.Result gameResult = GameBoard.Result.NotYet;

	public BoardLogic ()
	{
		
	}

	public void Reset()
	{
		for(int i=0; i<kNumGrid; i++){
			for(int j=0; j<kNumGrid; j++){
				mChessArray[i, j] = 0;
			}
		}

		winLine = GameBoard.WinLine.None;
		gameResult = GameBoard.Result.NotYet;
		winningPlayer = 0;
	}

	public void PlaceChess(int playerID, int x, int y)
	{
		mChessArray[x, y] = playerID;
	}


	public GameBoard.Result Check()
	{
		GameBoard.Result result;

		result = CheckForHorizontalLine();
		if(result != GameBoard.Result.NotYet) {
			return result;
		}

		result = CheckForVerticalLine();
		if(result != GameBoard.Result.NotYet) {
			return result;
		}

		result = CheckForDiagnoalLine();
		if(result != GameBoard.Result.NotYet) {
			return result;
		}

		return IsAllPlaced() ? GameBoard.Result.DrawGame : GameBoard.Result.NotYet;
	}

	public string Info()
	{
		return "";
	}

	private bool IsAllPlaced()
	{
		for(int i=0; i<kNumGrid; i++){
			for(int j=0; j<kNumGrid; j++){
				if(mChessArray[i, j] == 0) {
					return false;
				}
			}
		}

		return true;
	}


	private GameBoard.WinLine GetVerticalWinLine(int x){
		switch(x) {
			case 0:			return GameBoard.WinLine.LineV1;
			case 1:			return GameBoard.WinLine.LineV2;
			case 2:			return GameBoard.WinLine.LineV3;
			default:		return GameBoard.WinLine.None;
		}
	}

	private GameBoard.WinLine GetHorizontalWinLine(int y){
		switch(y) {
		case 0:			return GameBoard.WinLine.LineH1;
		case 1:			return GameBoard.WinLine.LineH2;
		case 2:			return GameBoard.WinLine.LineH3;
		default:		return GameBoard.WinLine.None;
		}
	}



	private GameBoard.Result CheckForVerticalLine()
	{

		GameBoard.Result result = GameBoard.Result.NotYet;

		// for x = 0 -> 2


		for(int x=0; x<kNumGrid; x++) {
			int firstValue = mChessArray[x, 0];	//

			if(firstValue == 0) {
				continue;	// check next column
			}

			bool hasMatch = true;
			for(int y=1; y<kNumGrid; y++) {
				if(mChessArray[x, y] != firstValue) {
					hasMatch = false;
					break;
				}
			}

			//
			if(hasMatch) {
				winningPlayer = firstValue;
				winLine = GetVerticalWinLine(x);
				gameResult = winningPlayer == 1 ? 
					GameBoard.Result.P1Win : GameBoard.Result.P2Win;

				return gameResult;
			}
		}

		return result;
	}

	private GameBoard.Result CheckForHorizontalLine()
	{
		GameBoard.Result result = GameBoard.Result.NotYet;

		// for x = 0 -> 2


		for(int y=0; y<kNumGrid; y++) {
			int firstValue = mChessArray[0, y];	//

			if(firstValue == 0) {
				continue;	// check next column
			}

			bool hasMatch = true;
			for(int x=1; x<kNumGrid; x++) {
				if(mChessArray[x, y] != firstValue) {
					hasMatch = false;
					break;
				}
			}

			//
			if(hasMatch) {
				winningPlayer = firstValue;
				winLine = GetHorizontalWinLine(y);
				gameResult = winningPlayer == 1 ? 
					GameBoard.Result.P1Win : GameBoard.Result.P2Win;

				return gameResult;
			}
		}

		return result;
	}

	private GameBoard.Result CheckForDiagnoalLine()
	{
		int firstValue;
		bool anyMatch;
		// Check from Top to Down
		firstValue = mChessArray[0, 0];
		if(firstValue != 0) {
			anyMatch = true;

			for(int i=1; i<kNumGrid; i++) {
				if(firstValue != mChessArray[i, i]) {
					anyMatch = false;
					break;
				}
			}

			if(anyMatch) {
				winningPlayer = firstValue;
				winLine = GameBoard.WinLine.LineD2;
				gameResult = winningPlayer == 1 ? 
					GameBoard.Result.P1Win : GameBoard.Result.P2Win;

				return gameResult;
			}
		}


		// Check from Down to Top
		firstValue = mChessArray[0, kNumGrid-1];
		if(firstValue != 0) {
			anyMatch = true;

			for(int x=1; x<kNumGrid; x++) {
				int y=kNumGrid-x-1;

				if(firstValue != mChessArray[x, y]) {
					anyMatch = false;
					break;
				}
			}

			if(anyMatch) {
				winningPlayer = firstValue;
				winLine = GameBoard.WinLine.LineD1;
				gameResult = winningPlayer == 1 ? 
					GameBoard.Result.P1Win : GameBoard.Result.P2Win;

				return gameResult;
			}
		}

		return GameBoard.Result.NotYet;
	}

}

