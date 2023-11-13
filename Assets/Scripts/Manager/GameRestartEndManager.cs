using System.Collections;
using UnityEngine;

public class GameRestartEndManager : MonoBehaviour
{
    [SerializeField] private LivesLeftUpdateChannelSO livesLeftUpdateChannel;
    [SerializeField] private PlayerEatenChannelSO playerEatenChannel;
    [SerializeField] private StopEntitiesChannelSO stopEntitiesChannel;
    [SerializeField] private InvisibleGhostsChannelSO invisibleGhostsChannel;
    [SerializeField] private GameRestartChannelSO gameRestartChannel;
    [SerializeField] private GameEndChannelSO gameEndChannel;
    [SerializeField] private Material playerMaterial;
    [SerializeField] private float waitBeforeDeath = 2.0f;
    [SerializeField] private float deathDuration = 1.5f;
    [SerializeField] private float waitAfterDeath = 2.0f;
    [SerializeField] private float waitBeforeMenu = 2.0f;
    private int livesLeft;
    private int dyingProgressHash;

    private void Awake()
    {
        livesLeftUpdateChannel.AddListener(OnLifesLeftUpdate);
        playerEatenChannel.AddListener(OnPlayerEaten);
        dyingProgressHash = Shader.PropertyToID("_MinAngle");
    }

    private void OnLifesLeftUpdate(int livesLeft)
    {
        this.livesLeft = livesLeft;
    }

    private void OnPlayerEaten()
    {
        StartCoroutine(GameRestartEndAnimation());
    }

    private IEnumerator GameRestartEndAnimation()
    {
        float time;
        float dyingProgress;

        stopEntitiesChannel.Invoke();
        yield return new WaitForSeconds(waitBeforeDeath);
        invisibleGhostsChannel.Invoke(true);
        time = 0;
        while (time < 1)
        {
            dyingProgress = Mathf.Lerp(0.0f, 3.2f, time);
            playerMaterial.SetFloat(dyingProgressHash, dyingProgress);
            time += Time.deltaTime / deathDuration;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(waitAfterDeath);
        if (livesLeft >= 0)
        {
            invisibleGhostsChannel.Invoke(false);
            playerMaterial.SetFloat(dyingProgressHash, 0.0f);
            gameRestartChannel.Invoke();
        }
        else
        {
            gameEndChannel.Invoke();
            yield return new WaitForSeconds(waitBeforeMenu);
            playerMaterial.SetFloat(dyingProgressHash, 0.0f);
            Utility.LoadScene("MenuScene", false);
            Utility.UnloadScene("GameboardScene");
        }
    }
}