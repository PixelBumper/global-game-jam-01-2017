/// <summary>
/// Interface to implemented by the puzzle module behaviours
/// </summary>
public interface IPuzzleModule
{
    /// <summary>
    /// Determines if the puzzle module is solved
    /// </summary>
    bool IsSolved { get; }

    /// <summary>
    /// called by the game state whenever the player finished a certain game progress
    /// </summary>
    /// <param name="progress">the game progress</param>
    void OnPlayerProgress(GameProgress progress);

    /// <summary>
    /// Called when the module becomes interactable
    /// </summary>
    void OnBecomeInteractable();
}
