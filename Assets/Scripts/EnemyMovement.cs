using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float movementspeed = 1f;
    Rigidbody2D EnemyRigidbody;
    BoxCollider2D DetectionCollider;
    bool EnemyMoving = true;

    // Start is called before the first frame update
    void Start()
    {
        EnemyRigidbody = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (isFacingRight())
        {
            EnemyRigidbody.velocity = new Vector2(movementspeed, 0);
        }
        else
        {
            EnemyRigidbody.velocity = new Vector2(-movementspeed, 0);
        }
    }

    private bool isFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(EnemyRigidbody.velocity.x)), 1f);
    }
    

    

    /* IEnumerator MovingEnemy()    // Enemy Moving Ai With Coroutine
     {

         transform.localScale = new Vector2(1, 1);
         EnemyRigidbody.position = new Vector2(transform.position.x + 0.01f * movementspeed, transform.position.y);

         yield return new WaitForSeconds(2f);
         EnemyMoving = false;
         transform.localScale = new Vector2(-1, 1);
         EnemyRigidbody.position = new Vector2(transform.position.x - 0.01f * movementspeed, transform.position.y);

     } */
}
