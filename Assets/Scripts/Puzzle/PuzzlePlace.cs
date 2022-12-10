using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PuzzlePlace : MonoBehaviour
{
    [field: SerializeField] public bool IsPlaced { get; private set; }
    private SidesState _sidesState;

    [SerializeField] private Animator _animator;

    public bool isGrabbed;
    private bool _isFirstGrab, _newGrab;

    private GameObject _slot;
    private List<PuzzleSlot> _puzzleSlots;

    public Action<PuzzlePlace> Placed;

    private Transform _parent;

    [SerializeField] private RectTransform _rectTransform;

    private PuzzleSlot _nearest;

    private Vector3 _size;

    private Vector2 _startPos;
    private Transform _startParent;
    private Vector2 _scale;
    private int _startIndex;

    private void Awake() => _parent = transform.parent;

    private void Update()
    {
        if (!IsPlaced && isGrabbed)
        {
            if (!_newGrab)
                NewGrab();
            if (!_isFirstGrab)
            {
                _isFirstGrab = true;
                FirstGrab();
            }
            if (Input.touchCount > 0)
                transform.position = Input.GetTouch(0).position;
            else
                isGrabbed = false;
        }
        if (!IsPlaced && !isGrabbed)
        {
            if (_newGrab)
            {
                _newGrab = false;
                Place();
            }
        }
    }

    public void SetScale(Vector3 size) => _size = size;

    private void SetPlaced()
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
    }

    private void NewGrab()
    {
        _newGrab = true;
        _startParent = transform.parent;
        _startPos = transform.localPosition;
        _scale = transform.localScale;
        _startIndex = transform.GetSiblingIndex();
        _rectTransform.localScale = Vector3.one;
        if (_nearest != null && _nearest.PuzzlePlace == this) _nearest.OnRemove();
    }

    public void Place()
    {
        _nearest = FindSlot();
        if (_nearest.PuzzlePlace == null)
        {
            if (Vector2.Distance(transform.position, _nearest.transform.position) < 60)
            {
                transform.position = _nearest.transform.position;
                _nearest.OnPlace(this);
                if (Vector2.Distance(transform.position, _slot.transform.position) < 15 && _sidesState == _nearest.SidesState)
                {
                    transform.position = _slot.transform.position;
                    SetPlaced();
                }
            }
        }
        else
        {
            transform.SetParent(_startParent);
            transform.localPosition = _startPos;
            transform.localScale = _scale;
            transform.SetSiblingIndex(_startIndex);
            _nearest = FindSlot();
        }
    }

    public void Init(GameObject slot, SidesState sidesState)
    {
        _slot = slot;
        _sidesState = sidesState;
    }

    public void OnGenerationFinished(SlotsManager slotsManager, PuzzleManager puzzleManager)
    {
        puzzleManager.GenerationFinished -= OnGenerationFinished;
        _puzzleSlots = slotsManager.GetSlots();
    }

    private PuzzleSlot FindSlot()
    {
        float distance = Mathf.Infinity;

        foreach (PuzzleSlot slot in _puzzleSlots)
        {
            float curDistance = Vector2.Distance(transform.position, slot.transform.position);
            if (curDistance < distance)
            {
                _nearest = slot;
                distance = curDistance;
            }
        }
        return _nearest;
    }
}
