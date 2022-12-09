using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleSlot : MonoBehaviour, IPuzzle
{
    [field: SerializeField] public RectTransform RectTransform { get; set; }
    [field: SerializeField] public PuzzlePlace PuzzlePlace { get; private set; }
    
    public void OnPlace(PuzzlePlace puzzlePlace)
    {
        puzzlePlace.transform.SetParent(transform);
        PuzzlePlace = puzzlePlace;
    }

    public void OnRemove() => PuzzlePlace = null;
}
