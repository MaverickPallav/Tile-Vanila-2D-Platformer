using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    
    PlayerMovement Movements;
   
    void Start()
    {
        Movements = GetComponent<PlayerMovement>();
    }

    
   public void StartMoving(float speed)
    {
        GetComponent<Animator>().SetFloat("Speed", speed);
    }

    public void StopMoving()
    {
        GetComponent<Animator>().SetFloat("Speed", 0);
    }

    public void StartClimbing(float speed)
    {
        GetComponent<Animator>().SetFloat("ClimbSpeed", speed);
    }

    public void StopClimbing()
    {
        GetComponent<Animator>().SetFloat("ClimbSpeed", 0);
    }

    
}
