using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class InventoryFeedbackController : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip itemStoredClip;
    [SerializeField] private AudioClip itemEquippedClip;
    [SerializeField] private AudioClip inventoryToggleClip;
    [SerializeField] private AudioClip invalidActionClip;

    [Header("Haptics")]
    [SerializeField] private float storedHapticDuration = 0.15f;
    [SerializeField] private float storedHapticAmplitude = 0.5f;
    [SerializeField] private float storedHapticFrequency = 0.8f;

    [SerializeField] private float equipHapticDuration = 0.12f;
    [SerializeField] private float equipHapticAmplitude = 0.4f;
    [SerializeField] private float equipHapticFrequency = 0.9f;

    private void Awake()
    {
        if (audioSource == null)
        {
            Debug.LogError("[InventoryFeedbackController] Missing reference: audio source");
        }

        if (itemStoredClip == null)
        {
            Debug.LogError("[InventoryFeedbackController] Missing reference: item stored clip");
        }

        if (itemEquippedClip == null)
        {
            Debug.LogError("[InventoryFeedbackController] Missing reference: item equipped clip");
        }

        if (inventoryToggleClip == null)
        {
            Debug.LogError("[InventoryFeedbackController] Missing reference: inventory toggle clip");
        }

        if (inventoryToggleClip == null)
        {
            Debug.LogError("[InventoryFeedbackController] Missing reference: invalid action clip");
        }
    }

    public void PlayItemStoredFeedback()
    {
        audioSource.PlayOneShot(itemStoredClip);

        StartCoroutine(HapticPulse(storedHapticDuration, storedHapticAmplitude, storedHapticFrequency));
    }

    public void PlayItemEquippedFeedback()
    {
        audioSource.PlayOneShot(itemEquippedClip);

        StartCoroutine(HapticPulse(equipHapticDuration, equipHapticAmplitude, equipHapticFrequency));
    }

    public void PlayInventoryToggleFeedback()
    {
        audioSource.PlayOneShot(inventoryToggleClip);
    }
    public void PlayInvalidActionFeedback()
    {
        audioSource.PlayOneShot(invalidActionClip);
    }

    private IEnumerator HapticPulse(float duration, float amplitude, float frequency)
    {
        // Simple one-shot haptic pulse on right controller
        OVRInput.SetControllerVibration(frequency, amplitude, OVRInput.Controller.RTouch);
        yield return new WaitForSeconds(duration);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
    }
}
