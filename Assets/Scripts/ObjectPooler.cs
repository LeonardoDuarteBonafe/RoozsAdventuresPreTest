using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject pooledObject;
    //public GameObject[] pooledObjectVector;


    public int pooledAmount;

    //int count = 0;

    List<GameObject> pooledObjects;

    // Start is called before the first frame update
    void Start()
    {
        //count = 0;
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject) Instantiate(pooledObject);
            //pooledObject = pooledObjectVector[count];
            //GameObject obj = (GameObject) Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        GameObject obj = (GameObject)Instantiate(pooledObject);
        //pooledObject = pooledObjectVector[count++];
        //count = count % 2;
        //GameObject obj = (GameObject)Instantiate(pooledObject);
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }
}
