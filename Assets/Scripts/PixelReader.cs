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
[Serializable]
public struct TargetTileGrid
{
    public Tilemap[] targetTilemap;
    public ListWrapper<List<ColorTile>> listOfColorTile;
}
[ExecuteInEditMode]
public class PixelReader : MonoBehaviour
{
    //Learn while we create this Pixel to Map Generator 

    //first Step: read an image pixels
    public Texture2D backgroundImage;
    public Texture2D platformImage;
    public Texture2D foregroundImage;
    public TilemapHolder tiles;
    public PixelData pixelData = new PixelData();
    public int x = 0;
    public int y = 0;
    public List<ColorTile> colorTiles;
    //ALl Tile Properties
    [Tooltip("This is the grid where you place Tiles")]

    public Grid targetTileGrid;
    public Tilemap tileMap;
    [NonSerialized] public Color32 testPixelColor;
    public float generationSpeed = 0.001f;
    [SerializeField] List<Texture2D> referenceTexture = new List<Texture2D>();
    [SerializeField] List<Tilemap> tilemapsToBePainted = new List<Tilemap>();
    [SerializeField] List<ColorTileHolder> tileHolders;
    public void DebugRGBA()
    {
        testPixelColor = platformImage.GetPixel(x, y);
        Debug.Log(ColorUtility.ToHtmlStringRGBA(testPixelColor).ToString());
    }
    public void LoadColorPalette()
    {
        pixelData = new PixelData(platformImage);

        pixelData.LoadPixelColors();
        ColorTile temp = new ColorTile();
        foreach (var pix in pixelData.imageColorList)
        {
            temp.colorHex = ColorUtility.ToHtmlStringRGBA(pix).ToString();
            temp.colorVal = pix;
            colorTiles.Add(temp);
        }
    }
    public void LoadColorPaletteSet()
    {
        pixelData = new PixelData();
        List<Texture2D> imageSet = new List<Texture2D>();
        imageSet.Add(backgroundImage);
        imageSet.Add(platformImage);
        imageSet.Add(foregroundImage);
        pixelData.pixelImages = imageSet.ToArray();
        Debug.Log("image counts: " + imageSet.Count);
        for (int i = 0; i < imageSet.Count; i++)
        {
            tiles.tileWrapper.listOfColorTileHolder.Add(new ColorTileHolder());
        }

        Debug.Log("ListCount: " + tiles.tileWrapper.listOfColorTileHolder.Count);
        for (int i = 0; i < imageSet.Count; i++)
        {

            pixelData.pixelImage = imageSet[i];
            pixelData.pixelColorHash = new HashSet<Color32>();
            pixelData.LoadPixelColors();
            Debug.Log("pixel hash count: " + pixelData.pixelColorHash.Count);
            ColorTile temp = new ColorTile();
            foreach (var pix in pixelData.imageColorList)
            {
                temp.colorHex = ColorUtility.ToHtmlStringRGBA(pix).ToString();
                temp.colorVal = pix;
                //                tiles.tileWrapper.listOfColorTileHolder[i].colorTiles = new List<ColorTile>(pixelData.imageColorList.Count);
                Debug.Log("colorTile set count: " + tiles.tileWrapper.listOfColorTileHolder[i].colorTiles.Capacity);
                tiles.tileWrapper.listOfColorTileHolder[i].colorTiles.Add(temp);
                Debug.Log(tiles.tileWrapper.listOfColorTileHolder[i].colorTiles.Count);

            }
        }
    }



    public void PlaceTiles(Tilemap currentlyTilePainting)
    {// check if a tilemap exist
     // if none then create a new  tilemap

        //        if (!tileMap.gameObject.GetComponent<TilemapCollider2D>()) tileMap.gameObject.AddComponent<TilemapCollider2D>();
        for (int i = 0; i < pixelData.textureHeight; i++)
            for (int j = 0; j < pixelData.textureHeight; j++)
            {
                if (pixelData.pixelImage.GetPixel(j, i).a == 1)
                {
                    TileBase tile;

                    if (colorTiles.First(x => x.colorHex == ColorUtility.ToHtmlStringRGBA(pixelData.pixelImage.GetPixel(j, i))).colorTile != null)
                    {
                        tile = colorTiles.First(x => x.colorHex == ColorUtility.ToHtmlStringRGBA(pixelData.pixelImage.GetPixel(j, i))).colorTile;

                        currentlyTilePainting.SetTile(new Vector3Int(j, i, 0), tile);
                    }
                    else
                        continue;
                }
            }
    }
    public void DebugProperties()
    {
        Debug.Log("Texture Count: " + pixelData.pixelImages.Length);
        Debug.Log("Color Tile List  Count: " + tiles.tileWrapper.listOfColorTileHolder.Count);
        Debug.Log("Color Tile List ColorTile in index 0 Count: " + tiles.tileWrapper.listOfColorTileHolder[0].colorTiles.Count);
        for (int i = 0; i < pixelData.pixelImages.Length; i++)
        {// same as tilemaps
            // iterate to each HxW
            for (int j = 0; j < pixelData.pixelImages[i].height; j++)
            {
                for (int l = 0; l < pixelData.pixelImages[i].width; j++)
                {
                    if (pixelData.pixelImages[l].GetPixel(j, i).a == 1)
                    {

                    }

                }
            }

        }
    }

