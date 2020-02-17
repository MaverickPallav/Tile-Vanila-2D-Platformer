using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementRB : MonoBehaviour
{
    [Header("CacheReferences")]
    Rigidbody2D rb;
    [HideInInspector] public CapsuleCollider2D PlayerBodyCollider;
    BoxCollider2D PlayerLegsCollider;
    AnimationController anim;
    AudioSource audiosrc;

    [Header("PlayerMovement")]
    public float jumpforce;
    public float movementspeed;
    public float Climbspeed;
    public Vector2 deathkick = new Vector2(10f, 25f);
    float GravityatStart;

    [Header("BoolCache")]
    bool isAlive = true;

    [Header("PlayerAudio")]
    public AudioClip jumpaudio;
    public AudioClip deathaudio;
    //private float offsetpos = 0.04f;

    private void Awake()
    {
        audiosrc = GetComponent<AudioSource>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<AnimationController>();
        PlayerBodyCollider = GetComponent<CapsuleCollider2D>();
        PlayerLegsCollider = GetComponent<BoxCollider2D>();
        GetComponent<Animator>().SetBool("ClimbIdle", false);
        GravityatStart = rb.gravityScale;
        StartCoroutine(DisbleInstuctionsLabel());
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive)
        {
            Run();
            FlippingPlayer();
            handlejump();
            ClimbingLadder();
            PlayerDeath();
        }
        else
        {
            return;
        }   
    }

    IEnumerator DisbleInstuctionsLabel()
    {
        yield return new WaitForSeconds(5f);
        FindObjectOfType<GameSession>().DisableInstructionsLabel();
        FindObjectOfType<GameSession>().ShowScoreLabel();
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

            // Need to Work on this piece of Code to improve player climbing Movement in Future
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
            // Need to Work on this piece of Code to improve player climbing Movement in Future
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
        if (PlayerLegsCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AudioSource.PlayClipAtPoint(jumpaudio, Camera.main.transform.position, 0.4f);
                Vector2 JumpvelocitytoAdd = new Vector2(0f, jumpforce);
                rb.velocity += JumpvelocitytoAdd;
                // rb.gravityScale = 50;
            }
        }
    }

    private void Run()
    {
        
       
        float horizontal = Input.GetAxis("Horizontal"); // this value lies between -1 to +1
        if(horizontal != 0)
        {
            FindObjectOfType<GameSession>().DisableInstructionsLabel();
            FindObjectOfType<GameSession>().ShowScoreLabel();
        }
        
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
     
    private void PlayerDeath()
    {

        if (PlayerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")) 
            || PlayerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")) 
            || PlayerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Lava")))
        {

            StartCoroutine(MainAudio());
            FindObjectOfType<GameSession>().OnPlayerDeath();
            isAlive = false;
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<Rigidbody2D>().velocity = deathkick;
           // FindObjectOfType<GameSession>().ProcessPlayerDeath();
            Destroy(this.gameObject, 2f);
        }
    }
 
    IEnumerator MainAudio()
    {
        audiosrc.volume = 0.02f;
        AudioSource.PlayClipAtPoint(deathaudio, Camera.main.transform.position, 0.4f);
        yield return new WaitForSeconds(10f);
        audiosrc.Stop();
    }
}
