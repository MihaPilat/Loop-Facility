using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float _sensitivity=0.1f;
    [SerializeField] private Transform _character;

    private float _xRotation;

    private float _mouseX;
    private float _mouseY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        _mouseX = Input.GetAxis("Mouse X") * _sensitivity * 100f * Time.deltaTime;
        _mouseY = Input.GetAxis("Mouse Y") * _sensitivity * 100f * Time.deltaTime;

        _character.Rotate(Vector3.up * _mouseX);
    }

    void LateUpdate()
    {
        _xRotation -= _mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -80f, 80f);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
    }
}
