//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 04/11/2022

using System.Collections;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    #region Inspector Variables

    [Header("Player Transform")]
    [Space(15)]
    [SerializeField]
    private Transform target;

    [Header("Smooth Damp Value")]
    [Space(15)]
    [SerializeField]
    [Range(0f, 1f)]
    private float smoothTime = 0.25f;

    #endregion Inspector Variables

    private Vector3 offset;

    private Vector3 currentVelocity = Vector3.zero;

    private void Awake()
    {
        //Gets the offset distance between player and camera
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        Vector3 targetPosition = target.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }
}