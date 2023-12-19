using UnityEngine;

public class ScreenResizer : MonoBehaviour
{
    private Vector2Int screenRes = new Vector2Int();
    private float arcadeAspectRatio = 7.0f / 9.0f;

    private void Awake()
    {
        screenRes.x = Screen.width;
        screenRes.y = Screen.height;
    }

    private void Update()
    {
        if (screenRes.x != Screen.width ||
            screenRes.y != Screen.height)
        {
            screenRes.y = Screen.height;
            screenRes.x = (int)(screenRes.y * arcadeAspectRatio);
            Screen.SetResolution(screenRes.x, screenRes.y, false);
        }
    }
}