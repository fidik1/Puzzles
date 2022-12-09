using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SlotsManager
{
    private readonly List<PuzzleSlot> _puzzleSlots;

    public SlotsManager(List<PuzzleSlot> puzzleSlots) => _puzzleSlots = puzzleSlots;

    public void AddSlot(PuzzleSlot puzzleSLot) => _puzzleSlots.Add(puzzleSLot);

    public List<PuzzleSlot> GetSlots() => _puzzleSlots;
}
