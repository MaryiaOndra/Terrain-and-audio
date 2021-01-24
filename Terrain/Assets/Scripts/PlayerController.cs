using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform spherePoint;
    [SerializeField] GameObject spherePrefab;
    [SerializeField] Transform verticalTr;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float shootForce;
    [SerializeField] float rechargeTime;

    CharacterController chController;
    SoundPlayer soundPlayer;
    Vector3 speedVector;

    float verticalSpeed;
    float shootTimer;
    float moveSpeed;

    void Awake() 
    {
        chController = GetComponent<CharacterController>();
        soundPlayer = GetComponent<SoundPlayer>();
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        ControlMouse();
        Move();
        Jump();
        Shoot();

        chController.Move(speedVector);
    }

    void ControlMouse()
    {
        float _mouseX = Input.GetAxis("Mouse X");
        float _mouseY = Input.GetAxis("Mouse Y") * -1;

        chController.transform.Rotate(Vector3.up, _mouseX);
        verticalTr.Rotate(Vector3.right, _mouseY, Space.Self);
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed;
            soundPlayer.Run();
        }
        else
        {
            moveSpeed = walkSpeed;
            soundPlayer.Walk();
        }

        float _vertical = Input.GetAxis("Vertical");
        float _horisontal = Input.GetAxis("Horizontal");

        speedVector = transform.forward * _vertical * moveSpeed * Time.deltaTime + transform.right * _horisontal * moveSpeed * Time.deltaTime;
    }

    void Jump() 
    {
        verticalSpeed = chController.velocity.y;
        verticalSpeed += Physics.gravity.y * Time.deltaTime;

        if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer < 0)
                shootTimer = 0;
        }

        float _jumpAxis = Input.GetAxis("Jump");

        if (_jumpAxis > 0 && chController.isGrounded)
        {
            verticalSpeed = jumpForce;
        }

        speedVector += transform.up * verticalSpeed * Time.deltaTime;
    }

    void Shoot()
    {
        float _fireAxis = Input.GetAxis("Fire1");
        if (_fireAxis > 0 && !IsRecharge)
        { 
            Rigidbody _sphereRgBd = Instantiate(spherePrefab).GetComponent<Rigidbody>();
            _sphereRgBd.transform.position = spherePoint.position;
            _sphereRgBd.transform.rotation = spherePoint.rotation;

            _sphereRgBd.AddForce(_sphereRgBd.transform.forward * shootForce, ForceMode.Impulse);

            soundPlayer.Shoot();

            shootTimer = rechargeTime;
        }
    }

    bool IsRecharge => shootTimer > 0;

}
