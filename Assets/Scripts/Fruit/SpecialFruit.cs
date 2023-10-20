using System.Collections;
using UnityEngine;

public class SpecialFruit : AbstractPickable
{
    [SerializeField] private float secondsBeforeDestroy = 1.0f;

    private void Awake()
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
}
