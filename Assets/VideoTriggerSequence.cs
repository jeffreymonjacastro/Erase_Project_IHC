using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Collections;

public class VideoTriggerSequence : MonoBehaviour
{
    [Header("--- Configuración ---")]
    [Tooltip("Arrastra aquí el objeto que tiene el componente Video Player")]
    public VideoPlayer videoPlayer;

    [Tooltip("Arrastra aquí el Panel Negro que tiene el componente Canvas Group")]
    public CanvasGroup blackFadeCanvasGroup;

    [Tooltip("Tiempo que tarda en oscurecerse/aclararse la pantalla")]
    public float fadeDuration = 1.0f;

    private bool hasPlayed = false;

    private void Start()
    {
        // 1. Configuración inicial de la cortina negra
        if (blackFadeCanvasGroup != null)
        {
            blackFadeCanvasGroup.alpha = 0;
        }

        // 2. CORRECCIÓN AUTOMÁTICA DE CÁMARA
        // Si se te olvidó poner la cámara en el inspector, el script busca la que tenga el tag MainCamera
        if (videoPlayer.targetCamera == null)
        {
            if (Camera.main != null)
            {
                videoPlayer.targetCamera = Camera.main;
                Debug.Log("¡Cámara asignada automáticamente gracias al tag MainCamera! Toma esa, Ian.");
            }
            else
            {
                Debug.LogError("¡Amigue! No encuentro ninguna cámara con el tag 'MainCamera'. ¡Revísalo!");
            }
        }

        // 3. Preparar el evento de fin de video
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            hasPlayed = true;
            StartCoroutine(PlayCinematicSequence());
        }
    }

    IEnumerator PlayCinematicSequence()
    {
        // --- PASO 1: OSCURECER ---
        yield return StartCoroutine(FadeCanvasGroup(blackFadeCanvasGroup, 0f, 1f, fadeDuration));

        // --- PASO 2: REPRODUCIR VIDEO ---
        videoPlayer.Play();

        while (videoPlayer.isPlaying)
        {
            yield return null;
        }

        // --- PASO 3: ACLARAR ---
        yield return StartCoroutine(FadeCanvasGroup(blackFadeCanvasGroup, 1f, 0f, fadeDuration));
    }

    IEnumerator FadeCanvasGroup(CanvasGroup cg, float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            yield return null;
        }
        cg.alpha = endAlpha;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        videoPlayer.Stop();
    }
}