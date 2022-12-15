using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomScrollRect : ScrollRect
{
    private bool _canDrag = true;
    private Grabber _grabber;

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

    protected override void Start()
    {
        base.Start();
        _grabber = Grabber.Instance;
        _grabber.IsGrabbed += Grabbed;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _grabber.IsGrabbed -= Grabbed;
    }
}
