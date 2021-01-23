using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [Header("Attributes")]
    [SerializeField] float radiusAttack = 15f;
    [SerializeField] float turnSpeed = 10f;

    [Header("UnitySetUpFields")]
    [SerializeField] Transform partToRotate;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject explotion;

    private Transform target;
    private string enemyTag = "Enemy";

    private int towersLevel = 2;
    private int health;
    private int armor;
    private float fireRate;
    private float fireCountdouwn;
    private int damage;
    private int maxLevel = 3;

    void Start()
    {
        SetTowersLevel(towersLevel);
        InvokeRepeating("UpdateTarget", 0, 0.5f);
    }

    void Update()
    {
        LookOnTarget();

        if (target && fireCountdouwn <= 0f)
        {
            Shoot();
            fireCountdouwn = 1f / fireRate;
        }

        fireCountdouwn -= Time.deltaTime;
    }

    private void Shoot()
    {
        GameObject explotionEffects = (GameObject)Instantiate(explotion, firePoint.position, firePoint.rotation);
        Destroy(explotionEffects, 2f);

        GameObject bulletGameObject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGameObject.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    private void LookOnTarget()
    {
        if (target)
        {
            Vector3 enemyDirection = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(enemyDirection);
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radiusAttack);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }

            //Set nearest enemy
            if (nearestEnemy != null && shortestDistance <= radiusAttack)
            {
                target = nearestEnemy.transform;
            }
            else
            {
                target = null;
            }
        }
    }

    public void Upgrade()
    {
        if (towersLevel <= maxLevel)
        {
            SetTowersLevel(towersLevel++);
        }
    }

    private void SetTowersLevel(int level)
    {
        switch (level)
        {
            case 1:
                health = 500;
                armor = 50;
                fireRate = 1f;
                damage = 40;
                break;
            case 2:
                health = 700;
                armor = 60;
                fireRate = 1.3f;
                damage = 50;
                break;
            case 3:
                health = 900;
                armor = 70;
                fireRate = 1.7f;
                damage = 60;
                break;
            default:
                break;
        }
    }

}
