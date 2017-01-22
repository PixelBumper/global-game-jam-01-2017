using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPuzzleKey : PuzzleModule
{
    public GameObject key;

    public override void OnPlayerProgress(GameProgress progress)
    {
        if (GameProgress.TakenInitialPostIt.Equals(progress))
        {
            MakeMeInteractable();
        }
    }

    public override GameProgress OwnGameProgressName
    {
        get { return GameProgress.LightUpColorMixPanel; }
    }

    private void OnMouseUpAsButton()
    {
        key.SetActive(false);
        GameState.GetGlobalGameState().PickInventoryItem(GameInventory.InitialKey);
        MarkAsSolved();
    }
}
