using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Grabber : MonoBehaviour
{
    private PuzzlePlace _puzzlePlace;
    private bool _isGrabbed;
    public Action IsGrabbed;
    private float _time;

    public static Grabber Instance;

    private void Awake() => Instance = this;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            _time += Time.deltaTime;
            if (_time >= 0.3f)
            {
                RaycastHit2D hit = Physics2D.Raycast(Input.GetTouch(0).position, Vector3.forward);
                if (hit.collider != null)
                {
                    _puzzlePlace = hit.collider.GetComponent<PuzzlePlace>();
                    if (_puzzlePlace != null)
                    {
                        if (!_puzzlePlace.IsPlaced && !_isGrabbed)
                        {
                            _puzzlePlace.isGrabbed = true;
                            _isGrabbed = true;
                            IsGrabbed?.Invoke();
                        }
                    }
                }
            }
        }
        else
        {
            if (_puzzlePlace != null) _puzzlePlace.isGrabbed = false;
            _isGrabbed = false;
            _time = 0;
        }
    }
}
