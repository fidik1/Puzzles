using UnityEngine;
using System;

public class Grabber : MonoBehaviour
{
    private PuzzlePlace _puzzlePlace;
    private bool _isGrabbed, _isScroll;
    public Action IsGrabbed;
    private float _time;

    public static Grabber Instance;

    private void Awake()
    { 
        if (Instance == null) Instance = this; 
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (!_isScroll)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Stationary)
                    _time += Time.deltaTime;
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    _isScroll = true;
                if (_time >= 0.2f)
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
        }
        else if (Input.touchCount <= 0 || Input.GetTouch(0).phase != TouchPhase.Stationary)
        {
            if (_puzzlePlace != null) _puzzlePlace.isGrabbed = false;
            _isGrabbed = false;
            _isScroll = false;
            _time = 0;
        }
    }
}
