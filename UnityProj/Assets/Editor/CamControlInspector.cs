using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(CamControl))]
public class CamControlInspector : Editor
{
    private Transform pathContainer;

    private void GeneratePath()
    {
        CamControl cam = target as CamControl;
        Transform child;
        Vector3 euler;
        float distance = 0.0f;

        cam.pathCurveX = new AnimationCurve();
        cam.pathCurveY = new AnimationCurve();
        cam.pathCurveZ = new AnimationCurve();
        cam.rotationCurveY = new AnimationCurve();
        cam.rotationCurveX = new AnimationCurve();
        cam.rotationCurveZ = new AnimationCurve();
        for (int i = 0, size = this.pathContainer.childCount; i < size; ++i)
        {
            child = this.pathContainer.GetChild(i);
            if (i != 0)
                distance += (child.position - this.pathContainer.GetChild(i - 1).position).magnitude;
            cam.pathCurveX.AddKey(distance, child.position.x);
            cam.pathCurveY.AddKey(distance, child.position.y);
            cam.pathCurveZ.AddKey(distance, child.position.z);

            euler = child.rotation.eulerAngles;
            cam.rotationCurveX.AddKey(distance, euler.x);
            cam.rotationCurveY.AddKey(distance, euler.y);
            cam.rotationCurveZ.AddKey(distance, euler.z);
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        this.pathContainer = EditorGUILayout.ObjectField("Container for Path nodes", this.pathContainer, typeof(Transform)) as Transform;
        if (this.pathContainer != null)
        {
            EditorGUI.indentLevel += 1;
            for (int i = 0, size = this.pathContainer.childCount; i < size; ++i)
                EditorGUILayout.LabelField("Child " + i, this.pathContainer.GetChild(i).name);
            GUI.enabled = true;
        }
        else
            GUI.enabled = false;
        if (GUILayout.Button("GeneratePath") == true)
            this.GeneratePath();
        GUI.enabled = true;

        // ADD BEZIER CURVE (find function from points -> get tangents)
    }
}
