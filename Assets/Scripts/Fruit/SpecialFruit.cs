using System.Collections;
using UnityEngine;

public class SpecialFruit : AbstractEatable
{
    [SerializeField] private PointsTextChannelSO pointsTextChannel;
    [SerializeField] private PlayerEatenChannelSO playerEatenChannel;
    [SerializeField] private GameStartChannelSO gameStartChannel;
    [SerializeField] private float secondsBeforeDestroy = 10.0f;
    private IEnumerator destroyCorout;

    private void Start()
    {
        playerEatenChannel.AddListener(OnPlayerEaten);
        gameStartChannel.AddListener(OnGameStart);
        destroyCorout = DestroyCoroutine();
        StartCoroutine(destroyCorout);
    }

    private IEnumerator DestroyCoroutine()
    {
        float time;

        time = 0;
        while (time < secondsBeforeDestroy)
        {
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }

    private void OnPlayerEaten()
    {
        StopCoroutine(destroyCorout);
    }

    private void OnGameStart()
    {
        StartCoroutine(destroyCorout);
    }

    protected override void OnPlayerDetection()
    {
        pointsTextChannel.Invoke(GetPoints(), transform.position);
        Destroy(gameObject);
    }

    protected override void OnNextLevel()
    {
        Destroy(gameObject);
    }
}