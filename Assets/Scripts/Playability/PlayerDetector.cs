using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField]
    private float sphereRadius = 20f;

    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        CheckForEntities();
    }

    private void CheckForEntities()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, sphereRadius);

        if (hits.Length > 0)
        {
            foreach (Collider collider in hits)
            {
                if (collider.gameObject.transform == null)
                    continue;

                if (collider.gameObject.CompareTag("Player") && !enemy.HasPath())
                {
                    enemy.RequestPath(collider.transform);
                    Debug.Log("Player found");
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.35f);

        Gizmos.DrawSphere(transform.position, sphereRadius);
    }
}