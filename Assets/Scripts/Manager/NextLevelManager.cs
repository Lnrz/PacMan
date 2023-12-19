using System.Collections;
using UnityEngine;

public class NextLevelManager : MonoBehaviour
{
    [SerializeField] private DotChannelSO dotChannel;
    [SerializeField] private ResetBGMChannelSO resetBGMChannel;
    [SerializeField] private StopEntitiesChannelSO stopEntitiesChannel;
    [SerializeField] private InvisibleGhostsChannelSO invisibleGhostsChannel;
    [SerializeField] private GameRestartChannelSO gameRestartChannel;
    [SerializeField] private NextLevelChannelSO nextLevelChannel;
    [SerializeField] private Material wallMaterial;
    [SerializeField] private float waitBeforeFlashing = 2.0f;
    [SerializeField] private float flashingDuration = 2.0f;
    private int dotNum;
    private int dotEaten = 0;
    private int flashingHash;

    private void Awake()
    {
        dotNum = FindObjectsByType<Dot>(FindObjectsSortMode.None).Length;
        dotChannel.AddListener(OnDotEaten);
        flashingHash = Shader.PropertyToID("_IsFlashing");
    }

    private void OnDotEaten()
    {
        dotEaten++;
        if (dotEaten == dotNum)
        {
            dotEaten = 0;
            StartCoroutine(NextLevelAnimation());
        }
    }

    private IEnumerator NextLevelAnimation()
    {
        resetBGMChannel.Invoke();
        stopEntitiesChannel.Invoke();
        yield return new WaitForSeconds(waitBeforeFlashing);
        invisibleGhostsChannel.Invoke(true);
        wallMaterial.SetInteger(flashingHash, 1);
        yield return new WaitForSeconds(flashingDuration);
        wallMaterial.SetInteger(flashingHash, 0);
        invisibleGhostsChannel.Invoke(false);
        gameRestartChannel.Invoke();
        nextLevelChannel.Invoke();
    }
}