using System.Linq.Expressions;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameMaster : MonoBehaviour
{

    public static GameMaster instance;
    public Log log;
    public Vector2 lastCheckPointPos;
    public int numOfHearts = 5;
    public int numOfPoints = 0;
    public int numOfBullets = 0;
    public GameObject formularioButton;

    void Awake(){
        if(instance == null){

                instance = this;
                DontDestroyOnLoad(instance);
                log = FindObjectOfType<Log>();
        } else {

            Destroy(gameObject);
        }

        formularioButton.SetActive(false);

        if (GameHandler.hasShowedForm)
        {
            formularioButton.SetActive(true);
        }
    }

    void Start(){

        AttHud();
    }

    void Update(){

        AttHud();

    }

    public void SetNumOfHearts(int hearts){

        numOfHearts += hearts;
        GameHandler.numberOfRemainingLife = numOfHearts;
        AttHud();
    }

    public int GetNumOfHearts(){

        return numOfHearts;

    }

    public void ResetNumOfHearts(){

        numOfHearts = 0;
    }

    public void SetNumOfPoints(int diamondPoints){

        numOfPoints += diamondPoints;
        AttHud();

    }


    public int GetNumOfPoints(){

        return numOfPoints;
    }

    public void ResetNumOfPoints(){

        numOfPoints = 0;
    }

    public void SetNumOfBullets(int bullets){

        numOfBullets += bullets;
        AttHud();

    }

    public void SetNumOfBulletsFromFitness(int bullets)
    {

        numOfBullets = bullets;
        AttHud();

    }

    public int GetNumOfBullets(){

        return numOfBullets;
    }

    public void ResetNumOfBullets(){

        numOfBullets = 0;
    }



    public void AttHud(){
        GameObject.Find("HeartPointsText").GetComponent<Text>().text = numOfHearts.ToString();
        GameObject.Find("BulletsText").GetComponent<Text>().text = numOfBullets.ToString();
        GameObject.Find("DiamondsPointsText").GetComponent<Text>().text = numOfPoints.ToString();
        if(PlayerPrefs.GetInt("level") > 10)
        {
            GameObject.Find("FaseAtualText").GetComponent<Text>().text = "Fase atual: " + (PlayerPrefs.GetInt("level")).ToString();
        }
        else
        {
            GameObject.Find("FaseAtualText").GetComponent<Text>().text = "Fase atual: " + (PlayerPrefs.GetInt("level")).ToString() + "/10";
        }
        if (GameHandler.maximumScore > 0)
        {
            int distanceInPercetange = ((int)((GameHandler.currentDistance / (float)GameHandler.maximumScore) * 100));
            if(distanceInPercetange >= 100)
            {
                distanceInPercetange = 100;
            }
            GameObject.Find("MaiorDistanciaText").GetComponent<Text>().text = "Distância percorrida: " + distanceInPercetange.ToString() + "%";
        }
        //GameObject.Find("DistanciaAtualText").GetComponent<Text>().text = "Distância atual: " + GameHandler.currentDistance.ToString();
        //GameObject.Find("DistanciaFinalText").GetComponent<Text>().text = "Distância total: " + ((GameHandler.maximumScore + 1).ToString());
        GameObject.Find("DificuldadeText").GetComponent<Text>().text = "Dificuldade: " + GameHandler.FitnessValue.ToString() + "/100";
    }


}
