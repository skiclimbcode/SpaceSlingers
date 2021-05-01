using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;

    public List<GameObject> pooledObjects;

    public GameObject objectToPool;

    public int amountToPool;

    void Awake()
    {
        SharedInstance = this;
        pooledObjects = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting Object Pool for " + amountToPool + " objects.");
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    public List<GameObject> GetPooledObjects()
    {
        return pooledObjects;
    }
}
