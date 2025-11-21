using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SimpleLaserPointer : MonoBehaviour
{
    [Tooltip("How far the laser goes in front of the controller.")]
    public float length = 5f;

    private LineRenderer line;

    private bool active;

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

        active = false;
        line.enabled = active;
    }

    private void Update()
    {
        if (active)
        {
            Vector3 start = transform.position;
            Vector3 end = transform.position + transform.forward * length;

            line.SetPosition(0, start);
            line.SetPosition(1, end);
        }
    }

    public void SetActive(bool active)
    {
        this.active = active;
        line.enabled = active;
    }
}
