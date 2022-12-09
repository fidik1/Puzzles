using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class CustomScrollRect : ScrollRect
{    
    public override void OnDrag(PointerEventData eventData) 
    {
        base.OnDrag(eventData);
        if (Input.GetTouch(0).position.y > Screen.height / 8 + 100)
            horizontal = false;
    }
    
    public override void OnEndDrag(PointerEventData eventData) 
    {
        base.OnEndDrag(eventData);
        horizontal = true;
    }
}
