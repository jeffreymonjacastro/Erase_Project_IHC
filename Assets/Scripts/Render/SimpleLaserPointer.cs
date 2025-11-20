using UnityEngine;

public class SimpleLaserPointer : MonoBehaviour
{
    public LineRenderer line;

    void Start()
    {
        if (line == null)
        {
            line = gameObject.AddComponent<LineRenderer>();
            line.startWidth = 0.01f;
            line.endWidth = 0.005f;
            line.positionCount = 2;
            line.material = new Material(Shader.Find("Sprites/Default"));
            line.startColor = Color.white;
            line.endColor = Color.white;
        }
    }

    void Update()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position + transform.forward * 5f);
    }
}
