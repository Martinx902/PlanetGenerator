using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    public Transform player { get; private set; }

    public Rigidbody rbPlayer { get; private set; }

    private bool canPull = true;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        rbPlayer = player.GetComponent<Rigidbody>();
    }

    public bool CanPull() => canPull;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (player != null)
            {
                canPull = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (player != null)
            {
                canPull = true;
            }
        }
    }
}