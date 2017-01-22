using UnityEngine;

public class ScrewDriverPickUp : PuzzleModule
{
    public GameObject screwDriver;

    public override void OnPlayerProgress(GameProgress progress)
    {
        if (GameProgress.ResolvedEnigmaPuzzle.Equals(progress))
        {
            MakeMeInteractable();
            screwDriver.SetActive(true);
        }
    }

    public void Start()
    {
        screwDriver.SetActive(false);
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
