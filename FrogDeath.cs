using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogDeath : MonoBehaviour
{
    public Animator animator;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            Destroy(gameObject, 1);
            animator.SetTrigger("Dies");
        }
    }
}
