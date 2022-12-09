using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountPlacedPuzzles : MonoBehaviour
{
    private readonly List<PuzzlePlace> _puzzlesPlaced = new();
    [SerializeField] private TMP_Text _text;
    private int _maxPuzzles;

    public Action AllPuzzlesPlaced;

    private void Start()
    {
        _maxPuzzles = World.Instance.PuzzleManager.GetPuzzles().Length;
        foreach (Puzzle puzzle in World.Instance.PuzzleManager.GetPuzzles())
        {
            puzzle.PuzzlePlace.Placed += PuzzlePlaced;
        }
    }

    private void PuzzlePlaced(PuzzlePlace puzzlePlace)
    {
        puzzlePlace.Placed -= PuzzlePlaced;
        _puzzlesPlaced.Add(puzzlePlace);
        _text.text = _puzzlesPlaced.Count + " / " + _maxPuzzles;
        if (_puzzlesPlaced.Count == _maxPuzzles) AllPuzzlesPlaced?.Invoke();
    }

    public List<PuzzlePlace> GetPlacedPuzzles() => _puzzlesPlaced;
}
