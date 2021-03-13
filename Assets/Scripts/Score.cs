using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;

     void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other){

	    if(other.CompareTag("Player")){

            //GetComponent<BoxCollider2D>().enabled = false;
            //GetComponent<CapsuleCollider2D>().enabled = false;
            Destroy(gameObject);
            //this.spriteRenderer.enabled = false;
            Debug.Log("E aqui que apaga mesmo?");
        }    
    }
}
