using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    [SerializeField, Range(1, 100)]
    private float size = 1;

    [SerializeField, Range(0, 7)]
    private int resolution = 1;

    [SerializeField]
    private bool enableColliders = false;

    [SerializeField]
    private int groundLayer = 1;

    [SerializeField]
    private ObjectGenerator objectGenerator;

    public ShapeSettings shapeSettings;
    public ColorSettings colorSettings;

    [HideInInspector]
    public bool foldOutShapeSetting;

    [HideInInspector]
    public bool foldOutColorSetting;

    private ShapeGenerator shapeGenerator;
    private ColorGenerator colorGenerator;

    private Vector3[] directions = new Vector3[]
    {
        Vector3.up,
        Vector3.right,
        Vector3.down,
        Vector3.back,
        Vector3.left,
        Vector3.forward
    };

    private MeshFilter[] filters;
    private MeshRenderer[] renderers;
    private MeshCollider[] colliders;

    private float currentSize;
    private int currentResolution;

    private void Start()
    {
        GenerateSphere();
    }

    private void OnValidate()
    {
        if (filters == null || filters.Length == 0)
            return;

        shapeGenerator = new ShapeGenerator(shapeSettings);

        if (size != currentSize || resolution != currentResolution)
        {
            for (int i = 0; i < 6; i++)
            {
                filters[i].mesh = SphereGenerator.UpdateSphere(filters[i], shapeSettings.planetSize, resolution, directions[i], shapeGenerator);
            }

            currentResolution = resolution;
            currentSize = size;
        }

        GenerateSphereColors();
        objectGenerator.UpdateItems(filters);
    }

    public void RegenerateColliders()
    {
        int i = 0;

        foreach (MeshFilter filter in filters)
        {
            colliders[i].sharedMesh = filter.mesh;
            i++;
        }
    }

    public void GenerateSphere()
    {
        shapeGenerator = new ShapeGenerator(shapeSettings);
        colorGenerator = new ColorGenerator(colorSettings);

        filters = new MeshFilter[6];
        renderers = new MeshRenderer[6];
        colliders = new MeshCollider[6];

        for (int i = 0; i < 6; i++)
        {
            //Instanciar un game object
            GameObject newFace = new GameObject("Sphere Face " + (i + 1));

            //Añadirle mesh filter y mesh renderer
            filters[i] = newFace.gameObject.AddComponent<MeshFilter>();
            renderers[i] = newFace.gameObject.AddComponent<MeshRenderer>();

            renderers[i].sharedMaterial = colorSettings.sphereMaterial;

            //Colocarlo con hijo de este objeto
            newFace.transform.parent = this.transform;

            //Make it ground layer so the player can jump
            newFace.layer = groundLayer;

            //Generar una malla con esos datos
            SphereGenerator.GenerateSphere(filters[i], shapeSettings.planetSize, resolution, directions[i], shapeGenerator);

            if (enableColliders)
                colliders[i] = newFace.gameObject.AddComponent<MeshCollider>();
        }

        currentResolution = resolution;
        currentSize = size;

        objectGenerator.GenerateItems(filters);

        SetMeshRenderers();

        GenerateSphereColors();
    }

    private void GenerateSphereColors()
    {
        colorGenerator.GenerateElevationColor(shapeGenerator.minMax);
        colorGenerator.UpdateColors();
    }

    private void SetMeshRenderers()
    {
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.sharedMaterial.color = colorSettings.sphereMaterial.color;
        }
    }
}