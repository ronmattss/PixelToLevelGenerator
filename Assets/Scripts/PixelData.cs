using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]    /// <summary>Class that holds Texture (Image) data</summary>
public class PixelData
{
    public Texture2D pixelImage;
    public int textureHeight { get; private set; }
    public int textureWidth { get; private set; }
    public List<Color32> imageColorList = new List<Color32>();
    public HashSet<Color32> pixelColorHash = new HashSet<Color32>();
    public PixelData()
    {
        if (pixelImage != null)
        {
            textureHeight = pixelImage.height;
            textureWidth = pixelImage.width;
        }
    }
    public PixelData(Texture2D image)
    {
        pixelImage = image;
        if (pixelImage != null)
        {
            textureHeight = pixelImage.height;
            textureWidth = pixelImage.width;
        }
    }

    public void LoadPixelColors()
    {
        var temp = pixelImage.GetPixels32().ToList();
        foreach (var pix in temp)
        {
            pixelColorHash.Add(pix);
        }
        imageColorList = pixelColorHash.ToList();
        textureWidth = pixelImage.width;
        textureHeight = pixelImage.height;
    }

}