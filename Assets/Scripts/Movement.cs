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


   
   






}
