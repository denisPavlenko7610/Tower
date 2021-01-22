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

    private Transform target;
    private string enemyTag = "Enemy";

    private int health;
    private int armor;
    private int towersLevel;
    private float fireRate;
    private float fireCountdouwn;
    private int damage;
    private bool levelUp;
    private int maxLevel = 3;

    void Start()
    {
        towersLevel = 1;
        levelUp = false;
        CheckTowersLevel();
        InvokeRepeating("UpdateTarget", 0, 0.5f);
    }

    void Update()
    {
        RotateTowerGun();
        if (levelUp)
        {
            NextLevel();
        }

        if (fireCountdouwn <= 0f)
        {
            Shoot();
            fireCountdouwn = 1f / fireRate;
        }

        fireCountdouwn -= Time.deltaTime;
    }

    private void Shoot()
    {
        GameObject bulletGameObject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    private void RotateTowerGun()
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

    void NextLevel()
    {
        if (towersLevel <= maxLevel)
        {
            towersLevel++;
            CheckTowersLevel();
            levelUp = false;
        }
    }

    private void CheckTowersLevel()
    {
        switch (towersLevel)
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
