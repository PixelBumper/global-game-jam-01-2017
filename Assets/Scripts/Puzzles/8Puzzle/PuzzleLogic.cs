using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLogic : PuzzleModule {

	/**
	 * Order of tiles:
	 * 7 8 9
	 * 4 5 6
	 *   2 3
	 */

	public GameObject tile2;
	public GameObject tile3;
	public GameObject tile4;
	public GameObject tile5;
	public GameObject tile6;
	public GameObject tile7;
	public GameObject tile8;
	public GameObject tile9;

	// Use this for initialization
	void Start () {

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
		Debug.Log("OnTileClicked " + tile);
	}
}
