using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CurveMonitorPostProcessingEffect : MonoBehaviour
{
    public Material effect;

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