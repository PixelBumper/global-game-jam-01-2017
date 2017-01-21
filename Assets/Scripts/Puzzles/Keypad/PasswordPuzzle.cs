using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordPuzzle : PuzzleModule
{

    public int riddleCode;

    private String digitsToType;

	// Use this for initialization
    public override void OnPlayerProgress(GameProgress progress)
    {
        if (progress == GameProgress.TakenEnigmaPostIt)
        {
            digitsToType = riddleCode+"";
            // make interacteble
        }
    }

    public override void OnBecomeInteractable()
    {
        throw new System.NotImplementedException();
    }

    void PressedKey(String key)
    {
        Debug.LogError("result: "+ key);
        int result;
        if (int.TryParse(key, out result))
        {
//            digitsToType
        }
        else
        {

            GameObject.Find("GameState").SendMessage("UnlockGameProgress", GameProgress.HamsterExplode);
        }
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
