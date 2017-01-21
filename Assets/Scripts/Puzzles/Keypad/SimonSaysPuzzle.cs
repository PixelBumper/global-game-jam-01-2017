using System;
using System.Collections;
using System.Collections.Generic;
using Microwave;
using UnityEngine;
using Random = UnityEngine.Random;


public class SimonSaysPuzzle : PuzzleModule
{
    private bool _active;


    private List<int> _sequence;

    private int _numberOfCorrectAnswers;

    private int _lastCorrectKeyPosition;


    private float _blinkLength = 0.5f;

    private float _blinkInterval = 0.1f;

    private float _pauseInterval = 1f;

    private float _playerInputTimeout = 4f;


    private int _nextButtonToGlow;

    private float _timePassed = 0.0f;


    private bool _waitForPlayerInput;

    private SimonSayStates _simonSayStates;

    public enum SimonSayStates
    {
        Disabled,
        ShowingSequenceGlowingKey,
        ShowingSequenceKeyOff,
        SequenceBreak,
        WaitingForInput,
        Solved
    }

    public override GameProgress OwnGameProgressName
    {
        get { return GameProgress.ResolvedSimonSaysPuzzle; }
    }

    void Start()
    {
        _active = false;
        _simonSayStates = SimonSayStates.Disabled;
        _numberOfCorrectAnswers = 0;
        _lastCorrectKeyPosition = 0;

//        OnPlayerProgress(GameProgress.ResolvedEnigmaPuzzle);
    }

    // Use this for initialization
    public override void OnPlayerProgress(GameProgress progress)
    {
        if (progress == GameProgress.ResolvedEnigmaPuzzle)
        {
            _sequence = new List<int>();
            for (int i = 0; i <= 1; i++)
            {
                _sequence.Add(i);
            }

            Shuffle(_sequence);
            _simonSayStates = SimonSayStates.ShowingSequenceKeyOff;
            _active = true;
        }
    }

    /// <summary>
    /// Fisher-Yates shuffle the list
    /// </summary>
    /// <param name="list"></param>
    private void Shuffle(List<int> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            int value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    void PressedKey(String key)
    {
        if (_active)
        {
            _simonSayStates = SimonSayStates.WaitingForInput;
            _timePassed = 0;

            int result;


            if (int.TryParse(key, out result) == false || result != _sequence[_lastCorrectKeyPosition])
            {
                MarkAsFailed();
            }
            else
            {

                _lastCorrectKeyPosition++;
                if (_lastCorrectKeyPosition > _numberOfCorrectAnswers)
                {
                    _numberOfCorrectAnswers++;
                    _lastCorrectKeyPosition = 0;
                    _simonSayStates = SimonSayStates.SequenceBreak;
                    _nextButtonToGlow = 0;
                }

                if (_numberOfCorrectAnswers >= _sequence.Count)
                {
                    _simonSayStates = SimonSayStates.Solved;
                    foreach (Transform child in transform)
                    {
                        child.GetComponent<Key>().SetPressedState();
                    }
                    MarkAsSolved();
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (_simonSayStates != SimonSayStates.Disabled && _simonSayStates != SimonSayStates.Solved)
        {
            _timePassed += Time.deltaTime;
            switch (_simonSayStates)
            {
                case SimonSayStates.Disabled:
                    break;
                case SimonSayStates.ShowingSequenceGlowingKey:
                    if (_timePassed >= _blinkLength)
                    {
                        GameObject.Find(_sequence[_nextButtonToGlow] + "").GetComponent<Key>().SetNormalState();
                        _simonSayStates = SimonSayStates.ShowingSequenceKeyOff;
                        _timePassed = 0;
                        _nextButtonToGlow++;
                    }
                    break;
                case SimonSayStates.ShowingSequenceKeyOff:
                    if (_timePassed >= _blinkInterval)
                    {
                        if (_nextButtonToGlow == _numberOfCorrectAnswers + 1)
                        {
                            _simonSayStates = SimonSayStates.SequenceBreak;
                        }
                        else
                        {
                            GameObject.Find(_sequence[_nextButtonToGlow] + "").GetComponent<Key>().StartGlowing();
                            _simonSayStates = SimonSayStates.ShowingSequenceGlowingKey;
                        }
                        _timePassed = 0;
                    }
                    break;
                case SimonSayStates.SequenceBreak:
                    if (_timePassed >= _pauseInterval)
                    {
                        _simonSayStates = SimonSayStates.ShowingSequenceKeyOff;
                        _nextButtonToGlow = 0;
                        _timePassed = 0;
                    }
                    break;
                case SimonSayStates.WaitingForInput:
                    if (_timePassed >= _playerInputTimeout)
                    {
                        _timePassed = 0;
                        _simonSayStates = SimonSayStates.ShowingSequenceKeyOff;
                    }
                    break;
                case SimonSayStates.Solved:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}