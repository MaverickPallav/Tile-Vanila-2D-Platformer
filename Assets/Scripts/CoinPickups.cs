using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickups : MonoBehaviour
{
    public AudioClip coinpickupSfx;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == FindObjectOfType<PlayerMovementRB>().PlayerBodyCollider )

        {
            FindObjectOfType<UpdateScore>().UpdateScoreonPickup();
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(coinpickupSfx, Camera.main.transform.position, 0.2f);
        }
    }


}
