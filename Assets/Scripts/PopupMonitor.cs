using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum PopupType
{
    NiceCatch,
    AThiefEscaped,
    WrongAccusation
}

public class PopupMonitor : MonoBehaviour
{
    [SerializeField] private GameObject goodCatchPopup;
    [SerializeField] private GameObject aThiefEscapedPopup;
    [SerializeField] private GameObject wrongAccusationPopup;
    private float popupDuration = 1;
    private float timer = 0;
    private Queue<PopupType> popups = new();

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer += popupDuration;
            if (popups.TryDequeue(out var popupType))
            {
                goodCatchPopup.SetActive(popupType == PopupType.NiceCatch);
                aThiefEscapedPopup.SetActive(popupType == PopupType.AThiefEscaped);
                wrongAccusationPopup.SetActive(popupType == PopupType.WrongAccusation);
            }
            else
            {
                goodCatchPopup.SetActive(false);
                aThiefEscapedPopup.SetActive(false);
                wrongAccusationPopup.SetActive(false);
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
