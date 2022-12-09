using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager
{
    private readonly Puzzle[,] _puzzles;

    private readonly int _minFieldX = 3;
    private readonly int _maxFieldX = 5;
    private readonly int _minFieldY = 4;
    private readonly int _maxFieldY = 8;

    private readonly float _offset = 175;

    private readonly Puzzle _prefabPuzzle;
    private readonly PuzzleSlot _prefabSlot;
    private readonly Transform _puzzleParent;

    private readonly int maxX, maxY;

    private readonly CalculateSides _calculateSides;

    private Vector3 _scale;
    public PuzzleManager(Color32[] color, Puzzle puzzlePrefab, PuzzleSlot puzzleSlotPrefab, Transform parent)
    {
        _calculateSides = new(color);
        _prefabPuzzle = puzzlePrefab;
        _prefabSlot = puzzleSlotPrefab;
        _puzzleParent = parent;
        maxX = Random.Range(_minFieldX, _maxFieldX+1);
        maxY = Random.Range(_minFieldY, _maxFieldY+1);
        _puzzles = new Puzzle[maxX, maxY];

        _scale = new Vector2(1, 0);
        _scale.y = _scale.x;
        _offset = _scale.x * 175;
        GeneratePuzzle();
        SetPosParent();
    }

    private void GeneratePuzzle()
    {
        for (int i = 0; i < maxX; i++)
        {
            for (int j = 0; j < maxY; j++)
            {
                Puzzle puzzle = CreatePiece();
                PuzzleSlot puzzleSlot = CreateSlot();

                SetScaleAndSize(puzzle, new Vector2(i * _offset + _offset / 2, j * _offset + _offset / 2));
                SetScaleAndSize(puzzleSlot, new Vector2(i * _offset + _offset / 2, j * _offset + _offset / 2));

                _puzzles[i, j] = puzzle;

                puzzle.Init(CalculateSides(GetLastSidesState(i, j), GetLeftSidesState(i, j), i, j));
                puzzle.PuzzlePlace.SetScale(_scale);
                puzzle.PuzzlePlace.SetSlot(puzzleSlot.gameObject);
            }
        }
    }

    private void SetPosParent() => _puzzleParent.localPosition = new(_offset * maxX / 2 - _offset * maxX, _offset * maxY / 2 - _offset * maxY);

    private void SetScaleAndSize(IPuzzle puzzle, Vector2 pos)
    {
        puzzle.RectTransform.localPosition = pos;
        puzzle.RectTransform.localScale = _scale;
    }

    private Puzzle CreatePiece() => Puzzle.Instantiate(_prefabPuzzle, _puzzleParent);

    private PuzzleSlot CreateSlot() => PuzzleSlot.Instantiate(_prefabSlot, _puzzleParent);

    public Puzzle[,] GetPuzzles() => _puzzles;

    private SidesState CalculateSides(SidesState lastState, SidesState leftState, int i, int j) => _calculateSides.Calculate(lastState, leftState, i, j, maxX - 1, maxY - 1);

    private SidesState GetLastSidesState(int i, int j)
    {
        if (i == 0 && j == 0) return _puzzles[i,j].SidesState;
        if (i > 0 && j == 0) return _puzzles[i - 1, 0].SidesState;
        return _puzzles[i, j - 1].SidesState;
    }

    private SidesState GetLeftSidesState(int i, int j)
    {
        if (i == 0) return _puzzles[i, j].SidesState;
        return _puzzles[i - 1, j].SidesState;
    }
}
