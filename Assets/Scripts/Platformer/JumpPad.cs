using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{

    [SerializeField] float power;
    [SerializeField] Rigidbody2D rb;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            rb.AddForce(new Vector2(0f, 1f) * power, ForceMode2D.Impulse);
        }    
    }
}
