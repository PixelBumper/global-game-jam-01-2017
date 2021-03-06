﻿// progress elements to indicate completion of areas in the game, cannot be removed
public enum GameProgress
{
    LightUpColorMixPanel, // This one is only for testing

    // Actual game states (sorted by expected appearance)
    TakenInitialPostIt,
    OpenedChain,
    TakenEnigmaPostIt,
    ResolvedEnigmaPuzzle,
    ResolvedScrewdriverPuzzle,
    RemovedSlidingPuzzlePanelBolts,
    RemovedSlidingPuzzlePanel,
    ResolvedSlidingPuzzle,
    SkirimLockerOpened,
    RemovedSimpleWiresPuzzlePanelBolts,
    RemovedSimpleWiresPuzzlePanel,
    ResolvedScissorPuzzle,
    TakenSimpleWiresPostIt,
    ResolvedSimpleWiresPuzzle,
    ResolvedSimonSaysPuzzle,
    RemovedLeversPuzzlePanelLocker,
    RemovedLeversPuzzlePanelBolts,
    RemovedLeversPuzzlePanel,
    ResolvedLeversPuzzle,
    ResolvedDoorOpen,
    HamsterRescued,
    HamsterExplode
}