    public void PaintTilemap(Tilemap currentTilemap, List<ColorTile> pixelToTileList, Texture2D currentImage)
    {
        Texture2D reference = currentImage;
        int height = currentImage.height;
        int width = currentImage.width;
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                TileBase tile;
                if (reference.GetPixel(j, i).a == 1)
                {
                    Debug.Log("WHy ios not working");
                    if (pixelToTileList.First(x => x.colorHex == ColorUtility.ToHtmlStringRGBA(reference.GetPixel(j, i))).colorTile != null)
                    {
                        tile = pixelToTileList.First(x => x.colorHex == ColorUtility.ToHtmlStringRGBA(reference.GetPixel(j, i))).colorTile;
                        currentTilemap.SetTile(new Vector3Int(j, i, 0), tile);
                    }

                }

            }
    }
    public IEnumerator PaintTilemapProcedurally(Tilemap currentTilemap, List<ColorTile> pixelToTileList, Texture2D currentImage)
    {
        Texture2D reference = currentImage;
        int height = currentImage.height;
        int width = currentImage.width;
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                TileBase tile;
                if (reference.GetPixel(j, i).a == 1)
                {
                    Debug.Log("WHy ios not working");
                    if (pixelToTileList.First(x => x.colorHex == ColorUtility.ToHtmlStringRGBA(reference.GetPixel(j, i))).colorTile != null)
                    {
                        tile = pixelToTileList.First(x => x.colorHex == ColorUtility.ToHtmlStringRGBA(reference.GetPixel(j, i))).colorTile;
                        currentTilemap.SetTile(new Vector3Int(j, i, 0), tile);
                        yield return new WaitForSecondsRealtime(generationSpeed);
                    }

                }

            }
        yield return null;
    }
    public void SetupTiles()
    {
        //unload here
        List<Texture2D> imageSet = new List<Texture2D>();
        imageSet.Add(backgroundImage);
        imageSet.Add(platformImage);
        imageSet.Add(foregroundImage);
        tilemapsToBePainted = tiles.tilemaps;
        tileHolders = tiles.tileWrapper.listOfColorTileHolder;
        for (int i = 0; i < tilemapsToBePainted.Count; i++)
        {
            PaintTilemap(tilemapsToBePainted[i], tileHolders[i].colorTiles, imageSet[i]);
        }

    }
    public void PlaceTiles(Tilemap currentlyTilePainting, List<ColorTile> tilesPerPixel)
    {// check if a tilemap exist
     // if none then create a new  tilemap

        //        if (!tileMap.gameObject.GetComponent<TilemapCollider2D>()) tileMap.gameObject.AddComponent<TilemapCollider2D>();
        for (int l = 0; l < pixelData.pixelImages.Length; l++)
        {
            for (int i = 0; i < pixelData.pixelImages[l].width; i++)
                for (int j = 0; j < pixelData.pixelImages[l].height; j++)
                {
                    if (pixelData.pixelImages[l].GetPixel(j, i).a == 1)
                    {
                        TileBase tile;

                        if (tilesPerPixel.First(x => x.colorHex == ColorUtility.ToHtmlStringRGBA(pixelData.pixelImages[l].GetPixel(j, i))).colorTile != null)
                        {
                            tile = tilesPerPixel.First(x => x.colorHex == ColorUtility.ToHtmlStringRGBA(pixelData.pixelImages[l].GetPixel(j, i))).colorTile;
                            Debug.Log(tile.name);
                            currentlyTilePainting.SetTile(new Vector3Int(j, i, 0), tile);
                        }
                        //else
                        //continue;
                    }
                }
        }
    }

    //one grid nTilemaps
    //

    public void PlaceTilesPerLayer()
    {
        // check if list contains something 
        foreach (var tilemap in tiles.tilemaps)
        {
            foreach (var colorTile in tiles.tileWrapper.listOfColorTileHolder)
            {
                Debug.Log("ColorTiles: " + colorTile.colorTiles[0].colorHex);
                PlaceTiles(tilemap, colorTile.colorTiles);
            }
        }
    }

    public void PlaceTilesProperly()
    {
        List<Texture2D> imageSet = new List<Texture2D>();
        imageSet.Add(backgroundImage);
        imageSet.Add(platformImage);
        imageSet.Add(foregroundImage);
        tilemapsToBePainted = tiles.tilemaps;
        tileHolders = tiles.tileWrapper.listOfColorTileHolder;
        for (int i = 0; i < tilemapsToBePainted.Count; i++)
        {
            StartCoroutine(PaintTilemapProcedurally(tilemapsToBePainted[i],tileHolders[i].colorTiles,imageSet[i]));
            StopAllCoroutines();
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

                        tileMap.SetTile(new Vector3Int(j, i, 0), tile);
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
        testPixelColor = platformImage.GetPixel(x, y);
        GameObject go = new GameObject();
        go.transform.position = Vector3.zero;

    }

}
