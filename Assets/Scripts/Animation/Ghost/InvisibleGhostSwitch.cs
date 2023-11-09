using UnityEngine;

public class InvisibleGhostSwitch : MonoBehaviour
{
    [SerializeField] private InvisibleGhostsChannelSO invisibleGhostsChannel;
    private SpriteRenderer[] rends;

    private void Awake()
    {
        invisibleGhostsChannel.AddListener(OnInvisibleSwitch);
        rends = GetComponentsInChildren<SpriteRenderer>();
    }

    private void OnInvisibleSwitch(bool mode)
    {
        foreach (SpriteRenderer rend in rends)
        {
            rend.enabled = !mode;
        }
    }
}