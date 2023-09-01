using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScorePopup : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    
    void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    public void SetScore(GameStats gameStats)
    {
        var scoreString = new StringBuilder();
        scoreString.AppendLineFormat($"CATCHED: {gameStats.catchedThieves}");
        scoreString.AppendLineFormat($"ESCAPED: {gameStats.thiefEscapes}");
        scoreString.AppendLineFormat($"WRONG ACCUSATIONS: {gameStats.wronglyAccused}");
        scoreText.text = scoreString.ToString();
    }
}
