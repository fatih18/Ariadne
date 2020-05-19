using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Observer
{
    void update(Vector3 mazeOrientation);
}

public class PlayerController : MonoBehaviour, Observer
{
    public float speed = 6.0F;
    public float gravity = 1.0F;

    public Camera cam;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController _controller;

    void Start()
    {
        // Store reference to attached component
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Character is on ground (built-in functionality of Character Controller)
        if (_controller.isGrounded)
        {

            // Use input up and down for direction, multiplied by speed
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _controller.Move(move * Time.deltaTime * speed);
            if (move != Vector3.zero)
                transform.forward = move;
        }
        // Apply gravity manually.
        moveDirection.y -= gravity * Time.deltaTime;
        // Move Character Controller
        _controller.Move(moveDirection * Time.deltaTime);
    }

    public void update(Vector3 MazeOrientation) {
          //TODO Tarik, character movement
    }
}
