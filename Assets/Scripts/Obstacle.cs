using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private Rigidbody2D rb;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Added Obstacle!");
        // Making it smaller since the PNG is pretty huge
        // transform.localScale -= new Vector3(.6f, .6f, .6f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
