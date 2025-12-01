using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryUIBase : MonoBehaviour
{
    [Header("Root")]
    [Tooltip("Root object to enable/disable. Usually the InventoryPanel.")]
    [SerializeField] protected GameObject root;

    [Header("UI References")]
    [SerializeField] protected Transform slotContainer;     // the Grid transform
    [SerializeField] protected GameObject slotPrefab;       // the Slot prefab

    [Header("Screen")]
    [SerializeField] protected InventoryScreenController screen;

    protected virtual void Awake()
    {
        // If root is not assigned, assume this GameObject is the root.
        if (root == null)
        {
            root = gameObject;
        }

        if (slotContainer == null)
        {
            Debug.LogError("[InventoryUIBase] Missing reference: slot container");
        }

        if (slotPrefab == null)
        {
            Debug.LogError("[InventoryUIBase] Missing reference: slot prefab");
        }

        if (screen == null)
        {
            Debug.LogError("[InventoryUIBase] Missing reference: screen controller");
        }
    }

    protected virtual void Start()
    {
        BuildSlots();
        Debug.Log($"[InventoryUIBase] Finalized start");
    }

    public void Show()
    {
        root.SetActive(true);
    }

    public void Hide()
    {
        root.SetActive(false);
    }

    protected abstract void BuildSlots();
}
