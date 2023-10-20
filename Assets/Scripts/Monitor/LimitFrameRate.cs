using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LimitFrameRate : MonoBehaviour
{
    [SerializeField] private int vSyncCount = 0;
    [SerializeField] private int frameRate = 60;

    private void Awake()
    {
        QualitySettings.vSyncCount = vSyncCount;
        Application.targetFrameRate = frameRate;
    }
}