using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VertexAndTriangles
{
    /// <summary>
    /// Gets the total number of vertices per row
    /// </summary>
    /// <param name="resolution"></param>
    /// <returns></returns>
    public static int GetVertexPerRow(int resolution)
    {
        int vertexCount = 2;

        for (int i = 0; i < resolution; i++)
        {
            vertexCount += (int)Mathf.Pow(2, i);
        }

        return vertexCount;
    }

    /// <summary>
    /// Generates the mesh triangles based on resolution
    /// </summary>
    /// <param name="resolution"></param>
    /// <param name="vertexPerRow"></param>
    /// <returns></returns>
    public static int[] GetTriangles(int resolution, int vertexPerRow)
    {
        int totalVertices = vertexPerRow * vertexPerRow;

        int spaceNeededToStoreATriangle = 6;

        int trianglesNumber = (2 * ((int)Mathf.Pow(2, 2 * resolution))) * 3;

        int[] triangles = new int[trianglesNumber * spaceNeededToStoreATriangle];

        int triangleIndex = 0;

        for (int i = 0; triangleIndex < (totalVertices - vertexPerRow); i += 6)
        {
            //Skip the last vertice of the row

            if (i != 0 && (triangleIndex + 1) % vertexPerRow == 0)
            {
                triangleIndex++;
                continue;
            }

            triangles[i] = triangleIndex + vertexPerRow + 1;
            triangles[i + 1] = triangleIndex + vertexPerRow;
            triangles[i + 2] = triangleIndex;

            triangles[i + 3] = triangleIndex + 1;
            triangles[i + 4] = triangleIndex + vertexPerRow + 1;
            triangles[i + 5] = triangleIndex;

            triangleIndex++;
        }

        //Resolution of 0

        //triangles[0] = 3;
        //triangles[1] = 2;
        //triangles[2] = 0;

        //triangles[3] = 1;
        //triangles[4] = 3;
        //triangles[5] = 0;

        return triangles;
    }

    /// <summary>
    /// Generates the array of vertices needed to create a sphere
    /// </summary>
    /// <param name="size"></param>
    /// <param name="vertexPerRow"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static Vector3[] GetSphereVertices(float size, int vertexPerRow, Vector3 direction, ShapeGenerator shapeGenerator)
    {
        Vector3[] vertices = new Vector3[vertexPerRow * vertexPerRow];

        Vector3 localUpDirection = direction;
        Vector3 Dx = new Vector3(localUpDirection.y, localUpDirection.z, localUpDirection.x);
        Vector3 Dy = Vector3.Cross(localUpDirection, Dx);

        for (int i = 0; i < vertexPerRow; i++)
        {
            for (int j = 0; j < vertexPerRow; j++)
            {
                float yVertexPercent = (float)i / (vertexPerRow - 1);
                float xVertexPercent = (float)j / (vertexPerRow - 1);

                vertices[j + i * vertexPerRow] = localUpDirection + (xVertexPercent - 0.5f) * 2 * Dx + (yVertexPercent - 0.5f) * 2 * Dy;
                vertices[j + i * vertexPerRow] = vertices[j + i * vertexPerRow].normalized;
                //vertices[j + i * vertexPerRow] = SetVertexNormals(vertices[j + i * vertexPerRow]);
                vertices[j + i * vertexPerRow] = shapeGenerator.CalculatePointElevation(vertices[j + i * vertexPerRow]);
            }
        }

        return vertices;
    }

    public static Vector3 SetVertexNormals(Vector3 v)
    {
        Vector3 s = new Vector3();

        float x2 = v.x * v.x;
        float y2 = v.y * v.y;
        float z2 = v.z * v.z;

        s.x = v.x * Mathf.Sqrt(1f - y2 / 2f - z2 / 2f + y2 * z2 / 3f);
        s.y = v.y * Mathf.Sqrt(1f - x2 / 2f - z2 / 2f + x2 * z2 / 3f);
        s.z = v.z * Mathf.Sqrt(1f - x2 / 2f - y2 / 2f + x2 * y2 / 3f);

        return s;
    }
}