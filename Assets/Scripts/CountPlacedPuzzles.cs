using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountPlacedPuzzles : MonoBehaviour
{
    private readonly List<PuzzlePlace> _puzzlesPlaced = new();
    [SerializeField] private TMP_Text _text;
    [SerializeField] private PuzzleManager _puzzleManager;
    private int _maxPuzzles;

    public Action AllPuzzlesPlaced;

    private void Start()
    {
        _maxPuzzles = _puzzleManager.GetPuzzles().Length;
        foreach (Puzzle puzzle in _puzzleManager.GetPuzzles())
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
