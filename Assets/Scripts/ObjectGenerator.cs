using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    public ObjectPooler coinPool;
    public ObjectPooler enemyPool;

    public GameObject gemBackground;

    public float distanceBetweenCoins;

    private int numberOfCoinsHorizontal = 0;
    private int numberOfCoinsVertical = 0;

    private float valueOfXAxis;
    private float valueOfYAxis;

    /*public void SpawnCoins(Vector3 startPosition)
    {
        int numberOfCoins = Mathf.RoundToInt(Random.Range(0.51f, 3.49f));
        Debug.Log("NUMBER OS COINS GENERATED: " + numberOfCoins);
        //GameHandler.numberOfTotalCoins += numberOfCoins;
        if(numberOfCoins == 1)
        {
            GameObject coin1 = coinPool.GetPooledObject();
            coin1.transform.position = new Vector3(startPosition.x, startPosition.y + Random.Range (1f, 2.3f), startPosition.z);
            coin1.SetActive(true);
            Instantiate(gemBackground, coin1.transform.position, Quaternion.identity);
        }
        else
        {
            if(numberOfCoins == 2)
            {
                GameObject coin2 = coinPool.GetPooledObject();
                coin2.transform.position = new Vector3(startPosition.x - distanceBetweenCoins, startPosition.y + Random.Range(1f, 2.3f), startPosition.z);
                coin2.SetActive(true);
                Instantiate(gemBackground, coin2.transform.position, Quaternion.identity);

                GameObject coin3 = coinPool.GetPooledObject();
                coin3.transform.position = new Vector3(startPosition.x + distanceBetweenCoins, startPosition.y + Random.Range(1f, 2.3f), startPosition.z);
                coin3.SetActive(true);
                Instantiate(gemBackground, coin3.transform.position, Quaternion.identity);
            }
            else{
                if(numberOfCoins == 3)
                {
                    GameObject coin1 = coinPool.GetPooledObject();
                    coin1.transform.position = new Vector3(startPosition.x, startPosition.y + Random.Range(1f, 2.3f), startPosition.z);
                    coin1.SetActive(true);
                    Instantiate(gemBackground, coin1.transform.position, Quaternion.identity);

                    GameObject coin2 = coinPool.GetPooledObject();
                    coin2.transform.position = new Vector3(startPosition.x - distanceBetweenCoins, startPosition.y + Random.Range(1f, 2.3f), startPosition.z);
                    coin2.SetActive(true);
                    Instantiate(gemBackground, coin2.transform.position, Quaternion.identity);

                    GameObject coin3 = coinPool.GetPooledObject();
                    coin3.transform.position = new Vector3(startPosition.x + distanceBetweenCoins, startPosition.y + Random.Range(1f, 2.3f), startPosition.z);
                    coin3.SetActive(true);
                    Instantiate(gemBackground, coin3.transform.position, Quaternion.identity);
                }
            }
        }
        *//*
        GameObject coin1 = coinPool.GetPooledObject();
        coin1.transform.position = startPosition;
        coin1.SetActive(true);

        GameObject coin2 = coinPool.GetPooledObject();
        coin2.transform.position = new Vector3(startPosition.x - distanceBetweenCoins, startPosition.y , startPosition.z);
        coin2.SetActive(true);

        GameObject coin3 = coinPool.GetPooledObject();
        coin3.transform.position = new Vector3(startPosition.x + distanceBetweenCoins, startPosition.y, startPosition.z);
        coin3.SetActive(true);
        *//*
    }*/

    public void SpawnCoins(Vector3 lastStartPositon, Vector3 lastEndPosition, float heightChange, float penultimateEndPosition, float penultimateEndPositionY)
    {
        numberOfCoinsHorizontal = Mathf.RoundToInt(Random.Range(-2f, 3.49f));

        Debug.Log("NUMBER OS COINS GENERATED: " + numberOfCoinsHorizontal);
        //GameHandler.numberOfTotalCoins += numberOfCoins;
        if (numberOfCoinsHorizontal == 1)
        {
            valueOfXAxis = lastStartPositon.x + (lastEndPosition.x - lastStartPositon.x) * 0.5f;
            valueOfYAxis = heightChange + Random.Range(1.3f, 2.3f);
            
            GameObject coin1 = coinPool.GetPooledObject();
            coin1.transform.position = new Vector3(valueOfXAxis, valueOfYAxis, lastStartPositon.z);
            coin1.SetActive(true);
            Instantiate(gemBackground, coin1.transform.position, Quaternion.identity);
        }
        else
        {
            if (numberOfCoinsHorizontal == 2)
            {
                valueOfXAxis = lastStartPositon.x + Random.Range(((lastEndPosition.x - lastStartPositon.x) * 0.1f), ((lastEndPosition.x - lastStartPositon.x) * 0.4f));
                //valueOfXAxis = lastStartPositon.x;// + Random.Range(lastStartPositon.x + ((lastEndPosition.x - lastStartPositon.x) * 0.0f), ((lastEndPosition.x - lastStartPositon.x) * 0.0f));
                valueOfYAxis = heightChange + Random.Range(1.3f, 2.3f);

                GameObject coin2 = coinPool.GetPooledObject();
                coin2.transform.position = new Vector3(valueOfXAxis, valueOfYAxis, lastStartPositon.z);
                coin2.SetActive(true);
                Instantiate(gemBackground, coin2.transform.position, Quaternion.identity);

                valueOfXAxis = lastStartPositon.x + Random.Range(((lastEndPosition.x - lastStartPositon.x) * 0.6f), ((lastEndPosition.x - lastStartPositon.x) * 0.9f));
                //valueOfXAxis = lastEndPosition.x;// + Random.Range(lastStartPositon.x + ((lastEndPosition.x - lastStartPositon.x) * 1f), ((lastEndPosition.x - lastStartPositon.x) * 1f));
                valueOfYAxis = heightChange + Random.Range(1.3f, 2.3f);

                GameObject coin3 = coinPool.GetPooledObject();
                coin3.transform.position = new Vector3(valueOfXAxis, valueOfYAxis, lastStartPositon.z);
                coin3.SetActive(true);
                Instantiate(gemBackground, coin3.transform.position, Quaternion.identity);
            }
            else
            {
                if (numberOfCoinsHorizontal == 3)
                {
                    valueOfXAxis = lastStartPositon.x + (lastEndPosition.x - lastStartPositon.x) * 0.5f;
                    valueOfYAxis = heightChange + Random.Range(1.3f, 2.3f);

                    GameObject coin1 = coinPool.GetPooledObject();
                    coin1.transform.position = new Vector3(valueOfXAxis, valueOfYAxis, lastStartPositon.z);
                    coin1.SetActive(true);
                    Instantiate(gemBackground, coin1.transform.position, Quaternion.identity);

                    valueOfXAxis = lastStartPositon.x + Random.Range(((lastEndPosition.x - lastStartPositon.x) * 0.1f), ((lastEndPosition.x - lastStartPositon.x) * 0.4f));
                    //valueOfXAxis = lastStartPositon.x;// + Random.Range(lastStartPositon.x + ((lastEndPosition.x - lastStartPositon.x) * 0.0f), ((lastEndPosition.x - lastStartPositon.x) * 0.0f));
                    valueOfYAxis = heightChange + Random.Range(1.3f, 2.3f);

                    GameObject coin2 = coinPool.GetPooledObject();
                    coin2.transform.position = new Vector3(valueOfXAxis, valueOfYAxis, lastStartPositon.z);
                    coin2.SetActive(true);
                    Instantiate(gemBackground, coin2.transform.position, Quaternion.identity);

                    valueOfXAxis = lastStartPositon.x + Random.Range(((lastEndPosition.x - lastStartPositon.x) * 0.6f), ((lastEndPosition.x - lastStartPositon.x) * 0.9f));
                    //valueOfXAxis = lastEndPosition.x;// + Random.Range(lastStartPositon.x + ((lastEndPosition.x - lastStartPositon.x) * 1f), ((lastEndPosition.x - lastStartPositon.x) * 1f));
                    valueOfYAxis = heightChange + Random.Range(1.3f, 2.3f);

                    GameObject coin3 = coinPool.GetPooledObject();
                    coin3.transform.position = new Vector3(valueOfXAxis, valueOfYAxis, lastStartPositon.z);
                    coin3.SetActive(true);
                    Instantiate(gemBackground, coin3.transform.position, Quaternion.identity);
                }
            }
        }
        /*
        GameObject coin1 = coinPool.GetPooledObject();
        coin1.transform.position = startPosition;
        coin1.SetActive(true);

        GameObject coin2 = coinPool.GetPooledObject();
        coin2.transform.position = new Vector3(startPosition.x - distanceBetweenCoins, startPosition.y , startPosition.z);
        coin2.SetActive(true);

        GameObject coin3 = coinPool.GetPooledObject();
        coin3.transform.position = new Vector3(startPosition.x + distanceBetweenCoins, startPosition.y, startPosition.z);
        coin3.SetActive(true);
        */
        if(Random.Range(0,100f) < 100)
        {
            //int numberOfCoinsVertical = Mathf.RoundToInt(Random.Range(-0.49f, 2.49f));
            numberOfCoinsVertical = Mathf.RoundToInt(Random.Range(-0.49f, 2f));

            valueOfXAxis = penultimateEndPosition + ((lastStartPositon.x - penultimateEndPosition) * 0.5f);
            valueOfYAxis = Random.Range(1.8f, 2f);

            if (numberOfCoinsVertical == 1)
            {
                GameObject coin1 = coinPool.GetPooledObject();
                coin1.transform.position = new Vector3(valueOfXAxis, penultimateEndPositionY + valueOfYAxis, lastStartPositon.z);
                coin1.SetActive(true);
                Instantiate(gemBackground, coin1.transform.position, Quaternion.identity);
            }
            else if(numberOfCoinsVertical == 2)
            {
                GameObject coin1 = coinPool.GetPooledObject();
                //valueOfXAxis = penultimateEndPosition + ((lastStartPositon.x - penultimateEndPosition) * 0.5f);
                //valueOfYAxis = Random.Range(1.7f, 1.7f);
                coin1.transform.position = new Vector3(valueOfXAxis, penultimateEndPositionY + valueOfYAxis, lastStartPositon.z);
                coin1.SetActive(true);
                Instantiate(gemBackground, coin1.transform.position, Quaternion.identity);

                GameObject coin2 = coinPool.GetPooledObject();
                //valueOfXAxis = penultimateEndPosition + ((lastStartPositon.x - penultimateEndPosition) * 0.5f);
                //valueOfYAxis = valueOfYAxis - 0.3f;
                coin2.transform.position = new Vector3(coin1.transform.position.x, coin1.transform.position.y - 0.3f, lastStartPositon.z);
                coin2.SetActive(true);
                Instantiate(gemBackground, coin2.transform.position, Quaternion.identity);
            }
        }

        if(numberOfCoinsHorizontal > 0 || numberOfCoinsVertical > 0)
        {
            GameHandler.isCoinGenerated = true;
        }
    }

    public void SpawnEnemy(Vector3 startPosition)
    {
        GameObject enemy = enemyPool.GetPooledObject();
        //enemy.transform.position = startPosition;
        enemy.transform.position = new Vector3(startPosition.x + distanceBetweenCoins, startPosition.y, startPosition.z);
        enemy.SetActive(true);
    }
}
