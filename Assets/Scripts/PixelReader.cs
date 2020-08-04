using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;



[Serializable]
public struct ColorTile
{
    public string colorHex;
    public Color colorVal;
    public TileBase colorTile;
}
[ExecuteInEditMode]
public class PixelReader : MonoBehaviour
{
    //Learn while we create this Pixel to Map Generator 

    //first Step: read an image pixels
    public Texture2D testImage;
    [NonSerialized] public PixelData pixelData;
    public int x = 0;
    public int y = 0;
    public List<ColorTile> colorTiles;
    //ALl Tile Properties
    [Tooltip("This is the grid where you place Tiles")]
    public Tilemap tileMapTest;
    public Tile baseTile;
    public TileBase baseTileSet;
    public RuleTile ruleTile;
    [NonSerialized] public Color32 testPixelColor;
    public float generationSpeed = 0.001f;

    public void DebugRGBA()
    {
        testPixelColor = testImage.GetPixel(x, y);
        Debug.Log(ColorUtility.ToHtmlStringRGBA(testPixelColor).ToString());
    }
    public void LoadColorPalette()
    {
        pixelData = new PixelData(testImage);
        pixelData.LoadPixelColors();
        ColorTile temp = new ColorTile();
        foreach (var pix in pixelData.imageColorList)
        {
            temp.colorHex = ColorUtility.ToHtmlStringRGBA(pix).ToString();
            temp.colorVal = pix;
            colorTiles.Add(temp);
        }
    }
    public void TestPlaceTile()
    {

        if (!tileMapTest.gameObject.GetComponent<TilemapCollider2D>()) tileMapTest.gameObject.AddComponent<TilemapCollider2D>();
        tileMapTest.SetTile(new Vector3Int(0, 0, 0), baseTile);
    }


    public void PlaceTiles()
    {
        //        if (!tileMapTest.gameObject.GetComponent<TilemapCollider2D>()) tileMapTest.gameObject.AddComponent<TilemapCollider2D>();
        for (int i = 0; i < pixelData.textureHeight; i++)
            for (int j = 0; j < pixelData.textureHeight; j++)
            {
                if (pixelData.pixelImage.GetPixel(j, i).a == 1)
                {
                    TileBase tile;

                    if (colorTiles.First(x => x.colorHex == ColorUtility.ToHtmlStringRGBA(pixelData.pixelImage.GetPixel(j, i))).colorTile != null)
                    {
                        tile = colorTiles.First(x => x.colorHex == ColorUtility.ToHtmlStringRGBA(pixelData.pixelImage.GetPixel(j, i))).colorTile;

                        tileMapTest.SetTile(new Vector3Int(j, i, 0), tile);
                    }
                    else
                        continue;
                }
            }
    }


    public IEnumerator GenerateTiles()
    {
        for (int i = 0; i < pixelData.textureHeight; i++)
            for (int j = 0; j < pixelData.textureHeight; j++)
            {
                if (pixelData.pixelImage.GetPixel(j, i).a == 1)
                {
                    TileBase tile;

                    if (colorTiles.First(x => x.colorHex == ColorUtility.ToHtmlStringRGBA(pixelData.pixelImage.GetPixel(j, i))).colorTile != null)
                    {
                        tile = colorTiles.First(x => x.colorHex == ColorUtility.ToHtmlStringRGBA(pixelData.pixelImage.GetPixel(j, i))).colorTile;

                        tileMapTest.SetTile(new Vector3Int(j, i, 0), tile);
                        yield return new WaitForSecondsRealtime(generationSpeed);
                    }
                    else
                        continue;
                }
            }
        yield return null;
    }

    public void ShowPixelValue()
    {
        testPixelColor = testImage.GetPixel(x, y);
    }

}
