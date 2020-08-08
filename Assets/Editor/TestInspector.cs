using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;


[CustomEditor(typeof(PixelReader))]
public class TestInspector : Editor
{
    // Start is called before the first frame update
    int x = 0;
    public override void OnInspectorGUI()
    {
        PixelReader pixelReader = (PixelReader)target;
        DrawDefaultInspector();
        //pixelReader.x = EditorGUILayout.IntField("X axis", pixelReader.x);
        //pixelReader.y = EditorGUILayout.IntField("Y axis", pixelReader.y);
        EditorGUILayout.LabelField("RGB Color: ", ColorUtility.ToHtmlStringRGBA(pixelReader.testPixelColor));
        x = EditorGUILayout.IntField("tilemaps: ", x);
        if (pixelReader.pixelData != null)
        {
            EditorGUILayout.LabelField("Texture Width: ", pixelReader.pixelData.textureWidth.ToString());
            EditorGUILayout.LabelField("Texture Height: ", pixelReader.pixelData.textureHeight.ToString());
        }






        // Buttons
        if (GUILayout.Button("Generate new Grid"))
        {
            if (pixelReader.tileMap == null)
            {
                GameObject grid = TileMapAdder.AddGameObject("Tile Grid", x);
                pixelReader.targetTileGrid = grid.GetComponent<Grid>();
                for (int i = 0; i < x; i++)
                {
                    pixelReader.tiles.tilemaps.Add(grid.transform.GetChild(i).GetComponent<Tilemap>());
                }
                pixelReader.tiles.tileWrapper.listOfColorTileHolder = new List<ColorTileHolder>();
                foreach (var pix in pixelReader.tiles.tileWrapper.listOfColorTileHolder)
                {
                    pix.colorTiles = new List<ColorTile>();
                }

            }

        }
        if (GUILayout.Button("ShowRGBA values"))
        {
            pixelReader.DebugRGBA();
            pixelReader.ShowPixelValue();
            pixelReader.pixelData.DebugNames();
        }
        if (GUILayout.Button("Generate Grid TileTest"))
        {
            pixelReader.PlaceTiles(pixelReader.tileMap);
        }
        if (GUILayout.Button("Generate Grid TileTestperLayer"))
        {
            pixelReader.SetupTiles();
            pixelReader.DebugProperties();
        }
        if (GUILayout.Button("Generate Room"))
        {
            pixelReader.StartCoroutine(pixelReader.GenerateTiles());
        }
        if (GUILayout.Button("Stop"))
        {
            pixelReader.StopAllCoroutines();
        }

        if (GUILayout.Button("Load Colors") && pixelReader.platformImage != null)
        {
            pixelReader.LoadColorPalette();
        }
        if (GUILayout.Button("Load Palette Set") && pixelReader.platformImage != null)
        {
            pixelReader.LoadColorPaletteSet();
            
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
