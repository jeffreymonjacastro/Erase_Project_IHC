using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class DoorProximityOpener : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DoorLock doorLock;
    [SerializeField] private DoorRotator doorRotator;

    [Header("Settings")]
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private float autoCloseDelay = 0.3f;

    private Transform player;
    private Coroutine closeRoutine;

    private void Reset()
    {
        // Ensure collider is a trigger
        Collider col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;

        if (doorLock == null)
            doorLock = GetComponentInParent<DoorLock>();

        if (doorRotator == null)
            doorRotator = GetComponentInParent<DoorRotator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        player = other.transform;

        // Determine open direction using dot product
        Vector3 toPlayer = player.position - doorRotator.transform.position;
        float dot = Vector3.Dot(doorRotator.transform.forward, toPlayer);

        bool shouldOpenOutwards = dot < 0;
        doorRotator.SetOpenDirection(shouldOpenOutwards);

        if (!doorLock.IsLocked)
        {
            doorLock.Open();
        }

        if (closeRoutine != null)
        {
            StopCoroutine(closeRoutine);
            closeRoutine = null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        player = null;

        if (doorLock != null && autoCloseDelay > 0f)
        {
            if (closeRoutine != null)
                StopCoroutine(closeRoutine);

            closeRoutine = StartCoroutine(AutoCloseAfterDelay());
        }
    }

    private IEnumerator AutoCloseAfterDelay()
    {
        yield return new WaitForSeconds(autoCloseDelay);

        if (doorLock != null && !doorLock.IsLocked)
        {
            doorLock.Close();
        }
    }
}
