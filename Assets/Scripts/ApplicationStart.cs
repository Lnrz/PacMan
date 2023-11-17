using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationStart : MonoBehaviour
{
    private void Awake()
    {
        Scene menuScene;
        Scene gameboardScene;
        string menuSceneString;

        menuSceneString = "MenuScene";
        menuScene = SceneManager.GetSceneByName(menuSceneString);
        gameboardScene = SceneManager.GetSceneByName("GameboardScene");
        if (!menuScene.isLoaded && !gameboardScene.isLoaded)
        {
            MySceneUtils.LoadScene(menuSceneString, true);
        }
    }
}