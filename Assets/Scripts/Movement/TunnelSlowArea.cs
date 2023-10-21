using UnityEngine;

public class TunnelSlowArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go;

        go = collision.gameObject;
        if (go.TryGetComponent<GhostMovement>(out GhostMovement gm))
        {
            gm.ChangeToTunnelSpeedMod();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject go;

        go = collision.gameObject;
        if (go.TryGetComponent<GhostMovement>(out GhostMovement gm))
        {
            gm.RestoreSpeedMod();
        }
    }
}