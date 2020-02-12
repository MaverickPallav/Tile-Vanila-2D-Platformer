using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;
using System;

public class VoiceMovement : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    public float movementspeed;
    public float jumpforce;
    bool movementright = false;
    bool movementleft = false;
    bool movementjump = false;
    bool movementup = false;
    bool movementdown = false;
    bool playerShoot = false;
    bool playerStop = false;

    Rigidbody2D rb;
    Collider2D mycollider;
    AnimationController anim;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mycollider = GetComponent<Collider2D>();
        anim = GetComponent<AnimationController>();
    }

    private void Start()
    {

        actions.Add("right", Moveright);
        actions.Add("left", MoveLeft);
        actions.Add("jump", JumpMovement);
        actions.Add("down", downMovement);
        actions.Add("up", upMovement);
        actions.Add("stop", StopPlayer);
        actions.Add("shoot", PlayerShoot);

        // system. linq is used to use toarray()

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedVoice;
        keywordRecognizer.Start();

        foreach (var device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
        }
        // var microphonedevicenumber = Microphone.devices.Length;
    }

    private void RecognizedVoice(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void Moveright()
    {
        movementleft = false;
        movementright = true;
        movementdown = false;
        movementup = false;
        playerShoot = false;
        playerStop = false;
       

    }

    private void MoveLeft()
    {

        movementleft = true;
        movementright = false;
        movementdown = false;
        movementup = false;
        playerShoot = false;
        playerStop = false;

    }

    private void JumpMovement()
    {
        
        movementjump = true;
        movementdown = false;
        movementup = false;
        playerShoot = false;
        playerStop = false;

        
    }
    private void downMovement()
    {

        movementleft = false;
        movementjump = false;
        movementright = false;
        movementdown = true;
        movementup = false;
        playerShoot = false;
        playerStop = false;

    }

    private void upMovement()
    {
        movementleft = false;
        movementjump = false;
        movementright = false;
        movementdown = false;
        movementup = true;
        playerShoot = false;
        playerStop = false;
    }

    private void PlayerShoot()
    {
        movementleft = false;
        movementjump = false;
        movementright = false;
        movementdown = false;
        movementup = false;
        playerShoot = true;
        playerStop = false;
    }

    private void StopPlayer()
    {
        movementleft = false;
        movementjump = false;
        movementright = false;
        movementdown = false;
        movementup = false;
        playerShoot = false;
        playerStop = true;
    }

    private void Update()
    {
        FlippingPlayer();
        if (movementright)
        {
            Vector2 playervelocity = new Vector2( movementspeed, rb.velocity.y);
            rb.velocity = playervelocity;
        }

       if(movementleft)
        {
            Vector2 playervelocity = new Vector2(-movementspeed, rb.velocity.y);
            rb.velocity = playervelocity;
        }
       
       if(playerShoot)
        {
            Debug.Log("Shot");
        }

       if(playerStop)
        {
            Vector2 playervelocity = new Vector2(0, 0);
            rb.velocity = playervelocity;
        }

       if(movementjump)
        {
            if (mycollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {

                    Vector2 JumpvelocitytoAdd = new Vector2(rb.velocity.x, jumpforce);
                    rb.velocity += JumpvelocitytoAdd;
                    // rb.gravityScale = 10;
                
            }
            else
            {
                movementjump = false;
            }
        }
        
       

    }

    private void FlippingPlayer()
    {
        bool PlayerhasHorizontalmovement = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        if (PlayerhasHorizontalmovement)
        {

            transform.localScale = new Vector2(Mathf.Sign(-rb.velocity.x), 1);
            anim.StartMoving(Mathf.Abs(rb.velocity.x));
        }
        else
        {
            anim.StopMoving();
        }
    }


}
