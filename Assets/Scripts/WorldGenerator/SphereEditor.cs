using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Sphere))]
public class SphereEditor : Editor
{
    private Editor shapeEditor;
    private Editor colorEditor;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Sphere sphere = (Sphere)target;

        EditorGUILayout.Space(10);

        if (GUILayout.Button("Generate Sphere"))
        {
            sphere.GenerateSphere();
        }

        EditorGUILayout.Space(10);

        if (GUILayout.Button("Re-Generate Colliders"))
        {
            sphere.RegenerateColliders();
        }

        EditorGUILayout.Space(10);

        ////Generate in the inspector the SO of the color settings

        sphere.foldOutColorSetting = EditorGUILayout.InspectorTitlebar(sphere.foldOutColorSetting, sphere.colorSettings);

        using (var check = new EditorGUI.ChangeCheckScope())
        {
            if (sphere.foldOutColorSetting)
            {
                CreateCachedEditor(sphere.colorSettings, null, ref colorEditor);
                colorEditor.OnInspectorGUI();
            }
        }

        EditorGUILayout.Space(10);

        //Generate in the inspector the SO of the shape settings

        sphere.foldOutShapeSetting = EditorGUILayout.InspectorTitlebar(sphere.foldOutShapeSetting, sphere.shapeSettings);

        using (var check = new EditorGUI.ChangeCheckScope())
        {
            if (sphere.foldOutShapeSetting)
            {
                CreateCachedEditor(sphere.shapeSettings, null, ref shapeEditor);
                shapeEditor.OnInspectorGUI();
            }
        }
    }
}