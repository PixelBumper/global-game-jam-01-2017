using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLogic : PuzzleModule {

	/**
	 * Order of tiles:
	 * 7 8 9
	 * 4 5 6
	 *   2 3
	 */

	public GameObject emptyTile;
	public GameObject tile2;
	public GameObject tile3;
	public GameObject tile4;
	public GameObject tile5;
	public GameObject tile6;
	public GameObject tile7;
	public GameObject tile8;
	public GameObject tile9;

	private static float TILE_SWAP_SPEED = 0.42f;
	private static int DIMENSIONS = 3;
	private GameObject[] board = new GameObject[DIMENSIONS*DIMENSIONS];

	// Use this for initialization
	void Start () {
		board [0] = emptyTile;
		board [1] = tile2;
		board [2] = tile3;
		board [3] = tile4;
		board [4] = tile5;
		board [5] = tile6;
		board [6] = tile7;
		board [7] = tile8;
		board [8] = tile9;
	}

	int GetIndexOfFreeAdjacentTile(int index) {
		if (board [index] == emptyTile) {
			return -1;
		}
		if (index - DIMENSIONS >= 0) {
			//previous row / up
			int adjacentIndex = index - DIMENSIONS;
			if (board [adjacentIndex] == emptyTile) {
				return adjacentIndex;
			}
		}
		if (((index + 1) / DIMENSIONS) == (index / DIMENSIONS)) {
			//right
			int adjacentIndex = index + 1;
			if (board[adjacentIndex] == emptyTile) {
				return adjacentIndex;
			}
		}
		if (index + DIMENSIONS < board.Length) {
			//next row / down
			int adjacentIndex = index + DIMENSIONS;
			if (board[adjacentIndex] == emptyTile) {
				return adjacentIndex;
			}
		}
		if (index - 1 >= 0 && ((index - 1) / DIMENSIONS) == (index / DIMENSIONS)) {
			int adjacentIndex = index - 1;
			if (board[adjacentIndex] == emptyTile) {
				return adjacentIndex;
			}        
		}
		return -1;
	}


	public override void OnPlayerProgress(GameProgress progress)
	{
		// do nothing
	}

    public override GameProgress OwnGameProgressName
    {
        get { return GameProgress.LightUpColorMixPanel; }
    }

    private void Awake()
	{
	}

	private float _test = 0.0f;
	private float _delta = 0.01f;
	private void Update()
	{
	}

	private void FixedUpdate()
	{
	}

	private void OnMouseUpAsButton()
	{
		Debug.Log("PuzzleLogic OnMouseUpAsButton");
	}

	public void OnTileClicked(GameObject tile) {
		int indexOfTile = Array.IndexOf (board, tile);
		int indexOfFreeTile = GetIndexOfFreeAdjacentTile (indexOfTile);
		if (indexOfFreeTile >= 0) {
			Debug.Log ("Free tile available from index " + indexOfTile + ": " + indexOfFreeTile);
			swapTiles (indexOfTile, indexOfFreeTile);
		} else {
			Debug.Log ("No free tile available from index " + indexOfTile);
		}
	}

	private void swapTiles(int tileIndexToSwap, int emptyTileIndex) {
		if (board [emptyTileIndex] != emptyTile) {
			throw new Exception ("Cannot swap tile at index " + tileIndexToSwap + " with non-empty tile at index.");
		}
		//swap tiles in model
		GameObject tileToSwap = board [tileIndexToSwap];
		board [tileIndexToSwap] = emptyTile;
		board [emptyTileIndex] = tileToSwap;

		//swap tiles on screen
		Vector3 oldTilePosition = tileToSwap.transform.position;
		LeanTween.move(tileToSwap, emptyTile.transform.position, TILE_SWAP_SPEED).setEaseInOutQuad();
		emptyTile.transform.position = oldTilePosition;
	}
}
