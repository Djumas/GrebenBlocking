using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(ChildrenRenderingSwitch))]
public class ChildrenRenderingSwitchEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var script = (ChildrenRenderingSwitch) target;

        var newValue = EditorGUILayout.Toggle("Children Rendering", script.childrenRendering);
        if (newValue != script.ChildrenRendering) {
            script.ChildrenRendering = newValue;
        }
        if (GUILayout.Button("Switch Rendering"))
        {
            script.SwitchChildrenRendering();
            //Debug.Log("Visiblity switched");
        }
    }
}
