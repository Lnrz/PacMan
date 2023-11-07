using UnityEngine.UIElements;
using UnityEngine;

public class LevelUIScript : MonoBehaviour
{
    [SerializeField] private NextLevelChannelSO nextLevelChannel;
    [SerializeField] private GameEndChannelSO gameEndChannel;
    private Label levelLabel;
    private int levelNum = 1;

    private void Awake()
    {
        UIDocument uiDoc;

        uiDoc = GetComponent<UIDocument>();
        levelLabel = uiDoc.rootVisualElement.Q<Label>("level");
        nextLevelChannel.AddListener(OnNextLevel);
        gameEndChannel.AddListener(OnGameEnd);
    }

    private void OnNextLevel()
    {
        levelNum++;
        levelLabel.text = levelNum.ToString();
    }

    private void OnGameEnd()
    {
        levelNum = 1;
        levelLabel.text = "1";
    }
}