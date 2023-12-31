using UnityEngine;
using UnityEngine.UIElements;

public class FruitsUIScript : MonoBehaviour
{
    [SerializeField] private GameStartChannelSO gameStartChannel;
    [SerializeField] private SpecialFruitSettingsChannelSO specialFruitSettingsChannel;
    [SerializeField] private NextLevelChannelSO nextLevelChannel;
    private VisualElement fruitsContainer;
    private bool isImgAdded = false;
    private int fruitsCount = 0;
    private int maxFruitCount = 6;
    private Image lastImg;
    private Sprite lastSprite;

    private void Awake()
    {
        UIDocument uiDoc;

        uiDoc = GetComponent<UIDocument>();
        fruitsContainer = uiDoc.rootVisualElement.Q<VisualElement>("fruitsContainer");
        AddImageToContainer();
        gameStartChannel.AddListener(OnGameStart);
        specialFruitSettingsChannel.AddListener(OnSpecialFruitsSettingsChange);
        nextLevelChannel.AddListener(OnNextLevel);
    }

    private void OnGameStart()
    {
        isImgAdded = false;
    }

    private void OnSpecialFruitsSettingsChange(SpecialFruitSettingsSO specialFruitSettings)
    {
        lastSprite = specialFruitSettings.GetSprite();
        if (isImgAdded)
        {
            lastImg.sprite = lastSprite;
        }
    }

    private void OnNextLevel()
    {
        AddImageToContainer();
        if (fruitsCount > maxFruitCount)
        {
            fruitsContainer.RemoveAt(0);
            fruitsCount--;
        }
    }

    private void AddImageToContainer()
    {
        lastImg = new Image();
        lastImg.AddToClassList("fruits-container__fruit");
        lastImg.sprite = lastSprite;
        fruitsContainer.Add(lastImg);
        fruitsCount++;
        isImgAdded = true;
    }
}