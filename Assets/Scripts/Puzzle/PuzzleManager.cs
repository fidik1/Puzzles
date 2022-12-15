using UnityEngine;
using System;

public class PuzzleManager : MonoBehaviour
{
    private Puzzle[,] _puzzles;

    [SerializeField] private Color32[] _color;

    [SerializeField] private int _minFieldX = 3;
    [SerializeField] private int _maxFieldX = 5;
    [SerializeField] private int _minFieldY = 4;
    [SerializeField] private int _maxFieldY = 8;

    [SerializeField] private float _offset = 175;

    [SerializeField] private Puzzle _prefabPuzzle;
    [SerializeField] private PuzzleSlot _prefabSlot;
    [SerializeField] private Transform _puzzleParent;

    private int _maxX, _maxY;

    private CalculateSides _calculateSides;
    public SlotsController SlotsManager { get; private set; } = new();

    private Vector3 _scale;

    public Action<SlotsController, PuzzleManager> GenerationFinished;

    private void Start()
    {
        _calculateSides = new(_color);

        _maxX = UnityEngine.Random.Range(_minFieldX, _maxFieldX+1);
        _maxY = UnityEngine.Random.Range(_minFieldY, _maxFieldY+1);
        _puzzles = new Puzzle[_maxX, _maxY];

        _scale = new Vector2(1, 0);
        _scale.y = _scale.x;
        _offset = _scale.x * 175;
        GeneratePuzzle();
        SetPosParent();
    }

    private void GeneratePuzzle()
    {
        for (int i = 0; i < _maxX; i++)
        {
            for (int j = 0; j < _maxY; j++)
            {
                Puzzle puzzle = CreatePiece();
                PuzzleSlot puzzleSlot = CreateSlot();

                SetScaleAndSize(puzzle, new Vector2(i * _offset + _offset / 2, j * _offset + _offset / 2));
                SetScaleAndSize(puzzleSlot, new Vector2(i * _offset + _offset / 2, j * _offset + _offset / 2));

                _puzzles[i, j] = puzzle;
                SlotsManager.AddSlot(puzzleSlot);

                puzzle.Init(CalculateSides(GetLastSidesState(i, j), GetLeftSidesState(i, j), i, j));
                puzzleSlot.SetSidesState(puzzle.SidesState);

                GenerationFinished += puzzle.PuzzlePlace.OnGenerationFinished;
                puzzle.PuzzlePlace.SetScale(_scale);
                puzzle.PuzzlePlace.Init(puzzleSlot.gameObject, puzzle.SidesState);
            }
        }
        GenerationFinished?.Invoke(SlotsManager, this);
    }

    private void SetPosParent() => _puzzleParent.localPosition = new(_offset * _maxX / 2 - _offset * _maxX, _offset * _maxY / 2 - _offset * _maxY);

    private void SetScaleAndSize(IPuzzle puzzle, Vector2 pos)
    {
        puzzle.RectTransform.localPosition = pos;
        puzzle.RectTransform.localScale = _scale;
    }

    private Puzzle CreatePiece() => Puzzle.Instantiate(_prefabPuzzle, _puzzleParent);

    private PuzzleSlot CreateSlot() => PuzzleSlot.Instantiate(_prefabSlot, _puzzleParent);

    public Puzzle[,] GetPuzzles() => _puzzles;

    private SidesState CalculateSides(SidesState lastState, SidesState leftState, int i, int j) => _calculateSides.Calculate(lastState, leftState, i, j, _maxX - 1, _maxY - 1);

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
