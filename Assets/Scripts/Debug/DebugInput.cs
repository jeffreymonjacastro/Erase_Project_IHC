using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.A))
            Debug.LogWarning("RAW A");
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
            Debug.LogWarning("RAW TRIGGER");
    }
}
