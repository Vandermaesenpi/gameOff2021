using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Object references")]
    [SerializeField] Transform cameraTransform;

    [Header("Movement parametters")]
    [SerializeField] float movementSpeed;


    [Header("Look parametters")]
    [SerializeField] float lookSpeed;
    [SerializeField] Vector2 lookVerticalBounds;

    Transform _transform;
    Vector3 _movementDirection = Vector3.zero;
    float _rotY = 0;

    private void Awake() {
        _transform = transform;
    }

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        HandleMovementInput();
        HandleLookDirection();
        Move();
    }

    private void HandleMovementInput()
    {
        Vector3 movementVector = Vector3.zero;

        movementVector += Vector3.forward * Input.GetAxis("Vertical");
        movementVector += Vector3.right * Input.GetAxis("Horizontal");

        _movementDirection = movementVector.normalized;
    }

    private void HandleLookDirection()
    {
        float horizontalLookDelta = Input.GetAxis("Mouse X");
        float horizontalRotation = horizontalLookDelta * lookSpeed * Time.deltaTime;
        _transform.Rotate(0,horizontalRotation,0,Space.Self);

        float verticalLookDelta = Input.GetAxis("Mouse Y");
        float verticalRotation = -verticalLookDelta * lookSpeed * Time.deltaTime;
        _rotY += verticalRotation;
        _rotY = Mathf.Clamp(_rotY, lookVerticalBounds.x, lookVerticalBounds.y);
        cameraTransform.localRotation = Quaternion.Euler(_rotY, 0f, 0f);
    }

    private void Move()
    {
        _transform.position += _transform.TransformDirection(_movementDirection) * Time.deltaTime * movementSpeed; 
    }

}
