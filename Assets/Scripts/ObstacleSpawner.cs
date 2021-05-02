using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public int numberOfSpawns;
    private List<Vector3> spawnPoints;

    private List<GameObject> obstacles;

    // Start is called before the first frame update
    void Start()
    {
        obstacles = ObjectPool.SharedInstance.GetPooledObjects();
        Debug.Log("Size of pooled objects list: " + obstacles.Count);
        spawnPoints = new List<Vector3>();
        for (int i = 0; i < numberOfSpawns; i++)
        {   
            Vector3 randomSpawnPoint = RandomSpawnPoint();
            // check if random spawn point is too close to another one
            spawnPoints.Add(randomSpawnPoint);
        }

        SpawnObjects(obstacles, spawnPoints);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Vector3 RandomSpawnPoint()
    {
        float spawnPointX = Random.Range(corners(gameObject)[1].x, corners(gameObject)[0].x);
        float spawnPointY = Random.Range(corners(gameObject)[1].y, corners(gameObject)[2].y);

        return new Vector3(spawnPointX, spawnPointY, 0f);
    }

    private List<Vector3> corners(GameObject go)
    {
        float width = go.GetComponent<Renderer>().bounds.size.x;
        float height = go.GetComponent<Renderer>().bounds.size.y;

        Vector3 topRight = go.transform.position, topLeft = go.transform.position, bottomRight = go.transform.position, bottomLeft = go.transform.position;

        topRight.x += width / 2;
        topRight.y += height / 2;

        topLeft.x -= width / 2;
        topLeft.y += height / 2;

        bottomRight.x += width / 2;
        bottomRight.y -= height / 2;

        bottomLeft.x -= width / 2;
        bottomLeft.y -= height / 2;

        List<Vector3> cor_temp = new List<Vector3>();
        cor_temp.Add(topRight);
        cor_temp.Add(topLeft);
        cor_temp.Add(bottomRight);
        cor_temp.Add(bottomLeft);

        return cor_temp;
    }

    private void SpawnObjects(List<GameObject> gameObjects, List<Vector3> locations)
    {
        Debug.Log("Hello in SpawnObjects");
        List<GameObject> remainingGameObjects = new List<GameObject>(gameObjects);
        List<Vector3> freeLocations = new List<Vector3>(locations);

        if (locations.Count < gameObjects.Count)
            Debug.LogWarning("There are not enough locations for all the gameObjects. Some won't spawn.");

        StartCoroutine(ActivateObstacle(remainingGameObjects, freeLocations));
    }

    private IEnumerator ActivateObstacle(List<GameObject> remainingGameObjects, List<Vector3> freeLocations)
    {
        while (remainingGameObjects.Count > 0)
        {
            int gameObjectIndex = Random.Range(0, remainingGameObjects.Count);
            int locationIndex = Random.Range(0, freeLocations.Count);
            Debug.Log("Activating obstacle!");
            ActivateObject(remainingGameObjects[gameObjectIndex], freeLocations[locationIndex]);
            remainingGameObjects.RemoveAt(gameObjectIndex);
            freeLocations.RemoveAt(locationIndex);
            yield return new WaitForSeconds(Random.Range(.5f, 2));
        }
    }

    private void ActivateObject(GameObject go, Vector3 location)
    {
        go.transform.position = location;
        go.transform.rotation = Quaternion.identity;
        go.SetActive(true);
    }
}
