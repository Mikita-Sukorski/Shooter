using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))] 
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float rotateSpeed = 10f;

    [SerializeField]
    private float _thrusterForce = 1000f;

    [Header("Spring settings:")]

    [SerializeField]
    [System.Obsolete]
    private JointDriveMode _jointMode = JointDriveMode.Position;

    [SerializeField]
    private float _jointSpring = 20f;

    [SerializeField]
    private float _jointMaxForce = 40f;

    private PlayerMotor motor;

    private ConfigurableJoint _joint;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        _joint = GetComponent<ConfigurableJoint>();

        SetJointSettings(_jointSpring);
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

        Vector3 thrusterForce = Vector3.zero;
        if(Input.GetButton("Jump"))
        {
            thrusterForce = Vector3.up * _thrusterForce;

            SetJointSettings(0f);
        }
        else
        {
            SetJointSettings(_jointSpring);
        }

        motor.ApplyThruster(thrusterForce);
    }

    private void SetJointSettings(float jointSpring)
	{
		_joint.yDrive = new JointDrive { 
			mode = _jointMode, 
			positionSpring = jointSpring, 
			maximumForce = _jointMaxForce
		};
	}
}
