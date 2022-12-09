using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleSlot : MonoBehaviour, IPuzzle
{
    [field: SerializeField] public RectTransform RectTransform { get; set; }
}
