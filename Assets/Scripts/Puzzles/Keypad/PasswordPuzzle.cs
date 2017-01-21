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
            digitsToType = riddleCode + "";
            // make interacteble
        }
    }

    public override void OnBecomeInteractable()
    {
        throw new System.NotImplementedException();
    }

    void PressedKey(String key)
    {
        Debug.LogError("result: " + key);
        int result;


        if (int.TryParse(key, out result) == false || key[0] != digitsToType[0])
        {
            Debug.LogError("Hamster dead");
            GameState.GetGlobalGameState().UnlockGameProgress(GameProgress.HamsterExplode);
        }
        else
        {
            digitsToType = digitsToType.Substring(1);
            if (digitsToType.Length == 0)
            {
                //blink ui to notice that the riddle has been solved
                //move to next riddle
                

                Debug.LogError("password riddle solved");
                GameState.GetGlobalGameState().UnlockGameProgress(GameProgress.ResolvedEnigmaPuzzle);
            }
        }
    }

    void Start()
    {
        OnPlayerProgress(GameProgress.TakenEnigmaPostIt);
    }

    // Update is called once per frame
    void Update()
    {
    }
}