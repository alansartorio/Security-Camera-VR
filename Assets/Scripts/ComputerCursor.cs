using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class ComputerCursor : MonoBehaviour
{
    [SerializeField] private List<GameObject> monitors;
    [SerializeField] private InputActionReference horizontalCursorMovement;
    [SerializeField] private InputActionReference verticalCursorMovement;
    [SerializeField] private InputActionReference cursorClick;
    [SerializeField] private float sensitivity = 1;
    [SerializeField] private NavMeshAgent agent;
    private List<GameObject> cursors;

    private Vector2 monitorSize = new(0.404f, 0.262f);

    private Vector2 cursorPosition = Vector2.zero;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        horizontalCursorMovement.action.performed += context =>
        {
            cursorPosition.x += context.ReadValue<float>() * sensitivity;
            UpdatedCursorPosition();
        };
        verticalCursorMovement.action.performed += context =>
        {
            cursorPosition.y += context.ReadValue<float>() * sensitivity;
            UpdatedCursorPosition();
        };
        cursorClick.action.performed += context =>
        {
            var index = GetMonitorIndex();
            var pos = GetInMonitorPosition();
            var cameraMonitor = monitors[index].GetComponentInChildren<CameraMonitor>();
            var camera = cameraMonitor.securityCamera.GetComponentInChildren<Camera>();
            Ray ray = camera.ViewportPointToRay(pos / monitorSize);
            if (Physics.Raycast(ray, out var hitInfo))
            {
                agent.SetDestination(hitInfo.point);
            }
        };

        cursors = monitors.Select(m => m.transform.Find("Screen").Find("Canvas").Find("Cursor").gameObject).ToList();

        cursorPosition.Set(monitorSize.x * monitors.Count / 2, monitorSize.y / 2);
        UpdatedCursorPosition();
    }

    int GetMonitorIndex()
    {
        
        return math.clamp((int)math.floor(cursorPosition.x / monitorSize.x), 0, cursors.Count - 1);
    }

    Vector2 GetInMonitorPosition()
    {
        Vector2 pos;
        pos.x = cursorPosition.x % monitorSize.x;
        pos.y = cursorPosition.y;
        return pos;
    }

    void UpdatedCursorPosition()
    {
        cursorPosition.x = math.clamp(cursorPosition.x, 0, monitorSize.x * monitors.Count);
        cursorPosition.y = math.clamp(cursorPosition.y, 0, monitorSize.y);

        foreach (var cursor in cursors)
        {
            cursor.SetActive(false);
        }

        int monitorIndex = GetMonitorIndex();
        cursors[monitorIndex].SetActive(true);
        var pos = GetInMonitorPosition();
        pos -= monitorSize / 2;
        cursors[monitorIndex].transform.localPosition = pos;
        // cursors[monitorIndex].transform.localPosition.y
    }

    // Update is called once per frame
    void Update()
    {
    }
}