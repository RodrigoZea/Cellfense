using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMov : MonoBehaviour
{
    public float health = 0;
    NavMeshAgent nva;
    public GameObject impactEffect;
    private Transform target;
    public string playerTag = "Player";

    // Start is called before the first frame update
    void Start()
    {
        health = ResourceManager.Instance.enemyHealth;
        nva = GetComponent<NavMeshAgent>();
        ResourceManager.Instance.enemyList.Add(gameObject);
        InvokeRepeating("UpdateTarget", 0f, 0.3f);
    }

    void UpdateTarget()
    {
        //GameObject[] players = GameObject.FindGameObjectsWithTag(playerTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject player in ResourceManager.Instance.playerList)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = player;
            }
        }

        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }
        else
        {
            nva.SetDestination(target.position);
        }
    }
        

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Bullet")
        {
            health -= 35;
            if (health <= 0)
            {
                destroyEnemy();
            }
        }
    }

    void destroyEnemy()
    {
        ResourceManager.Instance.enemyList.Remove(gameObject);
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);
        Destroy(gameObject);
        if (ResourceManager.Instance.enemyList.Count == 0)
        {
            ResourceManager.Instance.waveClear();
        }
    }
}
