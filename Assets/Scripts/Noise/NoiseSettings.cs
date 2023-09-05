using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings
{
    public float strength = 0.5f;

    [Range(1f, 10f)]
    public int layers = 1;

    public float baseRoughness = 1;
    public float roughness = 1;
    public float persistence = 1;
    public Vector3 centre;
    public float minValue;
}