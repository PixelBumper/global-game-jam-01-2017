using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSaysPuzzle : PuzzleModule {

    public float _blinkInterval = 0.25f;

    public float _pauseInterval = 0.5f;


    // Use this for initialization
    public override void OnPlayerProgress(GameProgress progress)
    {
        if (progress == GameProgress.ResolvedSimonSaysPuzzle)
        {

        }
    }

    public override GameProgress OwnGameProgressName
    {
        get { return GameProgress.ResolvedSimonSaysPuzzle; }
        }

    void Start()
    {

    }
	
    // Update is called once per frame
	void Update () {
	    if (_isSolved == false)
	    {

	    }
	}
}
