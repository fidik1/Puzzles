using UnityEngine;

public class PuzzleSlot : MonoBehaviour, IPuzzle
{
    [field: SerializeField] public RectTransform RectTransform { get; set; }
    [field: SerializeField] public PuzzlePlace PuzzlePlace { get; private set; }
    
    public SidesState SidesState { get; private set; }

    public void SetSidesState(SidesState sidesState) => SidesState = sidesState;
    
    public void OnPlace(PuzzlePlace puzzlePlace)
    {
        puzzlePlace.transform.SetParent(transform);
        PuzzlePlace = puzzlePlace;
    }

    public void OnRemove() => PuzzlePlace = null;
}
