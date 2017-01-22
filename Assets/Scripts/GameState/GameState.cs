﻿using System.Collections.Generic;
using UnityEngine;

// Global game state GO
public class GameState : MonoBehaviour
{
    public float StartingTimeInSeconds = 30f;
    public List<GameInventory> CurrentInventory = new List<GameInventory>();
    public List<GameProgress> PlayerProgress = new List<GameProgress>();
    public GameInventory HeldInventoryItem = GameInventory.None;

    public GameInventoryHolder InventoryHolder;
    public HamsterController HamsterController;
    public GameObject MicrowaveFront;
    public GameObject MicrowaveBack;
    public Texture2D KeyCursorTexture;
    public Texture2D ScissorCursorTexture;
    public Texture2D ScrewDriverCursorTexture;
    public List<GameObject> ObjectsToDisable;

    private float CurrentTimeInSeconds;

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

        CurrentTimeInSeconds = StartingTimeInSeconds;
    }

    void Update()
    {
        CurrentTimeInSeconds -= Time.deltaTime;

        var timer = GameObject.FindGameObjectWithTag("Timer");

        if (timer != null)
        {
            timer.GetComponent<Timer>().UpdateTime((int) CurrentTimeInSeconds);
        }

        if (CurrentTimeInSeconds <= 10)
        {
            // TODO: play sound
        }

        if (CurrentTimeInSeconds <= 0)
        {
            GetGlobalGameState().UnlockGameProgress(GameProgress.HamsterExplode);
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

    public static void AddTimeInSeconds(int seconds)
    {
        GetGlobalGameState().CurrentTimeInSeconds += seconds;
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
        if (GameProgress.HamsterExplode.Equals(progress))
        {
            foreach (var objectToDisable in ObjectsToDisable)
            {
                objectToDisable.SetActive(false);
            }
            MicrowaveFront.SetActive(true);
            HamsterController.Explode();
            CurrentTimeInSeconds = 0;
            // TODO: play sound
        }
        else
        {
            NotifyListenersAboutProgress(progress);
        }
    }

    private void NotifyListenersAboutProgress(GameProgress progress)
    {
        // temporarily activate all sides, the GameJam Unity (R) way of finding all objects :-)
        var frontActive = MicrowaveFront.activeSelf;
        var backActive = MicrowaveBack.activeSelf;

        MicrowaveFront.SetActive(true);
        MicrowaveBack.SetActive(true);

        foreach (var puzzleModule in GameObject.FindGameObjectsWithTag("PuzzleModule"))
        {
            foreach (var puzzleModuleBehaviour in puzzleModule.GetComponents<PuzzleModule>())
            {
                puzzleModuleBehaviour.OnPlayerProgress(progress);
            }
        }

        MicrowaveFront.SetActive(frontActive);
        MicrowaveBack.SetActive(backActive);
    }

    public void PickInventoryItem(GameInventory gameItem)
    {
        CurrentInventory.Add(gameItem);
        InventoryHolder.PickedItem(gameItem);
    }

    public void DropInventoryItem(GameInventory gameItem)
    {
        CurrentInventory.Remove(gameItem);
        InventoryHolder.DropItem(gameItem);
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
