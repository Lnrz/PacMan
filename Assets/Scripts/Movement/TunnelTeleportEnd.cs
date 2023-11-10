using UnityEngine;

public class TunnelTeleportEnd : MonoBehaviour
{
    [SerializeField] private TunnelTeleportEnd otherEnd;
    [SerializeField] private int exitDirection = -1;
    private Vector2 exitPosition;

    private void Awake()
    {
        exitPosition = (Vector2)transform.position + 2.0f * Utility.Int2Dir(exitDirection);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go;

        go = collision.gameObject;
        if (go.TryGetComponent<AbstractMovingEntity>(out AbstractMovingEntity me))
        {
            go.transform.position = otherEnd.GetExitPosition();
        }
    }

    public Vector2 GetExitPosition()
    {
        return exitPosition;
    }
}