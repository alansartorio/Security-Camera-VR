using System;
using UnityEngine;

public class AmbienceController : MonoBehaviour
{
    private GameStateManager gameStateManager;
    private AudioSource audioSource;
    private readonly float maxMusicVolume = 0.05f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.mute = true;
        audioSource.volume = maxMusicVolume;
    }

    private void OnEnable()
    {
        gameStateManager = GameObject.FindWithTag("GameManager").GetComponent<GameStateManager>();
        gameStateManager.OnGameOver.AddListener(OnGameOver);
        gameStateManager.OnGameStart.AddListener(OnGameStart);
    }

    private void OnDisable()
    {
        gameStateManager.OnGameOver.RemoveListener(OnGameOver);
        gameStateManager.OnGameStart.RemoveListener(OnGameStart);
    }

    private void Update()
    {
        var clientCount = GameObject.FindGameObjectsWithTag("Client").Length;
        audioSource.volume = (float)clientCount / 8;
    }

    private void OnGameStart()
    {
        audioSource.mute = false;
        audioSource.volume = 0;
    }

    private void OnGameOver(GameStats gameStats)
    {
        audioSource.mute = true;
    }
}
