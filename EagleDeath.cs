using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleDeath : MonoBehaviour
{
    public Animator animator;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            animator.SetTrigger("Dies");
            Destroy(gameObject,1);
        }
    }
}
