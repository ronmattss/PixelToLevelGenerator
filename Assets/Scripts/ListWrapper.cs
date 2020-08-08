using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

[Serializable]
public class ListWrapper<T>
{
    // Start is called before the first frame update
    public List<T> listOfColorTileHolder = new List<T>();
}

[Serializable]
public class TilemapHolder
{
    public List<Tilemap> tilemaps = new List<Tilemap>();
    public ListWrapper<ColorTileHolder> tileWrapper = new ListWrapper<ColorTileHolder>();

}
[Serializable]
public class ColorTileHolder
{
    public List<ColorTile> colorTiles = new List<ColorTile>();
}