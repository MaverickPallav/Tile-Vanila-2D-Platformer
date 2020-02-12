using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontal;
    float vertical;
    AnimationController anim;
    public float movementSpeed;
    public float climbspeed;
    Rigidbody2D rb;
    public float JumpForce;
    public float gravity;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<AnimationController>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        CheckForanimChange();
    }

    private void CheckForanimChange()
    {
        if (horizontal > 0 || horizontal < 0)
        {
            anim.StartMoving(movementSpeed);
        }
        else if (horizontal == 0)
        {
            anim.StopMoving();
        }
        if (vertical > 0 || vertical < 0)
        {
            anim.StartClimbing(climbspeed);
           
        }
        else if (vertical == 0 )
        {
            anim.StopClimbing();
        }
    }

    private void FixedUpdate()
    {
        if(horizontal >0 || horizontal <0)
        {
            transform.localScale = new Vector2(1*horizontal, 1);
            transform.Translate(Vector2.right*horizontal * movementSpeed * Time.fixedDeltaTime);
            
            rb.gravityScale = gravity;

        } 
        else if(horizontal == 0)
        {
           
            rb.gravityScale = gravity;
        }

        if(vertical > 0 || vertical <0)
        {
            transform.Translate(Vector2.up*vertical * climbspeed * Time.fixedDeltaTime);
            
            rb.gravityScale = 0;
        }
       
        else if(vertical == 0 && horizontal ==0)
        {
            
            rb.gravityScale = 0;
        }

        

      /*  if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, JumpForce));
            if (Input.GetKey(KeyCode.Space) && horizontal != 0)
            {
                rb.AddForce(new Vector2(0, JumpForce));

            }
        }
        */
        
    }
}
