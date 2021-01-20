using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Vector3 spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        Create();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Create()
    {
        Instantiate(bullet, spawnPosition ,Quaternion.identity);
    }
}
