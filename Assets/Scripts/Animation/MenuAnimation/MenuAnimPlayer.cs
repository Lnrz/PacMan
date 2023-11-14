using System.Collections;
using UnityEngine;

public class MenuAnimPlayer : MonoBehaviour
{
    [SerializeField] private MenuAnimationEndChannelSO menuAnimationEndChannel;
    [SerializeField] private Sprite[] sprites;
    private bool canMove = false;
    private bool directionAlreadyChanged = false;
    private Vector3 direction = Vector3.left;
    private float movingAnimInterval = 0.1f;
    private float speedMod = 1.5f;
    private float xBound = 5.0f;
    private SpriteRenderer rend;

    private void Awake()
    {
        menuAnimationEndChannel.AddListener(OnMenuAnimationEnd);
        rend = GetComponent<SpriteRenderer>();
        StartCoroutine(MovingAnimation());
    }

    private void Update()
    {
        if (canMove)
        {
            transform.position = transform.position + Time.deltaTime * speedMod * direction;
        }
        if (transform.position.x > xBound)
        {
            Destroy(gameObject);
        }
    }

    private void OnMenuAnimationEnd()
    {
        canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!directionAlreadyChanged)
        {
            directionAlreadyChanged = true;
            direction = Vector3.right;
            transform.rotation = Quaternion.identity;
        }
    }

    private IEnumerator MovingAnimation()
    {
        while (true)
        {
            rend.sprite = sprites[1];
            yield return new WaitForSeconds(movingAnimInterval);
            rend.sprite = sprites[2];
            yield return new WaitForSeconds(movingAnimInterval);
            rend.sprite = sprites[0];
            yield return new WaitForSeconds(movingAnimInterval);
        }
    }
}