using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BrickFloorData))]
public class BrickFloorDataEditor : Editor
{
    SerializedProperty bricksSettingsProperty, brickWallSettingsProperty;

    BrickFloorData brickFloorData;

    Color baseColor;

    private void OnEnable()
    {
        brickFloorData = target as BrickFloorData;

        bricksSettingsProperty = serializedObject.FindProperty("bricksSettings");
        brickWallSettingsProperty = serializedObject.FindProperty("brickWallSettings");

        baseColor = GUI.color;


    }

    int[] displayedLevel;
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(bricksSettingsProperty, new GUIContent("Bricks Settings : "));
        EditorGUILayout.PropertyField(brickWallSettingsProperty, new GUIContent("Brick Wall Settings : "));

        serializedObject.ApplyModifiedProperties();

        displayedLevel = new int[brickFloorData.brickWallSettings.rowAmount * brickFloorData.brickWallSettings.lineAmount];
        if(brickFloorData.brickIndexes.Length != displayedLevel.Length)
        {
            Reset();
            return;
        }
        else
        {
            GetDisplayedLevel();
        }

        if (GUILayout.Button("Fill"))
        {
            Fill();
        }

        if (GUILayout.Button("Reset"))
        {
            Reset();
        }

        DisplayPaintingButtons();

        DisplayLevel();

        brickFloorData.brickIndexes = displayedLevel;
        EditorUtility.SetDirty(brickFloorData);

        serializedObject.ApplyModifiedProperties();      


    }

    void Fill()
    {
        displayedLevel = new int[brickFloorData.brickWallSettings.rowAmount * brickFloorData.brickWallSettings.lineAmount];
        brickFloorData.brickIndexes = new int[displayedLevel.Length];
        for (int i = 0; i < brickFloorData.brickIndexes.Length; i++)
        {
            displayedLevel[i] = brickFloorData.selectedSetting;
        }
    }

    void Reset()
    {
        
        displayedLevel = new int[brickFloorData.brickWallSettings.rowAmount * brickFloorData.brickWallSettings.lineAmount];
        brickFloorData.brickIndexes = new int[displayedLevel.Length];
        for (int i = 0; i < brickFloorData.brickIndexes.Length; i++)
        {
            displayedLevel[i] = -1;
            brickFloorData.brickIndexes[i] = -1;
        }
    }

    void GetDisplayedLevel()
    {
        for(int i = 0; i < displayedLevel.Length; i++)
        {
            displayedLevel[i] = brickFloorData.brickIndexes[i];
        }
    }


    void DisplayPaintingButtons()
    {
        EditorGUILayout.BeginHorizontal("Box");
        for (int i = 0; i < brickFloorData.bricksSettings.bricks.Count; i++)
        {
            GUI.color = brickFloorData.bricksSettings.bricks[i].color;
            if (GUILayout.Button(""))
            {
                brickFloorData.selectedSetting = i;
            }
            GUI.color = baseColor;
        }
        EditorGUILayout.EndHorizontal();
    }

    void PaintButton(int row, int line, int brickIndex)
    {
        int index = (row * brickFloorData.brickWallSettings.lineAmount) + line;
        displayedLevel[index] = brickIndex;
    }


    void DisplayLevel()
    {
        EditorGUILayout.BeginHorizontal("Box");
        for (int row = 0; row < brickFloorData.brickWallSettings.rowAmount; row++)
        {
            EditorGUILayout.BeginVertical();
            for (int line = brickFloorData.brickWallSettings.lineAmount-1; line >= 0; line--)
            {
                EditorGUILayout.BeginHorizontal();
                int index = (row * brickFloorData.brickWallSettings.lineAmount) + line;
                GUIContent newContent;
                if(displayedLevel[index] >= 0)
                {
                    GUI.color = brickFloorData.bricksSettings.bricks[displayedLevel[index]].color;
                    newContent = new GUIContent("");
                }
                else
                {
                    newContent = new GUIContent("");
                    GUI.color -= new Color(0.2f, 0.2f, 0.2f, 0.7f);
                    int half = Mathf.RoundToInt(brickFloorData.brickWallSettings.rowAmount / 2);
                    if(row == half-1 || row == half)
                    {
                        GUI.color -= new Color(0.1f, 0.1f, 0, 0);
                    }
                    half = Mathf.RoundToInt(half / 2);
                    if (row == half - 1 || row == half)
                    {
                        GUI.color -= new Color(0.1f, 0f, 0.1f, 0);
                    }
                    half = Mathf.RoundToInt(brickFloorData.brickWallSettings.rowAmount / 2);
                    half += Mathf.RoundToInt(half /2);
                    if (row == half + 1 || row == half)
                    {
                        GUI.color -= new Color(0.1f, 0f, 0.1f, 0);
                    }
                    half = Mathf.RoundToInt(brickFloorData.brickWallSettings.lineAmount / 2);
                    if (line == half-1 || line == half)
                    {
                        GUI.color -= new Color(0.1f, 0.1f, 0, 0);
                    }


                }

                if(GUILayout.Button(newContent))
                {
                    if(Event.current.button == 0)
                    {
                        PaintButton(row, line, brickFloorData.selectedSetting);

                    }
                    else
                    {
                        PaintButton(row, line, -1);
                    }

                }

                GUI.color = baseColor;

                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();
    }
}
