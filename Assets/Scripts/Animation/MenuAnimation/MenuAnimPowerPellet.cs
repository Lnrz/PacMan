using System.Collections;
using UnityEngine;

public class MenuAnimPowerPellet : MonoBehaviour
{
    [SerializeField] private MenuAnimationEndChannelSO menuAnimationEndChannel;
    [SerializeField] private float frequency = 5.0f;
    private SpriteRenderer rend;
    private Sprite sprite;

    private void Awake()
    {
        menuAnimationEndChannel.AddListener(OnMenuAnimationEnd);
        rend = gameObject.GetComponent<SpriteRenderer>();
        sprite = rend.sprite;
        StartCoroutine(BlinkingAnim());
    }

    private void OnMenuAnimationEnd()
    {
        rend.enabled = true;
    }

    private IEnumerator BlinkingAnim()
    {
        while (true)
        {
            rend.sprite = null;
            yield return new WaitForSeconds(1.0f / frequency);
            rend.sprite = sprite;
            yield return new WaitForSeconds(1.0f / frequency);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MenuAnimGhost[] ghosts;

        ghosts = transform.parent.GetComponentsInChildren<MenuAnimGhost>();
        foreach (MenuAnimGhost ghost in ghosts)
        {
            ghost.OnPowerPelletEaten();
        }
        Destroy(gameObject);
    }
}