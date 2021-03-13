using System;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class NextLevel : MonoBehaviour
{

    public float transitionTime = .1f;

    public GameHandler gh;

    private string player;
	private int level; 

    private Log log;

    private void Awake()
    {
        gh = FindObjectOfType<GameHandler>();
        log = FindObjectOfType<Log>();
    }
    void OnTriggerEnter2D(Collider2D trig){

         if(trig.CompareTag("Player")){
            //  Debug.Log("Bateu na casa");
             LoadNextLevel();
       
        }
    }

    public void LoadNextLevel(){

        gh.CalculatePlayerFitnessByScore();
        //log.StartRegisterLog();
        player  =  PlayerPrefs.GetString("playersName");
		level = PlayerPrefs.GetInt("level");
        level++;

        
        //StartCoroutine(GameMaster.instance.log.RegisterLog(player, level));
        PlayerPrefs.SetInt("level", level);
        if(PlayerPrefs.GetInt("level") <= 10)
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
            GameHandler.hasShowedForm = false;
        }
        else
        {
            if (!GameHandler.hasShowedForm)
            {
                gh.FormCanvas.SetActive(true);
                GameHandler.hasShowedForm = true;
            }
            else
            {
                StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
            }
        }
        GameMaster.instance.AttHud();
    }

     IEnumerator  LoadLevel(int levelIndex){

       //Calcular o score

        yield return new WaitForSeconds(transitionTime);
        GameMaster.instance.ResetNumOfHearts();
        GameMaster.instance.ResetNumOfPoints();
        GameMaster.instance.ResetNumOfBullets();

        GameMaster.instance.SetNumOfHearts(5);
        GameMaster.instance.SetNumOfPoints(0);
        GameMaster.instance.SetNumOfBullets(0);

        SceneManager.LoadScene(levelIndex);
        

    }


}
