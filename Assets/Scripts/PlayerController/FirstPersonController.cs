using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivityX = 250f;

    [SerializeField]
    private float mouseSensitivityY = 250f;

    [SerializeField]
    private float walkSpeed = 10f;

    [SerializeField]
    private float jumpForce = 10f;

    [SerializeField]
    private float smoothMovementTime = 0.1f;

    [SerializeField]
    private LayerMask groundedMask;

    private Transform mainCamera;

    private Vector3 moveAmount;

    private bool grounded;

    private Vector3 currentVelocity = Vector3.zero;
    private float verticalLookRotation;

    private Rigidbody rb;

    // Start is called before the first frame update
    private void Start()
    {
        mainCamera = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        LookAt();
        GatherInput();
        Jump();
    }

    private void GatherInput()
    {
        //Get the input movement
        float inputX = Input.GetAxisRaw("Horizontal");

        float inputY = Input.GetAxisRaw("Vertical");

        Vector3 input = new Vector3(inputX, 0, inputY).normalized;

        Vector3 targetMovement = input * walkSpeed;

        //Move towards that input position smoothing it
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMovement, ref currentVelocity, smoothMovementTime);
    }

    private void FixedUpdate()
    {
        //Then we have to change the movePosition to local space and add it to the rb previous position
        Vector3 targetDir = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;

        //Move the rb position
        rb.MovePosition(rb.position + targetDir);
    }

    private void Jump()
    {
        //Jump
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            }
        }

        // Grounded check
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1 + .1f, groundedMask))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    private void LookAt()
    {
        #region FPC

        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);

        verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;

        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60, 60);

        mainCamera.localEulerAngles = Vector3.left * verticalLookRotation;

        #endregion FPC

        #region TPC

        ////Rotation of the player towards the movement

        //float targetAngle = Mathf.Atan2(moveAmount.x, moveAmount.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;

        //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref tempAngleVelocity, angleSmoothTime);

        //transform.rotation = Quaternion.Euler(0f, angle, 0f);

        ////Facing and moving the player towards the direction of the camera and the one selected

        //moveAmount = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        #endregion TPC
    }
}