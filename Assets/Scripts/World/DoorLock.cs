using UnityEngine;
using UnityEngine.Events;

public class DoorLock : MonoBehaviour
{
    [Header("Lock Settings")]
    [Tooltip("Key id that can unlock this door (should match ItemData.keyId)")]
    [SerializeField] private string requiredKeyId;

    public string RequiredKeyId => requiredKeyId;

    [Tooltip("Initial locked state of the door")]
    [SerializeField] private bool isLocked = true;
    public bool IsLocked => isLocked;

    [Tooltip("Maximum distance from player root to allow interaction")]
    [SerializeField] private float interactionRadius = 1.5f;

    [Header("Door References")]
    [SerializeField] private Transform doorTransform;        // used for distance check
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private string openTriggerName = "Open";
    [SerializeField] private string closeTriggerName = "Close";

    [Header("Events")]
    public UnityEvent onUnlocked;
    public UnityEvent onLocked;

    private void Awake()
    {
        if (string.IsNullOrEmpty(requiredKeyId))
        {
            Debug.LogError("[DoorLock] Empty or null: required key id");
        }

        if (doorTransform == null)
        {
            doorTransform = transform;
        }

        if (doorAnimator == null)
        {
            Debug.LogWarning("[DoorLock] Missing reference: door animator");
        }
    }

    public bool IsInRange(Transform playerRoot)
    {
        if (playerRoot == null)
            return false;

        float dist = Vector3.Distance(playerRoot.position, doorTransform.position);
        return dist <= interactionRadius;
    }

    public void Unlock()
    {
        if (!isLocked)
            return;

        isLocked = false;

        if (doorAnimator != null && !string.IsNullOrEmpty(openTriggerName))
            doorAnimator.SetTrigger(openTriggerName);

        onUnlocked?.Invoke();
    }

    public void Lock()
    {
        if (isLocked)
            return;

        isLocked = true;

        if (doorAnimator != null && !string.IsNullOrEmpty(closeTriggerName))
            doorAnimator.SetTrigger(closeTriggerName);

        onLocked?.Invoke();
    }
}
