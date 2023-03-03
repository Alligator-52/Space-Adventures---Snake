using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFoodDestroy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(this.gameObject); //it throws an error if I try to destroy the transform.
        }
    }
}
