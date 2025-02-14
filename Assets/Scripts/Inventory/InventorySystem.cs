using System.Collections;
using System.Collections.Generic;
using DigitalRubyShared;
using UnityEngine;

namespace ACG{
public class InventorySystem : Singleton<InventorySystem>
{

    private Transform itemBeingDragged;

    void Start()
    {
        GestureManager.Instance.RegisterGestureDelegate(GestureManager.GestureRecognizerType.LongPress, LongPressCallback);
    }

    void LongPressCallback(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Began)
        {
            BeginDrag(gesture.FocusX, gesture.FocusY);
        }
        else if (gesture.State == GestureRecognizerState.Executing)
        {
            DragTo(gesture.FocusX, gesture.FocusY);
        }
        else if (gesture.State == GestureRecognizerState.Ended)
        {
            EndDrag(gesture.FocusX, gesture.FocusY);
        }
    }

    void BeginDrag(float x, float y)
    {
        var hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(new Vector3(x, y)), Mathf.Infinity, 1 << LayerMask.NameToLayer("Item"));
        if (hit)
        {
            itemBeingDragged = hit.transform;
        }
    }

    void DragTo(float x, float y)
    {
        if (itemBeingDragged != null)
        {
            itemBeingDragged.position = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 10));
        }
    }

    void EndDrag(float x, float y)
    {
        if (itemBeingDragged != null)
        {
            itemBeingDragged = null;
        }
    }
}

}