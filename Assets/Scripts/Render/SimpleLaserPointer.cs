using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SimpleLaserPointer : MonoBehaviour
{
    [Tooltip("How far the laser goes in front of the controller.")]
    [SerializeField] private float length = 2f;
    [SerializeField] private bool startEnabled = false;

    private LineRenderer line;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();

        // basic setup
        line.useWorldSpace = true;
        line.positionCount = 2;

        // nice thin beam
        line.startWidth = 0.005f;
        line.endWidth = 0.005f;

        // simple material so it isn't magenta
        if (line.material == null)
        {
            line.material = new Material(Shader.Find("Unlit/Color"));
            line.material.color = Color.white;
        }

        line.enabled = startEnabled;
    }

    private void Update()
    {
        if (!line.enabled) return;
     
        Vector3 start = transform.position;
        Vector3 end = transform.position + transform.forward * length;

        line.SetPosition(0, start);
        line.SetPosition(1, end);
    }

    public void SetActive(bool active)
    {
        line.enabled = active;
    }
}
