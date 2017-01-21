using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenButtonPuzzle : PuzzleModule
{

    public bool CanBeOpened;
    public Animator animator;

	// Use this for initialization
    public override void OnPlayerProgress(GameProgress progress)
    {
        if (GameProgress.ResolvedLeversPuzzle.Equals(progress))
        {
            CanBeOpened = true;
        }
    }

    public override GameProgress OwnGameProgressName
    {
        get
        {
            return GameProgress.ResolvedDoorOpen;
        }
    }

    private void OnMouseUpAsButton()
    {
        var globalGameState = GameState.GetGlobalGameState();
        if (CanBeOpened)
        {
            MarkAsSolved();
        }
        else
        {
            MarkAsFailed();
        }
    }

    private void OnMouseEnter()
    {
        animator.SetBool("IsHighlighted", true);
    }

    private void OnMouseExit()
    {
        animator.SetBool("IsHighlighted", false);
    }

    private void Start()
    {
        MakeMeInteractable();
    }
}
