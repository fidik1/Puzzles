using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IssuingPuzzles : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    private readonly List<Puzzle> _puzzles = new(); 
    private int _currentIndex;

    private void Start()
    {
        int index = 0;
        foreach (Puzzle puzzle in World.Instance.PuzzleManager.GetPuzzles())
        {
            if (index == World.Instance.PuzzleManager.GetPuzzles().Length / 2)
                puzzle.PuzzlePlace.SetPlaced();
            else
            {
                _puzzles.Add(puzzle);
                puzzle.transform.position = new Vector3(0, -4000);
            }
            puzzle.PuzzlePlace.FirstGrabbed += AddPuzzleToParent;
            index++;
        }
        Shaffle(_puzzles);
        for (int i = 0; i < 4; i++)
        {
            AddPuzzleToParent(_puzzles[^1].PuzzlePlace);
        }
    }

    private void AddPuzzleToParent(PuzzlePlace puzzlePlace)
    {
        puzzlePlace.FirstGrabbed -= AddPuzzleToParent;
        if (_currentIndex >= _puzzles.Count) return;
        _puzzles[_currentIndex].transform.SetParent(_parent);
        _puzzles[_currentIndex].transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        _currentIndex++;
    }

    private void Shaffle(List<Puzzle> data)
    {
        for (int i = data.Count - 1; i >= 1; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = data[j];
            data[j] = data[i];
            data[i] = temp;
        }
    }
}
