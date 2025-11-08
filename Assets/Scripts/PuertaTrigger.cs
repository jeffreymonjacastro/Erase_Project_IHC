using UnityEngine;

public class PuertaTrigger : MonoBehaviour
{
    // Este script solo detecta al jugador y le dice al hijo qué hacer
    [HideInInspector] // Oculta la variable del inspector
    public bool debeAbrirse = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            debeAbrirse = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            debeAbrirse = false;
        }
    }
}