using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuUIAnimationScript : MonoBehaviour
{
    [SerializeField] private MenuAnimationEndChannelSO menuAnimationEndChannel;
    [SerializeField] private float initialWait;
    [SerializeField] private float imgCharacterWait;
    [SerializeField] private float characterNicknameWait;
    [SerializeField] private float waitBetweenCharacters;
    [SerializeField] private float characterPointsWait;
    [SerializeField] private float pointsCreditsWait;
    private VisualElement root;
    private VisualElement points;
    private VisualElement credits;
    private bool isCompleted = false;

    private void Awake()
    {
        UIDocument uiDoc;

        uiDoc = GetComponent<UIDocument>();
        root = uiDoc.rootVisualElement;
        points = root.Q<VisualElement>("points");
        credits = root.Q<VisualElement>("credits");
        StartCoroutine(MenuAnimation());
    }

    private IEnumerator MenuAnimation()
    {
        yield return new WaitForSeconds(initialWait);
        StartCoroutine(ImgCharacterNicknameAnimation("akabei"));
        yield return new WaitUntil(() => isCompleted);
        isCompleted = false;
        yield return new WaitForSeconds(waitBetweenCharacters);
        StartCoroutine(ImgCharacterNicknameAnimation("pinky"));
        yield return new WaitUntil(() => isCompleted);
        isCompleted = false;
        yield return new WaitForSeconds(waitBetweenCharacters);
        StartCoroutine(ImgCharacterNicknameAnimation("aosuke"));
        yield return new WaitUntil(() => isCompleted);
        isCompleted = false;
        yield return new WaitForSeconds(waitBetweenCharacters);
        StartCoroutine(ImgCharacterNicknameAnimation("guzuta"));
        yield return new WaitUntil(() => isCompleted);
        yield return new WaitForSeconds(characterPointsWait);
        MakeVisible(points);
        yield return new WaitForSeconds(pointsCreditsWait);
        MakeVisible(credits);
        menuAnimationEndChannel.Invoke();
    }

    private IEnumerator ImgCharacterNicknameAnimation(string nickname)
    {
        VisualElement imgVE;
        VisualElement characterVE;
        VisualElement nicknameVE;

        imgVE = root.Q<VisualElement>(nickname + "Img");
        characterVE = root.Q<Label>(nickname + "Character");
        nicknameVE = root.Q<Label>(nickname + "Nickname");
        MakeVisible(imgVE);
        yield return new WaitForSeconds(imgCharacterWait);
        MakeVisible(characterVE);
        yield return new WaitForSeconds(characterNicknameWait);
        MakeVisible(nicknameVE);
        isCompleted = true;
    }

    private void MakeVisible(VisualElement element)
    {
        element.style.visibility = Visibility.Visible;
    }
}