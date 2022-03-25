using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject startPoint;
    public GameObject bulletPrefab;
    public float reloadDelay = 1;
    public float currentDelay = 0;
    
    private bool canShoot = true;
    private Collider2D[] _tankColliders;
    
    private ObjectPool _bulletPool;
    [SerializeField] private int bulletPoolCount = 10;

    private void Awake()
    {
        _tankColliders = GetComponentsInParent<Collider2D>();

        _bulletPool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        _bulletPool.Initialize(bulletPrefab, bulletPoolCount);
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

            //GameObject bullet = Instantiate(bulletPrefab);
            GameObject bullet = _bulletPool.CreateObject();
            bullet.transform.position = startPoint.transform.position;
            bullet.transform.localRotation = startPoint.transform.rotation;
            bullet.GetComponent<Bullet>().Initialize();
            
            foreach (var collider in _tankColliders)
            {
                Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), collider);
            }
            
        }
    }
}
