using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 moveVector;
    public float speed = 2f;
    public float fastspeed = 4f;
    private float realSpeed;
    public int lungeImpulse = 5000;
    public Transform frontCheck;
    private bool isWallFront = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        realSpeed = speed;
    }

    void Update()
    {
        walk();
        run();
        Lunge();
        Jump();
        CheckingGroung();
        

        
    }

    void walk()
    {
        moveVector.x = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveVector.x * realSpeed, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.A))

        {
            Vector3 scale = transform.localScale;
            scale.x = -1;
            transform.localScale = scale;
        }
        if (Input.GetKeyDown(KeyCode.D))

        {
            Vector3 scale = transform.localScale;
            scale.x = 1;
            transform.localScale = scale;
        }

    }

    void run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            realSpeed = fastspeed;
        }
        else
        {
            realSpeed = speed;
        }
    }

    void Lunge()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {


            rb.velocity = new Vector2(0, 0);
            if (rb.transform.localScale.x < 0)
            {
                rb.AddForce(Vector2.left * lungeImpulse);
            }
            else
            {
                rb.AddForce(Vector2.right * lungeImpulse);
            }
        }
    }


    public bool onGround;

    public float JumpForce = 60;
    private bool JumpControl;
    private float JumpTime = 0;
    public float JumpControlTime = 0.3f;

    public int JumpCount = 2;
    public int MomentJumpCount = 0;
    public float DoubleJumpForce = 20;

    

   


    void Jump()
    {
       
        Collider2D[] colliders = Physics2D.OverlapCircleAll(frontCheck.position, 0.1f);
        isWallFront = colliders.Length > 0;
        if (isWallFront == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, -0.5f);
        }

          if (Input.GetKey(KeyCode.Space) && isWallFront)
        {
            if (transform.rotation.y == 0)
            {
                
                rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            }
            else
            {
               
                rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            }
        }

           else if (Input.GetKey(KeyCode.Space))
        {
            if (onGround)
            {
                JumpControl = true;
            }
        }
        else
        {
            JumpControl = false;
        }

        if (JumpControl)
        {
            if ((JumpTime += Time.fixedDeltaTime) < JumpControlTime)
            {
                rb.AddForce(Vector2.up * JumpForce / (JumpTime * 10));
            }
        }
        else
        {
            JumpTime = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !onGround && (++MomentJumpCount < JumpCount))
        {
            rb.velocity = new Vector2(0, DoubleJumpForce);
        }
        if (onGround)
        {
            MomentJumpCount = 0;
        }
    }

    public Transform GroundCheck;
    public float checkRadius = 0.5f;
    public LayerMask Ground; 

    void CheckingGroung()
    {
        onGround = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, Ground);
    }
}
