using System;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWires : PuzzleModule
{
    public GameObject wireYellow;
    public GameObject wireRed;
    public GameObject wireBlue;
    public GameObject wireGreen;
    public GameObject wireYellowCut;
    public GameObject wireRedCut;
    public GameObject wireBlueCut;
    public GameObject wireGreenCut;
    public List<SimpleWireType> wireSequence;

    private readonly Dictionary<SimpleWireType, Tuple<GameObject, GameObject>> _wires = new Dictionary<SimpleWireType, Tuple<GameObject, GameObject>>();

    // Use this for initialization
    public override void OnPlayerProgress(GameProgress progress)
    {
    }

    public override void OnBecomeInteractable()
    {
    }

    private void Start()
    {
        _wires.Add(SimpleWireType.Red, Tuple.Create(wireRed, wireRedCut));
        _wires.Add(SimpleWireType.Blue, Tuple.Create(wireBlue, wireBlueCut));
        _wires.Add(SimpleWireType.Green, Tuple.Create(wireGreen, wireGreenCut));
        _wires.Add(SimpleWireType.Yellow, Tuple.Create(wireYellow, wireYellowCut));

        foreach (var keyValuePair in _wires)
        {
            keyValuePair.Value.Second.SetActive(false);
        }
    }

    public void OnWireClick(SimpleWireType simpleWireType)
    {
        if (wireSequence[0] == simpleWireType)
        {
            _wires[simpleWireType].First.SetActive(false);
            _wires[simpleWireType].Second.SetActive(true);

            wireSequence.RemoveAt(0);

            if (wireSequence.Count == 0)
            {
                GameState.GetGlobalGameState().UnlockGameProgress(GameProgress.ResolvedSimpleWiresPuzzle);
                MarkAsSolved();
            }
        }
        else
        {
            GameState.GetGlobalGameState().UnlockGameProgress(GameProgress.HamsterExplode);
        }
    }
}
