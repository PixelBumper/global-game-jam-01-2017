using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Global game state GO
public class GameState : MonoBehaviour
{
    public double SecondsRemaining;
    public List<GameInventory> CurrentInventory;
    public List<GameProgress> PlayerProgress;
    public GameInventory HeldInventoryItem;

    public GameInventoryHolder InventoryHolder;


    // Use this for initialization
    void Start()
    {
        CurrentInventory = new List<GameInventory> {GameInventory.None};
        HeldInventoryItem = GameInventory.None;
        PlayerProgress = new List<GameProgress>();
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