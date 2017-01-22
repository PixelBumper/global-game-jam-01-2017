using UnityEngine;

public class ScissorPickUp : PuzzleModule
{
    public GameObject scissor;
    public GameObject LockingPanel;

    public override void OnPlayerProgress(GameProgress progress)
    {
        if (GameProgress.RemovedSimpleWiresPuzzlePanel.Equals(progress))
        {
            MakeMeInteractable();
            LeanTween.alpha(LockingPanel, 0f, 0.5f)
                .setOnComplete(() => LockingPanel.SetActive(false));
        }
    }

    public override GameProgress OwnGameProgressName
    {
        get { return GameProgress.ResolvedScissorPuzzle; }
    }

    private void OnMouseUpAsButton()
    {
        scissor.SetActive(false);
        GameState.GetGlobalGameState().PickInventoryItem(GameInventory.Scissors);
        MarkAsSolved();
    }
}
