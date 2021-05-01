using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTether : MonoBehaviour
{
    [SerializeField]
    private float orbitSpeed = 200.0f;

    private Obstacle obstacleToTetherTo;

    private bool isSameObstacle = false;

    private bool isObstacleBehindPlayer = false;
    
    void Update()
    {
        // Get initial click/touch to find nearest obstacle to tether to
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0) {
            obstacleToTetherTo = FindClosestObstacle();
            // Adjust Z-axis on the fly to maintain integrity of rocket nose position
            if (!isSameObstacle) {
                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z - 90);
            }
            Debug.Log("Closest obstacle at position = " + obstacleToTetherTo.gameObject.transform.position);
        }
        if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)) {
            Vector3 obstaclePos = obstacleToTetherTo.gameObject.transform.position;
            // While user is holding button down, begin to orbit
            // If obstacle is behind player, change motion of rotation
            Vector3 desiredAxis = isObstacleBehindPlayer ? Vector3.back : Vector3.forward; 
            transform.RotateAround(obstaclePos, desiredAxis, orbitSpeed * Time.deltaTime);
        } else {
            transform.rotation = Quaternion.Euler(0, 0, 0);
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
        isSameObstacle = closestObstacle == obstacleToTetherTo;
        isObstacleBehindPlayer = closestObstacle.transform.position.x < this.transform.position.x;
        return closestObstacle;
    }
}
