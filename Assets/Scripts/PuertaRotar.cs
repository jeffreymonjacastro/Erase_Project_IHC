using UnityEngine;

public class PuertaRotar : MonoBehaviour
{
    // Este script está en el HIJO (Cube_10)

    public float anguloApertura = 90.0f;
    public float velocidadSuavizado = 2.0f;

    private Quaternion _rotacionCerrada;
    private Quaternion _rotacionAbierta;
    private bool _abrir = false;

    // Necesitamos una referencia al padre para leer los Triggers
    private PuertaTrigger trigger;

    void Start()
    {
        // Encuentra el script de Trigger en el padre
        trigger = GetComponentInParent<PuertaTrigger>();
        if (trigger == null)
        {
            Debug.LogError("No se encontró el script 'PuertaTrigger' en el padre!");
        }

        // Guarda la rotación LOCAL inicial
        _rotacionCerrada = transform.localRotation;
        _rotacionAbierta = _rotacionCerrada * Quaternion.Euler(0, anguloApertura, 0);
    }

    void Update()
    {
        // Pregunta al script del padre si debe abrirse
        _abrir = trigger.debeAbrirse;

        Quaternion rotacionObjetivo = _abrir ? _rotacionAbierta : _rotacionCerrada;

        // Rota este objeto (Cube_10) sobre su propio eje local
        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            rotacionObjetivo,
            Time.deltaTime * velocidadSuavizado
        );
    }
}