using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosest : MonoBehaviour
{
    void Update()
    {
        FindClosestObstacle();
    }

    void FindClosestObstacle()
    {
        float distanceToClosestObstacle = Mathf.Infinity;
        Obstacle closestObstacle = null;
        Obstacle[] allObstacles = GameObject.FindObjectsOfType<Obstacle>();

        foreach (Obstacle currentObstacle in allObstacles) {
            float distanceToObstacle = (currentObstacle.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToObstacle < distanceToClosestObstacle) {
                distanceToClosestObstacle = distanceToObstacle;
                closestObstacle = currentObstacle;
            }
        }

        // for debugging purposes
        Debug.DrawLine (this.transform.position, closestObstacle.transform.position);
    }
}
