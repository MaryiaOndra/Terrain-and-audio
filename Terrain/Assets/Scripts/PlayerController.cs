using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform spherePoint;
    [SerializeField] GameObject spherePrefab;
    [SerializeField] Transform verticalTr;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float shootForce;
    [SerializeField] float rechargeTime;

    CharacterController chController;
    Vector3 speedVector;

    float vertivalSpeed;
    float shootTimer;

    void Awake() 
    {
        chController = GetComponent<CharacterController>();
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
        float _vertical = Input.GetAxis("Vertical");
        float _horisontal = Input.GetAxis("Horizontal");

        speedVector = transform.forward * _vertical * speed * Time.deltaTime + transform.right * _horisontal * speed * Time.deltaTime;

    }

    void Jump() 
    {
        vertivalSpeed = chController.velocity.y;
        vertivalSpeed += Physics.gravity.y * Time.deltaTime;

        if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer < 0)
                shootTimer = 0;
        }

        float _jumpAxis = Input.GetAxis("Jump");

        if (_jumpAxis > 0 && chController.isGrounded)
        {
            vertivalSpeed = jumpForce;
        }

        speedVector += transform.up * vertivalSpeed * Time.deltaTime;
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

            shootTimer = rechargeTime;
        }
    }

    bool IsRecharge => shootTimer > 0;

}
