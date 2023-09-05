using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SphereGenerator
{
    public static Mesh GenerateSphere(MeshFilter filter, float size, int resolution, Vector3 direction, ShapeGenerator shapeGenerator)
    {
        Mesh faceMesh = UpdateSphere(filter, size, resolution, direction, shapeGenerator);

        return faceMesh;
    }

    public static Mesh UpdateSphere(MeshFilter filter, float size, int resolution, Vector3 direction, ShapeGenerator shapeGenerator)
    {
        filter.mesh.Clear();

        Mesh mesh = new Mesh();

        int vertexPerRow = VertexAndTriangles.GetVertexPerRow(resolution);

        mesh.vertices = VertexAndTriangles.GetSphereVertices(size, vertexPerRow, direction, shapeGenerator);

        mesh.triangles = VertexAndTriangles.GetTriangles(resolution, vertexPerRow);

        mesh.RecalculateNormals();

        mesh.name = "Planet Face";

        filter.mesh = mesh;

        return mesh;
    }
}