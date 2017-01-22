using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PasswordPuzzle : PuzzleModule
{
    public int RiddleCode;

    public AudioClip clickSound;

    private String _digitsToType;

    private float _blinkInterval = 0.25f;

    private int _maxBlinkRepetitions = 3;

    private int _blinkCount = 0;

    private float _timePassed = 0.0f;

    private bool _glowing = true;

    private AudioSource _source;

    // Use this for initialization
    public override void OnPlayerProgress(GameProgress progress)
    {
        if (progress == GameProgress.TakenEnigmaPostIt)
        {
            _digitsToType = RiddleCode + "";
            MakeMeInteractable();
            EnableKeypad();
        }
    }

    public override GameProgress OwnGameProgressName
    {
        get { return GameProgress.ResolvedEnigmaPuzzle; } // TODO: still needs something
    }

    void PressedKey(String key)
    {
        if (_isSolved == false)
        {
            _source.PlayOneShot(clickSound);
            int result;

            if (int.TryParse(key, out result) == false || key[0] != _digitsToType[0])
            {
                DisableKeypad();
                MarkAsFailed();
            }
            else
            {
                _digitsToType = _digitsToType.Substring(1);
                if (_digitsToType.Length == 0)
                {
                    //blink ui to notice that the riddle has been solved
                    foreach (Transform child in transform)
                    {
                        child.GetComponent<Key>().DisallowUsage();
                        child.SendMessage("StartGlowing");
                    }
                    _isSolved = true;
                }
            }
        }
    }

    void Start()
    {
        _source = GetComponent<AudioSource>();
        DisableKeypad();
    }

    void DisableKeypad()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Key>().DisallowUsage();
        }
    }

    void EnableKeypad()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Key>().AllowUsage();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isSolved && _blinkCount <= _maxBlinkRepetitions)
        {
            _timePassed += Time.deltaTime;
            if (_timePassed > _blinkInterval)
            {
                if (_glowing)
                {
                    _glowing = false;
                    foreach (Transform child in transform)
                    {
                        child.GetComponent<Key>().SetPressedState();
                    }
                }
                else
                {
                    _glowing = true;
                    _blinkCount++;
                    foreach (Transform child in transform)
                    {
                        child.GetComponent<Key>().StartGlowing();
                    }
                }
                _timePassed = 0;
                if (_blinkCount > _maxBlinkRepetitions)
                {
                    foreach (Transform child in transform)
                    {
                        child.GetComponent<Key>().SetNormalState();
                        child.GetComponent<Key>().AllowUsage();
                    }
                    MarkAsSolved();
                    AddTimeInSeconds(30, gameObject);
                }
            }
        }
    }
}