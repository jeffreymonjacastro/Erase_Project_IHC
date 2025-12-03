using UnityEngine;

public class DoorRotator : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float openAngle = 90.0f;
    [SerializeField] private float smoothSpeed = 2.0f;
    [SerializeField] private DoorLock doorLock;

    private Quaternion closedRotation;
    private Quaternion openRotationOutwards;
    private Quaternion openRotationInwards;

    private bool openOutwards = true;

    private void Awake()
    {
        if (doorLock == null)
        {
            doorLock = GetComponentInParent<DoorLock>();
            if (doorLock == null)
            {
                Debug.LogError("DoorRotator: No DoorLock found in parent hierarchy.");
            }
        }

        // Save local closed rotation
        closedRotation = transform.localRotation;

        // Precompute open rotations
        openRotationOutwards = closedRotation * Quaternion.Euler(0, openAngle, 0);
        openRotationInwards = closedRotation * Quaternion.Euler(0, -openAngle, 0);
    }

    /// <summary>
    /// Called by the proximity script to determine which way the door opens.
    /// </summary>
    public void SetOpenDirection(bool outward)
    {
        openOutwards = outward;
    }

    private void Update()
    {
        if (doorLock == null) return;

        Quaternion targetRotation = closedRotation;

        if (doorLock.IsOpen)
        {
            targetRotation = openOutwards ? openRotationOutwards : openRotationInwards;
        }

        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            targetRotation,
            Time.deltaTime * smoothSpeed
        );
    }
}
