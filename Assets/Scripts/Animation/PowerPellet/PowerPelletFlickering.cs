using System.Collections;
using UnityEngine;

public class PowerPelletFlickering : MonoBehaviour
{
    [SerializeField] private GameStartChannelSO gameStartChannel;
    [SerializeField] private GameRestartChannelSO gameRestartChannel;
    [SerializeField] private float frequency = 5.0f;
    private SpriteRenderer rend;
    private Sprite sprite;
    private IEnumerator flickeringCorout;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        sprite = rend.sprite;
        flickeringCorout = Flickering();
        gameStartChannel.AddListener(OnGameStart);
        gameRestartChannel.AddListener(OnGameRestart);
    }

    private void OnGameStart()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(flickeringCorout);
        }
    }

    private void OnGameRestart()
    {
        StopCoroutine(flickeringCorout);
    }

    private IEnumerator Flickering()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f / frequency);
            rend.sprite = null;
            yield return new WaitForSeconds(1.0f / frequency);
            rend.sprite = sprite;
        }
    }
}