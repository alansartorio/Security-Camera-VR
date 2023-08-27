using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private InputActionReference horizontalLook;
    [SerializeField] private InputActionReference verticalLook;
    [SerializeField] private float lookSpeed = 1f;
    [SerializeField] private Transform cameraTransform;
    private float pitch = 0;
    private float yaw = 0;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        horizontalLook.action.performed += HandleHorizontalLookChange;
        verticalLook.action.performed += HandleVerticalLookChange;
    }

    void HandleHorizontalLookChange(InputAction.CallbackContext obj)
    {
        yaw += obj.ReadValue<float>() * lookSpeed;
        transform.localRotation = Quaternion.AngleAxis(yaw, Vector3.up);
    }

    void HandleVerticalLookChange(InputAction.CallbackContext obj)
    {
        pitch += obj.ReadValue<float>() * lookSpeed;
        float ninety_degrees = 90;
        if (pitch > ninety_degrees)
        {
            pitch = ninety_degrees;
        } else if (pitch < -ninety_degrees)
        {
            pitch = -ninety_degrees;
        }
        cameraTransform.localRotation = Quaternion.AngleAxis(pitch, Vector3.left);
    }
}
