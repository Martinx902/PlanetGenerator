using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    private TargetDetector target;

    [SerializeField]
    private float gravity = -9.81f;

    [SerializeField]
    private float intensity = 10f;

    [SerializeField]
    private Color gravitationalPullColor;

    private static float rotationalForce = 3f;

    private void Start()
    {
        target = GetComponent<TargetDetector>();
    }

    private void FixedUpdate()
    {
        Pull();
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        if (target.CanPull())
        {
            float distanceToPlayer = Vector3.Distance(target.player.position, transform.position);

            Vector3 targetDir = (transform.position - target.player.position).normalized / distanceToPlayer;

            Vector3 bodyUp = -target.player.up;

            Quaternion relative = Quaternion.FromToRotation(bodyUp, targetDir) * target.player.rotation;

            target.player.rotation = Quaternion.RotateTowards(target.player.rotation, relative, rotationalForce);
        }
    }

    private void Pull()
    {
        float distanceToPlayer = Vector3.Distance(target.player.position, transform.position);

        Vector3 pullForce = (target.player.position - transform.position).normalized / distanceToPlayer * intensity;

        target.rbPlayer.AddForce(gravity * pullForce, ForceMode.Force);
    }
}