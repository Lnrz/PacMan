using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoresUIScript : MonoBehaviour
{
    [SerializeField] private ScoreUpdateChannelSO scoreUpdateChannel;
    [SerializeField] private HighscoreUpdateChannelSO highscoreUpdateChannel;
    [SerializeField] private GameEndChannelSO gameEndChannel;
    private Label score;
    private Label highscore;

    private void Awake()
    {
        GetLabels();
        SetInitialHighscore();
        scoreUpdateChannel.AddListener(OnScoreUpdate);
        highscoreUpdateChannel.AddListener(OnHighscoreUpdate);
        gameEndChannel.AddListener(OnGameEnd);
    }

    private void SetInitialHighscore()
    {
        highscore.text = MyScoreUtils.GetHighscore().ToString();
    }

    private void GetLabels()
    {
        UIDocument uiDoc;

        uiDoc = GetComponent<UIDocument>();
        score = uiDoc.rootVisualElement.Q<Label>("score");
        highscore = uiDoc.rootVisualElement.Q<Label>("highscore");
    }

    private void OnScoreUpdate(int newScore)
    {
        score.text = newScore.ToString();
    }

    private void OnHighscoreUpdate(int newHighscore)
    {
        highscore.text = newHighscore.ToString();
    }

    private void OnGameEnd()
    {
        score.text = "0";
    }
}