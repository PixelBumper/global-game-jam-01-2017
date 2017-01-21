using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHandler : MonoBehaviour {

	public PuzzleLogic logic;

	private void OnMouseDown()
	{
		Debug.Log ("TileHandler OnMouseDown");
		logic.OnTileClicked (gameObject);
	}
}
