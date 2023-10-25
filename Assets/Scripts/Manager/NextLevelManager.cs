using UnityEngine;

public class NextLevelManager : MonoBehaviour
{
    [SerializeField] private DotChannelSO dotChannel;
    [SerializeField] private GameRestartChannelSO gameRestartChannel;
    [SerializeField] private NextLevelChannelSO nextLevelChannel;
    private int dotNum;
    private int dotEaten = 0;

    private void Awake()
    {
        dotNum = FindObjectsByType<Dot>(FindObjectsSortMode.None).Length;
        dotChannel.AddListener(DecreaseDotNum);
    }

    private void DecreaseDotNum()
    {
        dotEaten++;
        if (dotEaten == dotNum)
        {
            dotEaten = 0;
            gameRestartChannel.Invoke();
            nextLevelChannel.Invoke();
        }
    }
}