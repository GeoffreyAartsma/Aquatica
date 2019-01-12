using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaTurret : MonoBehaviour
{

    private Transform target;

    [SerializeField]
    float range = 15f;

    [SerializeField]
    string enemyTag = "Enemy";

    [SerializeField]
    Transform partToRotate;

    [SerializeField]
    float turnSpeed = 10f;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEmeny = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEmeny = enemy;

            }
        }

        if (nearestEmeny != null && shortestDistance <= range)
        {
            target = nearestEmeny.transform;
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }

        // Target lock on
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
