using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;


[CustomEditor(typeof(PixelReader))]
public class TestInspector : Editor
{
    // Start is called before the first frame update
    public override void OnInspectorGUI()
    {
        PixelReader pixelReader = (PixelReader)target;
        DrawDefaultInspector();
        //pixelReader.x = EditorGUILayout.IntField("X axis", pixelReader.x);
        //pixelReader.y = EditorGUILayout.IntField("Y axis", pixelReader.y);
        EditorGUILayout.LabelField("RGB Color: ", ColorUtility.ToHtmlStringRGBA(pixelReader.testPixelColor));
        if (pixelReader.pixelData != null)
        {
            EditorGUILayout.LabelField("Texture Width: ", pixelReader.pixelData.textureWidth.ToString());
            EditorGUILayout.LabelField("Texture Height: ", pixelReader.pixelData.textureHeight.ToString());
        }





        // Buttons
        if (GUILayout.Button("ShowRGBA values"))
        {
            pixelReader.DebugRGBA();
        }
        if (GUILayout.Button("Generate Grid TileTest"))
        {
            pixelReader.PlaceTiles();
        }
        if (GUILayout.Button("Generate Room"))
        {
            pixelReader.StartCoroutine(pixelReader.GenerateTiles());
        }
        if (GUILayout.Button("Stop"))
        {
            pixelReader.StopAllCoroutines();
        }

        if (GUILayout.Button("Load Colors") && pixelReader.testImage != null)
        {
            pixelReader.LoadColorPalette();
        }
        if (GUILayout.Button("Delete Cubes"))
        {
            GameObject[] objects;
            objects = GameObject.FindGameObjectsWithTag("Test");
            foreach (var x in objects)
            {
                DestroyImmediate(x);
            }
        }

    }
}
