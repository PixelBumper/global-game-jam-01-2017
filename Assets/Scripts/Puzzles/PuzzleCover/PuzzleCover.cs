using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCover : PuzzleModule
{

    public GameProgress ExpectedProgressToActivate;
    public GameProgress GameProgressItUnlocks;
    public Animator Animator;

	// Use this for initialization
    public override void OnPlayerProgress(GameProgress progress)
    {
        if (ExpectedProgressToActivate.Equals(progress))
        {
            MakeMeInteractable();
        }
    }

    public override GameProgress OwnGameProgressName
    {
        get { return GameProgressItUnlocks; }
    }

    private void OnMouseEnter()
    {
        if (Animator)
        {
            Animator.SetBool("IsHighlighted", true);
        }
    }

    private void OnMouseExit()
    {
        if (Animator)
        {
            Animator.SetBool("IsHighlighted", false);
        }
    }

    private void OnMouseUpAsButton()
    {
        MarkAsSolved();
        gameObject.SetActive(false);
    }
}
