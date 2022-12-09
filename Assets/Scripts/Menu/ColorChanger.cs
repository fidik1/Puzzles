using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Image[] _sides;
    [SerializeField] private Image[] _edges;
    [SerializeField] private Image[] _edges1;
    [SerializeField] private Color[] _colors;

    private void Start() => InvokeRepeating(nameof(SetSideColor), 0, 1);

    private void SetSideColor()
    {
        for (int i = 0; i < _sides.Length; i++)
        {
            Color color = _colors[Random.Range(0, _colors.Length)];
            _sides[i].color = color;
            _edges[i].color = color;
            _edges1[i].color = color;
        }
    }
}
