using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject startPoint;
    public GameObject bulletPrefab;
    public float reloadDelay = 1;

    private bool canShoot = true;
    private Collider2D[] tankColliders;
    private float currentDelay = 0;

    private void Awake()
    {
        tankColliders = GetComponentsInParent<Collider2D>();
    }

    private void Update()
    {
        if (canShoot == false)
        {
            currentDelay -= Time.deltaTime;
            if (currentDelay <= 0)
            {
                canShoot = true;
            }
        }

        if (Input.GetKey(KeyCode.J))
        {
            Shoot();
        }
    }
    
    
    
    public void Shoot()
    {
        if (canShoot)
        {
            canShoot = false;
            currentDelay = reloadDelay;

            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = startPoint.transform.position;
            bullet.transform.localRotation = startPoint.transform.rotation;
            bullet.GetComponent<Bullet>().Initialize();
            
            foreach (var collider in tankColliders)
            {
                Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), collider);
            }
        }
    }
}
