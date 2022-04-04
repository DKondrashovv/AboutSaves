using System;
using Newtonsoft.Json;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static event Action<int> OnWallCollision;
    private int health=100;
    private float speed = 7f;
    private float jumpSpeed = 10f;
    private float gravity = 20.0f;
    private Vector3 MoveDir = Vector3.zero;
    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        //Если играть без кнопок, то убрать комент

        // Cursor.visible = false; 
        // Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (controller.isGrounded)
        {
            MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            MoveDir = transform.TransformDirection(MoveDir);
            MoveDir *= speed;
        }

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            MoveDir.y = jumpSpeed;
        }

        MoveDir.y -= gravity * Time.deltaTime;
        controller.Move(MoveDir * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.collider.CompareTag("Wall"))
        {
            health--;
            OnWallCollision?.Invoke(health);

        }
    }
}