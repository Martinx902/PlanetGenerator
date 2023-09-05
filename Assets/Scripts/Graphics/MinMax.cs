using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMax
{
    public float max { get; private set; }
    public float min { get; private set; }

    public MinMax()
    {
        max = float.MinValue;
        min = float.MaxValue;
    }

    public void EvaluateValue(float point)
    {
        if (point > max)
        {
            max = point;
        }
        if (point < min)
        {
            min = point;
        }
    }
}