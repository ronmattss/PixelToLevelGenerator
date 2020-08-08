using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class TileMapAdder
{



    public static GameObject AddGameObject(string name = "Tilemap", int numOfLayers = 1)
    {
        GameObject newObject = new GameObject();
        newObject.transform.position = Vector3.zero;
        newObject.transform.name = name;
        newObject.AddComponent<Grid>();
        for (int i = 0; i < numOfLayers; i++)
        {
            GameObject newChildObject = new GameObject();
            newChildObject.transform.name = "tilemap";
            newChildObject.transform.parent = newObject.transform;
            newChildObject.AddComponent<Tilemap>();
            newChildObject.AddComponent<TilemapRenderer>();
        }
        return newObject;

    }
}
