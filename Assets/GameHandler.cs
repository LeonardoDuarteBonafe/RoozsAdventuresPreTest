using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public CameraFollow cameraFollow;
    public Transform playerTransform;
    public static double FitnessValue = 0;
    public static double oldFitnessValue = 0;
    public static int sizeTargetValue = 0;
    public static int highscore = 0;
    public static int maximumScore = 0;
    public static int currentDistance = 0;
    public static int numberOfTotalCoins = 0;
    public static int numberOfColectedCoins = 0;
    public static int numberOfRemainingLife = 5;
    public static int playerFitness = 0;
    public static int numberOfBonusLife = 0;
    public static int numberOfInitialLife = 5;
    public int constantOfLife = 75;
    public int constantOfScore = 75;
    public int constantOfCoinByDeath = 25;
    public int constantOfCoinByScore = 25;
    public double constantOfCoinError = 1.0f;
    public static int quantityEagle = 0;
    public static int quantityFrog = 0;
    public static int quantityOpossum = 0;
    public static int quantitySpyke = 0;
    public static int quantityLife = 0;
    public static bool isCoinGenerated = false;

    public int scoreFunction = 0;

    public static double weightOfSpyke;
    public static double weightOfEagle;
    public static double weightOfFrog;
    public static double weightOfOpossum;
    public static double weightOfLife;

    public static int totalEagle;
    public static int totalSpyke;
    public static int totalOpssum;
    public static int totalFrog;

    private int spykeIndex = 0;
    private int opossumIndex = 1;
    private int eagleIndex = 2;
    private int lifeIndex = 3;
    private int frogIndex = 4;

    public static bool gameIsReady = false;


    //spyke, opossum, eagle, life, frog
    public static double[] weightOfElements = new double[5];

    private Log log;

    public GameObject FormCanvas;

    public static bool hasShowedForm = false;

    private void Awake()
    {
        constantOfLife = 75;
        constantOfCoinByScore = 25;

        constantOfScore = 75;
        constantOfCoinByDeath = 25;

        log = FindObjectOfType<Log>();
        isCoinGenerated = false;
        gameIsReady = false;
        Debug.Log("Status of game is ready: " + gameIsReady);

        FormCanvas.SetActive(false);
    }

    private void Start()
    {
        cameraFollow.Setup(() => playerTransform.position);
    }

    public void setValues(double fV, int sV)
    {
        FitnessValue = fV;
        sizeTargetValue = sV;
    }

    public double getFitness()
    {
        return FitnessValue;
    }

    public double getPlayerFitness(){

        return playerFitness;
    }

    public double getScoreFunction(){

        return scoreFunction;
    }

    public int getSize()
    {
        return sizeTargetValue;
    }

    public void CalculatePlayerFitnessByDeath()
    {
        oldFitnessValue = FitnessValue;
        int highscoreValues = Mathf.RoundToInt(((float)highscore / maximumScore) * constantOfScore);
        Debug.Log("Valor da fitness AQUI >> : " + highscoreValues);
        Debug.Log("Maximum score: " + maximumScore);
        Debug.Log("HIGHSCORE: " + highscore);
        Debug.Log("Value of coins: " + CalculateCoinScore(false));
        playerFitness = highscoreValues + CalculateCoinScore(false);
        Debug.Log("PLAYER POR DEATH: " + playerFitness);
        //if()
        if (playerFitness >= 75)
        {
            //fitness deve manter igual
        }
        else
        {
            if(playerFitness < 75)
            {
                /*//fitness deve ser menor
                if(playerFitness < 20)
                {
                    FitnessValue = (double)((int)(FitnessValue * 0.8));
                    SetElementWeight(3, 2.5);
                }
                else
                {
                    FitnessValue = (double)((int)(FitnessValue * 0.9));
                    SetElementWeight(3, 2);
                }*/
                scoreFunction = Mathf.RoundToInt((playerFitness - 80) * 0.25f);
                FitnessValue += scoreFunction;
                if(FitnessValue <= 0)
                {
                    FitnessValue = 0;
                }
                SetSizeTarget();
                CalculateElementsWeight();
                Debug.Log("Valores || Fitness: " + FitnessValue + " || playerFit: " + playerFitness + " || scoreFun: " + scoreFunction);
            }
        }
    }

    public void CalculatePlayerFitnessByScore()
    {
        oldFitnessValue = FitnessValue;
        Debug.Log("Vidas restantes 3: " + ((float)(numberOfRemainingLife / (float)(numberOfInitialLife + numberOfBonusLife))) * constantOfLife);
        int lifeValues = Mathf.RoundToInt(((float)(numberOfRemainingLife / (float)(numberOfInitialLife + numberOfBonusLife))) * constantOfLife);
        Debug.Log("LIFE VALUES: " + lifeValues + " || vidas restantes: " + numberOfRemainingLife + " || vidas coletadas: " + numberOfBonusLife + " || vidas iniciais: " + numberOfInitialLife);
        playerFitness = lifeValues + CalculateCoinScore(true);
        Debug.Log("PLAYER POR SCORE: " + playerFitness);

        
        if (playerFitness >= 80)
        {
            //fitness aumenta
            //FitnessValue = (double) ((int)(FitnessValue * 1.2));
            //scoreFunction = Mathf.RoundToInt((playerFitness - 55) * 0.45f);
            scoreFunction = Mathf.RoundToInt((playerFitness - 75) * 0.5f);
            FitnessValue += scoreFunction;
            if(FitnessValue >= 100)
            {
                FitnessValue = 100;
            }
        }
        else if(playerFitness < 80 && playerFitness > 40)
        {
            //fitness mantem
        }
        else if (playerFitness <= 40)
        {
            //fitness deve ser menor
            //FitnessValue = (double)((int)(FitnessValue * 0.9));
            scoreFunction = Mathf.RoundToInt((playerFitness - 45) * 0.5f);
            FitnessValue += scoreFunction;
            if(FitnessValue <= 0)
            {
                FitnessValue = 0;
            }
        }
        SetSizeTarget();
        CalculateElementsWeight();
        Debug.Log("Valores || Fitness: " + FitnessValue + " || playerFit: " + playerFitness + " || scoreFun: " + scoreFunction);
    }

    private int CalculateCoinScore(bool byScore)
    {
        int coinValues = 0;
        //Debug.Log("COINS COLECTED: " + numberOfColectedCoins);
        if(!isCoinGenerated)
        {
            if (byScore)
            {
                coinValues = constantOfCoinByScore;
            }
            else
            {
                coinValues = constantOfCoinByDeath;
            }
            Debug.Log("No coins created");
        }
        else
        {
            if(numberOfColectedCoins <= 0)
            {
                coinValues = 0;
                Debug.Log("Zero coins colected");
            }
            else
            {
                if (byScore)
                {
                    coinValues = Mathf.RoundToInt(((float)(numberOfColectedCoins / ((float)(numberOfTotalCoins * constantOfCoinError)))) * constantOfCoinByScore);
                    if (coinValues > constantOfCoinByScore)
                    {
                        coinValues = constantOfCoinByScore;
                    }
                    Debug.Log("Chegou no final");
                }
                else
                {
                    coinValues = Mathf.RoundToInt(((float)(numberOfColectedCoins / ((float)(numberOfTotalCoins * constantOfCoinError)))) * constantOfCoinByDeath);
                    if (coinValues > constantOfCoinByDeath)
                    {
                        coinValues = constantOfCoinByDeath;
                    }
                    Debug.Log("Morreu antes");
                }
            }
        }
        return coinValues;
    }

    private void SetElementWeight(int index, double weight)
    {
        weightOfElements[index] = weight;
    }

    private void SetSizeTarget()
    {
        sizeTargetValue = Mathf.RoundToInt((float)(FitnessValue / 2)) + 1;
        Debug.Log("Startando o REGISTER LOG DO HANDLER");
        log.StartRegisterLog();
    }

    private void CalculateElementsWeight()
    {
        if (FitnessValue >= 80)
        {
            SetElementWeight(opossumIndex, 2.0d);
            SetElementWeight(eagleIndex, 2.0d);
            SetElementWeight(frogIndex, 1.9d);
            SetElementWeight(spykeIndex, 1.7d);
            SetElementWeight(lifeIndex, 1.6d);
        }
        else if (FitnessValue >= 60 && FitnessValue < 80)
        {
            SetElementWeight(opossumIndex, 2.0d);
            SetElementWeight(eagleIndex, 1.95d);
            SetElementWeight(frogIndex, 1.9d);
            SetElementWeight(spykeIndex, 1.8d);
            SetElementWeight(lifeIndex, 1.7d);
        }
        else if (FitnessValue >= 40 && FitnessValue < 60)
        {
            SetElementWeight(opossumIndex, 2.0d);
            SetElementWeight(eagleIndex, 2.0d);
            SetElementWeight(frogIndex, 2.0d);
            SetElementWeight(spykeIndex, 2.0d);
            SetElementWeight(lifeIndex, 1.85d);
        }
        else if (FitnessValue >= 25 && FitnessValue < 40)
        {
            SetElementWeight(opossumIndex, 1.75d);
            SetElementWeight(eagleIndex, 1.75d);
            SetElementWeight(frogIndex, 1.9d);
            SetElementWeight(spykeIndex, 2.0d);
            SetElementWeight(lifeIndex, 1.9d);
        }
        else if (FitnessValue >= 10 && FitnessValue < 25)
        {
            SetElementWeight(opossumIndex, 1.6d);
            SetElementWeight(eagleIndex, 1.7d);
            SetElementWeight(frogIndex, 1.95d);
            SetElementWeight(spykeIndex, 2.0d);
            SetElementWeight(lifeIndex, 1.95d);
        }
        else if (FitnessValue >= 5 && FitnessValue < 10)
        {
            SetElementWeight(opossumIndex, 1.5d);
            SetElementWeight(eagleIndex, 1.5d);
            SetElementWeight(frogIndex, 1.6d);
            SetElementWeight(spykeIndex, 1.9d);
            SetElementWeight(lifeIndex, 2.0d);
        }
        else
        {
            SetElementWeight(opossumIndex, 1.3d);
            SetElementWeight(eagleIndex, 1.4d);
            SetElementWeight(frogIndex, 1.5d);
            SetElementWeight(spykeIndex, 1.6d);
            SetElementWeight(lifeIndex, 2.0d);
        }
    }

}
