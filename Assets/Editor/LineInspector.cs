using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Line))]
public class LineInspector : Editor
{
    Line line;
    Transform handleTransform;
    Quaternion handleRotation;

    void OnSceneGUI()
    {
        line = target as Line;
        handleTransform = line.transform;
        handleRotation = Tools.pivotRotation == PivotRotation.Local ?
            handleTransform.rotation : Quaternion.identity;

        Vector3 p0 = ShowPoint(0);
        Vector3 p1 = ShowPoint(1);

        Handles.color = Color.white;
        Handles.DrawLine(p0, p1);
    }

    Vector3 ShowPoint(int index)
    {
        Vector3 point = handleTransform.TransformPoint(index==0?line.p0:line.p1);
        EditorGUI.BeginChangeCheck();
        point = Handles.DoPositionHandle(point, handleRotation);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(line, "Move Point");
            EditorUtility.SetDirty(line);
            if (index == 0)
                line.p0 = handleTransform.InverseTransformPoint(point);
            else
                line.p1 = handleTransform.InverseTransformPoint(point);
        }
        return point;
    }
}
