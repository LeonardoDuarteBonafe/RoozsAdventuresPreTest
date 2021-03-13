using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private System.Random random;
    private const float END_OF_PHASE = 200f;
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 5f;
    public float PLAYER_DISTANCE_UNTIL_THE_END = 100f;
    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private List<Transform> levelPartList;
    [SerializeField] private CharacterController2D player;
    [SerializeField] private Transform respawnPointTransform;
    [SerializeField] private GameObject beginBlock;

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
    public bool gameIsSet = false;
    public float randomCoinThreshold;
    public float randomEnemyThreshold;
    private float spawnPositionCoin;
    private float spawnPositionEnemy;

    private float penultimateEndPosition;
    private float penultimeEndPositionY;

    public bool HighLow = false;
    public bool spawnRespawnPoint = false;

    public GameObject enemyEagle;
	public GameObject enemySpyke;
	public GameObject enemyFrog;
	public GameObject enemyOpossum;
	public GameObject enemyLife;    

    public Test GA; 
    public List<GameObject> prefabsList = new List<GameObject>();

    private int randomElement; // Qual objeto vai ser instanciado
    private float randomX; //Onde vai instanciar os objetos

    public int valoresDoRange = 10;
    private int numberOfEnemies;
    private List<string> enemiesListed;

    private bool addHeightForEagleRespawn;
    private float valueHeightToAddForEagle = 0.5f;

    public GameObject textFaseSendoGerada;

    private void Awake()
    {
        GameHandler.highscore = 0;
        GameHandler.numberOfBonusLife = 0;
        GameHandler.numberOfTotalCoins = 0;
        GameHandler.numberOfColectedCoins = 0;
        Debug.Log("CRIOU NOVA FASE");
        //valoresDoRange = GA.sizeTarget;
        numberOfEnemies = 0;
        enemiesListed = new List<string>();
        textFaseSendoGerada.SetActive(true);

        widthChange = 0f;
        heightChange = 0f;

        maxAxisXChange = .5f;
        minAxisXWidht = 0.5f;
        maxAxisXWidht = 2f;

        theCoinGenerator = FindObjectOfType<ObjectGenerator>();
        theEnemyGenerator = FindObjectOfType<EnemyGenerator>();
        randomCoinThreshold = 80f;
        randomEnemyThreshold = 100f;
        gameReady = false;
        gameIsSet = false;
        spawnRespawnPoint = false;
        addHeightForEagleRespawn = false;
        valueHeightToAddForEagle = 0.5f;

        maxHeightChange = 1f;
        PLAYER_DISTANCE_UNTIL_THE_END = 150f;
        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;
        gameIsOver = false;
        lastEndPosition = levelPart_Start.Find("EndPosition").position;
        lastStartPosition = levelPart_Start.Find("StartPosition").position;
        int startingSpawnLevelParts = 0;

        for (int i = 0; i < startingSpawnLevelParts; i++)
        {
            SpawnLevelPart();
        }
        gameReady = true;

        prefabsList = GA.GetElementsPrefab();
        random = new System.Random();
    }
    
    private void Update()
    {
        if(gameIsOver)
        {
            if(GameHandler.quantityFrog > 0)
            {
                if (GameHandler.gameIsReady)
                {
                    textFaseSendoGerada.SetActive(false);
                    Destroy(beginBlock);
                }
            }
            else
            {
                textFaseSendoGerada.SetActive(false);
                Destroy(beginBlock);
            }
        }
        if (prefabsList.Count > 0)// && !gameIsSet)
        {
            if (!gameIsSet)
            {
                valoresDoRange = GA.sizeTarget;
            }
            //Debug.Log("Valores do range e: " + valoresDoRange);
            float distanceFromEnd = Vector3.Distance(player.transform.position, lastEndPosition);

            /*if (Random.value >= 0.5)
            {
                gameReady = true;
            }
            else
            {
                gameReady = false;
            }*/

            // Debug.Log("LastEndPosition value: " + lastEndPosition.x);
            //Debug.Log("Player: " + player.transform.position + " || End Position: " + lastEndPosition + " || Distancia: " + distanceFromEnd);
            if (!gameIsOver)
            {
                /*if (lastEndPosition.x >= PLAYER_DISTANCE_UNTIL_THE_END)
                {
                    *//*SpawnLastLevelPart();
                    // Debug.Log("Last Start Position: " + lastStartPosition + "- Last End Position: " + lastEndPosition);

                    // Random para gerar um inimigo ou alguma vida


                    // Instantiate(prefabsList[0], new Vector3(lastEndPosition.x, lastEndPosition.y, lastEndPosition.z), Quaternion.identity);
                    // prefabsList.RemoveAt(0);

                    gameIsOver = true;*//*
                }
                else
                {*/
                    //if (Vector3.Distance(player.transform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
                    //if ((lastEndPosition.x - player.transform.position.x) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
                    //{
                    // Spawn another level part
                    SpawnLevelPart();
                    // Debug.Log("Last Start Position: " + lastStartPosition.x + "- Last End Position: " + lastEndPosition.x);

                    // Debug.Log("Número de itens: " + prefabsList.Count);
                    if (prefabsList.Count == 0 && gameIsSet)
                    {
                        SpawnLastLevelPart();
                        gameIsOver = true;
                    }
                    else
                    {
                        enemiesListed.Clear();
                    int minimunEnemiesToInstantiate = 0;
                    if(numberOfEnemies == 3)
                    {
                        minimunEnemiesToInstantiate = 1;
                    }
                    else
                    {
                        minimunEnemiesToInstantiate = 0;
                    }
                    int numberOfEnemiesToInstantiate = Mathf.RoundToInt(Random.Range(minimunEnemiesToInstantiate - 0.49f, numberOfEnemies + 0.49f));
                    //int numberOfEnemiesToInstantiate = Mathf.RoundToInt(Random.Range(numberOfEnemies + 0.49f, numberOfEnemies + 0.49f));
                    Debug.Log("Numero de inimigos que pode ser gerado e: " + numberOfEnemies + " || MAS foi gerado: " + numberOfEnemiesToInstantiate);
                    spawnRespawnPoint = false;
                    addHeightForEagleRespawn = false;
                    valueHeightToAddForEagle = 0.5f;
                    //for (int i = 0; i < numberOfEnemies; i++)

                    SpawnCoins();

                    for (int i = 0; i < numberOfEnemiesToInstantiate; i++)
                        {
                            if (Random.Range(0, 10) >= 0 && prefabsList.Count > 0)
                            {

                                //randomElement = Random.Range(0, prefabsList.Count);
                                randomElement = Random.Range(0, valoresDoRange);
                                //randomElement = 0;

                                Debug.Log("elemento aleatório: " + prefabsList[randomElement].name.ToString());
                                if (enemiesListed.Count > 0)
                                {
                                    foreach (string enemyName in enemiesListed)
                                    {
                                        if (prefabsList[randomElement].name.ToString().Equals(enemyName))
                                        {
                                            if (prefabsList[randomElement].name.ToString().Contains("eagle"))
                                            {
                                                addHeightForEagleRespawn = true;
                                            }
                                            else
                                            {
                                                lastStartPosition.x += 0.3f;
                                            }
                                        }
                                    }
                                }
                                enemiesListed.Add(prefabsList[randomElement].name.ToString());
                                if(GameHandler.sizeTargetValue > 1)
                                {
                                    if (addHeightForEagleRespawn)
                                    {
                                        theEnemyGenerator.SpawnEnemy(prefabsList[randomElement], lastEndPosition, lastStartPosition, penultimateEndPosition, widthChange, heightChange + valueHeightToAddForEagle);
                                        valueHeightToAddForEagle -= 1f;
                                    addHeightForEagleRespawn = false;
                                    }   
                                    else
                                    {
                                        theEnemyGenerator.SpawnEnemy(prefabsList[randomElement], lastEndPosition, lastStartPosition, penultimateEndPosition, widthChange, heightChange);
                                    }
                                }
                                prefabsList.RemoveAt(randomElement);

                                valoresDoRange--;
                                if (valoresDoRange <= 0)
                                {
                                    valoresDoRange = GA.sizeTarget;
                                    spawnRespawnPoint = true;
                                }
                                gameIsSet = true;

                                /*switch (prefabsList[randomElement].name){

                                    case "eaglePlataform":
                                        //Instantiate(prefabsList[randomElement], new Vector3(lastEndPosition.x + 0.2f, lastEndPosition.y, lastEndPosition.z), Quaternion.identity);
                                        theEnemyGenerator.SpawnEnemy(prefabsList[randomElement], lastEndPosition, lastStartPosition, penultimateEndPosition, widthChange, heightChange);
                                        prefabsList.RemoveAt(randomElement);
                                    break;

                                    case "lifePoint":
                                        randomX = Random.Range(lastStartPosition.x, lastEndPosition.x);
                                        //Instantiate(prefabsList[randomElement], new Vector3(randomX, lastEndPosition.y + 0.05f, lastEndPosition.z), Quaternion.identity);
                                        theEnemyGenerator.SpawnEnemy(prefabsList[randomElement], lastEndPosition, lastStartPosition, penultimateEndPosition, widthChange, heightChange);
                                        prefabsList.RemoveAt(randomElement);
                                    break;

                                    case "spykes":

                                        //randomX = Random.Range(lastStartPosition.x + 0.3f, lastEndPosition.x - 0.3f);
                                        //Instantiate(prefabsList[randomElement], new Vector3(randomX, lastEndPosition.y, lastEndPosition.z), Quaternion.identity);
                                        theEnemyGenerator.SpawnEnemy(prefabsList[randomElement], lastEndPosition, lastStartPosition, penultimateEndPosition, widthChange, heightChange);
                                        prefabsList.RemoveAt(randomElement);
                                    break;

                                    default:
                                        //randomX = Random.Range(lastStartPosition.x, lastEndPosition.x);
                                        //Instantiate(prefabsList[randomElement], new Vector3(randomX, lastEndPosition.y, lastEndPosition.z), Quaternion.identity);
                                        theEnemyGenerator.SpawnEnemy(prefabsList[randomElement], lastEndPosition, lastStartPosition, penultimateEndPosition, widthChange, heightChange);
                                        prefabsList.RemoveAt(randomElement);
                                    break;

                                }*/
                            }
                        }
                        if (prefabsList.Count > 0 && spawnRespawnPoint && GameHandler.sizeTargetValue > 1)
                        {
                            SpawnRespawnPoint();
                        }
                    }
                    //}
                //}
            }

            /*if(lastEndPosition.x >= END_OF_PHASE){

                    SpawnFinalPart();

            }*/
        }
        else
        {
            prefabsList = GA.GetElementsPrefab();
        }
        if (prefabsList.Count == 0 && gameIsSet && !gameIsOver)
        {
            SpawnLastLevelPart();
            gameIsOver = true;
        }
    }

    private void PlatformHeight()
    {
        //Testing x variation
        

        maxHeightChange = (float) (GameHandler.FitnessValue / 100);
        float randomNumber = Random.Range(maxHeightChange, -maxHeightChange);
        //int roundedValue = Mathf.RoundToInt(randomNumber);
        //Debug.Log("NUMERO RANDOM: " + randomNumber + " || VALOR DO rounded: " + roundedValue);
        // Debug.Log("NUMERO RANDOM: " + randomNumber);
        heightChange += randomNumber;
        //heightChange += 0f;
        //heightChange += roundedValue;

        if (heightChange > maxHeight)
        {
            heightChange = maxHeight;
        }
        else
        {
            if (heightChange < minHeight)
            {
                heightChange = minHeight;
            }
        }

        maxAxisXWidht = (float)(GameHandler.FitnessValue / 50);
        /*if (maxAxisXChange > 2)
        {
            maxAxisXChange = 2;
        }*/
        if(GameHandler.FitnessValue >= 95)
        {
            maxAxisXWidht = (float)((5 - (randomNumber)) / 2) * 0.9f;
            //maxAxisXWidht = (float)((5 - (randomNumber)) / 2);
            if(maxAxisXWidht < 2)
            {
                maxAxisXWidht = 2;
            }
        }
        //Debug.Log("Fitness value e: " + GameHandler.FitnessValue + " || Max axis: " + maxAxisXChange + " || min axis: " + minAxisXWidht);
        minAxisXWidht = maxAxisXWidht * 1f;
        if (minAxisXWidht < 0)
        {
            minAxisXWidht = 0;
        }
        Debug.Log("Fitness é: " + GameHandler.FitnessValue + " || Max axis: " + maxAxisXWidht + " || min axis: " + minAxisXWidht);
        widthChange = Random.Range(maxAxisXWidht, minAxisXWidht);
        /*if (widthChange > maxAxisXWidht)
        {
            widthChange = maxAxisXWidht;
        }
        else
        {
            if (widthChange < minAxisXWidht)
            {
                widthChange = minAxisXWidht;
            }
        }*/

        // Debug.Log("Valor da altura: " + heightChange + "  |||| VALOR do X: " + widthChange);
    }

    private void SpawnLevelPart()
    {

        //Transform chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count - 1)];
        //Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);
        //lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;

        //SpawnCoins(); 

        /*if (gameIsSet)
        {
            if (Random.Range(0f, 70f) < randomCoinThreshold)
            //if (true)
            {
                // Debug.Log("Coin respawn");
                spawnPositionCoin = lastStartPosition.x + ((lastEndPosition.x - lastStartPosition.x) * 0.5f);
                //theCoinGenerator.SpawnCoins(new Vector3(spawnPositionCoin + widthChange, heightChange + 2.0f, lastEndPosition.z));
                //theCoinGenerator.SpawnCoins(new Vector3(spawnPositionCoin, heightChange + 0f, lastEndPosition.z)); //2.4f maximo de altura
                theCoinGenerator.SpawnCoins(lastStartPosition, lastEndPosition, heightChange); //2.4f maximo de altura
            }
        }*/

        PlatformHeight();
        Transform chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count -1 )];
        if (chosenLevelPart.name.Contains("15"))
        {
            numberOfEnemies = 3;
        }
        else if(chosenLevelPart.name.Contains("10"))
        {
            numberOfEnemies = 2;
        }
        else if (chosenLevelPart.name.Contains("5"))
        {
            numberOfEnemies = 1;
        }
        Debug.Log("Number of enemies: " + numberOfEnemies + " || plataforma: " + chosenLevelPart.name);
        Transform lastLevelPartTransform;
        lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, new Vector3(lastEndPosition.x + widthChange, heightChange, lastEndPosition.z));

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
        penultimeEndPositionY = lastEndPosition.y;
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        lastStartPosition = lastLevelPartTransform.Find("StartPosition").position;

        


        // Debug.Log(lastStartPosition + " - " +  lastEndPosition);
    }

    private void SpawnLastLevelPart()
    {
            Transform chosenLevelPart = levelPartList[levelPartList.Count-1];
            Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, new Vector3(lastEndPosition.x + widthChange, heightChange, lastEndPosition.z));
            lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
            lastStartPosition = lastLevelPartTransform.Find("StartPosition").position;
        Debug.Log("FINAL DE TUDO: " + ((int) (lastStartPosition.x + lastEndPosition.x)/2));
        Debug.Log("Numero de coins: " + GameHandler.numberOfTotalCoins);
        GameHandler.maximumScore = ((int)(lastStartPosition.x + lastEndPosition.x) / 2);

        GameMaster gm = FindObjectOfType<GameMaster>();
        gm.SetNumOfBulletsFromFitness(Mathf.RoundToInt((GameHandler.quantityFrog + GameHandler.quantityOpossum) * 0.15f));

        //Destroy(beginBlock);
    }

    private void SpawnRespawnPoint()
    {
        Debug.Log("Veio aqui");
        Transform lastLevelPartTransform = SpawnLevelPart(respawnPointTransform, new Vector3(lastEndPosition.x + widthChange, heightChange, lastEndPosition.z));
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        lastStartPosition = lastLevelPartTransform.Find("StartPosition").position;
    }

    private void SpawnFinalPart(){

            Transform chosenLevelPart = levelPartList[levelPartList.Count];
            Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, new Vector3(lastEndPosition.x + widthChange, heightChange, lastEndPosition.z));
            lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
            lastStartPosition = lastLevelPartTransform.Find("StartPosition").position;

    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }

    private void SpawnCoins()
    {

        /*if (gameIsSet)
        {
            if (Random.Range(0f, 70f) < randomCoinThreshold)
            //if (true)
            {
                // Debug.Log("Coin respawn");
                spawnPositionCoin = lastStartPosition.x + ((lastEndPosition.x - lastStartPosition.x) * 0.5f);
                //theCoinGenerator.SpawnCoins(new Vector3(spawnPositionCoin + widthChange, heightChange + 2.0f, lastEndPosition.z));
                //theCoinGenerator.SpawnCoins(new Vector3(spawnPositionCoin, heightChange + 0f, lastEndPosition.z)); //2.4f maximo de altura
                theCoinGenerator.SpawnCoins(lastStartPosition, lastEndPosition, heightChange); //2.4f maximo de altura
            }
        }*/

        if (Random.Range(0f, 70f) < randomCoinThreshold)
        //if (true)
        {
            // Debug.Log("Coin respawn");
            spawnPositionCoin = lastStartPosition.x + ((lastEndPosition.x - lastStartPosition.x) * 0.5f);
            //theCoinGenerator.SpawnCoins(new Vector3(spawnPositionCoin + widthChange, heightChange + 2.0f, lastEndPosition.z));
            //theCoinGenerator.SpawnCoins(new Vector3(spawnPositionCoin, heightChange + 0f, lastEndPosition.z)); //2.4f maximo de altura
            theCoinGenerator.SpawnCoins(lastStartPosition, lastEndPosition, heightChange, penultimateEndPosition, penultimeEndPositionY); //2.4f maximo de altura
        }
    }
}
