using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct GameStats
{
    public int wronglyAccused;
    public int catchedThieves;
    public int thiefEscapes;
}

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private List<Light> lights;
    private GameStats gameStats;
    [SerializeField] private PopupMonitor popupMonitor;
    public UnityEvent<GameStats> OnGameOver;
    public UnityEvent OnGameStart;
    public int toleratedSanctions;
    private int sanctionCount;
    
    void Start()
    {
        Reset();
    }

    public void IncrementWronglyAccused()
    {
        gameStats.wronglyAccused++;
    }

    public void IncrementCatchedThieves()
    {
        gameStats.catchedThieves++;
    }

    public void IncrementThiefEscapes()
    {
        gameStats.thiefEscapes++;
    }

    public void Reset()
    {
        gameStats.wronglyAccused = 0;
        gameStats.catchedThieves = 0;
        gameStats.thiefEscapes = 0;
        sanctionCount = 0;
    }

    public void Sanction()
    {
        sanctionCount++;
        if (sanctionCount >= toleratedSanctions)
        {
            GameOver();
        }
    }

    public void StartGame()
    {
        Reset();
        SetLightsEnabled(true);
        OnGameStart.Invoke();
    }

    public void GameOver()
    {
        OnGameOver.Invoke(gameStats);
        SetLightsEnabled(false);
    }

    private void SetLightsEnabled(bool enabled)
    {
        foreach (var light in lights)
        {
            light.enabled = enabled;
        }
    }
}
