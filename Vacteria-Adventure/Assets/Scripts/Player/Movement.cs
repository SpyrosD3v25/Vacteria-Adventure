using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Public Refrences")]
    public Rigidbody2D body;

    [Header("Public Variables")]
    public float horizontal;

    public float vertical;
    public float moveLimiter = 0.7f; //70%

    public float runSpeed = 20.0f;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.E) && !alib.isMoving)
        {
            alib.isEditMode = !alib.isEditMode;
        }
    }

    private void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0 && !alib.isEditMode)
        {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }
}