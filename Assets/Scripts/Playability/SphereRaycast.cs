using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereRaycast : MonoBehaviour
{
    public float raycastHeight = 200f; // Step size for each raycast
    public float raycastStep = 0.1f;
    public int positionMultipliyer = 1;

    public bool debugPath = false;

    public GameObject markerPrefab;

    private Vector3 rayDirection;

    public Vector3[] FindPath(Transform startPoint, Transform endPoint)
    {
        Vector3[] waypoints = new Vector3[0];

        List<Vector3> hits = new List<Vector3>();

        if (startPoint.position.y < 0 || endPoint.position.y < 0)
        {
            positionMultipliyer = -1;
            rayDirection = Vector3.up;
        }
        else
        {
            positionMultipliyer = 1;
            rayDirection = Vector3.down;
        }

        Vector3 direction = (endPoint.position - startPoint.position).normalized;  // Direction from start to end point

        float distance = Vector3.Distance(startPoint.position, endPoint.position);  // Distance between start and end point

        RaycastHit hit;

        for (float t = 0; t <= 1; t += raycastStep)  // Divide the distance into smaller steps for raycasting
        {
            Vector3 raycastOrigin = startPoint.position + (direction * (distance * t));
            raycastOrigin.y += raycastHeight * positionMultipliyer;  // Set the height from which the raycast starts

            if (Physics.Raycast(raycastOrigin, rayDirection, out hit, Mathf.Infinity))
            {
                // Instantiate a marker at the hit point
                hits.Add(hit.point);

                if (debugPath)
                    Instantiate(markerPrefab, hit.point, Quaternion.identity);
            }
        }

        waypoints = hits.ToArray();

        return waypoints;
    }
}