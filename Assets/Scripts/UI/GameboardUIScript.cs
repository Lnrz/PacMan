using UnityEngine;
using UnityEngine.UIElements;

public class GameboardUIScript : MonoBehaviour
{
    [SerializeField] private GameStartChannelSO gameStartChannel;
    [SerializeField] private GameRestartChannelSO gameRestartChannel;
    [SerializeField] private GameEndChannelSO gameEndChannel;
    private Label readyLabel;
    private Label gameOverLabel;


    private void Awake()
    {
        GetLabels();
        gameStartChannel.AddListener(OnGameStart);
        gameRestartChannel.AddListener(OnGameRestart);
        gameEndChannel.AddListener(OnGameEnd);
    }

    private void GetLabels()
    {
        UIDocument uiDoc;

        uiDoc = GetComponent<UIDocument>();
        readyLabel = uiDoc.rootVisualElement.Q<Label>("readyLabel");
        gameOverLabel = uiDoc.rootVisualElement.Q<Label>("gameOverLabel");
    }

    private void OnGameStart()
    {
        readyLabel.style.display = DisplayStyle.None;
    }

    private void OnGameRestart()
    {
        readyLabel.style.display = DisplayStyle.Flex;
    }

    private void OnGameEnd()
    {
        gameOverLabel.style.display = DisplayStyle.Flex;
    }
}
