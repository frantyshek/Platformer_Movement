using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    
    int counter = 0;
    bool collected = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        float randomX = Random.Range(-8, 8);
        float randomY = Random.Range(-3, 3);

        if(collision.gameObject.tag == "Player" && !collected)
        {
            collected = true;
            transform.position = new Vector2(randomX, randomY);
            counter ++;
            text.text = "Nazbierane coiny " + counter.ToString();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        collected = false;
    }
}
