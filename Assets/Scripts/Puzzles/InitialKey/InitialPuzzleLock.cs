using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPuzzleLock : PuzzleModule
{
    public List<GameObject> chains;

    // Use this for initialization
    public override void OnPlayerProgress(GameProgress progress)
    {
        if (GameProgress.TakenInitialPostIt.Equals(progress))
        {
            MakeMeInteractable();
        }
    }

    public override GameProgress OwnGameProgressName
    {
        get { return GameProgress.OpenedChain; }
    }

    void Start()
    {
        MakeMeInteractable(); // TODO: Remove this when initial post it is done
    }

    private void OnMouseUpAsButton()
    {
        var globalGameState = GameState.GetGlobalGameState();

        if (AmIHolding(GameInventory.InitialKey))
        {
            foreach (var chain in chains)
            {
                chain.SetActive(false);
            }
            globalGameState.DropInventoryItem(GameInventory.InitialKey);
            globalGameState.HeldInventoryItem = GameInventory.None;
            MarkAsSolved();
        }
    }
}
