using UnityEngine;

public class MusicController : MonoBehaviour
{
    private GameStateManager gameStateManager;
    private AudioSource audioSource;
    private readonly float maxMusicVolume = 0.05f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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

    private void OnGameStart()
    {
        var fade= AudioFadeScript.FadeOut(audioSource, 2);
        StartCoroutine(fade);
    }

    private void OnGameOver(GameStats gameStats)
    {
        var fade= AudioFadeScript.FadeIn(audioSource, 2, maxMusicVolume);
        StartCoroutine(fade);
    }
}
