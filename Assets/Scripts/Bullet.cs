using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    private float speed = 50f;

    public float explosionRadius = 0f;
    public GameObject impactEffect;

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
    }

    void FixedUpdate()
    {
        Vector3 directionToTarget = target.position - transform.position;
        float distanseThisFrame = speed * Time.deltaTime;

        transform.Translate(directionToTarget.normalized * distanseThisFrame, Space.World);
        transform.LookAt(target);

        //Check distanse to target
        if (directionToTarget.magnitude <= distanseThisFrame)
        {
            HitTarget();
            return;
        }
    }


    private void HitTarget()
    {
        GameObject effectsInstance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectsInstance, 2f);

        Destroy(gameObject);
    }

}
