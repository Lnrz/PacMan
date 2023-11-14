using UnityEngine;

public abstract class AbstractEatable : MonoBehaviour
{
    [SerializeField] private int points = 5;
    [SerializeField] private PointsChannelSO pointsChannel;
    [SerializeField] private NextLevelChannelSO nextLevelChannel;
    [SerializeField] private float eatingTime = 0.017f;
    [SerializeField] private float eatingSpeedReduction = 0.2f;
    private float sqrdEatRange = 0.0625f;

    protected void Awake()
    {
        nextLevelChannel.AddListener(OnNextLevel);
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        GameObject go;

        go = collision.gameObject;
        if (go.TryGetComponent<PlayerMovement>(out PlayerMovement pm))
        {
            Vector2 myPos;
            Vector2 playerPos;
            float sqrdDist;

            myPos = gameObject.transform.position;
            playerPos = go.transform.position;
            sqrdDist = (myPos - playerPos).sqrMagnitude;
            if (sqrdDist <= sqrdEatRange)
            {
                pointsChannel.Invoke(points);
                pm.OnEating(eatingTime, eatingSpeedReduction);
                gameObject.SetActive(false);
                OnPlayerDetection();
            }
        }
    }

    protected int GetPoints()
    {
        return points;
    }

    protected abstract void OnPlayerDetection();

    protected abstract void OnNextLevel();
}