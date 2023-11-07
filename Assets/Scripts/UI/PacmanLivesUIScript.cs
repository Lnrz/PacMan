using UnityEngine;
using UnityEngine.UIElements;

public class PacmanLivesUIScript : MonoBehaviour
{
    [SerializeField] private LivesLeftUpdateChannelSO livesLeftUpdateChannel;
    private VisualElement livesContainer;
    private int pacmanNum;

    private void Awake()
    {
        UIDocument uiDoc;

        uiDoc = GetComponent<UIDocument>();
        livesContainer = uiDoc.rootVisualElement.Q<VisualElement>("livesContainer");
        pacmanNum = livesContainer.childCount;
        livesLeftUpdateChannel.AddListener(OnLivesLeftUpdate);
    }

    private void OnLivesLeftUpdate(int livesLeft)
    {
        int diff;

        if (pacmanNum > livesLeft)
        {
            diff = pacmanNum - livesLeft;
            for (int i = 0; pacmanNum > 0 && i < diff; i++)
            {
                livesContainer.RemoveAt(0);
                pacmanNum--;
            }
        }
        else if (pacmanNum < livesLeft)
        {
            diff = livesLeft - pacmanNum;
            for (int i = 0; i < diff; i++)
            {
                Image pacmanImg;

                pacmanImg = new Image();
                pacmanImg.AddToClassList("lives-container__life");
                livesContainer.Add(pacmanImg);
                pacmanNum++;
            }
        }
    }
}