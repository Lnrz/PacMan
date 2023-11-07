using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuUIPowerPelletScript : MonoBehaviour
{
    private Image powerPelletImg;
    [SerializeField] private float frequency = 5;

    private void Awake()
    {
        UIDocument uiDoc;
        
        uiDoc = GetComponent<UIDocument>();
        powerPelletImg = uiDoc.rootVisualElement.Q<Image>("powerPellet");
        StartCoroutine(PowerPelletAnim());
    }

    private IEnumerator PowerPelletAnim()
    {
        yield return new WaitUntil(CheckForVisibility);
        while (true)
        {
            yield return new WaitForSeconds(1.0f / frequency);
            powerPelletImg.style.visibility = Visibility.Hidden;
            yield return new WaitForSeconds(1.0f / frequency);
            powerPelletImg.style.visibility = Visibility.Visible;
        }
    }

    private bool CheckForVisibility()
    {
        VisualElement ve;
        
        ve = powerPelletImg;
        do
        {
            if (!ve.visible) return false;
            ve = ve.parent;
        }
        while (ve is not null);
        return true;
    }
}