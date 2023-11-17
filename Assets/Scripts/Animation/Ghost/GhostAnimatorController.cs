using UnityEngine;

public class GhostAnimatorController : MonoBehaviour, LookController
{
    [SerializeField] private GameStartChannelSO gameStartChannel;
    [SerializeField] private GameRestartChannelSO gameRestartChannel;
    [SerializeField] private PowerPelletChannelSO powerPelletChannel;
    [SerializeField] private PowerUpEndChannelSO powerUpEndChannel;
    [SerializeField] private BlinkingChannelSO blinkingChannel;
    [SerializeField] private StopEntitiesChannelSO stopEntitiesChannel;
    [SerializeField] private bool startOutside;
    private Animator anim;
    private int[] lookHashes = new int[4];
    private int gameStartHash;
    private int frightenedHash;
    private int blinkingHash;
    private int eatenHash;
    private int isInHomeHash;
    private bool isEaten = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        lookHashes[0] = Animator.StringToHash("LookUp");
        lookHashes[1] = Animator.StringToHash("LookRight");
        lookHashes[2] = Animator.StringToHash("LookDown");
        lookHashes[3] = Animator.StringToHash("LookLeft");
        gameStartHash = Animator.StringToHash("IsGameStarted");
        frightenedHash = Animator.StringToHash("IsFrightened");
        blinkingHash = Animator.StringToHash("IsBlinking");
        eatenHash = Animator.StringToHash("IsEaten");
        isInHomeHash = Animator.StringToHash("IsInHome");
        gameStartChannel.AddListener(OnGameStart);
        gameRestartChannel.AddListener(OnGameRestart);
        powerPelletChannel.AddListener(OnFrightened);
        blinkingChannel.AddListener(OnBlinking);
        powerUpEndChannel.AddListener(OnPowerUpEnd);
        stopEntitiesChannel.AddListener(OnStopEntities);
        if (TryGetComponent<EatenEventInvoker>(out EatenEventInvoker eatenEventInvoker))
        {
            eatenEventInvoker.OnEaten(OnEaten);
        }
        if (TryGetComponent<InsideHomeEventInvoker>(out InsideHomeEventInvoker insideHomeEventInvoker))
        {
            insideHomeEventInvoker.OnInsideHome(OnInsideHome);
        }
        if (TryGetComponent<ExitingHomeEventInvoker>(out ExitingHomeEventInvoker exitingHomeEventInvoker))
        {
            exitingHomeEventInvoker.OnExitingHome(OnExitingHome);
        }
        anim.SetBool(isInHomeHash, !startOutside);
    }

    public void Look(int dirIndex)
    {
        if (dirIndex < 0 || dirIndex > 3) return;
        anim.SetTrigger(lookHashes[dirIndex]);
    }

    private void OnGameStart()
    {
        anim.SetBool(gameStartHash, true);
    }

    private void OnGameRestart()
    {
        anim.SetBool(gameStartHash, false);
        anim.SetBool(isInHomeHash, !startOutside);
        anim.SetBool(frightenedHash, false);
        anim.SetBool(eatenHash, false);
        anim.SetTrigger(lookHashes[1]);
    }

    private void OnFrightened()
    {
        isEaten = false;
        anim.SetBool(frightenedHash, true);
        anim.ResetTrigger(blinkingHash);
    }

    private void OnPowerUpEnd()
    {
        anim.SetBool(frightenedHash, false);
    }

    private void OnEaten()
    {
        isEaten = true;
        anim.SetBool(frightenedHash, false);
        anim.SetBool(eatenHash, true);
    }

    private void OnInsideHome()
    {
        anim.SetBool(eatenHash, false);
    }

    private void OnBlinking()
    {
        if (!isEaten)
        {
            anim.SetTrigger(blinkingHash);
        }
    }

    private void OnExitingHome()
    {
        anim.SetBool(isInHomeHash, false);
    }

    private void OnStopEntities()
    {
        anim.SetBool(isInHomeHash, false);
    }
}