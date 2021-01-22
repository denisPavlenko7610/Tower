using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] Transform emenyPrefab;
    [SerializeField] Transform spawnPoint;

    [SerializeField] float countDown = 2f;

    void Update()
    {
        if (countDown <= 0f)
        {
            Spawn();
            countDown = 2f;
        }

        countDown -= Time.deltaTime;
    }

    private void Spawn()
    {
        Instantiate(emenyPrefab,spawnPoint.position,spawnPoint.rotation);
    }
}
