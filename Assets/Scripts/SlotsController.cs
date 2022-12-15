using System.Collections.Generic;

public class SlotsController
{
    private readonly List<PuzzleSlot> _puzzleSlots = new();

    public void AddSlot(PuzzleSlot puzzleSLot) => _puzzleSlots.Add(puzzleSLot);

    public List<PuzzleSlot> GetSlots() => _puzzleSlots;
}
