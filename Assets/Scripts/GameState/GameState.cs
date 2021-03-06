﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject WinMessage;
    public GameObject LooseMessage;
    public Vector2 KeyCursorHotspot;
    public Vector2 ScissorCursorHotspot;
    public Vector2 ScrewDriverCursorHotspot;
    public GameObject BlackoutPanel;


    public AudioClip backGroundMusic;

    public AudioClip successSound;

    public AudioClip explosion;


    public GameObject ExtraTime;

    private float CurrentTimeInSeconds;

    private AudioSource _source;


    // Use this for initialization
    void Start()
    {
        _source = GetComponent<AudioSource>();
        if (!CurrentInventory.Contains(GameInventory.None))
        {
            CurrentInventory.Add(GameInventory.None);
        }

        foreach (var progress in PlayerProgress)
        {
            NotifyListenersAboutProgress(progress);
        }

        CurrentTimeInSeconds = StartingTimeInSeconds;
        _source.clip = backGroundMusic;
        _source.loop = true;
        _source.Play();
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

    public void AddTimeInSeconds(int seconds, GameObject origin)
    {
        CurrentTimeInSeconds += seconds;

        var newExtraTime = Instantiate(ExtraTime, gameObject.transform);
        var positionOrigin = origin.transform.position;
        newExtraTime.transform.position = new Vector3(positionOrigin.x, positionOrigin.y, -2.5f);
        newExtraTime.transform.localScale = new Vector3(2f, 2f, 1f);
        var floatingTime = newExtraTime.GetComponent<FloatingTime>();
        if (floatingTime)
        {
            floatingTime.SetValueAndFloat(seconds);
        }

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
            EndGameLost();
        }
        else if (GameProgress.HamsterRescued.Equals(progress))
        {
            EndGameWin();
        }
        else
        {
            _source.PlayOneShot(successSound);
            NotifyListenersAboutProgress(progress);
        }
    }

    private void EndGameWin()
    {
        CurrentTimeInSeconds = 999;
        foreach (var objectToDisable in ObjectsToDisable)
        {
            objectToDisable.SetActive(false);
        }
        WinMessage.SetActive(true);
        LeanTween.alpha(WinMessage, 1, 1);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        HamsterController.gameObject.SetActive(false);
        EnableBlackout(0.3f);
    }

    private void EndGameLost()
    {
        foreach (var objectToDisable in ObjectsToDisable)
        {
            objectToDisable.SetActive(false);
        }
        MicrowaveFront.SetActive(true);
        HamsterController.Explode();
        CurrentTimeInSeconds = 0;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        // TODO: play sound

        LeanTween.delayedCall(3.5f, () =>
        {
            LooseMessage.SetActive(true);
            LeanTween.alpha(LooseMessage, 1, 1);
            EnableBlackout(0.3f);
        });
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

    public void RemoveBlackout(float tweenTime)
    {
        if (BlackoutPanel != null)
        {
            LeanTween.alpha(BlackoutPanel, 0.0f, tweenTime)
                .setOnComplete(() => BlackoutPanel.SetActive(false));
        }
    }

    public void EnableBlackout(float tweenTime)
    {
        if (BlackoutPanel != null)
        {
            var blackoutRenderer = BlackoutPanel.GetComponent<SpriteRenderer>();
            var blackoutColor = blackoutRenderer.color;
            blackoutColor.a = 0.0f;
            blackoutRenderer.color = blackoutColor;
            LeanTween.alpha(BlackoutPanel, 0.7f, tweenTime);
            BlackoutPanel.SetActive(true);
        }

    }
}
