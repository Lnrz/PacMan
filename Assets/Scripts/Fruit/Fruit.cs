using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private int points = 5;
    [SerializeField] private FruitChannelSO channel;
    private float eatRange = 0.25f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject go;

        go = collision.gameObject;
        if (go.TryGetComponent<PlayerMovement>(out PlayerMovement pm))
        {
            Vector2 myPos = gameObject.transform.position;
            Vector2 playerPos = go.transform.position;

            if (Vector2.Distance(myPos, playerPos) <= eatRange)
            {
                channel.Raise(points);
                Destroy(gameObject);
            }
        }
    }
}