using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator
{
    private ShapeSettings shapeSettings;

    private NoiseFilter[] noiseFilters;

    public MinMax minMax;

    public ShapeGenerator(ShapeSettings _shapeSettings)
    {
        this.shapeSettings = _shapeSettings;

        noiseFilters = new NoiseFilter[shapeSettings.noiseLayers.Length];

        for (int i = 0; i < shapeSettings.noiseLayers.Length; i++)
        {
            noiseFilters[i] = new NoiseFilter(shapeSettings.noiseLayers[i].noiseSettings);
        }

        minMax = new MinMax();
    }

    public Vector3 CalculatePointElevation(Vector3 point)
    {
        float elevation = 0;
        float firstLayer = 0;

        if (noiseFilters.Length > 0)
        {
            firstLayer = noiseFilters[0].Evaluate(point);

            if (shapeSettings.noiseLayers[0].enabled)
            {
                elevation = firstLayer;
            }
        }

        for (int i = 1; i < noiseFilters.Length; i++)
        {
            if (shapeSettings.noiseLayers[i].enabled)
            {
                float mask = (shapeSettings.noiseLayers[i].useFirstLayerAsMask) ? firstLayer : 1;
                elevation += noiseFilters[i].Evaluate(point) * mask;
            }
        }

        elevation = shapeSettings.planetSize * (1 + elevation);

        minMax.EvaluateValue(elevation);

        return elevation * point;
    }
}