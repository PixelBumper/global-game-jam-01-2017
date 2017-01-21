using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHandler : MonoBehaviour {

	public PuzzleLogic logic;

	private void OnMouseUpAsButton()
	{
		Debug.Log ("TileHandler OnMouseUpAsButton");
		logic.OnTileClicked (gameObject);
	}
}
