using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public Animator anim;
    public float runSpeed = 1f;
    public bool isRight;

    void Update() {



        if(isRight){

            transform.Translate( - 1 *runSpeed * Time.deltaTime, 0,0);
            transform.localScale = new Vector2(2,2);
    
        } else{

            transform.Translate(  1 *  runSpeed * Time.deltaTime, 0,0);
            transform.localScale = new Vector2(-2,2);
    
        }

    }  

    void OnTriggerEnter2D(Collider2D trig){


        if(trig.gameObject.CompareTag("turnLeftRight") || trig.gameObject.name == "EndPosition" || trig.gameObject.name == "StartPosition")
        {

            if(isRight) //|| trig.gameObject.name == "EndPosition")
            {
                isRight = false;
            }
            else {
                isRight = true;
            }
            StartCoroutine(turn());
        }

        if(trig.gameObject.CompareTag("Bullet")){
            
            StartCoroutine(DestroyOpossum());
        }

    }

    IEnumerator turn()
    {

        runSpeed = 0f;
        float timeToWait = Random.Range(0.4f, 0.4f + ((float)GameHandler.FitnessValue / 100));
        //Debug.Log("Fitness e: " + GameHandler.FitnessValue + " || Valor random: " + timeToWait + " || valor do adicional do log: " + ((float)GameHandler.FitnessValue / 100));
        yield return new WaitForSeconds(timeToWait);
        //runSpeed = 1f + ((float)GameHandler.FitnessValue / 50);
        runSpeed = 1f;

    }

    IEnumerator DestroyOpossum(){
        GetComponent<BoxCollider2D>().enabled = false;
        anim.SetBool("isDead", true);
        yield return new WaitForSeconds(.25f);
        Destroy(gameObject);
        
    }


}
