using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gems : MonoBehaviour
{
    public int coinValue = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            ScoreManager.instance.changeScore(coinValue);
        }
    }
}
