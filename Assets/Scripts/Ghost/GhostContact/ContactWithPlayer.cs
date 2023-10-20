using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ContactWithPlayer : MonoBehaviour, EatenEventInvoker
{
    [SerializeField] private GhostEatenChannelSO ghostEatenChannel;
    private GhostContactState contactState = new IgnoreGhostContactState();
    private UnityEvent onEatenEvent = new UnityEvent();

    private void Awake()
    {
        if (TryGetComponent<ChangeStateEventInvoker>(out ChangeStateEventInvoker changeStateEventInvoker))
        {
            changeStateEventInvoker.OnChangeState(UpdateState);
        }
    }

    public void OnEaten(UnityAction listener)
    {
        onEatenEvent.AddListener(listener);
    }

    public void FireOnEatenEvent()
    {
        onEatenEvent.Invoke();
        ghostEatenChannel.Invoke();
    }

    private void UpdateState(GhostStateAbstractFactory factory)
    {
        contactState = factory.GetContactState();
        contactState.SetContext(this);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.TryGetComponent<PlayerMovement>(out PlayerMovement pm))
        {
            Vector2 myPos;
            Vector2 playerPos;
            float sqrdDist;

            myPos = transform.position;
            playerPos = go.transform.position;
            sqrdDist = (myPos - playerPos).sqrMagnitude;
            if (sqrdDist <= 0.0625f)
            {
                contactState.OnContactWithPlayer(go);
            }
        }
    }
}