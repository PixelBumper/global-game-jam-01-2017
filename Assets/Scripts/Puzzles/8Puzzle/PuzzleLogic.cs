﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLogic : PuzzleModule {

	/**
	 * Order of tiles:
	 * 1 2 3
	 * 4 5 6
	 * 7 8 X
	 * 
	 * Board indices
	 * 6 7 8
	 * 3 4 5
	 * 0 1 2
	 */

	public GameObject emptyTile;
	public GameObject tile1;
	public GameObject tile2;
	public GameObject tile3;
	public GameObject tile4;
	public GameObject tile5;
	public GameObject tile6;
	public GameObject tile7;
	public GameObject tile8;

	private static float TILE_SWAP_SPEED = 0.42f;
	private static int DIMENSIONS = 3;
	public int NUM_RANDOM_TILE_MOVEMENTS = 6;
	private GameObject[] board = new GameObject[DIMENSIONS*DIMENSIONS];
	private GameObject[] originalBoard = new GameObject[DIMENSIONS*DIMENSIONS];
	private bool inputEnabled = true;

	void Start () {
		//init board
		originalBoard [0] = tile7;
		originalBoard [1] = tile8;
		originalBoard [2] = emptyTile;
		originalBoard [3] = tile4;
		originalBoard [4] = tile5;
		originalBoard [5] = tile6;
		originalBoard [6] = tile1;
		originalBoard [7] = tile2;
		originalBoard [8] = tile3;

		Array.Copy (originalBoard, board, originalBoard.Length);
		PerformRandomMovements (NUM_RANDOM_TILE_MOVEMENTS);
		InitTilePositions ();
	}

	private void PerformRandomMovements(int numMovements) {
		int previousEmptyTileIndex = -1;
		for (int i = 0; i < numMovements; i++) {
			//select random adjacent tile from empty tile
			int emptyTileIndex = Array.IndexOf (board, emptyTile);
			List<int> adjacentTiles = GetAdjacentTileIndices (emptyTileIndex);
			adjacentTiles.Remove (previousEmptyTileIndex);
			int randomIndex = UnityEngine.Random.Range(0, adjacentTiles.Count);
			int randomTileIndex = adjacentTiles [randomIndex];
			previousEmptyTileIndex = emptyTileIndex;
			//swap tile with empty tile
			Debug.Log("Random movement " + i + "/" + numMovements + ": " + emptyTileIndex + " to " + randomTileIndex);
			GameObject temp = board[emptyTileIndex];
			board [emptyTileIndex] = board [randomTileIndex];
			board [randomTileIndex] = temp;
			//repeat			
		}
	}

	private List<int> GetAdjacentTileIndices(int index) {
		List<int> adjacentTileIndices = new List<int> ();
		if (index - DIMENSIONS >= 0) {
			//previous row / up
			adjacentTileIndices.Add(index - DIMENSIONS);
		}
		if (((index + 1) / DIMENSIONS) == (index / DIMENSIONS)) {
			//right
			adjacentTileIndices.Add(index + 1);
		}
		if (index + DIMENSIONS < board.Length) {
			//next row / down
			adjacentTileIndices.Add(index + DIMENSIONS);
		}
		if (index - 1 >= 0 && ((index - 1) / DIMENSIONS) == (index / DIMENSIONS)) {
			adjacentTileIndices.Add(index - 1);
		}	
		return adjacentTileIndices;
	}

	private void InitTilePositions() {
		Vector3[] originalPositions = new Vector3[originalBoard.Length];
		for (int i = 0; i < originalBoard.Length; i++) {
			originalPositions [i] = originalBoard [i].transform.position;
		}
		for (int i = 0; i < originalBoard.Length; i++) {
			GameObject tile = originalBoard [i];
			int newIndex = Array.IndexOf (board, tile);
			tile.transform.position = originalPositions [newIndex];
		}
	}

	int GetIndexOfFreeAdjacentTile(int index) {
		if (board [index] == emptyTile) {
			return -1;
		}
		foreach (int adjacentIndex in GetAdjacentTileIndices (index)) {
			if (board [adjacentIndex] == emptyTile) {
				return adjacentIndex;
			}
		}
		return -1;
	}


	public override void OnPlayerProgress(GameProgress progress)
	{
	    inputEnabled = GameProgress.RemovedSlidingPuzzlePanel.Equals(progress);
	}

    public override GameProgress OwnGameProgressName
    {
        get { return GameProgress.ResolvedSlidingPuzzle; }
    }

	private float _test = 0.0f;
	private float _delta = 0.01f;

	public void OnTileClicked(GameObject tile) {
		if (inputEnabled & AmIHolding(GameInventory.None)) {
			int indexOfTile = Array.IndexOf (board, tile);
			int indexOfFreeTile = GetIndexOfFreeAdjacentTile (indexOfTile);
			if (indexOfFreeTile >= 0) {
				swapTiles (indexOfTile, indexOfFreeTile);
			}
		}
	}

	private void swapTiles(int tileIndexToSwap, int emptyTileIndex) {
		if (board [emptyTileIndex] != emptyTile) {
			throw new Exception ("Cannot swap tile at index " + tileIndexToSwap + " with non-empty tile at index.");
		}
		//disable input while swap in progress
		inputEnabled = false;
		//swap tiles in model
		GameObject tileToSwap = board [tileIndexToSwap];
		board [tileIndexToSwap] = emptyTile;
		board [emptyTileIndex] = tileToSwap;

		//swap tiles on screen
		Vector3 oldTilePosition = tileToSwap.transform.position;
		LeanTween.move(tileToSwap, emptyTile.transform.position, TILE_SWAP_SPEED)
			.setEaseInOutQuad()
			.setOnComplete(() => {
				inputEnabled = true;
				checkSolved();
			});
		emptyTile.transform.position = oldTilePosition;
	}

	private void checkSolved() {
		for (int i = 0; i < originalBoard.Length; i++) {
			if (board [i] != originalBoard [i]) {
				return;
			}
		}
		inputEnabled = false;
		MarkAsSolved ();
	    AddTimeInSeconds(10, gameObject);
	}
}
