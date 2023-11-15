using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public static class MySceneUtils
{
    public static void LoadScene(string sceneName, bool setAsActive)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        if (setAsActive)
        {
            UnityAction<Scene, LoadSceneMode> onLoad = null;

            onLoad = (Scene scene, LoadSceneMode loadMode) =>
            {
                if (sceneName.Equals(scene.name))
                {
                    SceneManager.SetActiveScene(scene);
                    SceneManager.sceneLoaded -= onLoad;
                }
            };
            SceneManager.sceneLoaded += onLoad;
        }
    }

    public static void UnloadScene(string sceneName)
    {
        AsyncOperation op;

        op = SceneManager.UnloadSceneAsync(sceneName);
        op.completed += (AsyncOperation op) => Resources.UnloadUnusedAssets();
    }
}