using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoatPath))]
public class BoatPathEditor : Editor
{
    BoatPath boatPath;
    SerializedProperty pathLineRendererProperty, anchorPointProperty;

    private void OnEnable()
    {
        boatPath = target as BoatPath;
        pathLineRendererProperty = serializedObject.FindProperty("pathLineRenderer");
        anchorPointProperty = serializedObject.FindProperty("anchorPoint");
    }


    Vector3[] path;
    Vector3[] basePathWithPos;
    float newHeight;
    int[] newIndexes;

    public override void OnInspectorGUI()
    {
        

        serializedObject.Update();

        
        EditorGUILayout.PropertyField(pathLineRendererProperty, new GUIContent("Line Renderer : "));

        
        boatPath.pathSubdivisions = EditorGUILayout.IntSlider(new GUIContent("Path Subdivisions: "), boatPath.pathSubdivisions, 100, 1000);

        newHeight = boatPath.height;
        newHeight = EditorGUILayout.FloatField(new GUIContent("Height: "), newHeight);
        boatPath.height = newHeight;

        

        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Add Path Node"))
        {
            boatPath.AddBasePathNode();
        }

        if(boatPath.basePath.Length > 0)
        {
            if (GUILayout.Button("Remove Path Node"))
            {
                boatPath.RemoveBasePathNode();
            }
        }
        GUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();

        if (boatPath.basePath.Length <= 0) return;

        path = new Vector3[boatPath.basePath.Length * boatPath.pathSubdivisions];
        path[0] = boatPath.transform.position;

        basePathWithPos = new Vector3[boatPath.basePath.Length + 1];
        basePathWithPos[0] = boatPath.transform.position;
        for(int i = 0; i < boatPath.basePath.Length; i++)
        {
            basePathWithPos[i + 1] = boatPath.basePath[i];
        }

        for (int i = 0; i < basePathWithPos.Length-1;i++)
        {

            Vector3[] bezier = Handles.MakeBezierPoints(basePathWithPos[i], basePathWithPos[i + 1], boatPath.startingTangeants[i], boatPath.endingTangeants[i], boatPath.pathSubdivisions);
            for(int j = 0; j < bezier.Length; j++)
            {
                path[(i * boatPath.pathSubdivisions) + j] = bezier[j];
            }
        }

        for(int i = 0; i < path.Length; i++)
        {
            path[i].y = boatPath.height;
        }

        boatPath.bezierPath = path;

        boatPath.transform.position = new Vector3(boatPath.transform.position.x, boatPath.height, boatPath.transform.position.z);


        EditorGUILayout.PropertyField(anchorPointProperty, new GUIContent("Anchor Point Object: "));

        GUILayout.Label("Anchor Points Indexes : ");

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add"))
        {
            boatPath.AddAnchorPointsIndex();
        }

        if (boatPath.anchorPointsIndexes.Length > 0)
        {
            if (GUILayout.Button("Remove"))
            {
                boatPath.RemoveAnchorPointsIndex();
            }
        }
        GUILayout.EndHorizontal();

        
        newIndexes = boatPath.anchorPointsIndexes;

        for (int i = 0; i < newIndexes.Length;i++)
        {
            newIndexes[i] = EditorGUILayout.IntField(newIndexes[i]);
        }
        
        boatPath.anchorPointsIndexes = newIndexes;
        
 
        //EditorWindow.GetWindow<SceneView>().Repaint();
        serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI()
    {

        if (boatPath.basePath.Length <= 0) return;

        Handles.DrawPolyLine(boatPath.bezierPath);

        for(int i = 0; i < boatPath.basePath.Length; i++)
        {
            boatPath.basePath[i] = Handles.PositionHandle(new Vector3(boatPath.basePath[i].x, boatPath.height, boatPath.basePath[i].z), Quaternion.identity);
        }

        Handles.color = Color.red;
        for(int i = 0; i < boatPath.startingTangeants.Length; i++)
        {
            Handles.DrawLine(basePathWithPos[i], boatPath.startingTangeants[i]);
            boatPath.startingTangeants[i] = Handles.FreeMoveHandle(new Vector3(boatPath.startingTangeants[i].x, boatPath.height, boatPath.startingTangeants[i].z), Quaternion.identity, 0.22f, Vector3.zero, Handles.SphereHandleCap);
        }

        Handles.color = Color.blue;
        for (int i = 0; i < boatPath.endingTangeants.Length; i++)
        {
            Handles.DrawLine(basePathWithPos[i+1], boatPath.endingTangeants[i]);
            boatPath.endingTangeants[i] = Handles.FreeMoveHandle(new Vector3(boatPath.endingTangeants[i].x, boatPath.height, boatPath.endingTangeants[i].z), Quaternion.identity, 0.22f, Vector3.zero, Handles.SphereHandleCap);
        }

        Handles.color = Color.green;
        for(int i = 0; i < boatPath.anchorPointsIndexes.Length; i++)
        {
            Handles.DrawSphere(0, boatPath.bezierPath[boatPath.anchorPointsIndexes[i]], Quaternion.identity, 0.4f);
        }
    }
}
