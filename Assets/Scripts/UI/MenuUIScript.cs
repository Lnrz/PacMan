using UnityEngine;
using UnityEngine.UIElements;

public class MenuUIScript : MonoBehaviour
{
    [SerializeField] private MenuAnimationEndChannelSO menuAnimationEndChannel;
    private Label creditNum;
    private VisualElement beforeCoin;
    private VisualElement afterCoin;
    private bool isCoinInserted = false;

    private void Awake()
    {
        GetElements();
    }

    private void Update()
    {
        if (!isCoinInserted && Input.GetKeyDown(KeyCode.C))
        {
            isCoinInserted = true;
            beforeCoin.style.display = DisplayStyle.None;
            afterCoin.style.display = DisplayStyle.Flex;
            creditNum.text = "1";
            menuAnimationEndChannel.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            Utility.LoadScene("GameboardScene", true);
            Utility.UnloadScene("MenuScene");
        }
    }

    private void GetElements()
    {
        UIDocument uiDoc;

        uiDoc = GetComponent<UIDocument>();
        creditNum = uiDoc.rootVisualElement.Q<Label>("creditNum");
        beforeCoin = uiDoc.rootVisualElement.Q<VisualElement>("beforeCoin");
        afterCoin = uiDoc.rootVisualElement.Q<VisualElement>("afterCoin");
    }
}