using UnityEngine;

public class EatablesAudioManager : MonoBehaviour
{
    [SerializeField] private DotPowerPelletEatenChannelSO dotPowerPelletEatenChannel;
    [SerializeField] private SpecialFruitEatenChannelSO specialFruitEatenChannel;
    [SerializeField] private AudioSource audioSrc;
    [SerializeField] private AudioClip[] dotsPowerPelletsAudioClips;
    [SerializeField] private AudioClip specialFruitAudioClip;
    private int index = 0;

    private void Awake()
    {
        dotPowerPelletEatenChannel.AddListener(PlayDotPowerPelletEatenAudio);
        specialFruitEatenChannel.AddListener(PlaySpecialFruitEatenAudio);
    }

    private void PlayDotPowerPelletEatenAudio()
    {
        audioSrc.PlayOneShot(dotsPowerPelletsAudioClips[index]);
        index = 1 - index;
    }

    private void PlaySpecialFruitEatenAudio()
    {
        audioSrc.PlayOneShot(specialFruitAudioClip);
    }
}