using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    private PuzzlePlace _puzzlePlace;
    private bool _isGrabbed;

    private void Update()
    {
        if (Input.touchCount > 0)
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
                        print(_puzzlePlace);
                        _isGrabbed = true;
                    }
                }
            }
        }
        else
        {
            if (_puzzlePlace != null) _puzzlePlace.isGrabbed = false;
            _isGrabbed = false;
        }
    }
}
