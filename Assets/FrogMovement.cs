using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovement : MonoBehaviour
{

    public Animator anim;
    public float timeBetweenJump = 500f;
    private Rigidbody2D rb2D;

    public int counterFromJump = 0;
    public bool alreadyTouchedPlatform = false;
    public int counterTimeIncreaseTime = 0;

     private void Start() {
        counterFromJump = 0;
        alreadyTouchedPlatform = false;
        counterTimeIncreaseTime = 0;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        rb2D = this.GetComponent<Rigidbody2D>();
        timeBetweenJump = 50;
        rb2D.AddForce(new Vector2(0, 2), ForceMode2D.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        if(timeBetweenJump == 0){
            if (counterTimeIncreaseTime >= 6)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                rb2D.AddForce(new Vector2(0, 6), ForceMode2D.Impulse);
                //timeBetweenJump = 320f - ((float)(GameHandler.FitnessValue) * 2);
                timeBetweenJump = 500f;
                
            }
            else
            {
                rb2D.AddForce(new Vector2(0, 1f), ForceMode2D.Impulse);
                timeBetweenJump = 25f;
            }
            if(counterTimeIncreaseTime >= 7)
            {
                GameHandler.gameIsReady = true;
            }
            counterTimeIncreaseTime++;
            counterFromJump++;
            
        }
        else {
        
            timeBetweenJump -= 1f;
        }
        if(counterFromJump <= 0)
        {
            alreadyTouchedPlatform = false;
        }
        if(counterFromJump >= 2 && !alreadyTouchedPlatform)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y, gameObject.transform.position.z);
            counterFromJump = 0;
        }

    }

    void OnTriggerEnter2D(Collider2D trig){


        if(trig.gameObject.CompareTag("Bullet") ){

            StartCoroutine(DestroyFrog());
        }

        if (trig.gameObject.name.Contains("frog"))
        {
            Debug.Log("os sapinhos estão juntinhos");
        }

        if (trig.gameObject.CompareTag("platform"))
        {
            //Debug.Log("O SAPO PULA NA PLATAFORMA");
        }

        if (trig.gameObject.name.Contains("frog"))
        {
            Debug.Log("O sapo pulou no sapo e agora?");
        }

    }

    
    private void OnCollisionEnter2D(Collision2D collision){

		if (collision.collider.tag == "Player"){

            StartCoroutine(DestroyFrog());
           
        }

        if (collision.collider.name.Contains("frog"))
        {
            Debug.Log("Um sapo bateu no outro");

        }

        if (collision.collider.tag == "platform")
        {
            alreadyTouchedPlatform = true;
            gameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            //Debug.Log("O SAPO ESTA NESSA MERDA DE PLATAFORMA");
        }



    }

        IEnumerator DestroyFrog(){
        GetComponent<BoxCollider2D>().enabled = false;
        anim.SetBool("isDead", true);
        yield return new WaitForSeconds(.25f);
        Destroy(gameObject);
        
    }
}
