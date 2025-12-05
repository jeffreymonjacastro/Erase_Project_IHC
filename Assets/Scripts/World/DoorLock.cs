using UnityEngine;
using UnityEngine.Events;

public class DoorLock : MonoBehaviour
{
    [Header("Lock Settings")]
    [Tooltip("Key id that can unlock this door (should match ItemData.keyId)")]
    [SerializeField] private string requiredKeyId;

    public string RequiredKeyId => requiredKeyId;

    [Header("Initial State")]
    [SerializeField] private bool startLocked = false;
    [SerializeField] private bool startOpen = false;

    public bool IsLocked { get; private set; }
    public bool IsOpen { get; private set; }

    private void Awake()
    {
        if (startLocked && string.IsNullOrEmpty(requiredKeyId))
        {
            Debug.LogError("[DoorLock] Empty or null: required key id");
        }

        IsLocked = startLocked;
        IsOpen = startOpen;
    }

    public void Unlock()
    {
        if (!IsLocked) return;

        IsLocked = false;
    }
    public void Lock()
    {
        if (IsLocked) return;

        IsLocked = true;
    }

    public void Open()
    {
        if (IsLocked || IsOpen) return;

        IsOpen = true;
    }

    public void Close()
    {
        if (!IsOpen) return;

        IsOpen = false;
    }

    public void ToggleOpen()
    {
        if (IsLocked) return;

        if (IsOpen)
            Close();
        else
            Open();
    }
}
