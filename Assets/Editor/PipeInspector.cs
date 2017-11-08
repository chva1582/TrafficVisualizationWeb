using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Pipe))]
public class PipeInspector : Editor
{
    static Color[] modeColors = { Color.white, Color.yellow, Color.cyan };

    const int lineSteps = 10;
    const float directionScale = 0.5f;

    const float handleSize = 0.04f;
    const float pickSize = 0.06f;

    int selectedIndex = -1;

    private Pipe pipe;
    private Transform handleTransform;
    private Quaternion handleRotation;

    private void OnSceneGUI()
    {
        pipe = target as Pipe;
        handleTransform = pipe.transform;
        handleRotation = Tools.pivotRotation == PivotRotation.Local ?
            handleTransform.rotation : Quaternion.identity;

        Vector3 p0 = ShowPoint(0);
        for (int i = 1; i < pipe.ControlPointCount; i += 3)
        {
            Vector3 p1 = ShowPoint(i);
            Vector3 p2 = ShowPoint(i + 1);
            Vector3 p3 = ShowPoint(i + 2);

            Handles.color = Color.gray;
            Handles.DrawLine(p0, p1);
            Handles.DrawLine(p2, p3);

            Handles.DrawBezier(p0, p3, p1, p2, Color.white, null, 2f);
            p0 = p3;
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        pipe = target as Pipe;
        if (GUILayout.Button("Add Curve"))
        {
            Undo.RecordObject(pipe, "Add Curve");
            pipe.AddCurve();
            EditorUtility.SetDirty(pipe);
        }
    }

    Vector3 ShowPoint(int index)
    {
        Vector3 point = handleTransform.TransformPoint(pipe.GetControlPoint(index));
        float size = HandleUtility.GetHandleSize(point);
        Handles.color = modeColors[(int)pipe.GetControlPointMode(index)];
        if (Handles.Button(point, handleRotation, size * handleSize, size * pickSize, Handles.DotHandleCap))
        {
            selectedIndex = index;
            Repaint();
        }
        if (selectedIndex == index)
        {
            EditorGUI.BeginChangeCheck();
            point = Handles.DoPositionHandle(point, handleRotation);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(pipe, "Move Point");
                EditorUtility.SetDirty(pipe);
                pipe.SetControlPoint(index, handleTransform.InverseTransformPoint(point));
            }
        }
        return point;
    }
}
