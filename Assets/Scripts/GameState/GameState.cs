using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Global game state GO
public class GameState : MonoBehaviour
{
    public double SecondsRemaining;
    public List<GameInventory> CurrentInventory = new List<GameInventory>();
    public List<GameProgress> PlayerProgress = new List<GameProgress>();
    public GameInventory HeldInventoryItem = GameInventory.None;

    public GameInventoryHolder InventoryHolder;

    // Use this for initialization
    void Start()
    {
        if (!CurrentInventory.Contains(GameInventory.None))
        {
            CurrentInventory.Add(GameInventory.None);
        }

        foreach (var progress in PlayerProgress)
        {
            NotifyListenersAboutProgress(progress);
        }
    }

    /// <summary>
    /// Get the global game state from the scene
    /// </summary>
    /// <returns></returns>
    public static GameState GetGlobalGameState()
    {
        return GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
    }

    /// <summary>
    /// Unlocks a part of game progress for the player
    /// </summary>
    /// <param name="progress">the progress to unlock</param>
    public void UnlockGameProgress(GameProgress progress)
    {
        if (PlayerProgress.Contains(progress))
        {
            return;
        }

        PlayerProgress.Add(progress);
        NotifyListenersAboutProgress(progress);
    }

    private static void NotifyListenersAboutProgress(GameProgress progress)
    {
        foreach (var puzzleModule in GameObject.FindGameObjectsWithTag("PuzzleModule"))
        {
            var puzzleModuleBehaviour = puzzleModule.GetComponent<PuzzleModule>();
            if (puzzleModuleBehaviour != null)
            {
                puzzleModuleBehaviour.OnPlayerProgress(progress);
            }
        }
    }

    public void PickInventoryItem(GameInventory gameItem)
    {
        CurrentInventory.Add(gameItem);
        InventoryHolder.PickedItem(gameItem);
    }

    /// <summary>
    /// Check if a certain progress item has been unlocked
    /// </summary>
    /// <param name="progress"></param>
    /// <returns></returns>
    public bool IsProgressCompleted(GameProgress progress)
    {
        return PlayerProgress.Contains(progress);
    }
}