using System.Collections;
using TMPro;
using UnityEngine;

public class MenuAnimGhost : MonoBehaviour
{
    [SerializeField] private MenuAnimationEndChannelSO menuAnimationEndChannel;
    [SerializeField] private SpriteRenderer bodyRend;
    [SerializeField] private SpriteRenderer eyesRend;
    [SerializeField] private Sprite[] bodySprites;
    [SerializeField] private GameObject pointsTextPrefab;
    [SerializeField] private Color frightenedColor;
    [SerializeField] private Sprite frightenedEyes;
    [SerializeField] private int pointsOnEaten;
    private bool canMove = false;
    private Vector3 direction = Vector3.left;
    private float movingAnimInterval = 0.1f;
    private float speedMod = 1.5f;

    private void Awake()
    {
        menuAnimationEndChannel.AddListener(OnMenuAnimationEnd);
        StartCoroutine(MovingAnimation());
    }

    private void Update()
    {
        if (canMove)
        {
            transform.position = transform.position + Time.deltaTime * speedMod * direction;
        }
    }

    private void OnMenuAnimationEnd()
    {
        canMove = true;
    }

    private IEnumerator MovingAnimation()
    {
        while (true)
        {
            bodyRend.sprite = bodySprites[1];
            yield return new WaitForSeconds(movingAnimInterval);
            bodyRend.sprite = bodySprites[2];
            yield return new WaitForSeconds(movingAnimInterval);
            bodyRend.sprite = bodySprites[0];
            yield return new WaitForSeconds(movingAnimInterval);
        }
    }

    public void OnPowerPelletEaten()
    {
        speedMod = 0.9f;
        direction = Vector3.right;
        eyesRend.sprite = frightenedEyes;
        bodyRend.color = frightenedColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go;
        TMP_Text text;

        go = Instantiate(pointsTextPrefab, transform.position, Quaternion.identity);
        text = go.GetComponent<TMP_Text>();
        text.text = pointsOnEaten.ToString();
        text.fontSize = 2.25f;
        gameObject.SetActive(false);
        Destroy(go, 2.0f);
        Destroy (gameObject, 2.0f);
    }
}