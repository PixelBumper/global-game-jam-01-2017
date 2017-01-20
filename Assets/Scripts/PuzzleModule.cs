using UnityEngine;/// <summary>
/// Interface to implemented by the puzzle module behaviours
/// </summary>
public abstract class PuzzleModule : MonoBehaviour
{
    public delegate void PuzzleSolvedHandler(PuzzleModule solvedPuzzle);

    /// <summary>
    /// Used to listen for the puzzle being solved successfully
    /// </summary>
    public PuzzleSolvedHandler OnPuzzleSolved;

    /// <summary>
    /// Determines if the puzzle module is solved
    /// </summary>
    private bool _isSolved;

    /// <summary>
    /// called by the game state whenever the player finished a certain game progress
    /// </summary>
    /// <param name="progress">the game progress</param>
    public abstract void OnPlayerProgress(GameProgress progress);

    /// <summary>
    /// Called when the module becomes interactable
    /// </summary>
    public abstract void OnBecomeInteractable();

    /// <summary>
    /// Unity initializer
    /// </summary>
    void Start ()
    {
        _isSolved = false;
    }

    /// <summary>
    /// Mark this puzzle as solved informing all listeners
    /// </summary>
    protected void MarkAsSolved()
    {
        _isSolved = true;
        if (OnPuzzleSolved != null)
        {
            OnPuzzleSolved(this);
        }
    }

    /// <summary>
    /// Checks if the puzzle is solved, manual property because of unity serialization issues
    /// </summary>
    /// <returns></returns>
    public bool IsSolved()
    {
        return _isSolved;
    }
}
