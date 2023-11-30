using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;

    public Transform targetTransform;
    public Transform cameraTransform;
    public LayerMask collisionLayers;
    [SerializeField] private Transform cameraPivot;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    private float defaultPosition;
    public float cameraFollowSpeed = 0.15f;
    public float cameraCollisionRadius = 0.2f;
    public float cameraCollisionOffset = 0.2f; // how much the camera will jump off the collision object
    private Vector3 cameraVectorPosition;
    public float minimumCollisionOffset = 0.2f;
    public float lookAngle = 0f;
    public float pivotAngle = 0f;

    public float cameraLookSpeed = 1.25f;
    public float cameraPivotSpeed = 1.25f;

    private float minimumPivotAngle = -35f;
    private float maximumPivotAngle = 35f;

    private void Awake()
    {
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        //cameraPivot = transform.GetChild(0);
        inputManager = FindObjectOfType<InputManager>();
        cameraTransform = Camera.main.transform;
        defaultPosition = cameraTransform.localPosition.z;
    }
    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
        HandleCameraCollisions();
    }
    private void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);
        transform.position = targetPosition;

    }
    private void RotateCamera()
    {
        lookAngle = lookAngle + (inputManager.cameraInputX * cameraLookSpeed);
        pivotAngle = pivotAngle - (inputManager.cameraInputY * cameraPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivotAngle, maximumPivotAngle);

        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;


    }

    private void HandleCameraCollisions()
    {
        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();

        if (Physics.SphereCast(cameraPivot.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), collisionLayers))
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition = -(distance - cameraCollisionOffset);
        }

        if (Mathf.Abs(targetPosition) < minimumCollisionOffset)
        {
            targetPosition = targetPosition - minimumCollisionOffset;
        }

        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 2f);
        cameraTransform.localPosition = cameraVectorPosition;

    }
}

