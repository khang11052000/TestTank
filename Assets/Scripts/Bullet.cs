using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int dame = 5;
    public float maxDistance = 10f;

    private Vector2 _startPosition;
    private float _conqueredDistance = 0;
    private Rigidbody2D _rb2d;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    public void Initialize()
    {
        
        _startPosition = transform.position + new Vector3(0,0.6f, 0);
        _rb2d.velocity = transform.up * speed;
        //Debug.Log(_startPosition);
    }

    private void Update()
    {
        _conqueredDistance = Vector2.Distance(transform.position, _startPosition);
        if (_conqueredDistance >= maxDistance)
        {
            DisableOject();
        }
    }

    private void DisableOject()
    {
        _rb2d.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collider" + other.name);
        DisableOject();
    }
    
}
