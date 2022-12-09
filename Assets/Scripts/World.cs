using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField] private Puzzle _puzzlePrefab;
    [SerializeField] private PuzzleSlot _puzzlesSlot;
    [SerializeField] private Transform _puzzleParent;

    [SerializeField] private Color32[] _color;

    public PuzzleManager PuzzleManager { get; private set; }

    public static World Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        PuzzleManager = new(_color, _puzzlePrefab, _puzzlesSlot, _puzzleParent);
    }
}
