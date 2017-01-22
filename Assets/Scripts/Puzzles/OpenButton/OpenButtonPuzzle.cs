using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenButtonPuzzle : PuzzleModule
{

    public bool CanBeOpened;
    public Animator animator;

    public List<GameObject> OpenAssets;
    public List<GameObject> ClosedAsssets;

	// Use this for initialization
    public override void OnPlayerProgress(GameProgress progress)
    {
        if (GameProgress.ResolvedSimonSaysPuzzle.Equals(progress))
        {
            CanBeOpened = true;
            foreach (var openAsset in OpenAssets)
            {
                openAsset.SetActive(true);
            }

            foreach (var closedAssset in ClosedAsssets)
            {
                closedAssset.SetActive(false);
            }
        }
    }

    public override GameProgress OwnGameProgressName
    {
        get
        {
            return GameProgress.HamsterRescued;
        }
    }

    private void OnMouseUpAsButton()
    {
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
