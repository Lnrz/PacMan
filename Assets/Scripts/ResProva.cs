using UnityEngine;

public class ResProva : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            foreach (Resolution res in Screen.resolutions)
            {
                Debug.Log(res);
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log(Screen.currentResolution);
        }
    }
}