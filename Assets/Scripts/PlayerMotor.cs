using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    private Rigidbody _body;

    private Vector3 _velocity = Vector3.zero;

    private Vector3 _rotation = Vector3.zero;
    private Vector3 _rotationCamera = Vector3.zero;

    private Vector3 _thrusterForce = Vector3.zero;

    void Start()
    {
        _body = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 velocity)
    {
        _velocity = velocity;
    }
    public void Rotate(Vector3 rotation)
    {
        _rotation = rotation;
    }

    public void RotateCam(Vector3 rotationCamera)
    {
        _rotationCamera = rotationCamera;
    }

    public void ApplyThruster(Vector3 thrusterForce)
    {
        _thrusterForce = thrusterForce;
    }

    void FixedUpdate()
    {
        PerformMove();
        PerformRotation();
    }

    void PerformMove()
    {
        if(_velocity != Vector3.zero)
        {
            _body.MovePosition(_body.position + _velocity * Time.fixedDeltaTime);
        }

        if (_thrusterForce != Vector3.zero)
        {
            _body.AddForce(_thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

    void PerformRotation()
    {
        _body.MoveRotation(_body.rotation * Quaternion.Euler(_rotation));

        if (_camera != null)
        {
            float currentCameraRotationX = _camera.transform.localEulerAngles.x;
            float desiredCameraRotationX = currentCameraRotationX - _rotationCamera.x;

            if (desiredCameraRotationX > 180)
                desiredCameraRotationX -= 360;

            desiredCameraRotationX = Mathf.Clamp(desiredCameraRotationX, -75f, 75f);

            _camera.transform.localEulerAngles = new Vector3(desiredCameraRotationX, _camera.transform.localEulerAngles.y, _camera.transform.localEulerAngles.z);
        }
    }


}
