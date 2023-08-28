using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ClickToMove : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private InputActionReference interactInput;

    private void Start()
    {
        interactInput.action.performed += OnPointerClick;
    }

    void OnPointerClick(InputAction.CallbackContext obj)
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Debug.Log(ray);
        if (Physics.Raycast(ray, out var hitInfo))
        {
            agent.SetDestination(hitInfo.point);
        }
    }
}