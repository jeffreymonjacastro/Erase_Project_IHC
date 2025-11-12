using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [Header("UI")]
    public CanvasGroup menuGroup;
    public Button resumeButton;
    public OVRCameraRig cameraRig; // assign in Inspector

    bool isPaused;

    void Awake()
    {
        resumeButton.onClick.AddListener(Resume);
        HideMenuImmediate();
    }

    void Update()
    {
        // Example: use Oculus Start/Menu button to toggle pause
        if (OVRInput.GetDown(OVRInput.Button.Start))
            TogglePause();
    }

    public void TogglePause()
    {
        if (isPaused) Resume();
        else Pause();
    }

    void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        AudioListener.pause = true;
        ShowMenu();
    }

    void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
        HideMenuImmediate();
    }

    void ShowMenu()
    {
        // Reposition menu ~0.7 m in front of head each time
        Transform eye = cameraRig.centerEyeAnchor;
        Vector3 forward = eye.forward; forward.y = 0; forward.Normalize();
        menuGroup.transform.position = eye.position + forward * 0.7f;
        menuGroup.transform.rotation = Quaternion.LookRotation(forward, Vector3.up);

        menuGroup.alpha = 1;
        menuGroup.interactable = true;
        menuGroup.blocksRaycasts = true;
    }

    void HideMenuImmediate()
    {
        menuGroup.alpha = 0;
        menuGroup.interactable = false;
        menuGroup.blocksRaycasts = false;
    }
}
