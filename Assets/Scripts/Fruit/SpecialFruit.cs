using System.Collections;
using UnityEngine;

public class SpecialFruit : AbstractEatable
{
    [SerializeField] private float secondsBeforeDestroy = 10.0f;

    private void Start()
    {
        IEnumerator destroyCorout;

        destroyCorout = DestroyCoroutine();
        StartCoroutine(destroyCorout);
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(secondsBeforeDestroy);
        Destroy(gameObject);
    }

    protected override void OnPlayerDetection()
    {
        Destroy(gameObject);
    }

    protected override void OnNextLevel()
    {
        Destroy(gameObject);
    }
}