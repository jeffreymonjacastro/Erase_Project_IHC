using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ItemUseContext
{
    public Transform playerRoot;
    public Camera playerCamera;
    public EquipmentController equipment;
    public InventoryScreenController screen;
    
    public DoorLock targetedDoorLock; // optional for keys
    public SensorFeedbackController sensorFeedback;
}