using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3[] waypoints;

    private SphereRaycast pathfinder;

    public float speed = 1f;
    public float sleepTime = 5f;

    private int currentWaypointIndex = 0;

    private bool path = false;
    private bool sleep = false;

    private void Awake()
    {
        pathfinder = GameObject.FindObjectOfType<SphereRaycast>();
        waypoints = new Vector3[0];
    }

    public bool HasPath() => path;

    private void Update()
    {
        Move();
    }

    public void RequestPath(Transform player)
    {
        if (path || sleep)
            return;

        waypoints = pathfinder.FindPath(transform, player);

        path = true;
    }

    private void Move()
    {
        if (!path || sleep)
            return;

        // Get the current waypoint
        Vector3 currentWaypoint = waypoints[currentWaypointIndex];

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);

        // Check if the enemy has reached the current waypoint
        if (transform.position == currentWaypoint)
        {
            // Move to the next waypoint
            currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Length)
            {
                path = false;
                currentWaypointIndex = 0;

                sleep = true;
                StartCoroutine(SleepCoroutine());
            }
        }
    }

    private IEnumerator SleepCoroutine()
    {
        yield return new WaitForSeconds(sleepTime);

        sleep = false;
    }
}