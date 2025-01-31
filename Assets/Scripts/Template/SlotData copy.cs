using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlotData
{
    public ItemData item;
    public int count = 0;

    private Action Onchange;

    public bool CanAddItem()
    {
        return count < item.Maxcount;
    }
    public void AddOne()
    {
        count++;
        Onchange?.Invoke();
    }
    public void AddItem(ItemData item)
    {
        this.item = item;
        count = 1;
        Onchange?.Invoke();
    }

    public void AddListener(Action ONchange)
    {
        this.Onchange = ONchange;

    }
}

