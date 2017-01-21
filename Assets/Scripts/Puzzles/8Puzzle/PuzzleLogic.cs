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
	private static int NUM_RANDOM_TILE_MOVEMENTS = 20;
	private GameObject[] board = new GameObject[DIMENSIONS*DIMENSIONS];
	private GameObject[] originalBoard = new GameObject[DIMENSIONS*DIMENSIONS];

	void Start () {
		//init board
		originalBoard [0] = emptyTile;
		originalBoard [1] = tile2;
		originalBoard [2] = tile3;
		originalBoard [3] = tile4;
		originalBoard [4] = tile5;
		originalBoard [5] = tile6;
		originalBoard [6] = tile7;
		originalBoard [7] = tile8;
		originalBoard [8] = tile9;

		Array.Copy (originalBoard, board, originalBoard.Length);
		PerformRandomMovements (NUM_RANDOM_TILE_MOVEMENTS);
		InitTilePositions ();
	}

	private void PerformRandomMovements(int numMovements) {
		for (int i = 0; i < numMovements; i++) {
			//select random adjacent tile from empty tile
			int emptyTileIndex = Array.IndexOf (board, emptyTile);
			List<int> adjacentTiles = GetAdjacentTileIndices (emptyTileIndex);
			int randomIndex = UnityEngine.Random.Range(0, adjacentTiles.Count);
			int randomTileIndex = adjacentTiles [randomIndex];
			//swap tile with empty tile
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
