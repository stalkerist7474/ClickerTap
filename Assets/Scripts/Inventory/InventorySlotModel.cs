using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventorySlotModel 
{
    public TypeItem typeSlot;
    public Item currentItem;

    public InventorySlotModel(TypeItem typeSlot, Item currentItem = null)
    {
        this.typeSlot = typeSlot;
        this.currentItem = currentItem;
    }

}
