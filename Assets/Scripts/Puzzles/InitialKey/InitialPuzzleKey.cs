using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPuzzleKey : PuzzleModule{

	// Use this for initialization
    public override void OnPlayerProgress(GameProgress progress)
    {
        throw new System.NotImplementedException();
    }

    public override void OnBecomeInteractable()
    {
        throw new System.NotImplementedException();
    }

    private void OnMouseUpAsButton()
    {

        GameState.GetGlobalGameState().UnlockGameProgress(GameProgress.LightUpColorMixPanel);
        MarkAsSolved();
    }
}
