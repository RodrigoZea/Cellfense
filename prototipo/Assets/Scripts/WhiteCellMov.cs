
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WhiteCellMov : MonoBehaviour
{
    public GameObject impactEffect;
    public GameObject bullet;
    public Transform firePoint;
    public float range = 30f;

    NavMeshAgent agent;
    Vector3 startPosition;

    private float health = 170;
    private Transform target;

    public float fireRate = 1f;
    private float fireCountdown = 0f;

    public string enemyTag = "Enemy";
    // Start is called before the first frame update
    void Start()
    {
        startPosition = gameObject.transform.position;
        agent = GetComponent<NavMeshAgent>();
        ResourceManager.Instance.playerList.Add(gameObject);
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget() {
        //GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in ResourceManager.Instance.enemyList) {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance) {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range) {
            target = nearestEnemy.transform;
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (ResourceManager.Instance.attacking == true)
        {
            if (target == null)
            {
                agent.SetDestination(startPosition);
            }
            else {
                Vector3 whereTo = new Vector3(target.position.x - 5, target.position.y - 5, target.position.z - 5);
                agent.SetDestination(whereTo);
            }

            if (fireCountdown <= 0) {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
        else
        {
            agent.destination = startPosition;
        }
    }

    void Shoot() {
        GameObject bulletGO = (GameObject) Instantiate(bullet, firePoint.position, firePoint.rotation);
        Bullet bulletS = bulletGO.GetComponent<Bullet>();

        if (bulletS != null) {
            if (target != null) {
                bulletS.Seek(target);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Enemy")
        {
            health -= 1;
            if (health <= 0)
            {
                deleteWhiteBloodCell();
            }
        }

    }

    private void deleteWhiteBloodCell()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);
        ResourceManager.Instance.playerList.Remove(gameObject);
        Destroy(gameObject);
        if (ResourceManager.Instance.playerList.Count == 0)
        {
            ResourceManager.Instance.gameLost();
        }
    }
}
