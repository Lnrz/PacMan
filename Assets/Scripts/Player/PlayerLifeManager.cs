using UnityEngine;

public class PlayerLifeManager : MonoBehaviour
{
    [SerializeField] private LivesLeftUpdateChannelSO livesLeftUpdateChannel;
    [SerializeField] private GameStartChannelSO gameStartChannel;
    [SerializeField] private ScoreUpdateChannelSO scoreUpdateChannel;
    [SerializeField] private PlayerEatenChannelSO playerEatenChannel;
    [SerializeField] private AudioSource audioSrc;
    [SerializeField] private AudioClip extraLifeAudioClip;
    [SerializeField] private int livesLeft = 4;
    [SerializeField] private int pointsForBonusLife = 10000;
    private bool canBeEaten = false;

    private void Awake()
    {
        gameStartChannel.AddListener(OnGameStart);
        scoreUpdateChannel.AddListener(OnScoreUpdate);
    }

    private void Start()
    {
        livesLeftUpdateChannel.Invoke(livesLeft);
    }

    private void OnGameStart()
    {
        canBeEaten = true;
    }

    private void OnScoreUpdate(int points)
    {
        if (points >= pointsForBonusLife)
        {
            UpdateLivesLeft(1);
            scoreUpdateChannel.RemoveListener(OnScoreUpdate);
            audioSrc.PlayOneShot(extraLifeAudioClip);
        }
    }

    public void DecreaseLivesLeft()
    {
        if (canBeEaten)
        {
            canBeEaten = false;
            UpdateLivesLeft(-1);
            playerEatenChannel.Invoke();
        }
    }

    private void UpdateLivesLeft(int delta)
    {
        livesLeft += delta;
        livesLeftUpdateChannel.Invoke(livesLeft);
    }
}