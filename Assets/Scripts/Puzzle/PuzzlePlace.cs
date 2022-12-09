using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PuzzlePlace : MonoBehaviour
{
    [field: SerializeField] public bool IsPlaced { get; private set; }

    [SerializeField] private Animator _animator;

    public bool isGrabbed;
    private bool _isFirstGrab;

    private GameObject _slot;

    public Action<PuzzlePlace> Placed;
    public Action<PuzzlePlace> FirstGrabbed;

    [SerializeField] private Transform _parent;
    [SerializeField] private RectTransform _rectTransform;

    private Vector3 _size;

    private void Awake()
    {
        _parent = transform.parent;
    }

    private void Update()
    {
        if (!IsPlaced && isGrabbed)
        {
            if (!_isFirstGrab)
            {
                _isFirstGrab = true;
                FirstGrab();
            }
            if (Input.touchCount > 0)
                transform.position = Input.GetTouch(0).position;
            else
                isGrabbed = false;
            if (Vector2.Distance(transform.position, _slot.transform.position) < 15)
            {
                transform.position = _slot.transform.position;
                SetPlaced();
                isGrabbed = false;
            }
        }
    }

    public void SetScale(Vector3 size) => _size = size;

    public void SetPlaced()
    {
        IsPlaced = true;
        Placed?.Invoke(this);

        StartCoroutine(PlayAnim());
    }
    
    private IEnumerator PlayAnim()
    {
        _animator.SetBool("Placed", true);
        yield return new WaitForSeconds(.5f);
        _animator.SetBool("Placed", false);
    }

    private void FirstGrab()
    {
        transform.SetParent(_parent);
        _rectTransform.localScale = Vector3.one;
        _rectTransform.sizeDelta = _size;
        FirstGrabbed?.Invoke(this);
    }

    public void SetSlot(GameObject slot) => _slot = slot;
}
