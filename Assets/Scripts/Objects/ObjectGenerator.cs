using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectGenerator : MonoBehaviour
{
    [SerializeField, Range(1f, 2f)]
    private float verticalOffset;

    [SerializeField]
    private GameObject[] items;

    [SerializeField]
    private bool random = false;

    private Vector3[] midPoints = new Vector3[6];
    private List<GameObject> spawnedItems = new List<GameObject>();

    public UnityEvent<int> onObjectGeneration;

    private int coinsGenerated = 0;

    private Vector3 GetMidVertice(MeshFilter mesh)
    {
        Vector3 midPoint;

        if (!random)
            midPoint = mesh.sharedMesh.vertices[mesh.sharedMesh.vertices.Length / 2];
        else
            midPoint = mesh.sharedMesh.vertices[Random.Range(0, mesh.sharedMesh.vertices.Length)];

        midPoint *= verticalOffset;

        return midPoint;
    }

    public void GenerateItems(MeshFilter[] meshArray)
    {
        midPoints = new Vector3[meshArray.Length];

        for (int i = 0; i < meshArray.Length; i++)
        {
            midPoints[i] = GetMidVertice(meshArray[i]);
            GameObject newItem = Instantiate(items[i], midPoints[i], Quaternion.identity);

            if (newItem.CompareTag("Coin"))
                coinsGenerated++;

            newItem.transform.parent = this.transform;
            spawnedItems.Add(newItem);
        }

        onObjectGeneration.Invoke(coinsGenerated);
    }

    public void UpdateItems(MeshFilter[] meshArray)
    {
        for (int i = 0; i < meshArray.Length; i++)
        {
            items[i].transform.position = GetMidVertice(meshArray[i]);
        }
    }
}