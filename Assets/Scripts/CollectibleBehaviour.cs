using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CollectibleBehaviour : MonoBehaviour
{
    private int counter =0 ;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            counter += 1;
            PlayerPrefs.SetInt("collectibleCount", counter);
            FindObjectOfType<GameManager>().CollectibleCounter();
            Destroy(this.gameObject);
        }
    }
}
