using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

enum ClickAction
{
    Accuse,
    StartGame,
}

public class ComputerCursor : MonoBehaviour
{
    [SerializeField] private List<GameObject> monitors;
    [SerializeField] private InputActionReference horizontalCursorMovement;
    [SerializeField] private InputActionReference verticalCursorMovement;
    [SerializeField] private InputActionReference cursorClick;
    [SerializeField] private float sensitivity = 1;
    [SerializeField] private NavMeshAgent agent;
    private List<GameObject> cursors;
    private Highlight highlightedClient;
    private List<Camera> cameras;

    private int rows = 2;
    private int cols = 4;
    private Vector2 monitorSize = new(0.404f, 0.262f);

    private Vector2 cursorPosition = Vector2.zero;
    [SerializeField] private GameStateManager gameStateManager;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        horizontalCursorMovement.action.performed += context =>
        {
            cursorPosition.x += context.ReadValue<float>() * sensitivity;
            UpdateCursorPosition();
        };
        verticalCursorMovement.action.performed += context =>
        {
            cursorPosition.y += context.ReadValue<float>() * sensitivity;
            UpdateCursorPosition();
        };
        SetClickAction(ClickAction.StartGame);

        cursors = monitors.Select(m => m.transform.Find("Screen/Canvas/Cursor").gameObject).ToList();
        cameras = monitors.Select(m =>
            m.GetComponentInChildren<CameraMonitor>().securityCamera.GetComponentInChildren<Camera>()).ToList();


        cursorPosition.Set(monitorSize.x * cols / 2, monitorSize.y * rows / 2);
        UpdateCursorPosition();
    }

    void AccuseOnCursor(InputAction.CallbackContext callbackContext)
    {
        ClientOnCursor()?.AccuseOfTheft();
    }

    void StartGame(InputAction.CallbackContext callbackContext)
    {
        gameStateManager.StartGame();
    }

    void SetClickAction(ClickAction clickAction)
    {
        switch (clickAction)
        {
            case ClickAction.Accuse:
                cursorClick.action.performed -= StartGame;
                cursorClick.action.performed += AccuseOnCursor;
                break;
            case ClickAction.StartGame:
                cursorClick.action.performed -= AccuseOnCursor;
                cursorClick.action.performed += StartGame;
                break;
        }
    }

    private void OnDisable()
    {
        gameStateManager.OnGameStart.RemoveListener(OnGameStart);
        gameStateManager.OnGameOver.RemoveListener(OnGameOver);
    }

    private void OnEnable()
    {
        gameStateManager.OnGameStart.AddListener(OnGameStart);
        gameStateManager.OnGameOver.AddListener(OnGameOver);
    }

    private void OnGameOver(GameStats stats)
    {
        SetClickAction(ClickAction.StartGame);
    }

    private void OnGameStart()
    {
        SetClickAction(ClickAction.Accuse);
    }

    Ray CursorRay()
    {
        var index = GetMonitorIndex();
        var pos = GetInMonitorPosition();
        var verticalMargin = (monitorSize.x - monitorSize.y) / 2;
        
        return cameras[index].ViewportPointToRay((pos + new Vector2(0, verticalMargin)) / monitorSize.x);
    }

    Client ClientOnCursor()
    {
        var ray = CursorRay();
        if (Physics.Raycast(ray, out var hitInfo) && hitInfo.transform.gameObject.CompareTag("Client"))
        {
            return hitInfo.transform.GetComponent<Client>();
        }
        return null;
    }

    int GetMonitorIndex()
    {
        int xIndex = math.clamp((int)math.floor(cursorPosition.x / monitorSize.x), 0, cols - 1);
        int yIndex = math.clamp((int)math.floor(cursorPosition.y / monitorSize.y), 0, rows - 1);
        return yIndex * cols + xIndex;
    }

    Vector2 GetInMonitorPosition()
    {
        Vector2 pos;
        pos.x = cursorPosition.x % monitorSize.x;
        pos.y = cursorPosition.y % monitorSize.y;
        return pos;
    }

    void UpdateCursorPosition()
    {
        cursorPosition.x = math.clamp(cursorPosition.x, 0, monitorSize.x * cols);
        cursorPosition.y = math.clamp(cursorPosition.y, 0, monitorSize.y * rows);

        foreach (var cursor in cursors)
        {
            cursor.SetActive(false);
        }

        int monitorIndex = GetMonitorIndex();
        cursors[monitorIndex].SetActive(true);
        var pos = GetInMonitorPosition();
        pos -= monitorSize / 2;
        cursors[monitorIndex].transform.localPosition = pos;
    }

    void Update()
    {
        var client = ClientOnCursor()?.GetComponent<Highlight>();

        if (highlightedClient)
            highlightedClient.SetHighlight(false);
        client?.SetHighlight(true);
        highlightedClient = client;
    }
}
