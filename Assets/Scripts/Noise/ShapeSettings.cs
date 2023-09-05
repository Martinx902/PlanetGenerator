using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShapeSettings : ScriptableObject
{
    public float planetSize = 1;

    public NoiseLayer[] noiseLayers;

    [System.Serializable]
    public class NoiseLayer
    {
        public bool useFirstLayerAsMask = true;
        public bool enabled = true;
        public NoiseSettings noiseSettings;
    }
}