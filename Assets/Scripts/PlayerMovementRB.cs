using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementRB : MonoBehaviour
{
    Rigidbody2D rb;
    CapsuleCollider2D PlayerBodyCollider;
    BoxCollider2D PlayerLegsCollider;
    AnimationController anim;
    public float jumpforce;
    public float movementspeed;
    public float Climbspeed;
    float GravityatStart;
    //private float offsetpos = 0.04f;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<AnimationController>();
        PlayerBodyCollider = GetComponent<CapsuleCollider2D>();
        PlayerLegsCollider = GetComponent<BoxCollider2D>();
        GetComponent<Animator>().SetBool("ClimbIdle", false);
        GravityatStart = rb.gravityScale;
    }
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlippingPlayer();
        handlejump();
        ClimbingLadder();
    }

    private void ClimbingLadder()
    {
        if(PlayerLegsCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            
            GetComponent<Animator>().SetBool("LadderPresent", true);
            if(rb.velocity.x == 0)
            { anim.StopMoving(); }
           
            float vertical = Input.GetAxis("Vertical"); // this value lies between -1 to 1
            Debug.Log(vertical);

            //setting climbidle animation true or false
            if (rb.velocity.y == 0 )
            {
                GetComponent<Animator>().SetBool("ClimbIdle", true);
            }
            else
            {
                GetComponent<Animator>().SetBool("ClimbIdle", false);
            }

            
           /* if (rb.velocity.y == 0 && mycollider.IsTouchingLayers(LayerMask.GetMask("Ladder")) && mycollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                this.gameObject.GetComponent<Transform>().position =
                    new Vector2(transform.position.x + Mathf.Sign(rb.velocity.x), transform.position.y);

                Vector2 playerVelocityWhileClimbing = new Vector2(0, Climbspeed * vertical);
                rb.velocity = playerVelocityWhileClimbing;
                GetComponent<Animator>().SetBool("ClimbIdle", true);
            } */

            Vector2 playervelocity = new Vector2(rb.velocity.x, Climbspeed * vertical);
            anim.StartClimbing(Mathf.Abs(vertical));
            rb.velocity = playervelocity;
            rb.gravityScale = 0;
        }
        else
        {
            GetComponent<Animator>().SetBool("LadderPresent", false);

           
           /* if (rb.velocity.y == 0 && mycollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                GetComponent<Animator>().SetBool("ClimbIdle", false);
            } */
            GetComponent<Animator>().SetBool("ClimbIdle", false);
            anim.StopClimbing();
            rb.gravityScale = GravityatStart;
        }
    }


    private void handlejump()
    {
        if (PlayerLegsCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || PlayerLegsCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Vector2 JumpvelocitytoAdd = new Vector2(0f, jumpforce);
                rb.velocity += JumpvelocitytoAdd;
                // rb.gravityScale = 50;
            }
        }
    }

    private void Run()
    {
        float horizontal = Input.GetAxis("Horizontal"); // this value lies between -1 to +1
        
        Vector2 playervelocity = new Vector2(horizontal*movementspeed, rb.velocity.y);
        rb.velocity = playervelocity;
        
    }

    private void FlippingPlayer()
    {
        bool PlayerhasHorizontalmovement = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        if(PlayerhasHorizontalmovement)
        {
           
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1);
            anim.StartMoving(Mathf.Abs(rb.velocity.x));
        }
        else
        {
            anim.StopMoving();
        }
    }
}
