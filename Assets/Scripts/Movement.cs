using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 moveVector;
    public float speed = 2f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        walk();
        Jump();
        CheckingGroung();
    }

    void walk()
    {
        moveVector.x = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y);
    }

    public float jumpForce = 7f;
    private bool jumpControl;
    private int jumpIteration = 0; 
    public int jumpValueIteration = 60;

    void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (onGround) { jumpControl = true; }
        }
        else { jumpControl = false; }

        if (jumpControl)
        {
            if (jumpIteration++ < jumpValueIteration)
            {
                rb.AddForce(Vector2.up * jumpForce / jumpIteration);
            }
        }
        else { jumpIteration = 0; }
    }
    

    public bool onGround;
    public Transform GroundCheck;
    public float checkRadius = 0.5f;
    public LayerMask Ground; 

    void CheckingGroung()
    {
        onGround = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, Ground);
    }
}
