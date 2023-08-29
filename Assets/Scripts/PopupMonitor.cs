using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum PopupType
{
    GoodCatch,
    AThiefEscaped,
}

public class PopupMonitor : MonoBehaviour
{
    [SerializeField] private GameObject goodCatchPopup;
    [SerializeField] private GameObject aThiefEscapedPopup;
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
                goodCatchPopup.SetActive(popupType == PopupType.GoodCatch);
                aThiefEscapedPopup.SetActive(popupType == PopupType.AThiefEscaped);
            }
            else
            {
                goodCatchPopup.SetActive(false);
                aThiefEscapedPopup.SetActive(false);
            }
        }
    }

    public void ShowPopup(PopupType popupType)
    {
        popups.Enqueue(popupType);
    }
}
