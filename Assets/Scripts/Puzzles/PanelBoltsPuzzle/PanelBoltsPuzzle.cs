using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelBoltsPuzzle : PuzzleModule {

    public GameProgress ExpectedProgressToActivate;
    public GameProgress GameProgressItUnlocks;

    public List<SingleBoltPuzzle> bolts;
    public int BoltsUnscrewed;

    public bool IsInteractable;

    // Use this for initialization
    public override void OnPlayerProgress(GameProgress progress)
    {
        if (ExpectedProgressToActivate.Equals(progress))
        {
            MakeMeInteractable();
            IsInteractable = true;
        }
    }

    private void Start()
    {
        MakeMeInteractable();
        foreach (var singleBoltPuzzle in bolts)
        {
            singleBoltPuzzle.ParentPanel = this;
        }
    }

    public override GameProgress OwnGameProgressName
    {
        get { return GameProgressItUnlocks; }
    }

    public bool CanUnScreweBolt()
    {
        return AmIHolding(GameInventory.Screwdriver);
    }

    public void BoltUnscrewed()
    {
        BoltsUnscrewed++;
        if (BoltsUnscrewed >= bolts.Count)
        {
            MarkAsSolved();
            gameObject.SetActive(false);
        }
    }
}
