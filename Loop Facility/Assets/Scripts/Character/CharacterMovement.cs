using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundRadius = 0.25f;
    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _runSpeed = 8f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _jumpHeight = 1.5f;

    private CharacterController _controller;

    private float _verticalVelocity;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) ? _runSpeed : _walkSpeed;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 input = new Vector3(x, 0f, z);
        Vector3 move = Vector3.ClampMagnitude(input, 1f);
        move = transform.TransformDirection(move);

        _controller.Move(move * speed * Time.deltaTime);

        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            _verticalVelocity = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }

        if (IsGrounded())
        {
            if (_verticalVelocity < 0f)
                _verticalVelocity = -2f;
        }

        _verticalVelocity += _gravity * Time.deltaTime;
        _controller.Move(Vector3.up * _verticalVelocity * Time.deltaTime);
    }
    private bool IsGrounded()
    {
        return Physics.CheckSphere(
            _groundCheck.position,
            _groundRadius,
            _groundMask,
            QueryTriggerInteraction.Ignore
        );
    }
}
