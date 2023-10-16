using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractFruit : MonoBehaviour
{
    [SerializeField] private int points = 5;
    [SerializeField] private FruitChannelSO fruitChannel;
    private float sqrdEatRange = 0.0625f;

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
                fruitChannel.Raise(points);
                OnPlayerDetection();
                gameObject.SetActive(false);
            }
        }
    }

    protected abstract void OnPlayerDetection();
}