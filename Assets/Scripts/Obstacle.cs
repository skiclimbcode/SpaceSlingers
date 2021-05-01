using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private Rigidbody2D rb;
    public float moveSpeed = 2f;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Added Obstacle!");
        // Making it smaller since the PNG is pretty huge
        transform.localScale -= new Vector3(.8f, .8f, .8f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveSpeed * -1, rb.velocity.y);
    }
}
