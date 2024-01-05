using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera mainCamera;

    private void Start()
    {
        // If no camera is specified, we use the main camera by default
        if(mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void LateUpdate()
    {
        // Make objects always camera-oriented
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }
}
