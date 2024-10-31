using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))] 
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float rotateSpeed = 10f;

    private PlayerMotor motor;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        float xMove = Input.GetAxisRaw ("Horizontal");
        float zMove = Input.GetAxisRaw ("Vertical");

        Vector3 moveHor = transform.right * xMove;
        Vector3 moveVer = transform.forward * zMove;

        Vector3 velocity = (moveHor + moveVer).normalized * speed;

        motor.Move(velocity);

        float yRot = Input.GetAxis("Mouse X");
        Vector3 rotation = new Vector3 (0, yRot, 0) * rotateSpeed;

        motor.Rotate(rotation);

        float xRot = Input.GetAxis("Mouse Y");
        Vector3 camRotation = new Vector3(xRot, 0, 0) * rotateSpeed;

        motor.RotateCam(camRotation);
    }
}
