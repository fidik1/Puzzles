using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IssuingPuzzles : MonoBehaviour
{
    [SerializeField] private RectTransform _parent;
    [SerializeField] private CountPlacedPuzzles _countPlacedPuzzles;
    private readonly List<Puzzle> _puzzles = new(); 
    private int _currentIndex;

    private void Start()
    {
        Grabber.Instance.IsGrabbed += SetSize;
        SetSize();
        int index = 0;
        foreach (Puzzle puzzle in World.Instance.PuzzleManager.GetPuzzles())
        {
            if (index == World.Instance.PuzzleManager.GetPuzzles().Length / 2)
                puzzle.PuzzlePlace.Place();
            else
                _puzzles.Add(puzzle);
            index++;
        }
        Shaffle(_puzzles);
        for (int i = 0; i < _puzzles.Count; i++)
        {
            AddPuzzleToParent();
        }
    }

    private void SetSize() => _parent.sizeDelta = new(175 * (World.Instance.PuzzleManager.GetPuzzles().Length - _countPlacedPuzzles.GetPlacedPuzzles().Count), _parent.sizeDelta.y);

    private void AddPuzzleToParent()
    {
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
