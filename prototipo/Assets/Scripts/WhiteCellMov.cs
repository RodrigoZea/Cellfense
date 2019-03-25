
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WhiteCellMov : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;

    NavMeshAgent agent;
    Vector3 startPosition;
    private int indexInList;
    private float health = 170;
    private Transform target;

    public float fireRate = 1f;
    private float fireCountdown = 0f;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.gameObject.transform.position;
        agent = GetComponent<NavMeshAgent>();
        ResourceManager.Instance.playerSpawns.Add(agent);
        indexInList = ResourceManager.Instance.psIndex;
        ResourceManager.Instance.psIndex++;
    }

    // Update is called once per frame
    void Update()
    {
        if (ResourceManager.Instance.attacking == true)
        {
            findClosest();

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

    void findClosest()
    {
        NavMeshAgent nearestObj = new NavMeshAgent();
        foreach (var player in ResourceManager.Instance.enemySpawns)
        {
            nearestObj = ResourceManager.Instance.enemySpawns.FindClosest(player.transform.position);
        }
        if (nearestObj != null)
        {
            Vector3 wcd = new Vector3(nearestObj.transform.position.x - 3, nearestObj.transform.position.y - 3, nearestObj.transform.position.z - 3);
            agent.destination = wcd;
            target = nearestObj.transform;
            Debug.DrawLine(gameObject.transform.position, nearestObj.transform.position, Color.red);
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
        ResourceManager.Instance.playerSpawns.RemoveAt(indexInList);
        if (ResourceManager.Instance.playerSpawns.Count == 0)
        {
            ResourceManager.Instance.gameLost();
        }
        Destroy(gameObject);
    }
}
