using UnityEngine;

public static class MyScoreUtils
{
    public static int GetHighscore()
    {
        return PlayerPrefs.GetInt("highscore", 0);
    }

    public static void SetHighscore(int highscore)
    {
        PlayerPrefs.SetInt("highscore", highscore);
    }
}