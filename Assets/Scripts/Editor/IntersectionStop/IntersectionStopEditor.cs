using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(IntersectionStop))]
[CanEditMultipleObjects]
public class IntersectionStopInspector : Editor
{
    public VisualTreeAsset inspectorUXML;
    private float arcAngle = 90.0f;
    private float arcRadius = 0.44f;
    private float arcThickness = 2.5f;
    private IntersectionStop intersection;
    private Vector3 center;
    private Vector3 from;

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement inspector =  new VisualElement();

        inspectorUXML.CloneTree(inspector);
        inspector.Q<Toggle>("Up").bindingPath = "turnDirection.Array.data[0]";
        inspector.Q<Toggle>("Right").bindingPath = "turnDirection.Array.data[1]";
        inspector.Q<Toggle>("Down").bindingPath = "turnDirection.Array.data[2]";
        inspector.Q<Toggle>("Left").bindingPath = "turnDirection.Array.data[3]";

        return inspector;
    }

    private void OnSceneGUI()
    {
        intersection = target as IntersectionStop;
        center = intersection.transform.position;
        from = new Vector3(1, 1, 0);
        for (int i = 0; i < 4; i++)
        {
            Handles.color = intersection.CanTurn(i) ? Color.green : Color.red;
            Handles.DrawWireArc(center, Vector3.forward, from, arcAngle, arcRadius, arcThickness);
            if (i % 2 == 0)
            {
                from.y = -from.y;
            }
            else
            {
                from.x = -from.x;
            }
        }
    }
}