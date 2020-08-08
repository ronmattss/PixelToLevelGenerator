using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]    /// <summary>Class that holds Texture (Image) data</summary>
public class PixelData
{
    public Texture2D pixelImage;
    public Texture2D[] pixelImages;

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
    public void DebugNames()
    {
        foreach (var pix in pixelImages)
        {
            Debug.Log(pix.GetType().FullName);
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
        Debug.Log(nameof(pixelImage));
        var temp = pixelImage.GetPixels32().ToList();
        foreach (var pix in temp)
        {
            if(pix.a==255)
            pixelColorHash.Add(pix);
        }
        imageColorList = pixelColorHash.ToList();
        textureWidth = pixelImage.width;
        textureHeight = pixelImage.height;
    }

}