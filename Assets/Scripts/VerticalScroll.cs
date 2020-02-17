using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{
    public float MovementSpeed;
    Rigidbody2D rb;
    public float SpeedRate;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= SpeedRate)
        {
            SpeedRate = SpeedRate + 10;            
            MovementSpeed = MovementSpeed + 5;
            rb.velocity = new Vector2(0, MovementSpeed * Time.deltaTime);
        }
        else
        {
            rb.velocity = new Vector2(0, MovementSpeed * Time.deltaTime);
        }
       
    }
}
