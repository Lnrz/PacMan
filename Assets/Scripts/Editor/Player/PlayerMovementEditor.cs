using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

//[CustomEditor(typeof(PlayerMovement))]
public class PlayerMovementEditor : Editor
{
    public VisualTreeAsset inspectorUXML;
    private float arrowLength = 0.75f;
    private float arrowHeadLength = 0.1f;
    private float arrowHeadThickness = 0.03f;
    private float arrowThickness = 1.5f;
    private PlayerMovement pm;
    private Vector3 arrowTail;
    private Vector3 arrowDirection;
    private Vector3 arrowHead;
    private Vector3 arrowHeadBaseDirection;
    private Vector3 arrowHeadBaseOne;
    private Vector3 arrowHeadBaseTwo;

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement inspector = new VisualElement();

        inspectorUXML.CloneTree(inspector);
        inspector.Q<Toggle>("Up").bindingPath = "legalDir.Array.data[0]";
        inspector.Q<Toggle>("Right").bindingPath = "legalDir.Array.data[1]";
        inspector.Q<Toggle>("Down").bindingPath = "legalDir.Array.data[2]";
        inspector.Q<Toggle>("Left").bindingPath = "legalDir.Array.data[3]";

        return inspector;
    }

    private void OnSceneGUI()
    {
        PlayerMovement pm = target as PlayerMovement;
        arrowTail = pm.transform.position;
        Handles.color = Color.green;
        for (int i = 0; i < 4; i++)
        {
            if (!pm.GetIsLegalDir(i)) continue;
            arrowDirection = (i % 2 == 0) ? Vector3.up : Vector3.right;
            arrowHeadBaseDirection = Vector3.Cross(arrowDirection, Vector3.forward);
            arrowDirection = (i < 2) ? arrowDirection : -arrowDirection;
            arrowHead = arrowTail + arrowLength * arrowDirection;
            arrowHeadBaseOne = arrowHead - arrowHeadLength * arrowDirection + arrowHeadThickness * arrowHeadBaseDirection;
            arrowHeadBaseTwo = arrowHead - arrowHeadLength * arrowDirection - arrowHeadThickness * arrowHeadBaseDirection;
            Handles.DrawLine(arrowTail, arrowHead, arrowThickness);
            Handles.DrawAAConvexPolygon(arrowHeadBaseOne, arrowHeadBaseTwo, arrowHead);
        }
    }
}