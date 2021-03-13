using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject[] enemyList;
    private float percentageOfPlataformFrog = 0.30f;
    private float percentageOfPlataformSpyke = 0.25f;

    /*
    public void SpawnEnemy(int index, Vector3 startPosition)
    {
        GameObject enemyObject = enemyList[index];
        Debug.Log("NOME DO MONSTRAO: " + enemyObject.name);
        enemyObject = (GameObject)Instantiate(enemyObject);
        //enemyObject.transform.position = new Vector3(startPosition.x, startPosition.y + 0.2f, startPosition.z);
        enemyObject.transform.position = new Vector3(startPosition.x, startPosition.y + 0.2f, startPosition.z);
    }
    */
    
    //public void SpawnEnemy(int enemyIndex, Vector3 lastEndPosition, Vector3 lastStartPosition, float penultimateEndPosition, float widthChange, float heightChange)
    public void SpawnEnemy(GameObject enemyObject, Vector3 lastEndPosition, Vector3 lastStartPosition, float penultimateEndPosition, float widthChange, float heightChange)
    {
        //GameObject enemyObject = enemyList[enemyIndex];
        enemyObject = (GameObject)Instantiate(enemyObject);

        //Debug.Log("DEPOIS DE IR>> INDEX: | end pos.x: " + lastEndPosition.x + " | start pos.x: " + lastStartPosition.x + " | width: " + widthChange + " | height: " + heightChange);
        //enemyObject.transform.position = new Vector3(startPosition.x, startPosition.y + 0.2f, startPosition.z);
        if (enemyObject.name.Contains("eagle"))
        {
            //Debug.Log("VEIO ESSE NOME DESSA PORRA: " + enemyObject.name);
            float spawnPositionEnemy = penultimateEndPosition + ((lastStartPosition.x - penultimateEndPosition) / 2);
            //theEnemyGenerator.SpawnEnemy(enemyIndex, new Vector3(lastEndPosition.x + widthChange, heightChange, lastEndPosition.z));
            CreateEnemy(enemyObject, new Vector3(spawnPositionEnemy, heightChange, lastEndPosition.z));
            //Debug.Log("Entrou na condição do nome");
        }
        if (enemyObject.name.Contains("frog"))
        {
            //Debug.Log("VEIO ESSE NOME DESSA PORRA: " + enemyObject.name);
            float percentageOfPlatform = (lastEndPosition.x - lastStartPosition.x) * percentageOfPlataformFrog;
            float frogXRespawn = Random.Range(lastStartPosition.x + (percentageOfPlatform * 0.75f), lastEndPosition.x - percentageOfPlatform);
            //frogXRespawn = Random.Range(lastEndPosition.x - percentageOfPlatform, lastEndPosition.x - percentageOfPlatform);
            //float spawnPositionEnemy = penultimateEndPosition + ((lastStartPosition.x - penultimateEndPosition) / 2);
            //theEnemyGenerator.SpawnEnemy(enemyIndex, new Vector3(lastEndPosition.x + widthChange, heightChange, lastEndPosition.z));
            CreateEnemy(enemyObject, new Vector3(frogXRespawn, heightChange + 0.2f, lastEndPosition.z));
            //Debug.Log("Entrou na condição do nome do sapo");
        }
        if (enemyObject.name.Contains("opossum_platform"))
        {
            //Debug.Log("VEIO ESSE NOME DESSA PORRA: " + enemyObject.name);
            float distanceFromStartAndEnd = (lastEndPosition.x - lastStartPosition.x) * 0.33f;
            //Debug.Log("Value antes: " + distanceFromStartAndEnd);
            //distanceFromStartAndEnd = distanceFromStartAndEnd / 3;
            //Debug.Log("Valor depois: " + distanceFromStartAndEnd);
            float opossumXRespawn = Random.Range(lastStartPosition.x + distanceFromStartAndEnd, lastEndPosition.x - distanceFromStartAndEnd);
            //float opossumXRespawn = lastStartPosition.x;// + (lastEndPosition.x - lastStartPosition.x) * (Random.Range(0.35f, 0.65f)) ;
            //float spawnPositionEnemy = penultimateEndPosition + ((lastStartPosition.x - penultimateEndPosition) / 2);
            //theEnemyGenerator.SpawnEnemy(enemyIndex, new Vector3(lastEndPosition.x + widthChange, heightChange, lastEndPosition.z));
            //CreateEnemy(enemyObject, new Vector3(lastStartPosition.x, heightChange + 0.5f, lastEndPosition.z));
            CreateEnemy(enemyObject, new Vector3(opossumXRespawn, heightChange + 0.5f, lastEndPosition.z));
            //Debug.Log("Entrou na condição do nome do opossum 1");
        }
        if (enemyObject.name.Contains("life"))
        {
            //Debug.Log("VEIO ESSE NOME DESSA PORRA: " + enemyObject.name);
            float distanceFromStartAndEnd = (lastEndPosition.x - lastStartPosition.x) / 3;
            //Debug.Log("Value antes: " + distanceFromStartAndEnd);
            //distanceFromStartAndEnd = distanceFromStartAndEnd / 3;
            //Debug.Log("Valor depois: " + distanceFromStartAndEnd);
            float lifeXRespawn = Random.Range(lastStartPosition.x + distanceFromStartAndEnd, lastEndPosition.x - distanceFromStartAndEnd);
            float heightLifeChange = Random.Range(1f, 2f);
            //float spawnPositionEnemy = penultimateEndPosition + ((lastStartPosition.x - penultimateEndPosition) / 2);
            //theEnemyGenerator.SpawnEnemy(enemyIndex, new Vector3(lastEndPosition.x + widthChange, heightChange, lastEndPosition.z));
            //CreateEnemy(enemyObject, new Vector3(lastStartPosition.x, heightChange + 1.5f, lastEndPosition.z));
            CreateEnemy(enemyObject, new Vector3(lifeXRespawn, heightChange + heightLifeChange, lastEndPosition.z));
            //Debug.Log("Entrou na condição do nome do LIFE");
        }
        if (enemyObject.name.Contains("spyke"))
        {
            //Debug.Log("VEIO ESSE NOME DESSA PORRA: " + enemyObject.name);
            float distanceFromStartAndEnd = (lastEndPosition.x - lastStartPosition.x) * 0.2f;
            //Debug.Log("Value antes: " + distanceFromStartAndEnd);
            //distanceFromStartAndEnd = distanceFromStartAndEnd / 3;
            //Debug.Log("Valor depois: " + distanceFromStartAndEnd);
            float spykeXRespawn = Random.Range(lastStartPosition.x + 0.6f, lastEndPosition.x - 0.6f);
            //float spawnPositionEnemy = penultimateEndPosition + ((lastStartPosition.x - penultimateEndPosition) / 2);
            //theEnemyGenerator.SpawnEnemy(enemyIndex, new Vector3(lastEndPosition.x + widthChange, heightChange, lastEndPosition.z));
            CreateEnemy(enemyObject, new Vector3(spykeXRespawn, heightChange + 0.365f, lastEndPosition.z));
            //Debug.Log("Entrou na condição do nome do SPYKE");
        }
        /*
         * if (enemyIndex == 1)
        {
            float spawnPositionEnemy = penultimateEndPosition + ((lastStartPosition.x - penultimateEndPosition) / 2);
            //theEnemyGenerator.SpawnEnemy(enemyIndex, new Vector3(lastEndPosition.x + widthChange, heightChange, lastEndPosition.z));
            CreateEnemy(enemyObject, new Vector3(spawnPositionEnemy, heightChange, lastEndPosition.z));
            Debug.Log("Entrou na condição do nome");
        }
        */
    }

    private void CreateEnemy(GameObject enemyObject, Vector3 spawnPosition)
    {
        //enemyObject.transform.position = new Vector3(startPosition.x, startPosition.y + 0.2f, startPosition.z);
        enemyObject.transform.position = spawnPosition;
    }

    //SELECIONAR O TIPO DE INIMIGO E RESPAWNALO DE MANEIRA CORRETA
    public void TypeOfEnemy(int enemyIndex)
    {
        /*if (Random.Range(0f, 30f) < randomEnemyThreshold)
        //{
        Debug.Log("Enemy respawn");
        int enemyIndex = Mathf.RoundToInt(Random.Range(0f, 4f));
        enemyIndex = 1;
        Debug.Log("Enemy Index Value: " + enemyIndex);
        //ENCONTRA O MEIO
        //spawnPositionEnemy = lastStartPosition.x + (lastEndPosition.x - lastStartPosition.x) / 2;
        spawnPositionEnemy = penultimateEndPosition + ((lastStartPosition.x - penultimateEndPosition) / 2);
        //theEnemyGenerator.SpawnEnemy(enemyIndex, new Vector3(lastEndPosition.x + widthChange, heightChange, lastEndPosition.z));
        theEnemyGenerator.SpawnEnemy(enemyIndex, new Vector3(spawnPositionEnemy, heightChange, lastEndPosition.z));
        //}
        */
    }
}
