 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CamControlTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public class CustomInspectorObjects{
    public bool swapCameras = false;
    public bool panCameraOnContact;
    [HideInInspector] public CinemachineVirtualCamera cameraOnLeft;
    [HideInInspector] public CinemachineVirtualCamera cameraOnRight;
    [HideInInspector] public PanDirection panDirection;
    [HideInInspector] public float panDistance = 3f;
    [HideInInspector] public float panTime = 0.35f;
    public enum PanDirection{
        Up,
        Down,
        Left,
        Right
    }
}