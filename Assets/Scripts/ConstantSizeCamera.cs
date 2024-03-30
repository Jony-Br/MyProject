using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class ConstantSizeCamera : MonoBehaviour
{
    public float Width = 9f;  // Desired width 

    private Camera _camera;  // Camera component

    void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        // Calculate target height based on current width
        float targetHeight = Width / _camera.aspect;

        // Update camera size
        _camera.orthographicSize = targetHeight * 0.5f;
    }
}
