using UnityEngine;

public class BGMAudioManager : MonoBehaviour
{
    [SerializeField] private GameStartChannelSO gameStartChannel;
    [SerializeField] private DotChannelSO dotChannel;
    [SerializeField] private PowerPelletChannelSO powerPelletChannel;
    [SerializeField] private PowerUpEndChannelSO powerUpEndChannel;
    [SerializeField] private GhostEatenChannelSO ghostEatenChannel;
    [SerializeField] private GhostEnteredHomeChannelSO ghostEnteredHomeChannel;
    [SerializeField] private PlayerEatenChannelSO playerEatenChannel;
    [SerializeField] private ResetBGMChannelSO resetBGMChannel;
    [SerializeField] private AudioSource audioSrc;
    [SerializeField] private AudioClip[] BGMs;
    [SerializeField] private int[] dotsForBGMProgression;
    [SerializeField] private AudioClip pacmanPoweredBGM;
    [SerializeField] private AudioClip ghostGoingHomeBGM;
    private int dotEaten = 0;
    private int BGMIndex = 0;
    private int ghostsGoingHomeNum = 0;
    private bool isWaitingStart = true;
    private bool isPowerUpActive = false;
    private bool isAGhostGoingHome = false;

    private void Awake()
    {
        gameStartChannel.AddListener(OnGameStart);
        dotChannel.AddListener(OnDotEaten);
        powerPelletChannel.AddListener(OnPowerPelletEaten);
        powerUpEndChannel.AddListener(OnPowerUpEnd);
        ghostEatenChannel.AddListener(OnGhostEaten);
        ghostEnteredHomeChannel.AddListener(OnGhostEnteredHome);
        playerEatenChannel.AddListener(OnPlayerEaten);
        resetBGMChannel.AddListener(OnResetBGM);
    }

    private void OnGameStart()
    {
        isWaitingStart = false;
        audioSrc.clip = BGMs[BGMIndex];
        audioSrc.Play();
    }

    private void OnDotEaten()
    {
        dotEaten++;
        if (BGMIndex < dotsForBGMProgression.Length &&
            dotEaten >= dotsForBGMProgression[BGMIndex])
        {
            BGMIndex++;
            if (!isPowerUpActive && !isAGhostGoingHome)
            {
                audioSrc.Stop();
                audioSrc.clip = BGMs[BGMIndex];
                audioSrc.Play();
            }
        }
    }

    private void OnPowerPelletEaten()
    {
        isPowerUpActive = true;
        if (isAGhostGoingHome) return;
        audioSrc.Stop();
        audioSrc.clip = pacmanPoweredBGM;
        audioSrc.Play();
    }

    private void OnPowerUpEnd()
    {
        isPowerUpActive = false;
        if (isAGhostGoingHome || isWaitingStart) return;
        audioSrc.Stop();
        audioSrc.clip = BGMs[BGMIndex];
        audioSrc.Play();
    }

    private void OnGhostEaten(Vector3 pos)
    {
        ghostsGoingHomeNum++;
        isAGhostGoingHome = true;
        audioSrc.Stop();
        audioSrc.clip = ghostGoingHomeBGM;
        audioSrc.Play();
    }

    private void OnGhostEnteredHome()
    {
        ghostsGoingHomeNum--;
        isAGhostGoingHome = ghostsGoingHomeNum != 0;
        if (!isAGhostGoingHome)
        {
            audioSrc.Stop();
            audioSrc.clip = isPowerUpActive ? pacmanPoweredBGM : BGMs[BGMIndex];
            audioSrc.Play();
        }
    }

    private void OnPlayerEaten()
    {
        isWaitingStart = true;
        audioSrc.Stop();
        audioSrc.clip = null;
        isPowerUpActive = false;
        isAGhostGoingHome = false;
        ghostsGoingHomeNum = 0;
    }

    private void OnResetBGM()
    {
        isWaitingStart = true;
        audioSrc.Stop();
        audioSrc.clip = null;
        isPowerUpActive = false;
        isAGhostGoingHome = false;
        ghostsGoingHomeNum = 0;
        dotEaten = 0;
        BGMIndex = 0;
    }
}