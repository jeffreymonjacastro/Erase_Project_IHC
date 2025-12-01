using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ItemUseContext
{
    public Transform player;
    public Transform rightHand;
    public ItemData item;
    public InventoryController inventory;
    public EquipmentController equipment;
    public InventoryScreenController screen;
}