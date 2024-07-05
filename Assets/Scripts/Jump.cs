using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpForce = 5.0f;

    public bool isGround;
    public float rayDistance = 0.6f;

    public bool doubleJump = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.down, rayDistance, LayerMask.GetMask("Ground"));

        if (hit.collider != null)
        {
            isGround = true;
            doubleJump = false;
        }
        else
        {
            isGround = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGround)
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
            else if (!doubleJump && rb.velocity.y < 0)
            {
                doubleJump = true;
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }    
    }
}

