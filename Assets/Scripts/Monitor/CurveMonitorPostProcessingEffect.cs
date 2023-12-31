using UnityEngine;

[ExecuteInEditMode]
public class CurveMonitorPostProcessingEffect : MonoBehaviour
{
    [SerializeField] private Material effect;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (effect is null)
        {
            Graphics.Blit(source, destination);
        }
        else
        {
            Graphics.Blit(source, destination, effect);
        }
    }
}