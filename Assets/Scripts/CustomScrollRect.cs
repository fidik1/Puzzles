using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class CustomScrollRect : ScrollRect
{
    private bool _canDrag = true;

    protected override void Start()
    {
        base.Start();
        Grabber.Instance.IsGrabbed += Grabbed;
    }

    private void Grabbed() => _canDrag = false;

    public override void OnDrag(PointerEventData eventData) 
    {
        base.OnDrag(eventData);
        if (!_canDrag)
            horizontal = false;
    }
    
    public override void OnEndDrag(PointerEventData eventData) 
    {
        base.OnEndDrag(eventData);
        horizontal = true;
        _canDrag = true;
    }
}
