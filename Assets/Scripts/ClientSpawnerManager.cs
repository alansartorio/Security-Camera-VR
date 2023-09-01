using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSpawnerManager : MonoBehaviour
{
    private GameStateManager gameStateManager;
    [SerializeField] private ClientSpawner clientSpawner;

    private void Awake()
    {
        gameStateManager = GetComponent<GameStateManager>();
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
        var clients = GameObject.FindGameObjectsWithTag("Client");
        foreach (var client in clients)
        {
            Destroy(client);
        }

        clientSpawner.enabled = false;
    }

    private void OnGameStart()
    {
        clientSpawner.enabled = true;
    }
}
