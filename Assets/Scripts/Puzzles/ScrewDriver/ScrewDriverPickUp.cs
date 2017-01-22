using UnityEngine;

public class ScrewDriverPickUp : PuzzleModule
{
    public GameObject screwDriver;
    public GameObject LockingPanel;

    public override void OnPlayerProgress(GameProgress progress)
    {
        if (GameProgress.ResolvedEnigmaPuzzle.Equals(progress))
        {
            MakeMeInteractable();
            LeanTween.alpha(LockingPanel, 0f, 0.5f).setOnComplete(() => LockingPanel.SetActive(false));
        }
    }

    public override GameProgress OwnGameProgressName
    {
        get { return GameProgress.ResolvedScrewdriverPuzzle; }
    }

    private void OnMouseUpAsButton()
    {
        screwDriver.SetActive(false);
        GameState.GetGlobalGameState().PickInventoryItem(GameInventory.Screwdriver);
        MarkAsSolved();
    }
}
