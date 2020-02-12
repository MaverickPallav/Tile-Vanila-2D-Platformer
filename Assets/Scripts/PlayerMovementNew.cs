using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementNew : MonoBehaviour
{
    public float movememntspeed;
    private AnimationController anim;
   

    private void Awake()
    {
        anim = gameObject.GetComponent<AnimationController>();
    }

    private void Update()
    {
        updatemovement();
    }

    private void updatemovement()
    {
        float MoveX = 0;
        float MoveY = 0;
        if (Input.GetKey(KeyCode.D))
        {
            MoveX = +1f;
        }
        if(Input.GetKey(KeyCode.A))
        {
            MoveX = -1f;
        }
        if(Input.GetKey(KeyCode.W))
        {
            MoveY = 1f;
        }
        if(Input.GetKey(KeyCode.S))
        {
            MoveY = -1f;
        }

        Vector3 movedir = new Vector3(MoveX, MoveY);
        transform.position += movedir * movememntspeed * Time.deltaTime;

    }
}
