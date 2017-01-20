using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPuzzleModuleDependent : PuzzleModule {

    private float _test = 0.0f;
    private float _delta = 0.00f;

    // Use this for initialization
    void Start () {
    }

    public override void OnPlayerProgress(GameProgress progress)
    {
        if (progress == GameProgress.LightUpColorMixPanel)
        {
            _delta = 0.01f;
        }
    }

    public override void OnBecomeInteractable()
    {
    }

    private void Awake()
    {
    }

    private void Update()
    {
        _test += _delta;
        if (_test > 1.0f)
        {
            _test = 0.0f;
        }
        if (_test < 0.0f)
        {
            _test = 1.0f;
        }
        GetComponent<SpriteRenderer>().color = new Color(0.0f, _test, 1 - _test);
    }

    private void FixedUpdate()
    {
    }

    private void OnMouseUpAsButton()
    {
        _delta *= -1.0f;
    }
}
