using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGenerator
{
    private ColorSettings colorSettings;
    private Texture2D texture;
    private int textureResolution = 200;

    public ColorGenerator(ColorSettings colorSettings)
    {
        this.colorSettings = colorSettings;
        texture = new Texture2D(textureResolution, 1, TextureFormat.ARGB32, false);
    }

    public void GenerateElevationColor(MinMax minMax)
    {
        colorSettings.sphereMaterial.SetVector("_elevation", new Vector4(minMax.min, minMax.max));
    }

    public void UpdateColors()
    {
        Color[] colors = new Color[textureResolution];

        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = colorSettings.sphereGradient.Evaluate(i / (textureResolution - 1f));
        }

        texture.SetPixels(colors);
        texture.Apply();
        colorSettings.sphereMaterial.SetTexture("_texture", texture);
    }
}