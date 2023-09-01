using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum PopupType
{
    None,
    NiceCatch,
    AThiefEscaped,
    WrongAccusation
}

public class PopupMonitor : MonoBehaviour
{
    [SerializeField] private GameObject goodCatchPopup;
    [SerializeField] private GameObject aThiefEscapedPopup;
    [SerializeField] private GameObject wrongAccusationPopup;
    [SerializeField] private GameObject gameOverPopup;
    [SerializeField] private GameObject startPopup;
    private float popupDuration = 1;
    private float timer = 0;
    private Queue<PopupType> popups = new();
    private GameStateManager gameStateManager;

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

    private void OnGameStart()
    {
        gameOverPopup.SetActive(false);
        startPopup.SetActive(false);
        timer = 0;
        popups.Clear();
    }

    private void OnGameOver(GameStats gameStats)
    {
        popups.Clear();
        SetActivePopup(PopupType.None);
        gameOverPopup.SetActive(true);
        gameOverPopup.GetComponentInChildren<ScorePopup>().SetScore(gameStats);
    }

    void SetActivePopup(PopupType popupType)
    {
        goodCatchPopup.SetActive(popupType == PopupType.NiceCatch);
        aThiefEscapedPopup.SetActive(popupType == PopupType.AThiefEscaped);
        wrongAccusationPopup.SetActive(popupType == PopupType.WrongAccusation);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer += popupDuration;
            if (popups.TryDequeue(out var popupType))
            {
                SetActivePopup(popupType);
            }
            else
            {
                SetActivePopup(PopupType.None);
            }
        }
    }

    public void ShowPopup(PopupType popupType)
    {
        if (popups.Count == 0)
            timer = 0;
        popups.Enqueue(popupType);
    }
}
