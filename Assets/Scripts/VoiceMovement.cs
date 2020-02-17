using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;
using System;
using UnityEngine.UI;

public class VoiceMovement : MonoBehaviour
{
    [Header("SpeechEssentials")]
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    [Header("PlayerMovement")]
    public float movementspeed;
    public float jumpforce;
    float GravityatStart;
    public float Climbspeed;
    public Vector2 deathkick = new Vector2(10f, 25f);

    [Header("BoolCache")]
    bool movementright = false;
    bool movementleft = false;
    bool movementjump = false;
    bool movementup = false;
    bool movementdown = false;
    bool playerShoot = false;
    bool playerStop = false;
    bool isAlive = true;

    [Header("Cached References")]
    VoiceToText voicetotext;
    Rigidbody2D rb;
    BoxCollider2D PlayerLegsCollider;
    CapsuleCollider2D PlayerBodyCollider;
    AnimationController anim;
    AudioSource audiosrc;

    [Header("PlayerAudio")]
    public AudioClip jumpaudio;
    public AudioClip deathaudio;

    private void Awake()
    {
        audiosrc = GetComponent<AudioSource>();
        voicetotext = FindObjectOfType<VoiceToText>();
        rb = GetComponent<Rigidbody2D>();
        PlayerLegsCollider = GetComponent<BoxCollider2D>();
        PlayerBodyCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<AnimationController>();
        GravityatStart = rb.gravityScale;
        gameObject.transform.localScale = new Vector2(1, 1);
        StartCoroutine(DisbleInstuctionsLabel());
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

    IEnumerator DisbleInstuctionsLabel()
    {
        yield return new WaitForSeconds(5f);
        FindObjectOfType<GameSession>().DisableInstructionsLabel();
        FindObjectOfType<GameSession>().ShowScoreLabel();
    }

    private void Update()
    {
        PlayerDeath();
        FlippingPlayer();
        if (isAlive)
        {
            if (movementright) //Right Moving Functionality
            {
                FindObjectOfType<GameSession>().DisableInstructionsLabel();
                FindObjectOfType<GameSession>().ShowScoreLabel();
                voicetotext.RightMovementRecognised();
                Vector2 playervelocity = new Vector2(movementspeed, rb.velocity.y);
                rb.velocity = playervelocity;
            }

            if (movementleft) //Left Moving Functionality
            {
                FindObjectOfType<GameSession>().DisableInstructionsLabel();
                FindObjectOfType<GameSession>().ShowScoreLabel();
                voicetotext.LeftMovementRecognised();
                Vector2 playervelocity = new Vector2(-movementspeed, rb.velocity.y);
                rb.velocity = playervelocity;
            }

            if (playerShoot) //Player Shooting Functionality
            {
                voicetotext.ShootRecognised();
                Debug.Log("Shot");
            }

            if (playerStop) //Player Stoping Functionality
            {
                voicetotext.StopRecognised();
                Vector2 playervelocity = new Vector2(0, 0);
                rb.velocity = playervelocity;
            }

            if (movementjump) //Player Jump Functionality
            {
                if (PlayerLegsCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
                {
                    AudioSource.PlayClipAtPoint(jumpaudio, Camera.main.transform.position, 0.4f);
                    voicetotext.JumpRecognised();
                    Vector2 JumpvelocitytoAdd = new Vector2(0f, jumpforce);
                    rb.velocity += JumpvelocitytoAdd;
                }
                else
                {
                    movementjump = false;
                }
            }

            if (PlayerLegsCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) //Player ClimbingFunctionality
            {
                if (rb.velocity.x == 0)
                { anim.StopMoving(); }

                if (rb.velocity.y == 0)
                {
                    GetComponent<Animator>().SetBool("ClimbIdle", true);
                }
                else
                {
                    GetComponent<Animator>().SetBool("ClimbIdle", false);
                }

                if (movementup) //Player Climbing Up Facility
                {
                    Vector2 playervelocity = new Vector2(rb.velocity.x, Climbspeed);
                    anim.StartClimbing(Mathf.Abs(Climbspeed));
                    rb.velocity = playervelocity;
                    rb.gravityScale = 0;
                    voicetotext.ClimbingUpRecognised();
                }

                if (movementdown) //Player Climbing Down Faciltiy
                {
                    Vector2 playervelocity = new Vector2(rb.velocity.x, -Climbspeed);
                    anim.StartClimbing(Mathf.Abs(Climbspeed));
                    rb.velocity = playervelocity;
                    rb.gravityScale = 0;
                    voicetotext.ClimbingDownRecognised();
                }
            }
            else
            {
                
                GetComponent<Animator>().SetBool("ClimbIdle", false);
                anim.StopClimbing();
                rb.gravityScale = GravityatStart;
            }
             

        }
    }

    private void FlippingPlayer() //Player Flipping Functionality
    {
        bool PlayerhasHorizontalmovement = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        if (PlayerhasHorizontalmovement)
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
