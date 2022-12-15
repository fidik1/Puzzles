using UnityEngine;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour, IPuzzle
{
    [SerializeField] private Image[] _sides;
    [SerializeField] private Image[] _edges;
    [SerializeField] private Image[] _edges1;
    [field: SerializeField] public RectTransform RectTransform { get; set; }

    [field: SerializeField] public PuzzlePlace PuzzlePlace { get; private set; }

    public SidesState SidesState { get; private set; } = new();

    public void Init(SidesState sidesState)
    {
        SidesState = sidesState;
        SetSideColor();
        SetEdges(); 
    }

    private void SetSideColor()
    {
        for (int i = 0; i < _sides.Length; i++)
        {
            _sides[i].color = SidesState.color[i];
        }
    }

    private void SetEdges()
    {
        for (int i = 0; i < _edges.Length; i++)
        {
            _edges[i].gameObject.SetActive(SidesState.edgesInner[i]);
            _edges[i].color = SidesState.color[i];
            _edges1[i].gameObject.SetActive(SidesState.edgesOuter[i]);
            _edges1[i].color = SidesState.color[i];
        }
    }
}
