using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTether : MonoBehaviour
{
    private Obstacle obstacleToTetherTo;

    [SerializeField]
    private float orbitSpeed = 100.0f;
    
    void Update()
    {
        // Get initial click/touch to find nearest obstacle to tether to
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0) {
            obstacleToTetherTo = FindClosestObstacle();
            Debug.Log("Closest obstacle at position = " + obstacleToTetherTo.gameObject.transform.position);
        }
        if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)) {
            // While user is holding button down, begin to orbit
            transform.RotateAround(obstacleToTetherTo.gameObject.transform.position, Vector3.forward, orbitSpeed * Time.deltaTime);
        }
    }

    Obstacle FindClosestObstacle()
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
        return closestObstacle;
    }
}
