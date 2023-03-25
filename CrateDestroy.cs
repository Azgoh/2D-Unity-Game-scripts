using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateDestroy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            Destroy(collision.gameObject);
        }
    }
}
