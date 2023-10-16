using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ContactWithPlayer : MonoBehaviour, GhostStateManagerObserver
{
    private GhostContactState contactState;

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.TryGetComponent<PlayerMovement>(out PlayerMovement pm))
        {
            Vector2 myPos = transform.position;
            Vector2 playerPos = go.transform.position;
            float sqrdDist = (myPos - playerPos).sqrMagnitude;
            if (sqrdDist <= 1.0f / 16.0f)
            {
                contactState.OnContactWithPlayer(gameObject, go.gameObject);
            }
        }
    }

    public void UpdateState(GhostStateAbstractFactory factory)
    {
        contactState = factory.GetContactState();
    }
}