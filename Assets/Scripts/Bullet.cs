using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int dame = 5;
    public float maxDistance = 10f;

    private Vector2 startPosition;
    private float conqueredDistance = 0;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Initialize()
    {
        
        startPosition = transform.position + new Vector3(0,0.6f, 0);
        rb2d.velocity = transform.up * speed;
        Debug.Log(startPosition);
    }

    private void Update()
    {
        conqueredDistance = Vector2.Distance(transform.position, startPosition);
        if (conqueredDistance >= maxDistance)
        {
            DisableOject();
        }
    }

    private void DisableOject()
    {
        rb2d.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collider" + other.name);
        DisableOject();
    }
    
}
