using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GameHandler.Start!");
        GameObject obstacle = ObjectPool.SharedInstance.GetPooledObject();
        obstacle.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
