using UnityEngine;

public class PuertaRotar : MonoBehaviour
{
    // Este script está en el HIJO (por ejemplo, Cube_10)
    public float anguloApertura = 90.0f;
    public float velocidadSuavizado = 2.0f;

    private Quaternion _rotacionCerrada;
    private Quaternion _rotacionAbiertaAfuera;
    private Quaternion _rotacionAbiertaAdentro;
    private bool _abrir = false;

    private PuertaTrigger trigger;

    void Start()
    {
        // Encuentra el script de Trigger en el padre
        trigger = GetComponentInParent<PuertaTrigger>();
        if (trigger == null)
        {
            Debug.LogError("No se encontró el script 'PuertaTrigger' en el padre!");
        }

        // Guarda la rotación LOCAL inicial (puerta cerrada)
        _rotacionCerrada = transform.localRotation;

        // Calcula las dos posibles aperturas (hacia afuera e hacia adentro)
        _rotacionAbiertaAfuera = _rotacionCerrada * Quaternion.Euler(0, anguloApertura, 0);
        _rotacionAbiertaAdentro = _rotacionCerrada * Quaternion.Euler(0, -anguloApertura, 0);
    }

    void Update()
    {
        _abrir = trigger.debeAbrirse;

        // Elegir la rotación objetivo según desde qué lado se entra
        Quaternion rotacionObjetivo;

        if (_abrir)
        {
            rotacionObjetivo = trigger.abrirHaciaAfuera ? _rotacionAbiertaAfuera : _rotacionAbiertaAdentro;
        }
        else
        {
            rotacionObjetivo = _rotacionCerrada;
        }

        // Interpolamos suavemente
        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            rotacionObjetivo,
            Time.deltaTime * velocidadSuavizado
        );
    }
}
