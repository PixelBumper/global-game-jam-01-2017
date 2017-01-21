using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPuzzleKey : PuzzleModule
{

    public GameObject key;

    // Use this for initialization
    public override void OnPlayerProgress(GameProgress progress)
    {
        if (GameProgress.TakenInitialPostIt.Equals(progress))
        {
            MakeMeInteractable();
        }
    }

    public override void OnBecomeInteractable()
    {
    }

    public override GameProgress OwnGameProgressName
    {
        get { return GameProgress.LightUpColorMixPanel; }
    }

    private void Start()
    {
        MakeMeInteractable(); // TODO: Remove this when initial post it is done
    }

    private void OnMouseUpAsButton()
    {
        Debug.Log("HAHAHAHAHA");
        key.SetActive(false);
        var globalGameState = GameState.GetGlobalGameState();
        globalGameState.PickInventoryItem(GameInventory.InitialKey);
        MarkAsSolved();
    }
}
