using System;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1f;

    private string player;
	private int level; 

    // public GameHandler gh = new GameHandler();

    void OnTriggerEnter2D(Collider2D trig){

         if(trig.CompareTag("Player")){

             LoadNextLevel();
       
        }
    }

    public void LoadNextLevel(){

        //StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        
        // gh.CalculatePlayerFitnessByScore();
		player  =  PlayerPrefs.GetString("playersName");
		level = PlayerPrefs.GetInt("level");
        level++;

        //StartCoroutine(GameMaster.instance.log.RegisterLog(player, level));
        PlayerPrefs.SetInt("level", level);

        //StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
        GameMaster.instance.AttHud();
    }

     IEnumerator LoadLevel(int levelIndex){

        transition.SetTrigger("Start");
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
