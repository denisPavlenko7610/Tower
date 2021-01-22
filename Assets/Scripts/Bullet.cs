using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    [SerializeField] float speed = 50f;

    //Find different targets and add to bullet
    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 directionToTarget = target.position - transform.position;
        float distanseThisFrame = speed * Time.deltaTime;

        //Check distanse to target
        if (directionToTarget.magnitude <= distanseThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(directionToTarget.normalized * distanseThisFrame, Space.World);

    }

    private void HitTarget()
    {
        Destroy(gameObject);
    }
}
