using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator_v1 : MonoBehaviour
{

    private const float END_OF_PHASE = 200f;
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 5f;
    public float PLAYER_DISTANCE_UNTIL_THE_END = 100f;
    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private List<Transform> levelPartList;
    [SerializeField] private CharacterController2D player;

    private bool gameIsOver = false;

    private Vector3 lastStartPosition;
    private Vector3 lastEndPosition;

    private float minHeight;
    public Transform maxHeightPoint;
    private float maxHeight;
    public float maxHeightChange;
    private float heightChange;

    private float maxAxisXWidht, minAxisXWidht, maxAxisXChange, widthChange;

    private ObjectGenerator theCoinGenerator;
    private EnemyGenerator theEnemyGenerator;
    private bool gameReady = false;
    public float randomCoinThreshold;
    public float randomEnemyThreshold;
    private float spawnPositionCoin;
    private float spawnPositionEnemy;

    private float penultimateEndPosition;

    public bool HighLow = false;

    //delete this code
    //public List<int[]> geneList;
    //private int[] spyke = { 1, 2, -1 };
    /*private int[] eagle = { 2, 2, -1 };
    private int[] frog = { 2, 1, -1 };
    int score;*/

    private void Awake()
    {
        //delete this code
        /*geneList = new List<int[]>();
        geneList.Add(spyke);
        geneList.Add(eagle);
        geneList.Add(frog);
        score = 0;*/

        widthChange = 0f;
        heightChange = 0f;

        maxAxisXChange = .5f;
        minAxisXWidht = 0.5f;
        maxAxisXWidht = 2f;

        theCoinGenerator = FindObjectOfType<ObjectGenerator>();
        theEnemyGenerator = FindObjectOfType<EnemyGenerator>();
        randomCoinThreshold = 25f;
        randomEnemyThreshold = 100f;
        gameReady = false;

        maxHeightChange = 1f;
        PLAYER_DISTANCE_UNTIL_THE_END = 150f;
        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;
        gameIsOver = false;
        lastEndPosition = levelPart_Start.Find("EndPosition").position;
        Debug.Log("LASTTTTTTTTTTTTTTTTTT:  " + lastEndPosition.x);
        lastStartPosition = levelPart_Start.Find("StartPosition").position;
        int startingSpawnLevelParts = 1;

        for (int i = 0; i < startingSpawnLevelParts; i++)
        {
            SpawnLevelPart();
        }
        gameReady = true;
    }

    private void Update()
    {
        //delete this code
        /*score = 0;
        for (int i = 0; i < geneList.Count; i++)
        {
            for (int j = 0; j < geneList[i].Length; j++)
            {
                Debug.Log("Valores do geneList em I = " + i + "e J = " + j + " : " + geneList[i].GetValue(j));
                score += (int) geneList[i].GetValue(j);
                Debug.Log("Score is: " + score);
            }
        }*/

        float distanceFromEnd = Vector3.Distance(player.transform.position, lastEndPosition);

        if (Random.value >= 0.5){
            gameReady = true;
        }
        else {
            gameReady = false;
        }

        // Debug.Log("LastEndPosition value: " + lastEndPosition.x);
        //Debug.Log("Player: " + player.transform.position + " || End Position: " + lastEndPosition + " || Distancia: " + distanceFromEnd);
        if (!gameIsOver)
        {
            if (lastEndPosition.x >= PLAYER_DISTANCE_UNTIL_THE_END)
            {
                SpawnLastLevelPart();
                gameIsOver = true;
            }
            else
            {
                //if (Vector3.Distance(player.transform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
                if ((lastEndPosition.x - player.transform.position.x) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
                {
                    // Spawn another level part
                    SpawnLevelPart();
                }
            }
        }

        if(lastEndPosition.x >= END_OF_PHASE){

                SpawnFinalPart();

        }

    }

    private void PlatformHeight()
    {
        //Testing x variation
        widthChange = Random.Range(maxAxisXWidht, minAxisXWidht);
        if(widthChange > maxAxisXWidht)
        {
            widthChange = maxAxisXWidht;
        }
        else
        {
            if(widthChange < minAxisXWidht)
            {
                widthChange = minAxisXWidht;
            }
        }


        float randomNumber = Random.Range(maxHeightChange, -maxHeightChange);
        //int roundedValue = Mathf.RoundToInt(randomNumber);
        //Debug.Log("NUMERO RANDOM: " + randomNumber + " || VALOR DO rounded: " + roundedValue);
        Debug.Log("NUMERO RANDOM: " + randomNumber);
        //heightChange += randomNumber;
        heightChange += 0f;
        /*if(Random.Range(0f, 1f) < 0.5)
        {
            heightChange += 0f;
        }
        else
        {
            heightChange += 0f;
        }*/
        //heightChange += randomNumber;
        //heightChange += roundedValue;

        /*if (heightChange > maxHeight)
        {
            heightChange = maxHeight;
        }
        else
        {
            if (heightChange < minHeight)
            {
                heightChange = minHeight;
            }
        }*/

        Debug.Log("Valor da altura: " + heightChange + "  |||| VALOR do X: " + widthChange);
    }

    private void SpawnLevelPart()
    {

        //Transform chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count - 1)];
        //Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);
        //lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        PlatformHeight();
        Transform chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count -1 )];
        Transform lastLevelPartTransform;
        Debug.Log("End pos: " + lastEndPosition.x + " || Valor do width: " + widthChange);
        //lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, new Vector3(lastEndPosition.x + widthChange, heightChange, lastEndPosition.z));
        lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, new Vector3(lastEndPosition.x + 2f, heightChange, lastEndPosition.z));
        //lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, new Vector3(lastEndPosition.x, 0, lastEndPosition.z));

        //Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, new Vector3(lastEndPosition.x + widthChange, heightChange, lastEndPosition.z));
        /*
        if (HighLow)
        {
            lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, new Vector3(lastEndPosition.x + widthChange, lastEndPosition.y + maxHeightChange, lastEndPosition.z));
            }

        else
        {
            lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, new Vector3(lastEndPosition.x + widthChange, lastEndPosition.y + maxHeightChange, lastEndPosition.z));
            HighLow = !HighLow;
        }
        */


        penultimateEndPosition = lastEndPosition.x;
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        lastStartPosition = lastLevelPartTransform.Find("StartPosition").position;

        if (gameReady)
        {
            //if (Random.Range(0f, 30f) < randomCoinThreshold)
            if (true)
            {
                Debug.Log("Coin respawn");
                spawnPositionCoin = lastStartPosition.x + (lastEndPosition.x - lastStartPosition.x) / 2;
                //theCoinGenerator.SpawnCoins(new Vector3(spawnPositionCoin + widthChange, heightChange + 0.8f, lastEndPosition.z));
                //theCoinGenerator.SpawnCoins(new Vector3(spawnPositionCoin, heightChange + 0.8f, lastEndPosition.z));
            }
            //if (Random.Range(0f, 30f) < randomEnemyThreshold)
            //{
            //    Debug.Log("Enemy respawn");
            //    int enemyIndex = Mathf.RoundToInt(Random.Range(0f, 4f));
            //    enemyIndex = 0;
            //    Debug.Log("Enemy Index Value: " + enemyIndex);
            //spawnPositionEnemy = lastStartPosition.x + (lastEndPosition.x - lastStartPosition.x) / 2;
            //    theEnemyGenerator.SpawnEnemy(enemyIndex, new Vector3(lastEndPosition.x + widthChange, heightChange, lastEndPosition.z));
            //}
        }

        // INSTANCIAR OBJETOS DEPOIS DAQUI

        //if (Random.Range(0f, 30f) < randomEnemyThreshold)
        //{
        Debug.Log("Enemy respawn");
        int enemyIndex = Mathf.RoundToInt(Random.Range(0f, 3f));
        //enemyIndex = 2;
        Debug.Log("Enemy Index Value: " + enemyIndex);
        //ENCONTRA O MEIO
        //spawnPositionEnemy = lastStartPosition.x + (lastEndPosition.x - lastStartPosition.x) / 2;
        //spawnPositionEnemy = penultimateEndPosition + ((lastStartPosition.x - penultimateEndPosition) / 2);
        //theEnemyGenerator.SpawnEnemy(enemyIndex, new Vector3(lastEndPosition.x + widthChange, heightChange, lastEndPosition.z));
        //theEnemyGenerator.SpawnEnemy(enemyIndex, new Vector3(spawnPositionEnemy, heightChange, lastEndPosition.z));
        Debug.Log("ANTES DE IR>> INDEX: " + enemyIndex + " | end pos.x: " + lastEndPosition.x + " | start pos.x: " + lastStartPosition.x + " | width: " + widthChange + " | height: " + heightChange);
        //theEnemyGenerator.SpawnEnemy(enemyIndex, lastEndPosition, lastStartPosition, penultimateEndPosition, widthChange, heightChange);
        //}
    }

    private void SpawnLastLevelPart()
    {
            Transform chosenLevelPart = levelPartList[levelPartList.Count-1];
            Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, new Vector3(lastEndPosition.x + widthChange, heightChange, lastEndPosition.z));
            lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
            lastStartPosition = lastLevelPartTransform.Find("StartPosition").position;
    }

    private void SpawnFinalPart(){

            Transform chosenLevelPart = levelPartList[levelPartList.Count];
            Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, new Vector3(lastEndPosition.x + widthChange, heightChange , lastEndPosition.z));
            lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
            lastStartPosition = lastLevelPartTransform.Find("StartPosition").position;

    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
}
