using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHandler : PuzzleModule {

	public PuzzleLogic logic;

	// Use this for initialization
	void Start () {

	}


	public override void OnPlayerProgress(GameProgress progress)
	{
		// do nothing
	}

	public override void OnBecomeInteractable()
	{
	}

	private void Awake()
	{
	}

	private float _test = 0.0f;
	private float _delta = 0.01f;
	private void Update()
	{
		_test += _delta;
		if (_test > 1.0f)
		{
			_test = 0.0f;
		}
		if (_test < 0.0f)
		{
			_test = 1.0f;
		}
		GetComponent<SpriteRenderer>().color = new Color(_test, 1 - _test, 0.0f);
	}

	private void FixedUpdate()
	{
	}

	private void OnMouseUpAsButton()
	{
		_delta = 0.0f;
		GameState.GetGlobalGameState().UnlockGameProgress(GameProgress.LightUpColorMixPanel);
		MarkAsSolved();
	}
}