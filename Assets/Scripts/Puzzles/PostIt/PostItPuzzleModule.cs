﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostItPuzzleModule : PuzzleModule
{
    public PuzzleModule SimpleWiresModule;

    public Keypad KeypadModule;
    public PostItMode PostItMode;

    public GameObject PostItSprite;
    public GameObject PostItUISprite;

    private SpriteRenderer _spriteRenderer;

    private bool _isFullscreen = false;

    private GameProgress _gameProgressName;
    public override GameProgress OwnGameProgressName {
        get { return _gameProgressName; }
    }

    void Awake()
    {
        _spriteRenderer = PostItSprite.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        // generate puzzle solution for followup puzzle
        switch (PostItMode)
        {
            case PostItMode.Initial:
                CreateInitialPostIt();
                break;
            case PostItMode.Enigma:
                CreateEnigmaPostIt();
                break;
            case PostItMode.Wires:
                CreateSimpleWiresPostIt();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        ConfigureUISprite();
        AdaptBoxColliderToSprite();
    }

    private void ConfigureUISprite()
    {
        if (PostItUISprite != null)
        {
            PostItUISprite.GetComponent<PostItUISprite>().ConfigureForSprite(_spriteRenderer.sprite);
        }
    }

    private void CreateInitialPostIt()
    {
        // _spriteRenderer.sprite = selectedPostItSprite;
        _gameProgressName = GameProgress.TakenInitialPostIt;
    }

    private void CreateSimpleWiresPostIt()
    {
        // generate wires combination
        if (SimpleWiresModule != null)
        {
            // TODO: SimpleWiresModule.Solution = ...
        }

        // _spriteRenderer.sprite = selectedPostItSprite;
        _gameProgressName = GameProgress.TakenSimpleWiresPostIt;
    }

    private void CreateEnigmaPostIt()
    {
        if (KeypadModule != null)
        {
            // TODO: KeypadModule.SetSomething
        }

        // _spriteRenderer.sprite = selectedPostItSprite;
        _gameProgressName = GameProgress.TakenEnigmaPostIt;
    }

    public override void OnPlayerProgress(GameProgress progress)
    {
        GameProgress? requiredProgressObject;
        switch (PostItMode)
        {
            case PostItMode.Initial:
                requiredProgressObject = null;
                break;
            case PostItMode.Enigma:
                requiredProgressObject = GameProgress.OpenedChain;
                break;
            case PostItMode.Wires:
                requiredProgressObject = GameProgress.OpenedChain; // TODO
                break;
            default:
                requiredProgressObject = null;
                break;
        }

        if (requiredProgressObject == null || progress == requiredProgressObject)
        {
            MakeMeInteractable();
        }
    }

    private void AdaptBoxColliderToSprite()
    {
        var sprite = _spriteRenderer.sprite;
        var boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        boxCollider2D.size = sprite.bounds.size;
    }

    private void ShowFullScreen()
    {
        const float tweenTime = 0.3f;

        var sprite = _spriteRenderer.sprite;

        var centerPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1.0f));
        LeanTween.move(gameObject, centerPosition, tweenTime).setEaseInOutQuad();

        var worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        var xScale = worldScreenWidth / sprite.bounds.size.x;
        var yScale = worldScreenHeight / sprite.bounds.size.y;
        var scale = Math.Min(xScale, yScale) * 0.8f; // scale to 80% of fullscreen

        LeanTween.scale(gameObject, new Vector3(scale, scale, 1.0f), tweenTime).setEaseInOutQuad();
    }

    private void OnMouseUpAsButton()
    {
        if (_isFullscreen)
        {
            MinimizeFromFullscreen();
        }
        else
        {
            ShowFullScreen();
        }

        _isFullscreen = !_isFullscreen;
    }

    private void MinimizeFromFullscreen()
    {
        const float tweenTime = 0.5f;
        LeanTween.scale(gameObject, new Vector3(0.8f, 0.8f, 1.0f), tweenTime).setEaseInOutCubic();
        LeanTween.alpha(gameObject, 0.0f, tweenTime)
            .setOnComplete(RemoveYourselfAndAlertGameSystem);
    }

    private void RemoveYourselfAndAlertGameSystem()
    {
        MarkAsSolved();
        GameObject.Destroy(gameObject);

        if (PostItUISprite != null)
        {
            PostItUISprite.GetComponent<PostItUISprite>().MakeAvailableInInventory();
        }
    }
}