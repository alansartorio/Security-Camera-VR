using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SanctionsLeftText : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private GameStateManager gameStateManager;

    void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        gameStateManager = GameObject.FindWithTag("GameManager").GetComponent<GameStateManager>();
    }

    public void OnEnable()
    {
        int sanctionsLeft = gameStateManager.toleratedSanctions - gameStateManager.SanctionCount;
        scoreText.text = $"{sanctionsLeft} SANCTIONS LEFT";
    }
}