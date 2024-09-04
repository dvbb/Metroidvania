using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public ItemData data;
    public int stackSize;

    public InventoryItem(ItemData data)
    {
        this.data = data;
    }

    public void AddStack() =>stackSize++;
    public void RemoveStack() =>stackSize--;
}
