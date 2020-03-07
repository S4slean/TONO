using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoatPath))]
public class BoatPathEditor : Editor
{
    BoatPath boatPath;

    private void OnEnable()
    {
        boatPath = target as BoatPath;
    }


    Vector3[] path;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        boatPath.pathSubdivisions = EditorGUILayout.IntSlider(new GUIContent("Path Subdivisions: "), boatPath.pathSubdivisions, 100, 1000);

        path = new Vector3[boatPath.basePath.Length * boatPath.pathSubdivisions];
        path[0] = boatPath.transform.position;

        for (int i = 0; i < boatPath.basePath.Length-1;i++)
        {

            Vector3[] bezier = Handles.MakeBezierPoints(boatPath.basePath[i], boatPath.basePath[i + 1], boatPath.startingTangeants[i], boatPath.endingTangeants[i], boatPath.pathSubdivisions);
            for(int j = 0; j < bezier.Length; j++)
            {
                path[(i+1 * boatPath.pathSubdivisions) + j] = bezier[j];
            }
        }

        for(int i = 0; i < path.Length; i++)
        {
            path[i].y = boatPath.height;
        }

        boatPath.path = path;

        serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI()
    {
        Handles.DrawPolyLine(boatPath.path);
    }
}
